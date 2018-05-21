using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DomeImages : MonoBehaviour {
	private string Filepath ;
	public List<Texture> Texture;
	public List<string> ImageName;
	public GetScenesInfo GetSceneInfo;
	public string ActiveImage;
	public bool Active;
	// Use this for initialization
	void Start () {
		Filepath = Application.dataPath + "/Images";
		Filepath = Filepath.Replace ("/Assets", "");
		Debug.Log ("filepath" + Filepath);

		LoadingImages ();
		// Load all textures into an array
	//	Object[] Textures = Resources.LoadAll("Images", typeof(Texture2D));

		// Load a single texture
//		for (int i = 0 ; i< textures.Length ; i++){
//		Texture tem = (Texture) textures[i];
//		texture.Add (tem);
	}
	public void LoadingImages(){
		StartCoroutine (LoadImage ());
	}
	IEnumerator LoadImage () {
		if (Directory.Exists (Filepath)) {
			Debug.Log ("THere is File Path");
			var files = Directory.GetFiles (Filepath);
			for (int i = 0; i < files.Length; i++) {
				string fileName = files [i];
				Debug.Log ("File Name " + fileName);
				WWW Image = new WWW (fileName);
				Debug.Log ("Load Image :" + files [i]);
				yield return Image;
				ImageName = GetSceneInfo.ImageNAMEs;
				Texture2D texture = Image.texture;
				Texture.Add (texture);
			}
		}
	}

	private void ActiveImageOnDome(string name){
	
		for (int i = 0; i < Texture.Count; i++) {
			Debug.Log (Texture [i].name + ":::" + name);
			if (Texture [i].name == name) {
				gameObject.GetComponent<MeshRenderer> ().material.mainTexture = Texture [i];
			}
		}
		Active = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Active) {
			ActiveImageOnDome (ActiveImage);
			Debug.Log ("Active");
		}
	}
}
