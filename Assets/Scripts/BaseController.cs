using UnityEngine;
using System.Collections;

public class BaseController : MonoBehaviour {
	private GameController gameController;

	public GameObject player;
	public GameObject explosion;

	public float fixWait;   // How long it takes for a scientist to repair the base after reaching it, in seconds?
	public int healthFromFixing;  // How much health the base recovers from a repair job.

	private int currentHealth;


	// Use this for initialization
	void Start () {
		gameController = GameController.GetInstance();

		currentHealth = gameController.initialBaseHealth / 2;

		gameController.UpdateBaseHealth(currentHealth);
	}

	
	public void OnTriggerEnter(Collider other) {
		//Debug.Log("base got hit by a " + other.gameObject.tag);

		if (other.gameObject.tag == "Boundary" || other.gameObject.tag == "Player") {
			return;
		}

		if (other.gameObject.tag == "Scientist") {
			ScientistReachedBase();
			return;
		}

		int damage = 1; // TODO: get this from the weapon type.
		TakeDamage(damage);
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


	void ScientistReachedBase()
	{
		StartCoroutine(FixBaseAfterDelay());
	}
	

	IEnumerator FixBaseAfterDelay()
	{
		yield return new WaitForSeconds(fixWait);
		currentHealth = Mathf.Min(currentHealth + healthFromFixing, gameController.initialBaseHealth);
		gameController.UpdateBaseHealth(currentHealth);
	}
}
