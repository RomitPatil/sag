using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoad : MonoBehaviour {
	public DomeImages DomeImage;
	public PlayerData playerData;
	private string Filepath ;
	public GameObject SavePanal;
	// Use this for initialization
	void Start () {
		Filepath = Application.dataPath;
		Filepath = Filepath.Replace ("/Assets", "");
		Debug.Log ("filepath" + Filepath);

		if (!Directory.Exists (Filepath + "/Images")) {
			Directory.CreateDirectory (Filepath + "/Images");
			Debug.Log (Filepath + "/Images");
		}
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			Save ();
//		}
	}

	void Load () {
	}

	public void Save () {
		
		string jsonString = JsonUtility.ToJson (playerData);
		File.WriteAllText (Filepath + "/JsonData.txt", jsonString);
		Debug.Log ("json File : " + Filepath + "/JsonData.txt");
		StartCoroutine(LoadImages ());

	}

	IEnumerator LoadImages () {
		SavePanal.SetActive (true);
	
		if (Directory.Exists (Filepath + "/Images")) {
			Debug.Log ("Files are Exists");
			var files = Directory.GetFiles (Filepath + "/Images");
			for (int i = 0; i < files.Length; i++) {
				Debug.Log (files [i]);
				File.Delete (files [i]);
			}
		}
		for (int i = 0; i < playerData.ImageURLs.Count; i++) {
			Debug.Log (playerData.ImageURLs [i]);
			WWW LoadImage = new WWW (playerData.ImageURLs [i]);
				yield return LoadImage;
			Texture2D texture = LoadImage.texture;
			byte[] Image = texture.EncodeToJPG();
			File.WriteAllBytes (Filepath + "/Images/" + playerData.ImageNAMEs[i] , Image);
			Debug.Log (Filepath + "/Images/" + playerData.ImageNAMEs [i]);
		}

		DomeImage.Active = true;
		SavePanal.SetActive (false);
	}
}
