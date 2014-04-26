using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float tilt;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;


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

		rigidbody.AddForce(movement * speed * Time.deltaTime);

		rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f + rigidbody.velocity.y * tilt);
	}
}
