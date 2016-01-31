using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dialog {

	public readonly string id;
	public string text;
	public float duration;
	public string actorID;
	public string animationType;
	public string nextID;

	public static Dialog Create (string _id) {
		Dialog dialog = new Dialog (_id);
		return dialog;
	}

	public void AddContent (string _text, float _duration, string _actorID, string _animationType, string _nextID = null) {
		this.text = _text;
		this.duration = _duration;
		this.nextID = _nextID;
	}

	public Dialog (string _id) {
		this.id = _id;
	}
}

public class DialogArchive {

	public static readonly List<Dialog> dialogs = new List<Dialog> ();

	public static int Count { get { return (dialogs.Count); } }

	public static void Add (Dialog dialog) {
		dialogs.Add (dialog);
	}

	public static Dialog Search (string _id) {
		for (int i = 0; i < dialogs.Count; i++) {
			if (dialogs[i].id == _id) {
				return dialogs[i];
			}
		}
		return null;
	}
}
