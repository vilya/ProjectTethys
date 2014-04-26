using UnityEngine;
using System.Collections;

public class BomberBehaviour : MonoBehaviour {
	public float speed;

	// Use this for initialization
	void Start () {
		float direction = (Random.value >= 0.5f) ? 1.0f : -1.0f;
		rigidbody.velocity = transform.right * speed * direction;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
