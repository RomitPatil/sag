using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingScript : MonoBehaviour {

	UnityEngine.Video.VideoPlayer videoPlayer; 
	// Use this for initialization
	void Start () {  

		videoPlayer = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();  
		videoPlayer.url = "";  
		
	}
//	file:///Users/abhishek/Downloads/videoplayback.mp4  

	
	// Update is called once per frame
	void Update () {
		

		if (Input.GetMouseButtonDown (0)) {
			videoPlayer.url = "file:///Users/abhishek/Downloads/videoplayback.mp4";  
		}
		
	}


}
