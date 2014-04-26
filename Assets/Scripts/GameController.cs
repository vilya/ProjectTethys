using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject bomber;
	public GameObject fighter;

	public Vector3 spawnValues;

	public float spawnWait;
	public float startWait;
	public float waveWait;


	// Use this for initialization
	void Start() {
		StartCoroutine(SpawnWaves(bomber));
	}
	

	IEnumerator SpawnWaves(GameObject enemy) {
		// Delay before launching the first wave.
		yield return new WaitForSeconds(startWait);
		while (true) {
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
	}
}
