using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryEvents : MonoBehaviour {

	static public StoryEvents instance;
	public AdventureLoader advLoader;

	public Transform speechBox;
	public Transform endingBox;
	public Transform transitionBox;
	public Transform tableBox;

	public static Speech currentSpeech;
	public static Dialog currentDialog;
	public static Transform currentTransition;
	public static Transform currentTable;
	public bool speaking = false;
	public bool inTransition = false;
	private float timer = 0;
	// Use this for initialization
	void Awake () {
		instance = this;
		advLoader.LoadAll ();
		speechBox = GameObject.Find ("SpeechBox").transform;
		endingBox = GameObject.Find ("GameOverBox").transform;
		transitionBox = GameObject.Find ("EpisodeBox").transform;
		tableBox = GameObject.Find ("TableBox").transform;
		SpeechArchive.Search ("s1").MoveToActive ();
		SpeechArchive.Search ("s2").MoveToActive ();
		SpeechArchive.Search ("s3").MoveToActive ();
		LoadQuestSection (1, 7);
		currentTransition = transitionBox.FindChild ("chapter1");
		currentTable = transitionBox.GetChild (0);

		inTransition = true;
		SpeechPrompt ();
	}

	void Start () {
	
	}
	#region CallOnActivate 
	public void ChangeLevel (List<string> questParams) {
		instance.endingBox.FindChild (questParams [0]).position = Vector3.zero;
	}
	public void ChapterTransition (List<string> questParams) {
		currentTransition = instance.transitionBox.FindChild (questParams [0]);
		inTransition = true;
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
		if (inTransition) {
			if (!speaking) {
				if (currentTransition.position != Vector3.zero) {
					currentTransition.position = Vector3.zero;
				}
				timer += Time.deltaTime;
				if (timer >= 3) {
					currentTransition.localPosition = Vector3.zero;
					currentTable.localPosition = Vector3.zero;
					switch (currentTransition.name) {
					case "chapter2":
						SpeechArchive.Search ("s14").MoveToActive ();
						SpeechArchive.Search ("s15").MoveToActive ();
						currentTable = tableBox.GetChild(1);
						break;
					case "chapter3":
						SpeechArchive.Search ("s68").MoveToActive ();
						currentTable = tableBox.GetChild(2);
						break;
					case "chapter4":
						SpeechArchive.Search ("s69").MoveToActive ();
						SpeechArchive.Search ("s70").MoveToActive ();
						currentTable = tableBox.GetChild(3);
						break;
					}
					currentTable.position = Vector3.zero;
					SpeechPrompt ();
					currentTransition = null;
					inTransition = false;
					timer = 0;
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
