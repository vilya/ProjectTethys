using UnityEngine;
using System.Collections;

public class BaseController : MonoBehaviour {
	private GameController gameController;

	public GameObject player;
	public GameObject explosion;

	public int initialHealth;
	private int currentHealth;

	// Use this for initialization
	void Start () {
		gameController = GameController.GetInstance();
		currentHealth = initialHealth;
		gameController.UpdateBaseHealth(currentHealth);
	}

	
	public void OnTriggerEnter(Collider other) {
		//Debug.Log("base got hit by a " + other.gameObject.tag);

		if (other.gameObject.tag == "Boundary" || other.gameObject.tag == "Player") {
			return;
		}

		int damage = 1; // TODO: get this from the weapon type.
		TakeDamage(damage);

		//if (other.gameObject.tag == "Bomb") {
		//	Instantiate(explosion, other.transform.position, Quaternion.identity);
		//}
		//Destroy(other.gameObject);
	}


	void TakeDamage(int damage) {
		currentHealth = Mathf.Max(0, currentHealth - damage);
		gameController.UpdateBaseHealth(currentHealth);
		if (currentHealth == 0) {
			Instantiate(explosion, transform.position, Quaternion.identity);
			Instantiate(explosion, player.transform.position, Quaternion.identity);
			gameController.GameOver();
			Destroy(gameObject);
			Destroy(player);
		}
	}
}
