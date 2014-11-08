using UnityEngine;
using System.Collections;

public class TimedBehavior : MonoBehaviour {

	public float lifeTime = 2.0f;
	
	// Use this for initialization
	void Start () {
	
		DestroyObject(gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
