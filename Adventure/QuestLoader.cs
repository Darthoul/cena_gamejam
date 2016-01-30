using UnityEngine;
using System.Collections;

public class QuestLoader : MonoBehaviour {

	public TextAsset questDefs;

	public bool HasDefs () {
		return (QuestArchive.Count > 0);
	}

	public void LoadAll () {

		if (questDefs != null) {
			QuestParser qp = new QuestParser ();
			qp.Start (questDefs.text);
			Debug.Log("Quest Definitions successfully Loaded");
		} else {
			Debug.LogError("File not reached: Non existent");
		}
	}
}
