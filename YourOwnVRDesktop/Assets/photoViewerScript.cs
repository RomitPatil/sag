using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

public class photoViewerScript : MonoBehaviour {

	public GameObject content; 
	// Use this for initialization
	void Start () {
		var button = GetComponent<Button>();
		button.onClick.AddListener(click);    
	}
	
	// Update is called once per frame
	void Update () {
		
	} 

	public void click(){
		for (int i = 0; i < content.transform.childCount; i++) {
			content.transform.GetChild (i).GetComponent<SelectFiles> ().UnSelectFile ();   
		}
	}
}
