using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DownloadFromURL : MonoBehaviour {
	public string URL;
	public ImageSetup DomeImage;
	public ProjectSetupOnDevice ProjectSetup;
	public GameObject DownloadPanal;
	public Text FileNameTXT;
	public Text TotalFileTXT;
	public Text PercentTXT;
	public Text DownloadTXT;
	public Button StartButton;
	public Image ForgroundImage;
	public Text StatusTXT;

	// Use this for initialization
	void Start () {
		DownloadPanal.SetActive (true);
		StartCoroutine (DownloadData ());
	}

	IEnumerator DownloadData (){

		WWW www = new WWW (URL);
		yield return www;
		print ("Data Loading " + www.text);

		File.WriteAllText (Application.persistentDataPath +"/jsonData.txt", www.text);
		Debug.Log (Application.persistentDataPath);

		ProjectSetup.Load ();

		for (int i = 0; i < ProjectSetup.ImageURLs.Count; i++) {
			WWW Images = new WWW (ProjectSetup.ImageURLs [i]);
			StartCoroutine (ShowProgress (Images));
			yield return Images;

			Texture2D texture = Images.texture;

			byte[] Image = texture.EncodeToJPG ();
			if (!Directory.Exists (Application.persistentDataPath + "/Images")) {
				Directory.CreateDirectory (Application.persistentDataPath + "/Images");
				Debug.Log (Application.persistentDataPath);
			} else {
				var files = Directory.GetFiles (Application.persistentDataPath + "/Images");
				for (int j = 0 ; j < files.Length ; j++){
					File.Delete (files [j]);
				}
			}
			File.WriteAllBytes (Application.persistentDataPath + "/Images/" + ProjectSetup.ImageNAMEs[i],Image);
			Debug.Log ("Images path:" + Application.persistentDataPath + "/Images/" + ProjectSetup.ImageNAMEs [i]);
		}
		DomeImage.LoadImagesOnDome ();
	}

	IEnumerator Downloading() {
	
		DownloadTXT.gameObject.SetActive (true);
		DownloadTXT.text = "Uploading.";
		yield return new WaitForSeconds (0.5f);
		DownloadTXT.text = "Uploading..";
		yield return new WaitForSeconds (0.5f);
		DownloadTXT.text = "Uploading...";
		yield return new WaitForSeconds (0.5f);
		StartCoroutine (Downloading ());
	}


	private IEnumerator ShowProgress (WWW www){
		StatusTXT.text = "Wait for Downloading Project";
		StartCoroutine (Downloading ());
		float tem = 0;
		while (!www.isDone) {
			tem = (www.progress * 100);
			Debug.Log (tem);
			float ratio = (www.progress);
			ForgroundImage.gameObject.SetActive (true);
			ForgroundImage.rectTransform.localScale = new Vector3 (ratio, 1, 1);
			PercentTXT.text = ((int)tem).ToString() + "%";
			yield return new WaitForSeconds (.1f);
		}

	}
	// Update is called once per frame
	void Update () {
		
	}

	public void StartBTN (){
		DownloadPanal.SetActive (false);
	}
}
