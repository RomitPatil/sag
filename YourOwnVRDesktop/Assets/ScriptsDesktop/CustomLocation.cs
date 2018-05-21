using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CustomLocation : MonoBehaviour {
	public Texture PutTheTexture;
	public GameObject detectionCam;
	public GameObject ActiveHotspot;
	public Texture2D screenCap ;
	public Texture2D DscreenCap ;
	public RawImage SmallPicture;
	public RawImage CLSmallPicture;
	public Button ChangeCusLocImagBUT;
	public GameObject HotspotDomeFull;
	public GameObject MainDome;
	public GameObject List;
	public string ShottoText;
	public string FullTextureInText;
	public List <Texture> SceneImagesThumbnil = new List<Texture> ();
	public List <string> SceneImagePath = new List<string> ();
	public List <string> SceneName = new List <string>();
	public List <Texture> SceneImageTexture = new List <Texture>();
	public Quaternion HotspotCamLoc;
	// Use this for initialization
	public void OnEnableCustomLocation () {

		GameObject temp = FindObjectOfType<HotspotControl> ().gameObject;
		ActiveHotspot = temp;
	}

	void Start () {
		//screenCap = new Texture2D (300, 200, TextureFormat.RGB24, false);
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void SetCustomLocation () {

		ChangeCusLocImagBUT.interactable = false;
		SceneImagesThumbnil.Clear ();
		StartCoroutine (PictureInPicture ());

	}
	IEnumerator PictureInPicture() {
		screenCap = new Texture2D (500, 300, TextureFormat.RGB24, false);
	
		yield return new WaitForEndOfFrame ();
		screenCap.ReadPixels (new Rect (Camera.main.pixelRect.position.x, Camera.main.pixelRect.position.y , Camera.main.pixelWidth , Camera.main.pixelHeight),0,0);


		screenCap.Apply ();



		SmallPicture.gameObject.SetActive (true);
	//	HotspotCamLoc = detectionCam.transform.rotation;
		SetupOnDome ();
	}
	IEnumerator PictureThumbnil() {
		SmallPicture.gameObject.SetActive (false);
		DscreenCap = new Texture2D (500, 300, TextureFormat.RGB24, false);
		yield return new WaitForEndOfFrame ();
		//screenCap.ReadPixels (new Rect (Camera.main.pixelRect.position.x, Camera.main.pixelRect.position.y, Camera.main.pixelWidth, Camera.main.pixelHeight), 0, 0);		
		DscreenCap.ReadPixels (new Rect (detectionCam.gameObject.GetComponent<Camera>().pixelRect.position.x, detectionCam.gameObject.GetComponent<Camera>().pixelRect.position.y, detectionCam.gameObject.GetComponent<Camera>().pixelRect.width,detectionCam.gameObject.GetComponent<Camera>().pixelRect.height), 0, 0);screenCap.Apply ();
		DscreenCap.Apply ();
		CLSmallPicture.gameObject.SetActive (true);
		CLSmallPicture.texture = DscreenCap;

		SceneImagesThumbnil.Add (DscreenCap);
		
			ActiveHotspot.GetComponent<HotspotControl> ().CustumLocImage = SceneImagesThumbnil[0]; 

		HotspotDomeFull.SetActive (false);
		MainDome.SetActive (true);
		ActiveHotspot.gameObject.transform.GetChild (0).gameObject.SetActive (true);
		ActiveHotspot.GetComponent<HotspotControl> ().CustumLocImage = DscreenCap;

		ChangeCusLocImagBUT.interactable = true;
		StopCoroutine (PictureThumbnil ());
		detectionCam.transform.rotation = HotspotCamLoc;
	}

	void SetupOnDome(){ 
		
		SceneName.Clear ();
		SceneImagePath.Clear ();
		SceneImageTexture.Clear ();
		SmallPicture.gameObject.SetActive (true);

		SmallPicture.texture = screenCap;

		//ActiveHotspot.gameObject.transform.GetChild (0).gameObject.SetActive (false);
		string NavigateSceneName = ActiveHotspot.GetComponent<HotspotControl> ().navigationSceneName;
		Debug.Log ("NavigateSceneName ::" + NavigateSceneName);
		for (int i =0 ; i < List.gameObject.transform.childCount ; i++){
			string tem = List.gameObject.transform.GetChild (i).GetComponent<DetailScene> ().nav_canvas_scene_Obj.name;
		     tem =  tem.Substring(0,tem.LastIndexOf ('_'));
			SceneName.Add(tem);
			SceneImagePath.Add(List.gameObject.transform.GetChild (i).GetComponent<DetailScene> ().ImagePath);
			SceneImageTexture.Add (List.gameObject.transform.GetChild (i).GetComponent<DetailScene> ().texture);
			Debug.Log ("Scene name" + tem);

			if (tem == ActiveHotspot.GetComponent<HotspotControl> ().navigationSceneName) {
				HotspotDomeFull.gameObject.SetActive (true);
				HotspotDomeFull.GetComponent<MeshRenderer> ().material.mainTexture = SceneImageTexture [i];
				MainDome.gameObject.SetActive (false);
			}
		}
	}

	public void done (){
		SceneImagesThumbnil.Clear ();
		ActiveHotspot.GetComponent<HotspotControl> ().CustomLocROt = Camera.main.transform.rotation;

		ActiveHotspot.GetComponent<HotspotControl> ().ui_Canvas_control.CustomLocFix.isOn = true;
		StartCoroutine (PictureThumbnil ());
		ChangeCusLocImagBUT.interactable = true;
		//detectionCam.transform.rotation = HotspotCamLoc;
		Debug.Log ("Done");
	}


}
