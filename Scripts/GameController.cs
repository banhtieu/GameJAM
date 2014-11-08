using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	// the information dialog
	public GameObject informationDialog;

	// the instance of the Game Controller
	private static GameController instance;

	// is game paused
	private bool isPaused;


	// Show information
	public void ShowInfo(string text) {
		informationDialog.GetComponent<Animator>().Play("InformationShow");
		GameObject story = informationDialog.transform.Find("Story").gameObject;
		story.GetComponent<Text>().text = text;
		isPaused = true;
	}

	public void Resume() {
		isPaused = false;
		informationDialog.GetComponent<Animator>().Play("InformationHide");
	}

	// pause the game
	public void Pause() {
		if (isPaused) {
			Resume();
		} else {
			isPaused = true;
		}
	}

	// check if game is paused
	public bool IsPaused() {
		return isPaused;
	}

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// get current game instance
	public static GameController GetInstance() {
		return instance;
	}

}
