using UnityEngine;
using System.Collections;

public class DialogParser : Parser {

	Dialog dialog;

	protected override void CallOnNode (ref string name, System.Collections.Generic.List<ParserValue> pairs)
	{
		if (name == "Dialogs/Dialog") {
			dialog = Dialog.Create(pairs[0].value);
			DialogArchive.Add(dialog);
		}
		if (name == "Dialogs/Dialog/Content") {
			if (pairs.Count <= 4) {
				dialog.AddContent (pairs [0].value, float.Parse (pairs [1].value), pairs[2].value, pairs[3].value);
			} else {
				dialog.AddContent (pairs [0].value, float.Parse (pairs [1].value), pairs[2].value, pairs[3].value, pairs[4].value);
			}
		}
	}
}
