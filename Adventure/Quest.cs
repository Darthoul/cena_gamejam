using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class QuestArchive {
	
	public static readonly List<Quest> active = new List<Quest>();
	public static readonly List<Quest> inactive = new List<Quest>();
	public static readonly List<Quest> completed = new List<Quest>();
	
	public static int Count { get { return (active.Count + inactive.Count + completed.Count); } }

	public static void Add(Quest _quest) {
		inactive.Add (_quest);
	}

	public static void Activate(string _id) {
		Quest quest = null;
		for (int i = 0; i < inactive.Count; i++) {
			quest = inactive [i];
			if (quest.id == _id) {
				break;
			} else {
				quest = null;
			}
		}
		if (quest != null) {
			inactive.Remove (quest);
			active.Add (quest);
			//CallOnActivate(quest);
		} else {
			Debug.LogError ("There's no quest with id: " + _id);
		}
	}

	public static void CallOnActivate (Quest quest) {
		foreach (Quest.Call call in quest.calls) {
			if (call.activateParams != null && call.activateParams.Count != 0) {
				StoryEvents.instance.SendMessage (call.onActivate, call.activateParams);
			} else if (call.onActivate != null) {
				StoryEvents.instance.SendMessage (call.onActivate);
			}
		}
	}

	public static void Check (string _action, string _target, int _ammount) {
		Quest quest;
		for (int i = 0; i < active.Count; i++) {
			quest = active [i];
			if (quest.Check (_action, _target, _ammount)) {
				i--;
				active.Remove(quest);
				completed.Add(quest);
				CallOnActivate(quest);
				Debug.LogWarning ("Quest " + quest.id + " completed!");
				foreach (string toActId in quest.toActivateIds) {
					Activate(toActId);
				}
			}
		}
	}

	public static void Check (string _action, string _target, string _overTarget, int _ammount) {
		Quest quest;
		for (int i = 0; i < active.Count; i++) {
			quest = active [i];
			if (quest.Check (_action, _target, _overTarget, _ammount)) {
				i--;
				active.Remove(quest);
				completed.Add(quest);
				CallOnActivate(quest);
				Debug.LogWarning ("Quest " + quest.id + " completed!");
				foreach (string toActId in quest.toActivateIds) {
					Activate(toActId);
				}
			}
		}
	}
}

public class Quest {

	public class Task {

		public string action;
		public string target;
		public string overTarget;
		public int ammount;

		private int current;
		private bool done;

		public bool Check(ref string _action, ref string _target, int _ammount) {
			if ((action == _action) && (target == _target)) {
				current += _ammount;
				if (current >= ammount) {
					done = true;
				}
			}
			return done;
		}

		public bool Check(ref string _action, ref string _target, ref string _overTarget, int _amount){
			if ((action == _action) && (target == _target) && (overTarget == _overTarget)) {
				current += _amount;
				if(current >= ammount){
					done = true;
				}
			}
			return done;
		}
	}

	public class Call {
		public string onActivate;
		public List<string> activateParams;
	}

	public readonly string id;
	public string description;
	public List<Task> tasks;
	public List<string> toActivateIds;

	public List<Call> calls;

	public Quest (string _id) {
		this.id = _id;
	}

	public static Quest Create (string _id) {
		Quest quest = new Quest (_id);
		quest.tasks = new List<Task> ();
		quest.toActivateIds = new List<string> ();
		quest.calls = new List<Call> ();

		return quest;
	}

	public Task AddTask (string _action, string _target, int _ammount, string _overTarget = null) {
		Task task = new Task ();
		task.action = _action;
		task.target = _target;
		task.overTarget = _overTarget;
		task.ammount = _ammount;

		tasks.Add (task);
		return task;
	}

	public Call AddCall (string _onActivate) {
		Call call = new Call ();
		call.onActivate = _onActivate;
		call.activateParams = new List<string> ();

		calls.Add (call);
		return call;
	}

	public bool Check (string _action, string _target, int _ammount) {
		Task task = new Task ();
		int count = 0;
		for (int i = 0; i < tasks.Count; i++) {
			task = tasks [i];
			if (task.Check (ref _action, ref _target, _ammount)) {
				count++;
				if (count >= tasks.Count) {
					return true;
				}
			}
		}
		return false;
	}

	public bool Check (string _action, string _target, string _overTarget, int _ammount) {
		Task task = new Task ();
		int count = 0;
		for (int i = 0; i < tasks.Count; i++) {
			task = tasks [i];
			if (task.Check (ref _action, ref _target, ref _overTarget, _ammount)) {
				count++;
				if (count >= tasks.Count) {
					return true;
				}
			}
		}
		return false;
	}

}
