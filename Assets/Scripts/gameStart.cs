﻿// @author Anthony Lim
// Main menu of the game

using UnityEngine;
using System.Collections;

public class gameStart : MonoBehaviour {
	// Declare properties
	private float _buttonWidth = 150;
	private bool _mainMenu = true;
	private string _wo = "";

	// Our Startscreen GUI
	void OnGUI ()
	{
		if (_mainMenu) {
			if (GUI.Button (new Rect (Screen.width/2 - _buttonWidth, Screen.height/2, _buttonWidth, 30), "Start Game")) {
				startGame();
			}

			if (GUI.Button (new Rect (Screen.width/2 - _buttonWidth, Screen.height/2 + 40, _buttonWidth, 30), "Options")) {
				optionsMenu();
			}

			if (GUI.Button (new Rect (Screen.width/2 - _buttonWidth, Screen.height/2 + 80, _buttonWidth, 30), "Exit Game")) {
				quitGame();
			}
		} else {
			//GUI.TextField(new Rect(50, 50, 50, 50), "He", 44);
			_wo = GUI.TextArea(new Rect(50, 50, 50, 50), "He", 3);
			GUI.Label(new Rect(150, 150, 50, 50), "Testing");

			if (GUI.Button (new Rect(100, 100, 100, 100), "Go Back")) {
				_mainMenu = true;
			}
		}
	}

	void Update() {
		print (_wo);
	}
	
	private void startGame() {
		print ("Starting game");

		// Initialize various managers for the game
		// Singleton pattern
		DontDestroyOnLoad(gameManager.Instance);
		DontDestroyOnLoad (inputManager.Instance);
		gameManager.Instance.startState();
	}

	private void optionsMenu() {
		print ("Entering Options menu");

		_mainMenu = false;
		DontDestroyOnLoad (inputManager.Instance);
	}

	private void quitGame() {
		print ("Quitting game");

		DontDestroyOnLoad(gameManager.Instance);
		gameManager.Instance.quitGame();  
	}
}