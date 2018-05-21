using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotationView : MonoBehaviour {
	 
	public GameObject DomeCamera,viewCamera,domeFull; 
	public float rotx, roty, rotz;    
	public GameObject navigationCanvas;  
	public GameObject manage;   
	public int temp; 
	public SetupDome domeSetup;  
	int randomint; 
	// Use this for initialization 
	void Start () { 
		domeSetup = FindObjectOfType<SetupDome> (); 
	}
	

	   
	public void onClick(){    
		
		viewCamera.transform.rotation = Quaternion.Euler(new Vector3(DomeCamera.transform.eulerAngles.x,DomeCamera.transform.eulerAngles.y,DomeCamera.transform.eulerAngles.z));  
		navigationCanvas.transform.GetChild (manage.GetComponent<ProjectData> ().temporaryInt).gameObject.SetActive (true);   
		domeFull.transform.GetComponent<MeshRenderer> ().material.mainTexture = navigationCanvas.transform.GetChild (manage.GetComponent<ProjectData> ().temporaryInt).gameObject.GetComponent<SceneProperties> ().SceneTexture;    
		Debug.Log ("child active2 "+manage.GetComponent<ProjectData> ().temporaryInt ); 

		domeSetup.selectedHotstop.GetComponent<navigateActionHotspot>().rotationView = new Vector3(DomeCamera.transform.eulerAngles.x,DomeCamera.transform.eulerAngles.y,DomeCamera.transform.eulerAngles.z);   
		Debug.Log ("dome camera rot "); 
		gameObject.SetActive (false);               
		    
	}

    public void Testing() {
        Debug.Log("other test");
    }  
		   
	private void temporaryFunc1(){
		randomint = 0;
	} 

	private void temporaryFunc2(){
		randomint = 1;  
	}

	private void coimore(){   
		Debug.Log("abd");
	}

}
