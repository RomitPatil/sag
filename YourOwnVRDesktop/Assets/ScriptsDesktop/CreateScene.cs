using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CreateScene : MonoBehaviour {

	public GameObject cam;
	public UICanvasControl ui_Canvas_control;
	public InputField scene_name_InputFeild;
	public GameObject navigatio_Canvas;
	public GameObject scenepanelPrefab;
	public GameObject item_sceneprefab;
	public GameObject item_scene_parent_prefab;

	// Use this for initialization
	void OnEnable () {
		scene_name_InputFeild.text = "";
	}

	// Update is called once per frame
	void Update () {

	}

	public void CreateNewScene(InputField sceneName){
		

		if (!string.IsNullOrEmpty (sceneName.text)) {
			GameObject.FindWithTag("Dome").GetComponent<MeshRenderer> ().material.mainTexture = null;
			cam.transform.position = Vector3.zero;
			cam.transform.eulerAngles = Vector3.zero;
			GameObject panel = GameObject.Instantiate (scenepanelPrefab);
			panel.transform.parent = navigatio_Canvas.transform;
			panel.gameObject.AddComponent<ImageName> ();
			panel.name = sceneName.text + "_"+(ui_Canvas_control.sceneList.Count).ToString();

			GameObject sceneObj = GameObject.Instantiate (item_sceneprefab);
			sceneObj.transform.parent = item_scene_parent_prefab.transform;
			sceneObj.name = sceneName.text + ui_Canvas_control.sceneList.Count;
			sceneObj.transform.localScale = Vector3.one;
			sceneObj.transform.GetChild (0).GetComponentInChildren<Text> ().text = sceneName.text;
			sceneObj.transform.GetComponent<DetailScene> ().nav_canvas_scene_Obj = panel;
			ui_Canvas_control.sceneList.Add (sceneObj);

			ui_Canvas_control.centerPanel.SetActive (false);
			ui_Canvas_control.rightPanel.SetActive (true);
			ui_Canvas_control.SceneThumbnail.texture = null;
			ui_Canvas_control.SceneTitle.text = sceneName.text;
			ui_Canvas_control.SceneTitle.name = (ui_Canvas_control.sceneList.Count -1).ToString();

			gameObject.SetActive (false);

		}
	}

	public void CancelClick(){
		gameObject.SetActive (false);
	}

}
