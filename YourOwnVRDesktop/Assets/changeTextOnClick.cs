using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

public class changeTextOnClick : MonoBehaviour {
	int s;
	public TriLib.Samples.PreviewHotspot previewHotspot; 
	// Use this for initialization  
	void Start () {
		s = 1;
		previewHotspot = GameObject.FindObjectOfType<TriLib.Samples.PreviewHotspot> ();  
		gameObject.transform.GetChild (0).transform.GetChild (0).GetComponent<Text> ().text = "acs";  
	}    
	
	// Update is called once per frame
	void Update () { 
		
	}

	public void changeText(){    
		previewHotspot = GameObject.FindObjectOfType<TriLib.Samples.PreviewHotspot> ();  
		if (s % 2 == 0) {   
			gameObject.transform.GetChild (0).transform.GetChild (0).GetComponent<Text> ().text = previewHotspot.UserActionName;          
			s++;    
		} else { 
			gameObject.transform.GetChild (0).transform.GetChild (0).GetComponent<Text> ().text = "456";   
			s++;
		}
	}
}
