using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;


public class CreateInfo : MonoBehaviour {

	public GameObject InfoPrefab;
	public UICanvasControl ui_Canvas_control;
	// Use this for initialization
	void Start () {
//		var button = GetComponent<Button> ();
//		button.onClick.AddListener (OnClick);
	}

	public void OnClick() {
		ui_Canvas_control.arrow = false;
		ui_Canvas_control.hotspot = false;
		ui_Canvas_control.Info = true;
		List<string> listItem = new List<string> ();
		for (int i = 0; i < ui_Canvas_control.sceneList.Count; i++) {
			if (ui_Canvas_control.sceneList [i].activeSelf) {
				string sceneName = ui_Canvas_control.sceneList [i].GetComponent<DetailScene> ().nav_canvas_scene_Obj.name.Substring (0, ui_Canvas_control.sceneList [i].GetComponent<DetailScene> ().nav_canvas_scene_Obj.name.LastIndexOf ('_'));
				listItem.Add (sceneName);
			}
		}

		ui_Canvas_control.centerPanel.SetActive (true);
		ui_Canvas_control.dropdownlistobject.ClearOptions ();
		ui_Canvas_control.dropdownlistobject.name = ui_Canvas_control.SceneTitle.name + "_" + ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Infolist.Count.ToString ();
		Debug.Log (ui_Canvas_control.dropdownlistobject.name);
		ui_Canvas_control.dropdownlistobject.value = 0;
		ui_Canvas_control.dropdownlistobject.AddOptions (listItem);


		GameObject InfoObj = GameObject.Instantiate (InfoPrefab);
		InfoObj.name = "Info " + ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Infolist.Count.ToString ();
		InfoObj.transform.parent = ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().nav_canvas_scene_Obj.transform;
		InfoObj.transform.eulerAngles = Camera.main.transform.eulerAngles;
		ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Infolist.Add (InfoObj);
		ui_Canvas_control.hotspotTitle.text = InfoObj.name;
		ui_Canvas_control.OnDropDownChangeValue ();

	}

	public void Savetext () {
	
		string temp = ui_Canvas_control.InfoTxt.text.ToString ();

	}
	// Update is called once per frame
	void Update () {
		
	}
}
