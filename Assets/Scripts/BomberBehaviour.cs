using UnityEngine;
using System.Collections;

public class BomberBehaviour : MonoBehaviour {
	public float speed;

	public GameObject bomb;
	public float startWait; // Number of seconds to wait before bombing commences.
	public float bombWait;  // Number of seconds to wait between firing off bombs.
	public Vector3 bombSpawnOffset;

	// Use this for initialization
	void Start () {
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
}
