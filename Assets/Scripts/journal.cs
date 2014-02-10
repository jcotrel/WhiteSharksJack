﻿//Adrian
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class journal : MonoBehaviour {

	private static journal instance;
	private static int MAX_NPC = 3;
	private static journal j;

	public static journal Instance {
		get {
			if (instance == null) {
				Debug.Log("JOURNAL: Instance null, creating new Journal");
				instance = new GameObject("journal").AddComponent<journal>();
			}
			return instance;
		}
	}

	public List<NPC> personsOfInterest;
	public ArrayList weaponList;

	//Defaults for non-visible NPC
	public static Sprite emptyPortrait;
	private string emptyName;

	//Grab view tab buttons. Will change to use gameobject find.
	public GameObject viewTab1;
	public GameObject viewTab2;
	public GameObject viewTab3;

	//Grab buttons and textfield from view. Will change to use gameobject find. Three lists for three different types of buttons.
	private static List<GameObject> viewTabList;
	private static List<GameObject> poiButtonList;
	public static List<GameObject> objectButtonList;

	public GameObject poiGrid;
	public GameObject objectGrid;

	public UILabel nameLabel;
	public UILabel descriptionLabel;
	
	public GameObject poiView;
	public GameObject mapView;

	//Handles player gamestate knowledge, not flavor text/progress
	public Dictionary knowledge; 

	public Dictionary getKnowledge(){
		return knowledge;
		}

	public void updateKnowledge(DictEntry newEntry){
		knowledge.updateDictionary (newEntry);
	}

	//Destroys duplicate UI Roots.
	void Awake () {
		if(!j){
			j = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy (gameObject);
		}
	}
	
	void Start () {
		//Default name for "invisible" person of interest.
		emptyName = "?????";

		//Persons of interest list.
		personsOfInterest = GameManager.npcList;

		//Weapon list once that's ready.
		//weaponList = GameManager.weaponList;


		//Listens for tab button presses in journal and runs onClick with button clicked as parameter.
		UIEventListener.Get (viewTab1).onClick += this.onClick;
		UIEventListener.Get (viewTab2).onClick += this.onClick;
		UIEventListener.Get (viewTab3).onClick += this.onClick;
		//Want to get rid of this too.
		viewTabList = new List<GameObject>();
		viewTabList.Add(viewTab1);
		viewTabList.Add(viewTab2);
		viewTabList.Add(viewTab3);

		//List of person of interest portrait buttons.
		poiButtonList = new List<GameObject>();
		objectButtonList = new List<GameObject>();

		initPoIView ();
		initObjView ();
		changeView (0);
	}

	//Single onclick function for any button in the journal.
	void onClick(GameObject button){
		if(viewTabList != null && viewTabList.Contains(button)){
			changeView (viewTabList.IndexOf(button));
			Debug.Log ("won't happen yet");
		}
		else if(poiButtonList.Contains(button)){
			changePOI(poiButtonList.IndexOf(button));
			Debug.Log ("poiButton!");
		}
		else if (objectButtonList.Contains (button)){
			changeObject(objectButtonList.IndexOf(button));
			Debug.Log ("objectbutton!");
		}
	}

	//----- Button type functions
	//Changes view when view tab is clicked.
	//Will make helper function for grid/view SetActive.
	void changeView(int viewNumber){
		switch (viewNumber) {
			case 0://PoI
				mapView.SetActive(false);
				poiView.SetActive(true);
				objectGrid.SetActive(false);
				poiGrid.SetActive(true);
				break;
			case 1://Object
				mapView.SetActive (false);
				poiView.SetActive(true);
				objectGrid.SetActive(true);
				poiGrid.SetActive(false);
				break;
			case 2://Map
				mapView.SetActive (true);
				poiView.SetActive(false);
				objectGrid.SetActive(false);
				poiGrid.SetActive(false);
				break;
		}
		clearLabels ();
	}

	//Changes PoI when a PoI portrait is clicked.
	void changePOI(int poiNumber){
		if(personsOfInterest[poiNumber].isVisible()){
			nameLabel.text = personsOfInterest[poiNumber].getElementName();
			descriptionLabel.text = personsOfInterest[poiNumber].getDescription();
		}
		else {
			nameLabel.text = emptyName; 
			descriptionLabel.text = emptyName;
		}
	}

	//Changes object/weapon being viewed when portrait is clicked.
	void changeObject(int objectNumber){
		//Code for grabbing weapon names and descriptions.
		/*if(weaponList[objectNumber].isVisible()){
			nameLabel.text = weaponList[objectNumber].getElementName();
			descriptionLabel.text = weaponList[objectNumber].getDescription();
		}
		else {
			nameLabel.text = emptyName; 
			descriptionLabel.text = emptyName;
		}*/
	}

	//Initialize PoI view.
	public void initPoIView(){
		//Add buttons to poi button list and put them in UI event listener.
		foreach (Transform child in poiGrid.transform){
			UIEventListener.Get(child.gameObject).onClick += this.onClick;
			poiButtonList.Add(child.gameObject);
		}

		for (int i = 0; i < personsOfInterest.Count; i++) {
			if(personsOfInterest[i] != null){
				poiButtonList[i].gameObject.GetComponent<UI2DSprite>().sprite2D = personsOfInterest[i].getProfileImage();
			}
			else {
				poiButtonList[i].gameObject.GetComponent<UI2DSprite>().sprite2D = emptyPortrait;
			}
		}
	}

	//Initialize obj view.
	public void initObjView(){
		//Add buttons to obj button list and put them in UI event listener.
		foreach (Transform child in objectGrid.transform){
			UIEventListener.Get(child.gameObject).onClick += this.onClick;
			objectButtonList.Add(child.gameObject);
		}
	}

	//Clear description labels. Might rename and add obj/poi grid on/off.
	void clearLabels(){
		nameLabel.text = "";
		descriptionLabel.text = "";
	}
}