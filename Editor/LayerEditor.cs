using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;


[CanEditMultipleObjects()]
[CustomEditor(typeof(MeshRenderer),true)]
public class LayerEditor : Editor {

	public override void OnInspectorGUI() {

		GUILayout.Label ("Custom Sorting Order for 2D");

		Renderer rend = target as Renderer;

		rend.sortingOrder = EditorGUILayout.IntField ("Sorting Order", rend.sortingOrder);

		if (GUI.changed) {
			EditorUtility.SetDirty (target);
		}
	}
}
