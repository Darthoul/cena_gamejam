using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestParser : Parser {

	Quest quest;

	protected override void CallOnNode (ref string name, List<ParserValue> pairs) {

		if (name == "Quests/Quest") {
			quest = Quest.Create (pairs [0].value);
			QuestArchive.Add(quest);
		}
		if (name == "Quests/Quest/Task") {
			if (pairs.Count <= 3) {
				quest.AddTask (pairs [0].value, pairs [1].value, int.Parse(pairs [2].value));
			} else {
				quest.AddTask (pairs [0].value, pairs [1].value, int.Parse(pairs [3].value), pairs[2].value);
			}
		}
		if (name == "Quests/Quest/Activates") {
			foreach (ParserValue pair in pairs) {
				quest.toActivateIds.Add(pair.value);
			}
		}
		if (name == "Quests/Quest/Adventure/Call") {
			quest.AddCall(pairs [0].value);
		}
		if (name == "Quests/Quest/Adventure/Call/ActivateParams") {
			for (int i = 0; i < pairs.Count; i++) {
				quest.calls [quest.calls.Count - 1].activateParams.Add (pairs [i].value);
			}
		}
	}
}
