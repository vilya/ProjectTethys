using UnityEngine;
using System.Collections;

public class HighScores {
	public string[] names;
	public int[] values;
	public string[] valueStrs;


	public HighScores() {
		names = new string[10];
		values = new int[10];
		valueStrs = new string[10];
		LoadHighScores();
	}


	public void LoadHighScores() {
		for (int i = 0; i < 10; ++i) {
			names[i] = PlayerPrefs.GetString("HighScoreName" + i);
			if (names[i] == "") {
				names[i] = "Unknown";
			}
			
			values[i] = PlayerPrefs.GetInt("HighScoreValue" + i, -1);
			if (values[i] == -1) {
				values[i] = (10 - i) * 1000;
			}
			valueStrs[i] = "" + values[i];
		}
	}


	public void SaveHighScores() {
		for (int i = 0; i < 10; ++i) {
			PlayerPrefs.SetString("HighScoreName" + i, names[i]);
			PlayerPrefs.SetInt("HighScoreValue" + i, values[i]);
		}
		PlayerPrefs.Save();
	}


	public bool IsHighScore(int score) {
		return (score > values[9]);
	}


	public void AddHighScore(string name, int score) {
		// Find the insertion point.
		int i = 10;
		while (i > 0 && score > values[i - 1]) {
			--i;
		}

		// Make space at the insertion point by shifting everything down 1 slot.
		for (int j = 9; j > i; --j) {
			names[j] = names[j - 1];
			values[j] = values[j - 1];
			valueStrs[j] = valueStrs[j - 1];
		}

		names[i] = name;
		values[i] = score;
		valueStrs[i] = "" + score;
	}
}
