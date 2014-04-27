using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject bomber;
	public GameObject fighter;
	public GameObject explosion;

	public GameObject player;

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
			// Randomly decide which side of the play area this wave will start from.
			float x = (Random.value < 0.5f) ? -spawnValues.x : spawnValues.x;
			for (int i = 0; i < 10; ++i) {
				float y = Random.Range(-spawnValues.y, spawnValues.y);
				Vector3 spawnPosition = new Vector3(x, y, 0.0f);
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
