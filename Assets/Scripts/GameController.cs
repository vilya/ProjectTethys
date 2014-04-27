using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour {
	public GameObject bomber;
	public GameObject fighter;
	public GameObject explosion;
	public GameObject shieldPickUp;

	public GameObject player;

	public Vector3 spawnValues;

	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText shieldText;
	public GUIText baseHealthText;
	public GUIText restartText;
	public GUIText gameOverText;

	private int score;

	private bool gameOver;
	private bool restart;

	// Use this for initialization
	void Start() {
		gameOver = false;
		restart = false;
		gameOverText.text = "";
		restartText.text = "";

		score = 0;
		UpdateScore();

		StartCoroutine(SpawnWaves(bomber));
	}


	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("TitleScreen");
			return;
		}

		if (restart) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
	

	IEnumerator SpawnWaves(GameObject enemy) {
		// Delay before launching the first wave.
		yield return new WaitForSeconds(startWait);
		while (!gameOver) {
			// Randomly decide which side of the play area this wave will start from.
			float x = -spawnValues.x;
			Quaternion spawnRotation = Quaternion.identity;
			if (Random.value >= 0.5f) {
				x = spawnValues.x;
				spawnRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
			}
			for (int i = 0; i < 10; ++i) {
				float y = Random.Range(-spawnValues.y, spawnValues.y);
				Vector3 spawnPosition = new Vector3(x, y, 0.0f);
				Instantiate(enemy, spawnPosition, spawnRotation);
				// Small delay in between launching each wave.
				yield return new WaitForSeconds(spawnWait);
			}
			SpawnPickUp();
			// Pause before launching the next wave.
			yield return new WaitForSeconds(waveWait);
		}

		restartText.text = "Press <space> to restart";
		restart = true;
	}


	void SpawnPickUp() {
		float x = Random.Range(-spawnValues.x, spawnValues.x);
		float y = spawnValues.y + 15.0f;
		Vector3 spawnPosition = new Vector3(x, y, 0.0f);
		Instantiate(shieldPickUp, spawnPosition, Quaternion.identity);
	}

	
	public void AddScore(int extraPoints) {
		score += extraPoints;
		UpdateScore();
	}


	public void UpdateBaseHealth(int newBaseHealth) {
		baseHealthText.text = "Base " + newBaseHealth + "% operational";
	}


	public void UpdateShieldLevel(int newShieldLevel) {
		if (newShieldLevel > 0) {
			shieldText.text = "Shields at " + newShieldLevel;
		}
		else {
			shieldText.text = "Shields down!";
		}
	}


	void UpdateScore() {
		scoreText.text = score + " pts";
	}


	public void GameOver() {
		gameOver = true;

		HighScores highScores = new HighScores();
		if (highScores.IsHighScore(score)) {
			gameOverText.text = "New high score!";
			highScores.AddHighScore("You", score);
			highScores.SaveHighScores();
			StartCoroutine(SwitchToLevelAfterPause("HighScores", 4.0f));
		}
		else {
			gameOverText.text = "Game Over";
		}
	}


	public static GameController GetInstance() {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject == null) {
			Debug.Log("Cannot find 'GameController' object");
			return null;
		}

		GameController gameController = gameControllerObject.GetComponent<GameController>();
		return gameController;
	}


	IEnumerator SwitchToLevelAfterPause(string levelName, float pauseInSeconds) {
		yield return new WaitForSeconds(pauseInSeconds);
		Application.LoadLevel(levelName);
	}
}
