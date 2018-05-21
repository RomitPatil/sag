using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MobileJsonData : MonoBehaviour {


	public GameObject ActionObj;
	public GameObject SceneObject;
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
		public Vector3 hotspotPosition;
		public Quaternion hotspotRotation;
		public int NavigateNo;
		public string NavigateScene;
		public Quaternion CustomLocationRot;


		public Transform ActionHotspot;
		public Vector3 ActionHotspotPos;
		public Vector3 ActionHotspotRot;
		public Vector3 NavigatePoint;
		public Vector3 ActionColliderPos;
		public Vector3[] ActionDots;
		public Quaternion ActionColliderPosRot;
		public Quaternion NavigatePointRot;
		public Quaternion ActionRotation;
	}

	public Container[] scene;
	//public GameObject LeftPanel;

	public MobileJsonData playerData;
	private string Filepath ;
	public bool start ;
	void Start () {
	//	Filepath = Path.Combine(Application.dataPath, "JsonData.txt");
		//Filepath = Filepath.Replace ("/Assets", "");

		Load ();
	}
	void Update () {

	}

	public void Load() {
		Debug.Log ("Load");
		TextAsset file = Resources.Load ("JsonData")as TextAsset;
		string jsonString = file.text ;
		Debug.Log (jsonString);
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

			//	sceneObject.GetComponent<SetSceneLoaction> ().CustumLocation = scene [i].CustomLocation;
			//	sceneObject.GetComponent<SetSceneLoaction> ().CustumRot = scene [i].CustomLocationRot;

			if (scene [i].hotspots.Length != 0) {
				for (int j = 0; j < scene [i].hotspots.Length; j++) {
					GameObject hotspot = null;

					if (scene [i].hotspots [j].typeName == "Action") {
						hotspot = GameObject.Instantiate (ActionObj);
					}
					hotspot.transform.parent = SceneContainer.transform.GetChild (i).transform;
					hotspot.gameObject.name = scene[i].hotspots[j].hotspotsName;

					hotspot.GetComponent<HotspotFunction> ().NavigateScene = scene [i].hotspots [j].NavigateScene;
					hotspot.GetComponent<HotspotFunction> ().NavigateNo = scene [i].hotspots [j].NavigateNo;
					hotspot.GetComponent<HotspotFunction> ().CustomLocROt = scene [i].hotspots [j].CustomLocationRot;
				
					hotspot.GetComponent<HotspotFunction> ().HotspotType = scene [i].hotspots [j].typeName; 
					hotspot.GetComponent<HotspotFunction> ().ActionDots = scene [i].hotspots [j].ActionDots;
					hotspot.GetComponent<HotspotFunction> ().ActionPos = scene [i].hotspots [j].NavigatePoint;
					hotspot.GetComponent<HotspotFunction> ().ActionRotation = scene [i].hotspots [j].ActionRotation;
					hotspot.GetComponent<HotspotFunction> ().ActionColliderPos = scene [i].hotspots [j].ActionColliderPos;
					hotspot.GetComponent<HotspotFunction> ().ActionColliderPosRot = scene [i].hotspots [j].ActionColliderPosRot;
					hotspot.GetComponent<HotspotFunction> ().NavigatePointRot = scene [i].hotspots [j].NavigatePointRot;





					hotspot.transform.position = scene [i].hotspots [j].hotspotPosition;
					hotspot.transform.rotation = scene [i].hotspots [j].hotspotRotation;
				//	hotspot.transform.localScale = scene [i].hotspots [j].hotspots.localScale;
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


}
