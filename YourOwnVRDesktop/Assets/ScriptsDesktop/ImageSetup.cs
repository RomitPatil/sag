using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ImageSetup : MonoBehaviour {

	public List<Texture2D> Texture;
	public string ActiveImage;
	public bool Active;
	// Use this for initialization
	void Start () {
		
	}
	public void LoadImagesOnDome() {
		StartCoroutine (LoadImages ());
	}

	IEnumerator LoadImages () {
		if (Directory.Exists (Application.persistentDataPath + "/Images")) {
			Debug.Log ("File are there");
			var files = Directory.GetFiles (Application.persistentDataPath + "/Images");
			for (int i = 0; i < files.Length; i++) {
				string Filename = files [i]; 
				Debug.Log ("FileName " + Filename); 
				WWW Image = new WWW(Filename);
				Debug.Log ("Load Image :"+ files [i]);
				yield return Image;
				Texture2D texture = Image.texture;
				Texture.Add (texture);

			}
			}
	
	}

	private void ActiveImageOnDome (string name){
		for (int i = 0; i < Texture.Count; i++) {
			if (Texture [i].name == name) {
				gameObject.GetComponent<MeshRenderer> ().material.mainTexture = Texture [i];
			}
		}
		Active = false;
	}

	// Update is called once per frame
	void Update () {
		if (Active) {
			LoadImagesOnDome ();
			ActiveImageOnDome (ActiveImage);
		}
	}
}
