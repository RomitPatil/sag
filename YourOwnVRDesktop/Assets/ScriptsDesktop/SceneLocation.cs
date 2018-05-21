using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLocation : MonoBehaviour {

	public UICanvasControl UI_Canvas_Control;

	public Quaternion HomeLocationRotation;


	public bool HomeLocation;


	// Use this for initialization

	void OnEnable (){

		if (HomeLocation) {
			Camera.main.transform.rotation = HomeLocationRotation;
		} else {
			Camera.main.transform.rotation = Quaternion.Euler (Vector3.zero);
		}
	}

	
	// Update is called once per frame


	void OnDisable () {
		Debug.Log ("Disable the object:");

	}
}
