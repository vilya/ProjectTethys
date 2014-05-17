using UnityEngine;
using System.Collections;

public class ScientistController : MonoBehaviour {
	public GameObject home; // Where the scientist is trying to get to.

	public float speed;


	// Use this for initialization
	void Start () {
		float dx = (transform.position.x < home.transform.position.x) ? 1.0f : -1.0f;
		rigidbody.velocity = new Vector3(dx, 0.0f, 0.0f) * speed;
	}

	
	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Laser" ||
		    other.gameObject.tag == "Player" ||
		    other.gameObject.tag == "Boundary") {
			return;
		}

		Destroy(gameObject);
	}
}
