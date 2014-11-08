using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	// the information dialog
	public GameObject informationDialog;

	public GameObject ingameMenuDialog;

	public Text scoreText;

	// the instance of the Game Controller
	private static GameController instance;

	// is game paused
	private bool isPaused;

	// score
	private int score;

	public void ShowInGameMenu() {
		ingameMenuDialog.GetComponent<Animator>().Play("InGameMenuShow");
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

	// get current game instance
	public static GameController GetInstance() {
		return instance;
	}

}
