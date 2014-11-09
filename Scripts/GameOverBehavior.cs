using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverBehavior : MonoBehaviour {

	public Text scoreText;


	public void PlayAgain() {
		Application.LoadLevel("InGame");
	}

	public void MainMenu() {
		Application.LoadLevel("MainMenu");
	}

	// Use this for initialization
	void Start () {
		scoreText.text = "" + GameController.GetInstance().GetScore();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
