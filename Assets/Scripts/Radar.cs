using UnityEngine;
using System.Collections;

public class Radar : MonoBehaviour {
	public Texture radarBackground;
	public Texture playerBlip;
	public Texture enemyBlip;
	public Texture powerupBlip;
	public Texture scientistBlip;

	public Rect radarBounds;
	public Rect gameBounds;

	public GameObject player;


	void Start() {
		radarBounds.x = (Screen.width - radarBounds.width) / 2.0f;
		radarBounds.y = Screen.height - radarBounds.height - 4.0f;
	}


	void OnGUI() {
		// Draw the background for the radar.
		GUI.DrawTexture(radarBounds, radarBackground);

		// Draw the enemies.
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
			DrawBlip(enemy, enemyBlip);
		}

		// Draw the power-ups.
		foreach (GameObject powerUp in GameObject.FindGameObjectsWithTag("ShieldPickUp")) {
			DrawBlip(powerUp, powerupBlip);
		}
		foreach (GameObject powerUp in GameObject.FindGameObjectsWithTag("ScientistPickUp")) {
			DrawBlip(powerUp, powerupBlip);
		}

		// Draw the scientists.
		foreach (GameObject scientist in GameObject.FindGameObjectsWithTag("Scientist")) {
			DrawBlip(scientist, scientistBlip);
		}
		
		// Draw the player.
		DrawBlip(player, playerBlip);
	}


	void DrawBlip(GameObject item, Texture tex) {
		float x = (item.transform.position.x - gameBounds.xMin) / gameBounds.width;
		float y = 1.0f - (item.transform.position.y - gameBounds.yMin) / gameBounds.height;
		if (x < 0.0f || x > 1.0f || y < 0.0f || y > 1.0f) {
			return;
		}
		x = x * 0.9f + 0.05f;
		y = y * 0.9f + 0.05f;

		float width = tex.width;
		float height = tex.height;
		float left = x * radarBounds.width + radarBounds.xMin - width / 2.0f;
		float top = y * radarBounds.height + radarBounds.yMin - height / 2.0f;

		GUI.DrawTexture(new Rect(left, top, width, height), tex);
	}
}
