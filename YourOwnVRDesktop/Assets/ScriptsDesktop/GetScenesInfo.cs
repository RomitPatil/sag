using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GetScenesInfo : MonoBehaviour {
	 
	public GameObject App;
	public GameObject appPreview;
	public GameObject navHotspotprefab;
	public GameObject ArrowObj;
	public GameObject InfoObj;
	public GameObject ActionObj;
	public GameObject SceneObject;
	public  GameObject UploadingPanel;
	public GameObject UploadingBackGround;
	public GameObject SceneContainer ;
	public int NumberOfScenes;
	public List<string> ImageURLs = new List<string> ();
	public List<string> ImageNAMEs = new List<string> ();
	[System.Serializable]
	public struct Container {public string SceneTitle; public string ImageName;public string ImageURLPath;  public Hotspot[] hotspots; }
	[System.Serializable]
	public struct Hotspot
	{
		public string typeName;
		public string hotspotsName;
		public Transform hotspots;
		public int NavigateNo;
		public string NavigateScene;
		public Quaternion CustomLocationRot;

		public Vector2 InfoBoxSize;
		public string InfoText;
		public int InfoFontSize;
		public Transform ActionHotspot;
		public Vector3 NavigatePoint;
		public Vector3 ActionColliderPos;
		public Vector3[] ActionDots;
		public Quaternion ActionColliderPosRot;
		public Quaternion NavigatePointRot;
		public Quaternion ActionRotation;
	}

	public Container[] scene;
	//public GameObject LeftPanel;

	public GetScenesInfo playerData;
	private string Filepath ;
	public bool start ;
	void Start () {
//		Debug.Log ("Start");
	appPreview = GameObject.Find ("AppPreview");
		Filepath = Path.Combine(Application.dataPath, "JsonData.txt");
		Filepath = Filepath.Replace ("/Assets", "");
		SceneContainer = GameObject.Find ("NavigationCanvasApp");
	
		App.SetActive (false);
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			BackToDesktopApp ();
		}

		if (start) {
			Load ();

			start = false;
		}
	}

	public void Load() {
		Debug.Log ("Load");
		string jsonString = File.ReadAllText (Filepath);
		JsonUtility.FromJsonOverwrite (jsonString, playerData);
		SceneSetup ();
	}

	public void SceneSetup(){
	
	
		Debug.Log ("SceneSatup");

			for (int i = 0; i < NumberOfScenes; i++) {
				GameObject sceneObject = GameObject.Instantiate (SceneObject);
				sceneObject.transform.parent = SceneContainer.transform;
				sceneObject.name = scene [i].SceneTitle;
				sceneObject.AddComponent<ImageName> ();
				sceneObject.GetComponent<ImageName> ().imageName = scene [i].ImageName;
				sceneObject.GetComponent<ImageName> ().AppPreview = true;
		//	sceneObject.GetComponent<SetSceneLoaction> ().CustumLocation = scene [i].CustomLocation;
		//	sceneObject.GetComponent<SetSceneLoaction> ().CustumRot = scene [i].CustomLocationRot;

				if (scene [i].hotspots.Length != 0) {
					for (int j = 0; j < scene [i].hotspots.Length; j++) {
					GameObject hotspot = null;
					if (scene [i].hotspots [j].typeName == "Hotspot") {
						hotspot = GameObject.Instantiate (navHotspotprefab);
					}
					if (scene [i].hotspots [j].typeName == "Arrow") {
						hotspot = GameObject.Instantiate (ArrowObj);
					}
					if (scene [i].hotspots [j].typeName == "Info") {
						hotspot = GameObject.Instantiate (InfoObj);
					}
					if (scene [i].hotspots [j].typeName == "Action") {
						hotspot = GameObject.Instantiate (ActionObj);
					}
					hotspot.transform.parent = SceneContainer.transform.GetChild (i).transform;
					hotspot.gameObject.name = scene[i].hotspots[j].hotspotsName;
				
						hotspot.GetComponent<HotspotFunction> ().NavigateScene = scene [i].hotspots [j].NavigateScene;
						hotspot.GetComponent<HotspotFunction> ().NavigateNo = scene [i].hotspots [j].NavigateNo;
						hotspot.GetComponent<HotspotFunction> ().CustomLocROt = scene [i].hotspots [j].CustomLocationRot;
					hotspot.GetComponent<HotspotFunction> ().InfoBoxSize = scene [i].hotspots [j].InfoBoxSize;
						hotspot.GetComponent<HotspotFunction> ().InfoText = scene [i].hotspots [j].InfoText;
				   		hotspot.GetComponent<HotspotFunction> ().InfoFontSize = scene [i].hotspots [j].InfoFontSize; 
					hotspot.GetComponent<HotspotFunction> ().HotspotType = scene [i].hotspots [j].typeName; 
					hotspot.GetComponent<HotspotFunction> ().ActionDots = scene [i].hotspots [j].ActionDots;
					hotspot.GetComponent<HotspotFunction> ().ActionPos = scene [i].hotspots [j].NavigatePoint;
					hotspot.GetComponent<HotspotFunction> ().ActionRotation = scene [i].hotspots [j].ActionRotation;
					hotspot.GetComponent<HotspotFunction> ().ActionColliderPos = scene [i].hotspots [j].ActionColliderPos;
					hotspot.GetComponent<HotspotFunction> ().ActionColliderPosRot = scene [i].hotspots [j].ActionColliderPosRot;
					hotspot.GetComponent<HotspotFunction> ().NavigatePointRot = scene [i].hotspots [j].NavigatePointRot;





					hotspot.transform.position = scene [i].hotspots [j].hotspots.position;
					hotspot.transform.rotation = scene [i].hotspots [j].hotspots.rotation;
					hotspot.transform.localScale = scene [i].hotspots [j].hotspots.localScale;
					}

				} else {
					GameObject.Destroy (sceneObject.transform.GetChild (0));
				}
			}

			for (int k = 0; k < SceneContainer.transform.childCount; k++) {
				SceneContainer.transform.GetChild (k).gameObject.SetActive (false);
			}

			SceneContainer.transform.GetChild (0).gameObject.SetActive (true);

		
	}

	public void BackToDesktopApp () {

		for (int i = 0; i < SceneContainer.transform.childCount; i++) {
			GameObject.Destroy (SceneContainer.transform.GetChild (i).gameObject);
		}
		App.SetActive (true);

		appPreview.GetComponent<EnableApp> ().enabled = false;
		appPreview.SetActive (false);
		UploadingPanel.SetActive (true);

	}

	public void MenuButton () {
		UploadingBackGround.SetActive (true);
		UploadingBackGround.transform.GetChild (0).gameObject.SetActive (true);
	}

	public void UploadButton () {
		UploadingBackGround.transform.GetChild (0).gameObject.SetActive (false);
		UploadingPanel.SetActive (true);
	}
	public void EditButton () {
		UploadingPanel.SetActive (false);
		UploadingBackGround.SetActive (false);

		for (int i = 0; i < SceneContainer.transform.childCount; i++) {
			GameObject.Destroy (SceneContainer.transform.GetChild (i).gameObject);
		}
		App.SetActive (true);

		appPreview.GetComponent<EnableApp> ().enabled = false;
		appPreview.SetActive (false);

	}
}
