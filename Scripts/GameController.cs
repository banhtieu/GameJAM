using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// the instance of the Game Controller
	private static GameController instance;

	// is game paused
	private bool isPaused;

	// pause the game
	public void Paused() {
		isPaused = true;
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
