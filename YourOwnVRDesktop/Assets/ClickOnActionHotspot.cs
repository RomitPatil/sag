using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnActionHotspot : MonoBehaviour {

	public NewActionHotspot newActionHotspotScript;
	// Use this for initialization
	void Start () {
		newActionHotspotScript = gameObject.transform.parent.GetComponent<NewActionHotspot> ();
	}

	public void OnClick () {
		Debug.Log ("On Hotspot Click");
		newActionHotspotScript.OnClickOnActionHotspot ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
