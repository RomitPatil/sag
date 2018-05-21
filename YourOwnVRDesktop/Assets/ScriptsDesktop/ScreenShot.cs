using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour {

	public Texture2D screenCap ;
	Texture2D border ;
	bool shot = false;
	// Use this for initialization
	void Start () {
		screenCap = new Texture2D (300, 200, TextureFormat.RGB24, false);
		border = new Texture2D (2, 2, TextureFormat.ARGB32, false);
		border.Apply ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Mouse0)){
			StartCoroutine("Capture");
		}
	}

	void OnGUI () {
	//	GUI.DrawTexture (new Rect (Camera.main.pixelRect.position.x+70f , Camera.main.pixelRect.position.y-100f ,Camera.main.pixelWidth-150f , Camera.main.pixelHeight - 60f), border, ScaleMode.StretchToFill);// top 

		if (shot) {
			GUI.DrawTexture (new Rect (10, 10, 60, 40), screenCap, ScaleMode.ScaleToFit);
				}
	}

			IEnumerator Capture() {
		yield return new WaitForEndOfFrame ();
		screenCap.ReadPixels (new Rect (Camera.main.pixelRect.position.x, Camera.main.pixelRect.position.y , Camera.main.pixelWidth , Camera.main.pixelHeight),0,0);


		screenCap.Apply ();
				shot = true;
			}
}
