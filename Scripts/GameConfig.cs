using UnityEngine;
using System.Collections;

public class GameConfig {

	// is background music on
	public bool backgroundMusicOn;

	// is sound effect on
	public bool soundEffectOn;

	public string levelToLoad;

	// the best score
	public int bestScore;

	private GameConfig() {
		bestScore = PlayerPrefs.GetInt("BestScore");
		if (PlayerPrefs.HasKey("BGSound")) {
			backgroundMusicOn = PlayerPrefs.GetInt("BGSound") == 1;
			soundEffectOn = PlayerPrefs.GetInt("SFX") == 1;
		} else {
			backgroundMusicOn = true;
			soundEffectOn = true;
		}

		levelToLoad = "InGame";
	}

	public void SaveConfig() {
		PlayerPrefs.SetInt("BestScore", bestScore);
		PlayerPrefs.SetInt("BGSound", backgroundMusicOn ? 1 : 0);
		PlayerPrefs.SetInt("SFX", soundEffectOn ? 1 : 0);
		PlayerPrefs.Save();
	}
	// 
	private static GameConfig instance;

	// get an instance of a game config
	public static GameConfig GetInstance() {
		if (instance == null) {
			instance = new GameConfig();
		}
		return instance;
	}
}
