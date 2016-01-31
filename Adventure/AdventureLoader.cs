using UnityEngine;
using System.Collections;

public class AdventureLoader : MonoBehaviour {

	public TextAsset diagDefs;
	public TextAsset questDefs;
	
	public bool HasDefs () {
		return (HasQuests() && HasDialogs());
	}

	public bool HasDialogs () {
		return (DialogArchive.Count > 0);
	}
	public bool HasQuests () {
		return (QuestArchive.Count > 0);
	}
	
	public void LoadAll () {

		if (diagDefs != null) {
			DialogParser dp = new DialogParser ();
			dp.Start (diagDefs.text);
			Debug.Log("Dialog Definitions successfully Loaded");
		} else {
			Debug.LogError("File not reached: Non existent");
		}


		if (questDefs != null) {
			QuestParser qp = new QuestParser ();
			qp.Start (questDefs.text);
			Debug.Log("Quest Definitions successfully Loaded");
		} else {
			Debug.LogError("File not reached: Non existent");
		}
	}
}
