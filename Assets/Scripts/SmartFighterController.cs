using UnityEngine;
using System.Collections;

// An enemy fighter controlled by this class will try to move towards the player.
// When the player is within weapon range and within some minimum y-distance of
// the enemy it will start firing as rapidly as possible.
// The enemy will try to avoid crashing into the player.
public class SmartFighterController : MonoBehaviour {
	private GameController gameController;

	public GameObject target; // This is what the fighter will be attacking.
	public float weaponRange; // The maximum horizontal distance to start firing at.
  public float verticalRange; // The maximum vertical distance to start firing at.
	public float minDistance;		// The horizontal distance at which to stop moving towards the target.

	public float speed;
	
	public GameObject shot;
	public Transform shotSpawn;
	public GameObject explosion;
	
	public float startWait;    // Number of seconds to wait before shooting commences.
	public float minShotWait;  // Minimum number of seconds to wait between shots.

	public int pointsValue;
	
  private float nextShotTime;

	
	// Use this for initialization
	void Start () {
		gameController = GameController.GetInstance();
		
		//float direction = (Random.value >= 0.5f) ? 1.0f : -1.0f;
		rigidbody.velocity = transform.up * -speed;
	
    nextShotTime = Time.time + startWait;
		//StartCoroutine(SpawnShots());
	}


  void Update() {
    // Check if we're ready to fire again.
    if (Time.time < nextShotTime) {
      return;
    }

    // Check that the target is within range.
    Vector3 delta = target.transform.position - transform.position;
    if (Mathf.Abs(delta.x) > weaponRange || Mathf.Abs(delta.y) > verticalRange) {
      return;
    }
    Debug.Log("delta = " + delta);

    // FIRE!!!
    nextShotTime = Time.time + minShotWait;
    Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
  }


	void FixedUpdate() {
		Vector3 delta = target.transform.position - transform.position;

    if (delta.x < 0.0f) {
      rigidbody.MoveRotation(Quaternion.Euler(0.0f, 0.0f, 270.0f));
    }
    else if (delta.x > 0.0f) {
      rigidbody.MoveRotation(Quaternion.Euler(0.0f, 0.0f, 90.0f));
    }

    if (Mathf.Abs(delta.x) <= minDistance) {
      delta.x = 0.0f;
    }
    if (Mathf.Abs(delta.y) < verticalRange * 0.2f) {
      delta.y = 0.0f;
    }

    delta.Normalize();
    delta *= speed;

    rigidbody.velocity = delta;
	}
	
	IEnumerator SpawnShots() {
		// Delay before launching the first bomb.
		yield return new WaitForSeconds(startWait);
		while (true) {
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			// Small delay in between launching each bomb.
			yield return new WaitForSeconds(minShotWait);
		}
	}
	
	
	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Boundary") {
			return;
		}
		
		//Debug.Log("bomber got hit by a " + other.gameObject.tag);
		if (other.gameObject.tag == "Laser" || other.gameObject.tag == "Player") {
			gameController.AddScore(pointsValue);
		}
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
