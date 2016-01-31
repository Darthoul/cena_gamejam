using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryEvents : MonoBehaviour {

	static public StoryEvents instance;
	public AdventureLoader advLoader;

	public Transform speechBox;
	public Transform endingBox;

	public static Speech currentSpeech;
	public static Dialog currentDialog;
	public bool speaking = false;
	private float timer = 0;
	// Use this for initialization
	void Awake () {
		instance = this;
		advLoader.LoadAll ();
		speechBox = GameObject.Find ("SpeechBox").transform;
		endingBox = GameObject.Find ("GameOverBox").transform;
		SpeechArchive.Search ("s1").MoveToActive ();
		SpeechArchive.Search ("s2").MoveToActive ();
		SpeechArchive.Search ("s3").MoveToActive ();
		LoadQuestSection (1, 7);
		SpeechPrompt ();
	}

	void Start () {
	
	}
	#region CallOnActivate 
	public static void ChangeLevel (List<string> questParams) {
		Debug.LogWarning (questParams [0]);
		instance.endingBox.FindChild (questParams [0]).position = Vector3.zero;
	}
	public static void ChapterTransition (List<string> questParams) {

	}
	#endregion

	public void LoadQuestSection (int _from, int _to) {
		for (int i = _from; i <= _to; i++) {
			QuestArchive.Activate("q" + i);
		}
	}
	// Update is called once per frame
	void Update () {
	
		if (speaking) {
			timer += Time.deltaTime;
			if (timer >= currentDialog.duration) {
				SetText ("");
				timer = 0;
				if (currentDialog.nextID != null) {
					MakeActorSay(currentDialog.nextID);
				} else {
					speaking = false;
					SpeechPrompt ();
				}
			}
		}
	}

	static public void MakeActorSay (string _responseID) {
		currentDialog = DialogArchive.Search (_responseID);
		instance.SetText (currentDialog.text);
		instance.speaking = true;
	}

	public void SetText (string _text) {
		GameObject.Find (currentDialog.actorID).transform.GetChild (0).GetComponent<TextMesh> ().text = _text;
	}

	static public void SpeechPrompt () {
		for (int i = 0; i < SpeechArchive.active.Count; i++) {
			Debug.LogError(SpeechArchive.active.Count);
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
