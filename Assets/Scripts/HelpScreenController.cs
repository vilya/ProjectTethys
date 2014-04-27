using UnityEngine;
using System.Collections;

public class HelpScreenController : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("TitleScreen");
			return;
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel("Main");
		}
	}
}
