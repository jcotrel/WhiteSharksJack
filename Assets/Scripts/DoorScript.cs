﻿/*
 door object.

changes: added some variables.-John Mai 1/12/2014
*/ 
using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	
	//id = 0 means go back one room, 1 means go to next room
	public int id;
	public float x, y;

	//Mouse icon information
	public string mouseOverIcon = "Door_Icon";
	private string defaultIcon = "Walk_Icon";		//The standard mouse icon when not hovering over an object
	private playerScript player;

	void Start(){

		player = (playerScript) FindObjectOfType(typeof(playerScript));

	}

	void Update(){

		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

	}

	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (defaultIcon);
	}

	public void OnMouseEnter(){
		if (player != null)
			if (player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}

}
