using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using SFB;

[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileImage : MonoBehaviour {

	public CustomLocation CustomLocation;
	public UICanvasControl ui_Canvas_control;
	public static string fileName = "";

	void Start() {
		var button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	public void OnClick() {
		var extensions = new [] {
			new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
		};
		var paths = StandaloneFileBrowser.OpenFilePanel("", "", extensions, false);
		if (paths.Length > 0) {
			StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
		}
	}

	public IEnumerator OutputRoutine(string url) {
		Debug.Log("URL: " + url);
		GetFileName (url);
		Debug.Log (fileName);
		var loader = new WWW(url);
		yield return loader;
		GameObject.FindWithTag("Dome").GetComponent<MeshRenderer>().material.mainTexture = loader.texture;
		Texture Image = loader.texture;

		ui_Canvas_control.SceneThumbnail.texture = loader.texture;
		ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().ImagePath = url;
		ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().texture = loader.texture ;
		GameObject tem = GameObject.Find ("NavigationCanvas");
		for (int i = 0; i < tem.transform.childCount; i++) {
			if (tem.transform.GetChild (i).gameObject.activeInHierarchy) {
				tem.transform.GetChild (i).gameObject.GetComponent<ImageName> ().imageURL = url;
				tem.transform.GetChild (i).gameObject.GetComponent<ImageName> ().imageName = fileName;
			}
		}
//		transform.parent.GetComponent<UICanvasControl> ().AddSceneBtn.gameObject.SetActive (true);
//		transform.parent.GetComponent<UICanvasControl> ().hotspotNameField.gameObject.SetActive (true);
//		CreateScene.instance.sceneList [CreateScene.instance.sceneList.Count - 1].GetComponent<DetailScene> ().ImagePath = url;
	}
	private string GetFileName(string hrefLink)
	{
		string[] parts = hrefLink.Split('/');


		if (parts.Length > 0)
			fileName = parts[parts.Length - 1];
		else
			fileName = hrefLink;

		return fileName;
	}
	
}