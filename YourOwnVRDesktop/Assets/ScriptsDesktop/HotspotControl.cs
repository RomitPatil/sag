using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotspotControl : MonoBehaviour {

	public Texture CustumLocImage;
	public Quaternion CustomLocROt;
	public float X_Value;
	public float Y_Value;
	public Slider X_Slider;
	public Slider Y_Slider;
	public Toggle FixHotspot;
	public string InfoText;
	public int InfoFontSize;
	public Vector2 InfoBoxSize;
	public int navigationNo;
	public string navigationSceneName;
	public UICanvasControl ui_Canvas_control;
	public GameObject NavigationCanvas;
	public Color ActiveItemColor = new Color();
	public Color InActiveItemColor = new Color();
	public bool hotspot;
	public bool arrow;
	public bool Info;
	public bool ActionHotspot;

	public bool Active;


	void OnEnable (){
		if (ui_Canvas_control != null) {
			if (hotspot) {
				ui_Canvas_control.gameObject.GetComponent<CustomLocation> ().ActiveHotspot = gameObject;
			}
		}
	}
	// Use this for initialization
	void Start () {
		ui_Canvas_control = GameObject.FindObjectOfType<UICanvasControl> ();
		//X_Slider = ui_Canvas_control.X_Slider;
	//	Y_Slider = ui_Canvas_control.Y_Slider;
		FixHotspot =	ui_Canvas_control.FixHotspot;


	//	float Cal_X = (X_Value * 360f) -180f;
	//	float Cal_Y = (Y_Value * 360f) -180f;
	//	transform.eulerAngles = new Vector3 (Cal_Y,Cal_X,0f);
	//	print (X_Value+","+Y_Value);
//		X_Slider.value = X_Value;
	//	Y_Slider.value = Y_Value;
//		print (X_Slider.value+","+Y_Slider.value);

		if (hotspot) {
			ui_Canvas_control.gameObject.GetComponent<CustomLocation> ().ActiveHotspot = gameObject;
		}
	}

	// Update is called once per frame
	void Update () {
		if (gameObject.name == ui_Canvas_control.hotspotTitle.text) {
			if (gameObject.transform.GetChild (0).GetComponent<Image> ()) {
				gameObject.transform.GetChild (0).GetComponent<Image> ().color = ActiveItemColor;
				Active = true;
			}
		} else {
			if (gameObject.transform.GetChild (0).GetComponent<Image> ()) {
				gameObject.transform.GetChild (0).GetComponent<Image> ().color = InActiveItemColor;
				Active = false;
			}
		}


		if (CustumLocImage != null) {
			gameObject.transform.GetChild (1).gameObject.SetActive (true);
			gameObject.transform.GetChild (1).GetComponent<RawImage> ().texture = CustumLocImage;
		}
	
		//		float Cal_X = (X_Slider.value * 360f) -180f;
		//		float Cal_Y = (Y_Slider.value * 360f) -180f;
		//
		//		X_Value = X_Slider.value;
		//		Y_Value = Y_Slider.value;
		//
		//		transform.eulerAngles = new Vector3 (Cal_Y,Cal_X,0f);
		//		Camera.main.transform.eulerAngles = new Vector3 (Cal_Y,Cal_X,0f);
	}


	public void HotspotClick(){
		
		ui_Canvas_control.FixHotspot.isOn = false;
		ui_Canvas_control.arrow = arrow;
		ui_Canvas_control.hotspot = hotspot;
		ui_Canvas_control.Info = Info;
		ui_Canvas_control.ActionHotspot = ActionHotspot;
		List<string> listItem = new List<string> ();
		for (int i = 0; i < ui_Canvas_control.sceneList.Count; i++) {
			if (ui_Canvas_control.sceneList [i].activeSelf) {
				string sceneName = ui_Canvas_control.sceneList [i].GetComponent<DetailScene>().nav_canvas_scene_Obj.name.Substring (0,ui_Canvas_control.sceneList [i].GetComponent<DetailScene>().nav_canvas_scene_Obj.name.LastIndexOf ('_'));
				listItem.Add (sceneName);
			}
		}
		ui_Canvas_control.centerPanel.SetActive (true);
		ui_Canvas_control.dropdownlistobject.name = ui_Canvas_control.SceneTitle.name+"_"+gameObject.name.Substring(gameObject.name.LastIndexOf(' ')+1);
		ui_Canvas_control.dropdownlistobject.value = navigationNo;
		ui_Canvas_control.dropdownlistobject.ClearOptions();
		ui_Canvas_control.dropdownlistobject.AddOptions (listItem);


	//	X_Slider.value = X_Value;
	//	Y_Slider.value = Y_Value;

		ui_Canvas_control.hotspotTitle.text = name;
		string deleteText = ui_Canvas_control.hotspotTitle.text.Substring(0 , ui_Canvas_control.hotspotTitle.text.Length - 2); 
		ui_Canvas_control.DeleteTitle.text = deleteText + " Delete";
		if (deleteText == "Hotspot") {
			ui_Canvas_control.dropdownlistobject.gameObject.SetActive (true);
			if (CustumLocImage != null) {
				ui_Canvas_control.CustomLocFix.isOn = true;
				ui_Canvas_control.gameObject.GetComponent<CustomLocation> ().CLSmallPicture.gameObject.SetActive (true);
				ui_Canvas_control.gameObject.GetComponent<CustomLocation> ().CLSmallPicture.texture = CustumLocImage;

			} else {
				ui_Canvas_control.CustomLocFix.isOn = false;
			}
		} else {
			ui_Canvas_control.dropdownlistobject.gameObject.SetActive (false);
		}

		if (deleteText == "Info") {
			ui_Canvas_control.InfoTxt.gameObject.transform.parent.gameObject.SetActive (true);
			InfoText = ui_Canvas_control.InfoTxt.text.ToString ();
		} else {
			ui_Canvas_control.InfoTxt.gameObject.transform.parent.gameObject.SetActive (false);
		}
			
		Debug.Log(gameObject.name + "::" + ui_Canvas_control.hotspotTitle.text);

		GameObject camera = Camera.main.gameObject;
		//camera.GetComponent<Raycasting> ().cube = gameObject;
		camera.GetComponent<Raycasting> ().OnCollider = true;
		ui_Canvas_control.OnDropDownChangeValue ();
	}

	public void HotspotDelete () {
		

		ui_Canvas_control.centerPanel.SetActive (false);
		string SeletedHotspot = ui_Canvas_control.SceneTitle.name+"_"+gameObject.name.Substring(gameObject.name.LastIndexOf(' ')+1);

		ui_Canvas_control.hotspotTitle.text = "";
		for (int i=0; i < NavigationCanvas.transform.childCount; i++) {
			if (NavigationCanvas.transform.GetChild (i).gameObject.activeInHierarchy) {
			
				for (int j = 0; j < NavigationCanvas.transform.GetChild (i).transform.childCount; j++) {
					string hotspotName = NavigationCanvas.transform.GetChild (i).transform.GetChild (j).name;
					Debug.Log (hotspotName);
					hotspotName = hotspotName.Substring (hotspotName.Length - 1);
					Debug.Log (hotspotName);
					hotspotName = hotspotName + "_" + navigationNo;
					Debug.Log (hotspotName);
					if (hotspotName == SeletedHotspot){
						GameObject.Destroy (NavigationCanvas.transform.GetChild (i).transform.GetChild (j).gameObject);
						}
				}
			}
		}

		}


}
