using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject bomber;
	public GameObject fighter;

	public Vector3 spawnValues;


	// Use this for initialization
	void Start() {
		SpawnWaves(bomber);
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	void SpawnWaves(GameObject enemy) {
		for (int i = 0; i < 10; ++i) {
			Vector3 spawnPosition = new Vector3(
				Random.Range(-spawnValues.x, spawnValues.x),
				Random.Range(-spawnValues.y, spawnValues.y),
				0.0f
			);
			Instantiate(enemy, spawnPosition, Quaternion.identity);
		}
	}
}
