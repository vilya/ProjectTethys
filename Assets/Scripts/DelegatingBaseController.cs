using UnityEngine;
using System.Collections;

public class DelegatingBaseController : MonoBehaviour {
	public BaseController baseController;

	void OnTriggerEnter(Collider other) {
		baseController.OnTriggerEnter(other);
	}
}
