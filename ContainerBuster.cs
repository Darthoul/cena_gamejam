using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContainerBuster : MonoBehaviour {
	static public Container container;
	// Use this for initialization
	void Start () {
		//Suck containers life
	}
	
	// Update is called once per frame
	void Update () {
		//Keep sucking containers life
		if (Input.GetMouseButtonDown (0) && container != null) {
			//The container is antiq, we dont want to keep track of him
			container.SetOffContainer();
			//Erase the container as it never existed
			container = null;
		}
	}

}
