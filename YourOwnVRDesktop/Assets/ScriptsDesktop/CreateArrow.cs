using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class CreateArrow : MonoBehaviour {

	public GameObject ArrowPrefab;
	public UICanvasControl ui_Canvas_control;
	// Use this for initialization
	void Start () {
//		var button = GetComponent<Button> ();
//		button.onClick.AddListener (OnClick);
	}

	public void OnClick() {
		ui_Canvas_control.arrow = true;
		ui_Canvas_control.hotspot = false;
		ui_Canvas_control.Info = false;
		List<string> listItem = new List<string> ();
		for (int i = 0; i < ui_Canvas_control.sceneList.Count; i++) {
			if (ui_Canvas_control.sceneList [i].activeSelf) {
				string sceneName = ui_Canvas_control.sceneList [i].GetComponent<DetailScene> ().nav_canvas_scene_Obj.name.Substring (0, ui_Canvas_control.sceneList [i].GetComponent<DetailScene> ().nav_canvas_scene_Obj.name.LastIndexOf ('_'));
				listItem.Add (sceneName);
			}
		}

		ui_Canvas_control.centerPanel.SetActive (true);
		ui_Canvas_control.dropdownlistobject.ClearOptions ();
		ui_Canvas_control.dropdownlistobject.name = ui_Canvas_control.SceneTitle.name + "_" + ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Arrowlist.Count.ToString ();
		Debug.Log (ui_Canvas_control.dropdownlistobject.name);
		ui_Canvas_control.dropdownlistobject.value = 0;
		ui_Canvas_control.dropdownlistobject.AddOptions (listItem);

		GameObject ArrowObj = GameObject.Instantiate (ArrowPrefab);
		ArrowObj.name = "Arrow " + ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Arrowlist.Count.ToString ();
		ArrowObj.transform.parent = ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().nav_canvas_scene_Obj.transform;
		ArrowObj.transform.eulerAngles = Camera.main.transform.eulerAngles;
		ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Arrowlist.Add (ArrowObj);
		ui_Canvas_control.hotspotTitle.text = ArrowObj.name;
		ui_Canvas_control.OnDropDownChangeValue ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
