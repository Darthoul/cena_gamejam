using UnityEngine;
using System.Collections;

public class SpeechParser : Parser {

	Speech speech;

	protected override void CallOnNode (ref string name, System.Collections.Generic.List<ParserValue> pairs)
	{
		if (name == "Speaches/Speech") {
			speech = Speech.Create (pairs [0].value);
			SpeechArchive.total.Add (speech);
		}
		if (name == "Speaches/Speech/Content") {
			if (pairs.Count <= 2) {
				speech.AddContent (pairs [0].value, pairs [1].value);
			} else {
				speech.AddContent (pairs [0].value, pairs [1].value, pairs [2].value);
			}
		}
		if (name == "Speaches/Speech/Content/ToActive") {
			foreach (ParserValue pair in pairs) {
				speech.toActive.Add (pair.value);
			}
		}
	}
}
