using UnityEngine;
using System.Collections;

public class ShieldPickUpController : MonoBehaviour {
	public GameObject explosion;

	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Boundary") {
			return;
		}

		if (other.gameObject.tag == "Laser" || other.gameObject.tag == "Bomb") {
			Instantiate(explosion, transform.position, Quaternion.identity);
		}

		//Debug.Log("shield pick-up got hit by a " + other.gameObject.tag);
		Destroy(gameObject);
	}
}
