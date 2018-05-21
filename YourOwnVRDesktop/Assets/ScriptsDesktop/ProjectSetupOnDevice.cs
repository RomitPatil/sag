using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ProjectSetupOnDevice : MonoBehaviour {
	public GameObject SceneObject;
	public GameObject NavHotspotPrfb;
	public GameObject ArrowOBJ;
	public GameObject SceneContainer;
	public int NumberOfScenes;

	public List<string> ImageURLs = new List<string> ();
	public List<string> ImageNAMEs = new List<string> ();
	[System.Serializable]
	public struct Container {public string SceneTitle; public string ImageName;public string ImageURLPath; public Hotspot[] hotspots; }
	[System.Serializable]
	public struct Hotspot
	{
		public string typeName;
		public string hotspotsName;
		public Vector3 hotspots;
		public int NavigateNo;
		public string NavigateScene;
	}

	public Container[] scene;
	public ProjectSetupOnDevice PlayerData;
	// Use this for initialization
	void Start () {
		Load ();
	}

	public void Load () {
		Debug.Log ("Load");
		string jsonString = File.ReadAllText (Application.persistentDataPath + "/JsonData.txt");
		JsonUtility.FromJsonOverwrite (jsonString, PlayerData);
	}

	public void SceneSetup () {

		Debug.Log ("Scene Setup");

	}

	// Update is called once per frame
	void Update () {
		
	}
}
