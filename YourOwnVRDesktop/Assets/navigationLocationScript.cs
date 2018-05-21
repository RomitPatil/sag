using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class navigationLocationScript : MonoBehaviour {  

	public GameObject locationDome, dome, setView,domefull;
	public GameObject navigationCanvas; 
	public int previousActiveIndex;
	public Texture previousActiveTexture; 
	public cameraRotationView script;
	public GameObject manage;  
	// Use this for initialization
	void Start () {
		script = FindObjectOfType<cameraRotationView> ();    
	}    
	public void onMouse() {      
		for (int i = 0; i < navigationCanvas.transform.childCount; i++) {
			if (navigationCanvas.transform.GetChild(i).gameObject.active) {
				navigationCanvas.transform.GetChild (i).gameObject.SetActive (false); 
				Debug.Log ("child active " + i);      
				manage.GetComponent<ProjectData> ().temporaryInt = i;  
			}
		}
		previousActiveTexture = dome.transform.GetComponent<MeshRenderer> ().material.mainTexture;  
		dome.transform.GetComponent<MeshRenderer> ().material.mainTexture = locationDome.transform.GetComponent<MeshRenderer> ().material.mainTexture; 
		setView.SetActive (true);        
	}
	  
}
