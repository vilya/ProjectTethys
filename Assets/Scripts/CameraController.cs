using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player;
	
	private const float maxDistance = 3.0f;
	
	// Use this for initialization
	void Start () {
		// Do nothing.
	}
	
	// LateUpdate is called once per frame
	void LateUpdate () {
		Vector3 offset = player.transform.position - transform.position;
		offset.z = 0.0f;
		
		float distance = offset.magnitude;
		if (distance > maxDistance) {
			offset *= ((distance - maxDistance) / distance);
			transform.position += offset;
		}
	}
}
