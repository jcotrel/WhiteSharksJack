﻿using UnityEngine;
using System.Collections;

public class TestMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.F12)) {

			MainMenu GS = new MainMenu(2);
			GS.startGame();

		}
	
	}

}
