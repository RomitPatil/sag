using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video; 

public class PreviewScene : MonoBehaviour
{      
	public string sceneName;
	public Vector3 camPos;
	public Texture SceneTexture;    
	public GameObject DomePreview;  
	public GameObject DomeCamera;  
	public bool video;   
	public string url;   
	bool Enter;
	public List<float> initialTime = new List<float> ();
	public List<float> finalTime = new List<float>();     
	public VideoPlayer player;  
	bool temporary; 
	public bool crossFade,radialBlur,cutToExternal,none;  
	public GameObject PreviewCamera;   
	public int transition;   
	  
	// Use this for initialization 
	void Start ()     
	{    
		GameObject one = GameObject.Find ("PreviewDomeObject"); 
		PreviewCamera = one.transform.GetChild (1).gameObject;  
		radialBlur = true;       
		if (video == false) { 
			if (transition == 0) {
				DomePreview.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", SceneTexture);  
			}   	  
			if (transition ==1) {
				DomePreview.GetComponent<MeshRenderer> ().material.SetTexture ("_SecondaryTex", SceneTexture);   
				DomePreview.GetComponent<MeshRenderer> ().material.SetFloat ("_LerpValue", 0f);
				temporary = true;
			}    
			if (transition ==2) {
				DomePreview.GetComponent<MeshRenderer> ().material.SetTexture ("_SecondaryTex", SceneTexture);   
				DomePreview.GetComponent<MeshRenderer> ().material.SetFloat ("_LerpValue", 0f); 
				temporary = true;
			}
            if (DomePreview.GetComponent<VideoPlayer>()) {  
			DomePreview.GetComponent<VideoPlayer> ().enabled = false;
            }     
		}       
		if(video==true) {       
			DomePreview.GetComponent<VideoPlayer> ().enabled = true;  
			DomePreview.GetComponent<VideoPlayer> ().url = url;     
		}    
		 
		player = DomePreview.GetComponent<VideoPlayer> ();   
	}   
	 
	void OnEnable ()
	{  
		radialBlur = true;  
		Enter = true; 
		DomePreview = GameObject.Find ("DomePreview");  
		DomeCamera = GameObject.Find ("DomeCamera");     
		if (DomePreview != null) {
			DomePreview.SetActive (true);
//			DomeCamera.GetComponent<MouseCameraDraging> ().pitch = 0;
//			DomeCamera.GetComponent<MouseCameraDraging> ().yaw = 0;
//			DomeCamera.transform.localEulerAngles = camPos;
			if (video == false) {  
				if (transition == 0) {
					DomePreview.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", SceneTexture);  
				}  
				if (transition ==1) {
					DomePreview.GetComponent<MeshRenderer> ().material.SetTexture ("_SecondaryTex", SceneTexture); 
					DomePreview.GetComponent<MeshRenderer> ().material.SetFloat ("_LerpValue", 0f); 
					temporary = true;  
				}
				if (transition ==2) { 
					DomePreview.GetComponent<MeshRenderer> ().material.SetTexture ("_SecondaryTex", SceneTexture);   
					DomePreview.GetComponent<MeshRenderer> ().material.SetFloat ("_LerpValue", 0f);
					temporary = true;
				}  
                if (DomePreview.GetComponent<VideoPlayer>()) {
					    
				DomePreview.GetComponent<VideoPlayer> ().enabled = false;
                }
			}   
			if(video==true) {    
				DomePreview.GetComponent<VideoPlayer> ().enabled = true;  
				DomePreview.GetComponent<VideoPlayer> ().url = url;    
			}   
		}
	}

	void FixedUpdate() { 
		for (int i = 0; i < initialTime.Count; i++) {   
			if (initialTime[i] > player.time) {
				transform.GetChild (i).gameObject.SetActive (false);   
			}   
			if ((initialTime[i] < player.time)&&(finalTime[i] > player.time)) { 
				transform.GetChild (i).gameObject.SetActive (true);     
			}
			if (finalTime[i] < player.time) {      
				transform.GetChild (i).gameObject.SetActive (false);     
			}
		}   
		if (transition ==1) {
			if (temporary) {
				for (int i = 0; i < gameObject.transform.childCount; i++) {  
					gameObject.transform.GetChild (i).gameObject.SetActive(false);  
				}
				DomePreview.GetComponent<MeshRenderer> ().material.SetFloat ("_LerpValue", DomePreview.GetComponent<MeshRenderer> ().material.GetFloat ("_LerpValue") + Time.deltaTime*0.3f);       
				if (DomePreview.GetComponent<MeshRenderer> ().material.GetFloat ("_LerpValue") > 1) {  
					DomePreview.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", SceneTexture);    
					DomePreview.GetComponent<MeshRenderer> ().material.SetFloat ("_LerpValue", 0f);   
					temporary = false;  
					for (int i = 0; i < gameObject.transform.childCount; i++) {
						gameObject.transform.GetChild (i).gameObject.SetActive(true);     
					}
				}
			}    
		}   
		if (transition ==2) {       
			if (temporary) {
				for (int i = 0; i < gameObject.transform.childCount; i++) {
					gameObject.transform.GetChild (i).gameObject.SetActive(false);     
				}
				DomePreview.GetComponent<MeshRenderer> ().material.SetFloat ("_LerpValue", DomePreview.GetComponent<MeshRenderer> ().material.GetFloat ("_LerpValue") + Time.deltaTime*0.3f);        

					PreviewCamera.GetComponent<Camera> ().fieldOfView = PreviewCamera.GetComponent<Camera> ().fieldOfView - 1;      
				if (PreviewCamera.GetComponent<Camera> ().fieldOfView < 15) {
					PreviewCamera.GetComponent<Camera> ().fieldOfView = 179;    
				}  
				 
				}  
				if (DomePreview.GetComponent<MeshRenderer> ().material.GetFloat ("_LerpValue") > 1) {    
					DomePreview.GetComponent<MeshRenderer> ().material.SetTexture ("_MainTex", SceneTexture);      
					DomePreview.GetComponent<MeshRenderer> ().material.SetFloat ("_LerpValue", 0f);   
					temporary = false;    
					PreviewCamera.GetComponent<Camera>().fieldOfView = 60;    
					for (int i = 0; i < gameObject.transform.childCount; i++) { 
					gameObject.transform.GetChild (i).gameObject.SetActive(true);       
					}
				}
			}      
			
	}
	 
	// Update is called once per frame
	void Update () 
	{
		if (DomeCamera != null) {
			if (Enter) {
				DomeCamera.GetComponent<MouseCameraDraging> ().pitch = 0;
				DomeCamera.GetComponent<MouseCameraDraging> ().yaw = 0;
				DomeCamera.transform.localEulerAngles = camPos;
				Enter = false;
				camPos = new Vector3 (0f,0f,0f);    
			}
		}
	}
} 