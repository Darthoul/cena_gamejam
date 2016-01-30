﻿using UnityEngine;
using System.Collections;

public class Container : MonoBehaviour {
	public Item item;
	private bool isActive = false;	

	// Use this for initialization
	void Start () {
		this.item = new Item (gameObject.name);
		ItemArchive.containers.Add (this);
	}
	
	// Update is called once per frame
	void Update () {

		//Debuging for Hilble
		if (isActive) {
			showActions(item);
			//Debug.LogError("Oh my gosh, Im super active");
		}

	}

	public void SetOffContainer(){
		isActive = false;
	}

	//Maybe this is an item.... what should happend if I pass my mouse over it? 
	void OnMouseOver(){
		//show glow
		ShowGlow ();
		//if mouse input change state to active
		if (Input.GetMouseButtonDown (0)) {
			isActive = !isActive;
		}
	}

	//Just a fancy glow for the item to shine out loud!
	void ShowGlow(){
		Debug.Log ("Showing my glow");
	}

	//Please handle my Teddy Container
	void OnMouseExit(){
		//Le pasamos el container al Container Buster
		if (isActive && ContainerBuster.container != this) {
			ContainerBuster.container = this;
		}

	}

	//I want to handle it bitch
	void OnMouseEnter(){
		if (isActive && ContainerBuster.container != this) {
			ContainerBuster.container = null;
		}
	}

	//Gimmie some actions baby!
	private void showActions(Item item){
		Debug.LogError (item.actionList.Count);
		//Separation between actions
		float rotAngle = 360/item.actionList.Count;

		for (int i = 0; i < item.actionList.Count; i++) {
			Vector3 actionPos = Vector3.up;
			actionPos = MathLib.Rotate(actionPos, rotAngle * i);
			Debug.DrawRay(transform.position, actionPos);
			Debug.LogWarning(item.actionList[i]);
		}
	}

}
