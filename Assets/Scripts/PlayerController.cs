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


	void Start() {
		gameController = GameController.GetInstance();
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
			rigidbody.MoveRotation(Quaternion.Euler(0.0f, 180.0f, 90.0f));
		}
		else if (x > 0.0f) {
			rigidbody.MoveRotation(Quaternion.Euler(0.0f, 0.0f, 90.0f));
		}
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "Bomb") {
			return;
		}

		Instantiate(explosion, transform.position, Quaternion.identity);
		Instantiate(explosion, other.transform.position, Quaternion.identity);
		gameController.GameOver();
		Destroy(gameObject);
		Destroy(other.gameObject);
	}
}
