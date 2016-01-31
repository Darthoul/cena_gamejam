using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryEvents : MonoBehaviour {

	static public StoryEvents instance;
	public AdventureLoader advLoader;

	public Transform speechBox;

	public static Speech currentSpeech;
	public static Dialog currentDialog;
	public bool speaking = false;
	// Use this for initialization
	void Awake () {
		instance = this;
		advLoader.LoadAll ();
		speechBox = GameObject.Find ("SpeechBox").transform;
		SpeechArchive.Search ("s1").MoveToActive ();
		SpeechArchive.Search ("s2").MoveToActive ();
		SpeechArchive.Search ("s3").MoveToActive ();
		SpeechPrompt ();
		QuestArchive.Activate ("q1");
		QuestArchive.Activate ("q2");
		QuestArchive.Activate ("q3");
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (speaking) {

		}
	}

	static public void MakeActorSay (string _responseID) {
		currentDialog = DialogArchive.Search (_responseID);
		Debug.LogWarning (DialogArchive.dialogs[0].actorID);
		GameObject.Find (currentDialog.actorID).transform.GetChild (0).GetComponent<TextMesh> ().text = currentDialog.text;
		instance.speaking = true;
	}

	static public void SpeechPrompt () {
		for (int i = 0; i < SpeechArchive.active.Count; i++) {
			instance.speechBox.GetChild(i).gameObject.SetActive(true);
			instance.speechBox.GetChild(i).GetComponent<Holder> ().speech = SpeechArchive.active[i];
			instance.speechBox.GetChild(i).GetComponent<Holder> ().SetText ();
		}
	}

	static public void SpeechClear () {
		for (int i = 0; i < instance.speechBox.childCount; i++) {
			instance.speechBox.GetChild(i).GetComponent<Holder> ().speech = null;
			instance.speechBox.GetChild(i).gameObject.SetActive(false);
		}
	}
}
