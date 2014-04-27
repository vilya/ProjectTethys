using UnityEngine;
using System.Collections;


[System.Serializable]
public class Boundary {
	public float xMin, xMax, yMin, yMax;
}


public class PlayerController : MonoBehaviour {
	private GameController gameController;
	public GameObject explosion;

	public float speed;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	private float maxSpeed;

	public int initialShieldLevel;
	private int currentShieldLevel;


	void Start() {
		gameController = GameController.GetInstance();
		currentShieldLevel = initialShieldLevel;
		gameController.UpdateShieldLevel(currentShieldLevel);
	}


	void Update() {
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}


	// Update is called once per frame
	void FixedUpdate() {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(x, y, 0);

		rigidbody.velocity = movement * speed;
		rigidbody.position = new Vector3(
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp(rigidbody.position.y, boundary.yMin, boundary.yMax),
			0.0f
		);
		if (x < 0.0f) {
			rigidbody.MoveRotation(Quaternion.Euler(0.0f, 180.0f, 0.0f));
		}
		else if (x > 0.0f) {
			rigidbody.MoveRotation(Quaternion.Euler(0.0f, 0.0f, 0.0f));
		}
	}


	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Laser" || other.gameObject.tag == "Boundary") {
			return;
		}

		//Debug.Log("player got hit by " + other.gameObject.tag);

		if (other.gameObject.tag == "ShieldPickUp") {
			RepairDamage();
		}
		else {
			TakeDamage();
		}
	}


	void TakeDamage() {
		--currentShieldLevel;
		if (currentShieldLevel < 0) {
			Instantiate(explosion, transform.position, Quaternion.identity);
			gameController.GameOver();
			Destroy(gameObject);
		}
		else {
			gameController.UpdateShieldLevel(currentShieldLevel);
		}
	}


	void RepairDamage() {
		if (currentShieldLevel < initialShieldLevel) {
			++currentShieldLevel;
		}
		gameController.UpdateShieldLevel(currentShieldLevel);
	}
}
