using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	private GameController gameController;
	public int scoreValue;

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameControllerObject == null) {
			Debug.Log("Cannot find 'GameController' object");
		}
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Boundary") {
			return;
		}
		Instantiate(explosion, transform.position, Quaternion.identity);
		if (other.gameObject.tag == "Player") {
			Instantiate(explosion, other.transform.position, Quaternion.identity);
			gameController.GameOver();
		}
		gameController.AddScore(scoreValue);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
