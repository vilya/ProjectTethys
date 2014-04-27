using UnityEngine;
using System.Collections;

public class HighScoreScreenController : MonoBehaviour {
	public GUIText namesText;
	public GUIText valuesText;

	HighScores highScores;


	void Start() {
		highScores = new HighScores();
		UpdateHighScoreTable();
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel("TitleScreen");
		}
	}


	void UpdateHighScoreTable() {
		string namesStr = string.Join("\n", highScores.names);
		string valuesStr = string.Join("\n", highScores.valueStrs);
		namesText.text = namesStr;
		valuesText.text = valuesStr;
	}
}
