using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryEvents : MonoBehaviour {

	static public StoryEvents instance;
	public AdventureLoader advLoader;
	// Use this for initialization
	void Awake () {
		instance = this;
		advLoader.LoadAll ();
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	static public void MakeActorSay (List<string> questParams) {

	}
}
