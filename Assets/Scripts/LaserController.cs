using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {
	public float speed;

	void Start() {
		rigidbody.velocity = transform.right * speed;
	}


	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Boundary" ||
		    other.gameObject.tag == "Player" ||
		    other.gameObject.tag == "Scientist" ||
		    other.gameObject.tag == "Laser" ||
		    other.gameObject.tag == "EnemyLaser") {
			return;
		}

		//Debug.Log("laser got hit by a " + other.gameObject.tag);
		Destroy(gameObject);
	}
}