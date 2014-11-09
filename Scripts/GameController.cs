using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	// the information dialog
	public GameObject informationDialog;

	public GameObject ingameMenuDialog;

	public Animator gameOverNotice;

	public Text scoreText;

	// 
	public Toggle backgroundSoundToggle;
	
	// sound fx toggle.
	public Toggle sfxSoundToggle;

	public AudioSource backgroundSound;

	public AudioSource gameOverSound;

	public AudioSource walkEffect;

	// the instance of the Game Controller
	private static GameController instance;

	// is game paused
	private bool isPaused;

	// score
	private int score;

	private bool isGameOver;

	public void GameOver() {
		// the game is over
		isPaused = true;
		gameOverNotice.enabled = true;

		if (GameConfig.GetInstance().backgroundMusicOn){
			backgroundSound.Stop();
		}

		if (GameConfig.GetInstance().soundEffectOn){
			walkEffect.Stop();
			gameOverSound.Play();
		}

		isGameOver = true;
		Invoke("LoadGameOverLevel", 3.0f);
	}

	// Is Game Over
	public bool IsGameOver() {
		return isGameOver;
	}

	// Load a level
	public void LoadLevel(string level) {
		Application.LoadLevel(level);
	}

	public void SetLevel(string name) {
		GameConfig.GetInstance().levelToLoad = name;
	}

	public void ShowTutorial() {
		Application.LoadLevel("Tutorial");
	}

	// load game over
	public void LoadGameOverLevel() {
		Application.LoadLevel("GameOver");
	}

	public void ShowInGameMenu() {
		ingameMenuDialog.GetComponent<Animator>().Play("InGameMenuShow");

		GameConfig gameConfig = GameConfig.GetInstance();
		backgroundSoundToggle.isOn = gameConfig.backgroundMusicOn;
		sfxSoundToggle.isOn = gameConfig.soundEffectOn;
	}

	// Show information
	public void ShowInfo(string text, Sprite image) {
		informationDialog.GetComponent<Animator>().Play("InformationShow");
		GameObject story = informationDialog.transform.Find("Story").gameObject;
		story.GetComponent<Text>().text = text;

		GameObject imageObject = informationDialog.transform.Find("Image").gameObject;
		imageObject.GetComponent<Image>().sprite = image;
		isPaused = true;
	}

	public void Resume() {
		isPaused = false;
		informationDialog.GetComponent<Animator>().Play("InformationHide");
		ingameMenuDialog.GetComponent<Animator>().Play("InformationHide");

		if (GameConfig.GetInstance().soundEffectOn) {
			walkEffect.Play();
		}
	}

	// pause the game
	public void Pause() {
		if (!isPaused) {
			isPaused = true;
			ShowInGameMenu();
		}
	}

	// check if game is paused
	public bool IsPaused() {
		return isPaused;
	}

	// Use this for initialization
	void Start () {
		instance = this;
		score = 0;

		GameConfig config = GameConfig.GetInstance();

		if (scoreText) {
			if (config.backgroundMusicOn) {
				backgroundSound.Play();
			}

			if (config.soundEffectOn) {
				walkEffect.Play();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Increase a score	
	public void IncreaseScore() {
		UpdateScore(score + 1);
	}

	// update score
	void UpdateScore(int score) {
		this.score = score;
		scoreText.text = "" + score;
	}

	// Get Current Score
	public int GetScore() {
		return score;
	}

	// get current game instance
	public static GameController GetInstance() {
		return instance;
	}

	public void PlayGame() {
		Application.LoadLevel(GameConfig.GetInstance().levelToLoad);
	}


	// Update sound settings
	public void UpdateSoundSettings() {
		GameConfig config = GameConfig.GetInstance();
		config.backgroundMusicOn = backgroundSoundToggle.isOn;
		config.soundEffectOn = sfxSoundToggle.isOn;
		
		config.SaveConfig();
		Debug.Log ("BG: " + config.backgroundMusicOn);
		Debug.Log("SFX: " + config.soundEffectOn);

		if (config.backgroundMusicOn) {
			backgroundSound.Play();
		} else {
			backgroundSound.Stop();
		}
	}
}
