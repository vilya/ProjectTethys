using UnityEngine;
using System.Collections;

public class ScientistPickUpController : MonoBehaviour {
	public GameObject explosion;
	public GameObject scientist;

	public AudioClip collectedSound;


	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Boundary" || other.gameObject.tag == "Player") {
			return;
		}

		if (other.gameObject.tag == "Laser" ||
		    other.gameObject.tag == "Bomb" || 
		    other.gameObject.tag == "EnemyLaser")
		{
			Instantiate(explosion, transform.position, Quaternion.identity);
		}

		//Debug.Log("shield pick-up got hit by a " + other.gameObject.tag);
		Destroy(gameObject);
	}


	public void OnTriggerExit(Collider other) {
		if (other.gameObject.tag != "Boundary") {
			return;
		}

		Vector3 spawnPosition = new Vector3(transform.position.x, -24.625f, 0.0f);
		Destroy(gameObject);
		Instantiate(scientist, spawnPosition, Quaternion.identity);
	}
}
