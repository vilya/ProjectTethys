using UnityEngine;
using System.Collections;

public class DelegatingBombController : MonoBehaviour {
	public BombController bombController;
	
	void OnTriggerEnter(Collider other) {
		bombController.OnTriggerEnter(other);
	}
}
