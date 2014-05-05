using UnityEngine;
using System.Collections;

public class ShieldPickUpController : MonoBehaviour {
	private GameController gameController;

	public GameObject explosion;
	public AudioClip collectedSound;


	void Start() {
		gameController = GameController.GetInstance();
	}


	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Boundary") {
			return;
		}

		if (other.gameObject.tag == "Laser" || other.gameObject.tag == "Bomb") {
			Instantiate(explosion, transform.position, Quaternion.identity);
		}

		if (other.gameObject.tag == "Player") {
			gameController.audio.PlayOneShot(collectedSound);
		}

		//Debug.Log("shield pick-up got hit by a " + other.gameObject.tag);
		Destroy(gameObject);
	}
}
