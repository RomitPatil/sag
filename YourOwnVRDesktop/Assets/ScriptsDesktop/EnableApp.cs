using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableApp : MonoBehaviour {
	public GameObject DApp;
	public GameObject GetScenesInfo;
	public GameObject uploadPanal;
	// Use this for initialization
	public bool start ;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			Debug.Log ("EnableApp");
			DApp.gameObject.SetActive (false);
			uploadPanal.SetActive (false);
			GetScenesInfo.GetComponent<GetScenesInfo> ().start = true;
			start = false;
		}
	}
}
