using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Boundary") {
			return;
		}
		Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
