using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;    

public class AddTimeline : MonoBehaviour {

	public GameObject timelinePanal;  
	public GameObject DomeObject;     
    public GameObject[] AllHotspotTemplets;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnClick (){
		if(DomeObject.transform.GetComponent<VideoPlayer> ().isActiveAndEnabled){      
			for (int j = 0; j < AllHotspotTemplets.Length; j++) {
				if (AllHotspotTemplets [j].gameObject.name == "Timeline") {
					AllHotspotTemplets [j].SetActive (true);
				} else {
					AllHotspotTemplets [j].SetActive (false);
				}  
			}
	        timelinePanal.SetActive (true);    
			timelinePanal.GetComponent<timelineScript>().instantiateButtonTemplates ();  
		} 
	}
}
