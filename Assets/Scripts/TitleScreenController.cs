using UnityEngine;
using System.Collections;

public class TitleScreenController : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (!Application.isWebPlayer) {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				Application.Quit();
			}
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel("HelpScreen");
		}
	}
}
