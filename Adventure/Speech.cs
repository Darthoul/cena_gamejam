using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Speech {

	public readonly string id;
	public string showText;
	public string text;
	public string responseID;

	public List<string> toActive;

	public void SendListToActive () {
		foreach (string speechID in toActive) {
			SpeechArchive.Search (speechID).MoveToActive ();
		}
	}

	public void MoveToActive () {
		SpeechArchive.total.Remove (this);
		SpeechArchive.active.Add (this);
	}

	public void AddContent (string _showText, string _text, string _responseID = null) {
		this.showText = _showText;
		this.text = _text;
		this.responseID = _responseID;
	}

	public static Speech Create (string _id) {
		Speech speech = new Speech (_id);
		speech.toActive = new List<string> ();
		return speech;
	}

	public Speech (string _id) {
		this.id = _id;
	}
}

public class SpeechArchive {

	public static readonly List<Speech> active = new List<Speech>();
	public static readonly List<Speech> total = new List<Speech>();

	public static int Count { get { return (total.Count + active.Count); } }

	public static Speech Search (string _id) {
		for (int i = 0; i < total.Count; i++) {
			if (total [i].id == _id) {
				return total [i];
			}
		}
		Debug.LogError ("Speech with id: " + _id + " not found");
		return null;
	}
	
	public static Speech SearchActive (string _id) {
		for (int i = 0; i < active.Count; i++) {
			if (active [i].id == _id) {
				return active [i];
			}
		}
		Debug.LogError ("Speech with id: " + _id + " not found");
		return null;
	}

	public static void SetToActive (string _fromID) {
		foreach (string nextActive in Search (_fromID).toActive) {
			active.Add(Search(nextActive));
		}
	}

	public static void ClearActive () {
		while (active.Count > 0) {
			total.Add (active [0]);
			active.RemoveAt (0);
		}
	}

}
