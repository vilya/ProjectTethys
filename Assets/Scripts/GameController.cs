using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject bomber;
	public GameObject fighter;

	public Vector3 spawnValues;

	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
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
			for (int i = 0; i < 10; ++i) {
				Vector3 spawnPosition = new Vector3(
					Random.Range(-spawnValues.x, spawnValues.x),
					Random.Range(-spawnValues.y, spawnValues.y),
					0.0f
				);
				Instantiate(enemy, spawnPosition, Quaternion.identity);
				// Small delay in between launching each wave.
				yield return new WaitForSeconds(spawnWait);
			}
			// Pause before launching the next wave.
			yield return new WaitForSeconds(waveWait);
		}

		restartText.text = "Press <space> to restart";
		restart = true;
	}


	public void AddScore(int extraPoints) {
		score += extraPoints;
		UpdateScore();
	}


	public void UpdateBaseHealth(int newBaseHealth) {
		baseHealthText.text = newBaseHealth + "% operational";
	}


	void UpdateScore() {
		scoreText.text = score + " pts";
	}


	public void GameOver() {
		gameOver = true;
		gameOverText.text = "Game Over";
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
}
