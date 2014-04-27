using UnityEngine;
using System.Collections;

public class DelegatingPlayerController : MonoBehaviour {
	public PlayerController playerController;

	void OnTriggerEnter(Collider other) {
		playerController.OnTriggerEnter(other);
	}
}
