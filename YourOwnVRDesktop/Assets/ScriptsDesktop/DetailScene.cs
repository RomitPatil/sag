using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailScene : MonoBehaviour {


	public GameObject nav_canvas_scene_Obj;
	public string ImagePath;
	public Texture texture;
	public GameObject MainDome;
	public GameObject HotspotDome;
	public List<GameObject> hotspotlist = new List<GameObject> ();
	public List<GameObject> Arrowlist = new List<GameObject> ();
	public List <GameObject> Infolist = new List<GameObject> ();
	public List <GameObject> ActionHotspot = new List<GameObject> ();
	public UICanvasControl ui_Canvas_control;

	public Color ActiveSceneColor = new Color();
	public Color InActiveSceneColor = new Color();

	// Use this for initialization
	void Start () {
		MainDome = GameObject.FindWithTag ("Dome");
		HotspotDome = GameObject.FindWithTag ("HotspotDome");
		if (HotspotDome != null) {
			HotspotDome.SetActive (false);
		}
		ui_Canvas_control = transform.parent.parent.parent.GetComponentInParent<UICanvasControl> ();
	}

	// Update is called once per frame
	void Update () {

		if (ui_Canvas_control.SceneTitle.text.Equals (nav_canvas_scene_Obj.name.Substring (0, nav_canvas_scene_Obj.name.LastIndexOf ('_'))))
			nav_canvas_scene_Obj.SetActive (true);
		else
			nav_canvas_scene_Obj.SetActive (false);

		string tem;
		tem = gameObject.name.Substring (0, gameObject.name.Length - 1); 
		Debug.Log (tem + "::" + ui_Canvas_control.SceneTitle.text);
		if (tem  == ui_Canvas_control.SceneTitle.text) {
			gameObject.transform.GetChild (0).GetComponent<Image> ().color = ActiveSceneColor;
		} else {
			gameObject.transform.GetChild (0).GetComponent<Image> ().color = InActiveSceneColor;
		}
	}

	public void SceneSelectClick(){
		Debug.Log ("Click On Scene");
	
		MainDome.SetActive (true);
		if (HotspotDome != null) {
			HotspotDome.SetActive (false);
		}

		ui_Canvas_control.centerPanel.SetActive (false);
		ui_Canvas_control.rightPanel.SetActive (true);
		GameObject.FindObjectOfType<MeshRenderer> ().material.mainTexture = null;
		ui_Canvas_control.SceneThumbnail.texture = null;
		string sceneName = nav_canvas_scene_Obj.name.Substring (0,nav_canvas_scene_Obj.name.LastIndexOf ('_'));
		int sceneNoInList = int.Parse(nav_canvas_scene_Obj.name.Substring (nav_canvas_scene_Obj.name.LastIndexOf ('_')+1));
		ui_Canvas_control.SceneTitle.text = sceneName;
		ui_Canvas_control.SceneTitle.name = sceneNoInList.ToString();
		if (texture != null) {
			GameObject dome = GameObject.FindGameObjectWithTag ("Dome");
			dome.GetComponent<MeshRenderer>().material.mainTexture = texture;
			ui_Canvas_control.SceneThumbnail.texture = texture;
		}
		Camera.main.transform.position = Vector3.zero;
		Camera.main.transform.eulerAngles = Vector3.zero;
	

	}

	public void SceneDeleteClick(){
		gameObject.SetActive (false);
		nav_canvas_scene_Obj.SetActive (true);
		nav_canvas_scene_Obj.GetComponent<ImageName> ().Delete = true;
		//nav_canvas_scene_Obj.SetActive (false);
		for (int i = 0; i < ui_Canvas_control.sceneList.Count; i++) {
			ui_Canvas_control.centerPanel.SetActive (false);
			ui_Canvas_control.rightPanel.SetActive (true);
			GameObject.FindObjectOfType<MeshRenderer> ().material.mainTexture = null;
			ui_Canvas_control.centerPanel.SetActive (false);
			ui_Canvas_control.rightPanel.SetActive (false);
			ui_Canvas_control.SceneThumbnail.texture = null;
			if (ui_Canvas_control.sceneList [i].activeSelf) {
				ui_Canvas_control.sceneList [i].GetComponent<DetailScene>().SceneSelectClick ();
				break;
			}
		}
	}
}
