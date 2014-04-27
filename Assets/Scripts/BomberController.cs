using UnityEngine;
using System.Collections;

public class BomberController : MonoBehaviour {
	private GameController gameController;

	public float speed;

	public GameObject bomb;
	public GameObject explosion;

	public float startWait; // Number of seconds to wait before bombing commences.
	public float bombWait;  // Number of seconds to wait between firing off bombs.
	public Vector3 bombSpawnOffset;

	public int pointsValue;

	// Use this for initialization
	void Start () {
		gameController = GameController.GetInstance();

		float direction = (Random.value >= 0.5f) ? 1.0f : -1.0f;
		rigidbody.velocity = transform.right * speed * direction;

		StartCoroutine(SpawnBombs());
	}


	IEnumerator SpawnBombs() {
		// Delay before launching the first bomb.
		yield return new WaitForSeconds(startWait);
		while (true) {
			Instantiate(bomb, transform.position + bombSpawnOffset, Quaternion.identity);
			// Small delay in between launching each bomb.
			yield return new WaitForSeconds(bombWait);
		}
	}


	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Boundary") {
			return;
		}

		//Debug.Log("bomber got hit by a " + other.gameObject.tag);
		if (other.gameObject.tag == "Laser" || other.gameObject.tag == "Player") {
			gameController.AddScore(pointsValue);
		}
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
