using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemArchive : MonoBehaviour {

	public class MyStruct {
		//suck my struct
		public string id;
		public List<string> actionList;

		public MyStruct(){
			actionList = new List<string>();
		}

	}

	static public List<Container> containers = new List<Container>();
	static public List<MyStruct> myStructList = new List<MyStruct>();

	// Use this for initialization
	void Start () {

		//Crear Archivo
		FillArchive ();

		//Revisar uno por uno los containers 
		foreach(Container _container in containers){
			//Revisar si el id del container está en el archivo (debería)
			//Y guardar el index del archivo en una variable
			int index = SearchForItemActions (_container.item.id);
			//Si en efecto lo encontramos, 
			Debug.Log ("Index: " + index);
			if(index != -1){
				//Agregar las acciones que están en el archivo al item.
				AddActionsToContainerItem(index, _container.item);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Create an archive of items to add custom data for them
	void FillArchive(){
		MyStruct temp = new MyStruct();
		// Item 1
		CustomItemCreator (temp, "item_test", "usar", "dar", "comentar", "mirar");
		myStructList.Add (temp);

		temp = new MyStruct();
		//Item 2
		CustomItemCreator(temp, "item_test2", "usar", "dar", "comentar");
		myStructList.Add (temp);
	}

	//Helper for FillArchive (adds the custom data we where talking about)
	void CustomItemCreator(MyStruct myStruct, string _id, string _action1, string _action2=null, string _action3=null, string _action4=null){
		myStruct.id = _id;
		myStruct.actionList.Add (_action1);
		if (_action2 != null) {
			myStruct.actionList.Add (_action2);
		}
		if (_action3 != null) {
			myStruct.actionList.Add (_action3);
		}
		if (_action4 != null) {
			myStruct.actionList.Add (_action4);
		}


	}


	//Method that compares containers (that already exist in the scene) with the archive and creates the correct actions on them.
	private int SearchForItemActions(string _id){
		Debug.Log (myStructList.Count);
		for (int i = 0; i < myStructList.Count; i++) {
			Debug.Log ("ID found: " + myStructList[i].id);
			if(myStructList[i].id == _id){
				return i;
			}
		}
		Debug.LogError ("No item found with id " + _id);
		return -1;
	}	



	private void AddActionsToContainerItem(int index, Item item){
		foreach (string action in myStructList[index].actionList) {
			item.actionList.Add(action);
		}
	}

}


	