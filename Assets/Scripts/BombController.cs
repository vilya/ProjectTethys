using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {
	private GameController gameController;
	public GameObject explosion;

	public int pointsValue;

	void Start() {
		gameController = GameController.GetInstance();
	}


	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Boundary") {
			return;
		}

		//Debug.Log("bomb got hit by a " + other.gameObject.tag);
		if (other.gameObject.tag == "Laser" || other.gameObject.tag == "Player") {
			gameController.AddScore(pointsValue);
		}
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
