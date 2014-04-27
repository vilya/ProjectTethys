using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	private GameController gameController;
	public int scoreValue;

	void Start() {
		gameController = GameController.GetInstance();
	}


	void OnTriggerEnter(Collider other) {
		Debug.Log("DestroyByContact is still being used when a " + gameObject.tag + " is hit by a " + other.gameObject.tag);
		/*
		if (other.gameObject.tag == "Boundary") {
			return;
		}
		Instantiate(explosion, transform.position, Quaternion.identity);
		if (other.gameObject.tag == "Player") {
			gameController.DamagePlayer();
		}
		gameController.AddScore(scoreValue);
		Destroy(other.gameObject);
		Destroy(gameObject);
		*/
	}
}
