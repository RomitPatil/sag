using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewAppData : MonoBehaviour {

	public List <string> SenceTexturePath = new List<string> ();
	[System.Serializable]
	public struct container
	{
		public string SceneName;
		public hotspotContainer[] HotspotData;
	}
	[System.Serializable]
	public struct hotspotContainer
	{
		public string hotspotName;
		public Vector3 Position;
		public string NagivateToScene;
	}
	public container[] ScenesData;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
