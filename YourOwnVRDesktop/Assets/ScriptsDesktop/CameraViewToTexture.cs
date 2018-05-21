using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewToTexture : MonoBehaviour {

	public Texture2D screenCap;

	// Use this for initialization
	void Start () {
		screenCap.ReadPixels (new Rect (Camera.main.pixelRect.position.x, Camera.main.pixelRect.position.y , Camera.main.pixelWidth , Camera.main.pixelHeight),0,0);
		screenCap.Apply ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
