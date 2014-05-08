using UnityEngine;
using System.Collections;

public class FighterController : MonoBehaviour {
	private GameController gameController;

	public float speed;

	public GameObject shot;
	public Transform shotSpawn;
	public GameObject explosion;

	public float startWait;    // Number of seconds to wait before shooting commences.
	public float minShotWait;  // Minimum number of seconds to wait between shots.
	public float maxShotWait;  // Maximum number of seconds to wait between shots.

	public int pointsValue;


	// Use this for initialization
	void Start () {
		gameController = GameController.GetInstance();

		//float direction = (Random.value >= 0.5f) ? 1.0f : -1.0f;
		rigidbody.velocity = transform.up * -speed;

		StartCoroutine(SpawnShots());
	}


	IEnumerator SpawnShots() {
		// Delay before launching the first bomb.
		yield return new WaitForSeconds(startWait);
		while (true) {
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			// Small delay in between launching each bomb.
			yield return new WaitForSeconds(Random.Range(minShotWait, maxShotWait));
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
