using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;   
using UnityEngine.UI;
using SFB;  

public class backgroundImageScript : MonoBehaviour {

	public InputField InputUrl; 
	public Image img;
	public Texture2D tex; 
	 
	// Use this for initialization
	void Start () {
		
	}
	  
	// Update is called once per frame
	void Update () {
		
	}

	public void Browse() { 

		var extensions = new[]
		{
			new ExtensionFilter("Image Files", "Jpg","Png","Jpeg"),
		};

		// Get the path of selected file.
		var path = StandaloneFileBrowser.OpenFilePanel("", "", extensions, false);

		InputUrl.text = path[0]; 
		if (InputUrl.text != "") {    
			StartCoroutine(download (path [0]));    
		}

	}
	 
	IEnumerator download(string url) {  
		WWW www = new WWW(url);  
		yield return www;
		tex = www.texture;   
		}
}
