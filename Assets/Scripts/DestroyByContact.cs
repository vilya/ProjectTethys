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
