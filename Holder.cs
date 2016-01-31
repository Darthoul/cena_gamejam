using UnityEngine;
using System.Collections;

public class Holder : MonoBehaviour {

	public Speech speech;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver () {
		if (Input.GetMouseButtonDown (0)) {
			QuestArchive.Check("say", speech.id, 1);
			StoryEvents.currentSpeech = speech;
			StoryEvents.MakeActorSay(speech.responseID);
			StoryEvents.SpeechClear();
		}
	}

	
	public void SetText () {
		GetComponent<TextMesh> ().text = speech.showText;
	}
}
