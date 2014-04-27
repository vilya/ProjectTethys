using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {
	public GameObject explosion;

	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Boundary") {
			return;
		}

		Debug.Log("bomb got hit by a " + other.gameObject.tag);

		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}


	public void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Boundary") {
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
