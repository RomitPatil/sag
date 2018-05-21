using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Camera.main.rect.x + " CAMERA X" + Camera.main.rect.y + "CAMERA Y" + Camera.main.rect.height+ "CAMERA HIGHT" + Camera.main.rect.width+"Camera width");
	}
//	IEnumerator PictureInPicture() {
//		screenCap = new Texture2D (300, 200, TextureFormat.RGB24, false);
//		yield return new WaitForEndOfFrame ();
//		screenCap.ReadPixels (new Rect (Camera.main.rect.x,  Camera.main.rect.y, Camera.main.rect.width, Camera.main.rect.height), 0, 0);
//		screenCap.Apply ();
}
