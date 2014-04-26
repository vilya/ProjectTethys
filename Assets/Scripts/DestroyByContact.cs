using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "Boundary") {
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}

}
