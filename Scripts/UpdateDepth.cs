﻿using UnityEngine;
using System.Collections;

public class UpdateDepth : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float y = transform.position.y;
		transform.position = new Vector3(transform.position.x, transform.position.y, y);
	}
}
