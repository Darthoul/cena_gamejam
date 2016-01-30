using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item {
	public string id;
	public List<string> actionList;

	public Item (string _id){
		this.id = _id;
		actionList = new List<string> ();
	}

	public void AddActionList (List<string> _actionList){
		foreach (string _action in _actionList) {
			actionList.Add(_action);
		}
	}
	
}
