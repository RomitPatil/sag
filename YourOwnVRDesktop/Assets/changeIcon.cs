using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class changeIcon : MonoBehaviour {

	public Sprite myImage; 
	public GameObject domeSetup;        
	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () { 
		   
	}   
	  
	public void setImageZero() {    
		myImage = gameObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite; 
		GameObject selectedObject = domeSetup.GetComponent<SetupDome> ().selectedHotstop; 
		if (selectedObject.GetComponent<MenuHotspot> ()) {
			selectedObject.GetComponent<MenuHotspot> ().hotspotSprite = myImage;   
			selectedObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite = myImage;  
		}  
		if (selectedObject.GetComponent<navigateActionHotspot> ()) {  
			selectedObject.GetComponent<navigateActionHotspot> ().hotspotSprite = myImage;      
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;
		}    
		if (selectedObject.GetComponent<MediaHotspot> ()) {   
			selectedObject.GetComponent<MediaHotspot> ().hotspotSprite = myImage;    
			selectedObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite = myImage;  
		}   
		if (selectedObject.GetComponent<textActionHotspot> ()) {   
			selectedObject.GetComponent<textActionHotspot> ().hotspotSprite = myImage;
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;
		} 
		if (selectedObject.GetComponent<NewActionHotspot> ()) {    
			selectedObject.GetComponent<NewActionHotspot> ().hotspotSprite = myImage;  
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;
		} 
		if (selectedObject.GetComponent<helpActionHotspot> ()) {       
			selectedObject.GetComponent<helpActionHotspot> ().hotspotSprite = myImage;     
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;   
		}    
	}   

	public void setImageOne() {       
		      
		myImage = gameObject.transform.GetChild (1).transform.GetChild(0).transform.GetComponent<Image> ().sprite;
		GameObject selectedObject = domeSetup.GetComponent<SetupDome> ().selectedHotstop;     
		if (selectedObject.GetComponent<MenuHotspot> ()) {
			selectedObject.GetComponent<MenuHotspot> ().hotspotSprite = myImage;   
			selectedObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite = myImage;  
		}  
		if (selectedObject.GetComponent<navigateActionHotspot> ()) {  
			selectedObject.GetComponent<navigateActionHotspot> ().hotspotSprite = myImage;      
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;
		}    
		if (selectedObject.GetComponent<MediaHotspot> ()) {   
			selectedObject.GetComponent<MediaHotspot> ().hotspotSprite = myImage;    
			selectedObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite = myImage;  
		}   
		if (selectedObject.GetComponent<textActionHotspot> ()) {   
			selectedObject.GetComponent<textActionHotspot> ().hotspotSprite = myImage;
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;
		} 
		if (selectedObject.GetComponent<NewActionHotspot> ()) {   
			selectedObject.GetComponent<NewActionHotspot> ().hotspotSprite = myImage;  
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;
		} 
		if (selectedObject.GetComponent<helpActionHotspot> ()) {       
			selectedObject.GetComponent<helpActionHotspot> ().hotspotSprite = myImage;     
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;   
		}    
	}


	public void setImageTwo() {       
		myImage = gameObject.transform.GetChild (2).transform.GetChild(0).transform.GetComponent<Image> ().sprite;
		GameObject selectedObject = domeSetup.GetComponent<SetupDome> ().selectedHotstop; 
		if (selectedObject.GetComponent<MenuHotspot> ()) {
			selectedObject.GetComponent<MenuHotspot> ().hotspotSprite = myImage;   
			selectedObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite = myImage;  
		}  
		if (selectedObject.GetComponent<navigateActionHotspot> ()) {  
			selectedObject.GetComponent<navigateActionHotspot> ().hotspotSprite = myImage;      
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;
		}    
		if (selectedObject.GetComponent<MediaHotspot> ()) {   
			selectedObject.GetComponent<MediaHotspot> ().hotspotSprite = myImage;    
			selectedObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite = myImage;  
		}   
		if (selectedObject.GetComponent<textActionHotspot> ()) {   
			selectedObject.GetComponent<textActionHotspot> ().hotspotSprite = myImage;
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;
		} 
		if (selectedObject.GetComponent<NewActionHotspot> ()) {   
			selectedObject.GetComponent<NewActionHotspot> ().hotspotSprite = myImage;  
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;
		} 
		if (selectedObject.GetComponent<helpActionHotspot> ()) {       
			selectedObject.GetComponent<helpActionHotspot> ().hotspotSprite = myImage;     
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;   
		}    
	}  

	  
	public void setImageThree() {        
		myImage = gameObject.transform.GetChild (3).transform.GetChild(0).transform.GetComponent<Image> ().sprite;    
		GameObject selectedObject = domeSetup.GetComponent<SetupDome> ().selectedHotstop; 
		if (selectedObject.GetComponent<MenuHotspot> ()) {  
			selectedObject.GetComponent<MenuHotspot> ().hotspotSprite = myImage;   
			selectedObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite = myImage;  
		}  
		if (selectedObject.GetComponent<navigateActionHotspot> ()) {  
			selectedObject.GetComponent<navigateActionHotspot> ().hotspotSprite = myImage;      
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;
		}    
		if (selectedObject.GetComponent<MediaHotspot> ()) {   
			selectedObject.GetComponent<MediaHotspot> ().hotspotSprite = myImage;    
			selectedObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite = myImage;  
		}   
		if (selectedObject.GetComponent<textActionHotspot> ()) {   
			selectedObject.GetComponent<textActionHotspot> ().hotspotSprite = myImage;
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;
		} 
		if (selectedObject.GetComponent<NewActionHotspot> ()) {   
			selectedObject.GetComponent<NewActionHotspot> ().hotspotSprite = myImage;  
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;
		} 
		if (selectedObject.GetComponent<helpActionHotspot> ()) {       
			selectedObject.GetComponent<helpActionHotspot> ().hotspotSprite = myImage;     
			selectedObject.transform.GetChild (0).transform.GetChild(0).transform.GetComponent<Image> ().sprite = myImage;   
		}    
	}  


}
  