using UnityEngine;
using System.Collections;

public class BaseBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// update when game is playing
	public virtual void ConditionalUpdate() {
	}

	// Update is called once per frame
	void Update () {

		if (!GameController.GetInstance().IsPaused()){
			ConditionalUpdate();
		}
	}
}
