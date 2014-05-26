using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour {
//	public GameObject[] waves;

	//public GameObject bomber;
	public GameObject fighter;

	public GameObject explosion;
	public GameObject shieldPickUp;
	public GameObject scientistPickUp;

	public GameObject player;
	public GameObject theBase;

	public Vector3 spawnValues;

	public float spawnWait;
	public float startWait;
	public float waveWait;

	public float minScientistHorizSpawnDistance;

	public int initialBaseHealth;

	public GUIText scoreText;
	public GUIText shieldText;
	public GUIText baseHealthText;
	public GUIText restartText;
	public GUIText gameOverText;

	private int score;

	private bool gameOver;
	private bool restart;
	private bool paused;

	static int nextScreenShotNum = 1;
	private float nextScreenShotTime = 0.0f;
	

	// Use this for initialization
	void Start() {
		gameOver = false;
		restart = false;
		paused = false;
		gameOverText.text = "";
		restartText.text = "";

		score = 0;
		UpdateScore();

		StartCoroutine(SpawnWaves());
	}


	void Update() {
		if (paused) {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				paused = false;
				Time.timeScale = 1.0f;
				Application.LoadLevel("TitleScreen");
			}
			else if (Input.GetKeyDown (KeyCode.Space)) {
				paused = false;
				gameOverText.text = "";
				restartText.text = "";
				Time.timeScale = 1.0f;
			}
			return;
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			paused = true;
			gameOverText.text = "Paused";
			restartText.text = "<Esc> to quit, <space> to resume";
			Time.timeScale = 0.0f;
		}

		if (Input.GetKeyDown(KeyCode.Return) && Time.time >= nextScreenShotTime) {
			Application.CaptureScreenshot("ProjectTethysScreenshot" + nextScreenShotNum + ".png");
			nextScreenShotNum++;
			nextScreenShotTime = Time.time + 1.0f;
		}

		if (restart) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}


  IEnumerator SpawnWaves() {
    // Delay before launching the first wave.
    yield return new WaitForSeconds(startWait);
    
    int waveIndex = 0;
    while (!gameOver) {
      for (int i = 0; i < 6; ++i) {
        Vector3 spawnPos = new Vector3(
          Random.Range(-spawnValues.x, spawnValues.x),
          Random.Range(-spawnValues.y, spawnValues.y),
          -30.0f
        );
        Quaternion spawnOrient = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        Instantiate(fighter, spawnPos, spawnOrient);
        yield return new WaitForSeconds(spawnWait);
      }
      waveIndex++;

      // Help the player out after every second wave.
      if (waveIndex % 2 == 0) {
        SpawnShieldPickUp();
      }
      
      // Drop in some scientists...
      if (waveIndex % 4 == 3) {
        SpawnScientistPickUp();
      }

      yield return new WaitForSeconds(waveWait - spawnWait);
    }

    restartText.text = "Press <space> to restart";
    restart = true;
  }

  /*
	IEnumerator SpawnWaves() {
		// Delay before launching the first wave.
		yield return new WaitForSeconds(startWait);

		int waveIndex = 0;
		while (!gameOver) {
			// Instantiate the next wave.
			GameObject wave = waves[waveIndex];
			waveIndex = (waveIndex + 1) % waves.Length;
			Instantiate(wave, wave.transform.position, wave.transform.rotation);

			// Help the player out after every second wave.
			if (waveIndex % 2 == 0) {
				SpawnShieldPickUp();
			}

			// Drop in some scientists...
			if (waveIndex % 4 == 3) {
				SpawnScientistPickUp();
			}

			// Pause before launching the next wave.
			yield return new WaitForSeconds(waveWait);
		}
		
		restartText.text = "Press <space> to restart";
		restart = true;
	}
 */ 


	void SpawnShieldPickUp() {
		float x = Random.Range(-spawnValues.x, spawnValues.x);
		float y = spawnValues.y + 15.0f;
		Vector3 spawnPosition = new Vector3(x, y, 0.0f);
		Instantiate(shieldPickUp, spawnPosition, Quaternion.identity);
	}


	void SpawnScientistPickUp() {
		// Ensure that scientists don't get dropped straight on to the base.
		float x = Random.Range(-spawnValues.x + minScientistHorizSpawnDistance, spawnValues.x - minScientistHorizSpawnDistance);
		if (x >= 0.0f) {
			x += minScientistHorizSpawnDistance;
		}
		else {
			x -= minScientistHorizSpawnDistance;
		}

		float y = spawnValues.y + 15.0f;
		Vector3 spawnPosition = new Vector3(x, y, 0.0f);
		Instantiate(scientistPickUp, spawnPosition, Quaternion.identity);
	}


	public void AddScore(int extraPoints) {
		score += extraPoints;
		UpdateScore();
	}


	public void UpdateBaseHealth(int newBaseHealth) {
		int percentage = (newBaseHealth * 100) / initialBaseHealth;
		baseHealthText.text = "Base " + percentage + "% operational";
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


	public bool IsPaused() {
		return paused;
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
