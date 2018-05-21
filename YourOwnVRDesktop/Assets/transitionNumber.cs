using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;     

public class transitionNumber : MonoBehaviour {
	 
	public int number; 
	public GameObject[] transitionObjects;  
	public GameObject navigationCanvas,dome,setView,locationDome,manage,navigationHotspotPanal;   
	public Texture previousActiveTexture,greyTexture;     

	// Use this for initialization
	void Start () { 
		    
	}
	  
	// Update is called once per frame
	void Update () {    
		   
	} 
	    
	public void setNumber(int number){
		GameObject one = GameObject.Find ("DomeFull");      
		one.GetComponent<SetupDome> ().selectedHotstop.gameObject.GetComponent<navigateActionHotspot> ().transition = number;   
		for (int i = 0; i < 4; i++) {
			if (i == number) { 
				transitionObjects [i].GetComponent<Outline> ().enabled = true; 
			} else {       
				transitionObjects [i].GetComponent<Outline> ().enabled = false;     
			}    
		}

		if (number == 3) {
			for (int i = 0; i < navigationCanvas.transform.childCount; i++) {
				if (navigationCanvas.transform.GetChild(i).gameObject.active) {
					navigationCanvas.transform.GetChild (i).gameObject.SetActive (false);     
				 
				}
			}
			previousActiveTexture = dome.transform.GetComponent<MeshRenderer> ().material.mainTexture;  
			dome.transform.GetComponent<MeshRenderer> ().material.mainTexture = greyTexture;   
			setView.SetActive (true); 
			navigationHotspotPanal.SetActive (false);  
		} 
	}
}
    