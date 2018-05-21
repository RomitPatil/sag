using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour {
	public GameObject desktopApp;
	public GameObject AppPreview;
	public Button SaveButton;
	public Button PreviewButton;


	public GameObject ContainerObj ;

	public List<string> ImageURLs = new List<string> ();
	public List<string> ImageNAMEs = new List<string> ();
	public int NumberOfScenes;
	[System.Serializable]
	public struct Container {public string SceneTitle; public string ImageName; public string ImageURLPath;  public Hotspot[] hotspots; }
	[System.Serializable]
	public struct Hotspot
	{
		public string typeName;
		public string hotspotsName;
		public Transform hotspots;
		public Vector3 hotspotPosition;
		public Quaternion hotspotRotation;
		public int NavigateNo;
		public string NavigateScene;
		public Quaternion CustomLocationRot;
		public Vector2 InfoBoxSize;
		public string InfoText;
		public int InfoFontSize;
		public Transform ActionHotspot;
		public Vector3 ActionHotspotPos;
		public Quaternion ActionHotspotRot;
		public Vector3 NavigatePoint;
		public Vector3 ActionColliderPos;
		public Quaternion ActionColliderPosRot;
		public Quaternion NavigatePointRot;
		public Quaternion ActionRotation;
		public Vector3[] ActionDots;
	}

	public Container[] scene;
	public GameObject LeftPanel;

	void Start () {
	//	desktopApp = GameObject.Find ("DesktopApp");
	//	 AppPreview = GameObject.Find ("AppPreview");
		ContainerObj = GameObject.Find ("NavigationCanvas");
		LeftPanel = GameObject.Find ("LeftPanel");
		AppPreview.SetActive (false);
	}

	public void FillData() {
		LeftPanel.SetActive (false);
		for (int i = 0; i < ContainerObj.transform.childCount; i++) {
			ContainerObj.transform.GetChild (i).gameObject.SetActive (true);
		}


		int no = ContainerObj.transform.childCount;
		NumberOfScenes = ContainerObj.transform.childCount;
		scene = new Container[no];
		ImageURLs.Clear ();
		ImageNAMEs.Clear ();
		for (int i = 0; i < ContainerObj.transform.childCount; i++) {
			scene [i].SceneTitle = ContainerObj.transform.GetChild (i).name;
			scene [i].ImageName = ContainerObj.transform.GetChild (i).GetComponent<ImageName> ().imageName;
			scene [i].ImageURLPath = ContainerObj.transform.GetChild (i).GetComponent<ImageName> ().imageURL;

			scene [i].hotspots = new Hotspot[ContainerObj.transform.GetChild(i).transform.childCount]; 
		
			ImageURLs.Add (scene [i].ImageURLPath);
			ImageNAMEs.Add (scene [i].ImageName);
			for (int j =0 ; j < ContainerObj.transform.GetChild(i).transform.childCount ; j++){
			//	string NavType = ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.name.Substring (0, 7);
				string NavType = "";
				if (ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.name.Length > 7) {
					if (ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.name.Substring (0, 7) == "Hotspot") {
						NavType = "Hotspot";
						scene [i].hotspots [j].typeName = NavType;


					}
				}
				if (ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.name.Length > 5) {
					if (ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.name.Substring (0, 5) == "Arrow") {
						NavType = "Arrow";
						scene [i].hotspots [j].typeName = NavType;
					}
				}
				if (ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.name.Length > 4) {
					if (ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.name.Substring (0, 4) == "Info") {
						NavType = "Info";
						scene [i].hotspots [j].typeName = NavType;
						scene [i].hotspots [j].InfoBoxSize = ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.GetComponent<HotspotControl> ().InfoBoxSize;
						scene [i].hotspots [j].InfoText = ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.GetComponent<HotspotControl> ().InfoText;
						scene [i].hotspots [j].InfoFontSize = ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.GetComponent<HotspotControl> ().InfoFontSize;
					}
				}
				if (ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.name.Length > 6) {
					if (ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.name.Substring (0, 6) == "Action") {
						NavType = "Action";
						scene [i].hotspots [j].typeName = NavType;
						scene [i].hotspots [j].ActionDots = ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.GetComponent<ActionHotspot> ().Dots;
						scene [i].hotspots [j].NavigatePoint = ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.GetComponent<ActionHotspot> ().NavigatePoint.gameObject.transform.localPosition;
						scene [i].hotspots [j].ActionColliderPos = ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.GetComponent<ActionHotspot> ().ActionCollider.gameObject.transform.position;
						scene [i].hotspots [j].NavigatePointRot = ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.GetComponent<ActionHotspot> ().NavigatePoint.gameObject.transform.localRotation;
						scene [i].hotspots [j].ActionColliderPosRot = ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.GetComponent<ActionHotspot> ().ActionCollider.gameObject.transform.rotation;
						scene [i].hotspots [j].ActionRotation = ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.GetComponent<ActionHotspot> ().dotPrf.gameObject.transform.rotation;
					}
				}
				Debug.Log ("Navetype" + NavType);
				scene [i].hotspots [j].hotspotsName = ContainerObj.transform.GetChild (i).transform.GetChild (j).gameObject.name;
				scene [i].hotspots [j].hotspots = ContainerObj.transform.GetChild (i).transform.GetChild (j).transform;
				scene [i].hotspots [j].hotspotPosition = ContainerObj.transform.GetChild (i).transform.GetChild (j).transform.position;
				scene [i].hotspots [j].hotspotRotation = ContainerObj.transform.GetChild (i).transform.GetChild (j).transform.rotation;
				scene [i].hotspots [j].NavigateNo = ContainerObj.transform.GetChild (i).transform.GetChild (j).GetComponent<HotspotControl> ().navigationNo;
				scene [i].hotspots [j].NavigateScene = ContainerObj.transform.GetChild (i).transform.GetChild (j).GetComponent<HotspotControl> ().navigationSceneName;
				scene [i].hotspots [j].CustomLocationRot = ContainerObj.transform.GetChild (i).transform.GetChild (j).GetComponent<HotspotControl> ().CustomLocROt;
			}
				
		}

		for (int i = 0; i < ContainerObj.transform.childCount; i++) {
			ContainerObj.transform.GetChild (i).gameObject.SetActive (false);
		}

		GameObject saveData = GameObject.Find ("Singleton");
		saveData.GetComponent<SaveLoad> ().Save ();
		ContainerObj.transform.GetChild (0).gameObject.SetActive (true);
		LeftPanel.SetActive (true);
		PreviewButton.interactable = true;
	}

	void Update () {
		if (ContainerObj.transform.childCount != 0) {
			SaveButton.interactable = true;
		}else{
			SaveButton.interactable = false;
			PreviewButton.interactable = false;
		}
	}

	public void PreviewApp () {



		AppPreview.SetActive (true);
		AppPreview.GetComponent<EnableApp> ().enabled = true;
		AppPreview.GetComponent<EnableApp> ().start = true;
		//GetScenesInfo.GetComponent<GetScenesInfo> ().start = true;
	}
}

