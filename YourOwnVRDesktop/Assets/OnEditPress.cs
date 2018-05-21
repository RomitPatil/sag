using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class OnEditPress : MonoBehaviour {
	int Index;  
	public SetupDome domeSetup;  
	public InputField size,textsize;  
	public int myint,myinttext;   
	GameObject current;   
	// Use this for initialization
	void Start () {
		Index = 2;
		myint = 10;  
		myinttext = 10; 
		domeSetup = GameObject.FindObjectOfType<SetupDome> ();     
	}

	
	// Update is called once per frame 
	void Update () {
		 
	}
	  
	public void onClick(){
		domeSetup.selectedHotstop.GetComponent<generalHotspot> ().polarToCartesian ();  
	}

	public void up(){    
		Debug.Log (SetupDome.SelectedHotspot);  
			domeSetup = GameObject.FindObjectOfType<SetupDome> (); 
			current = domeSetup.selectedHotstop; 
			current.GetComponent<generalHotspot> ().radius++; 
			myint = current.GetComponent<generalHotspot> ().radius;   
		//	size.text = myint.ToString ();    
			current.transform.localScale = new Vector3 ((float)myint / 10, (float)myint / 10, (float)myint / 10);    
	}

	public void textup(){
		domeSetup = GameObject.FindObjectOfType<SetupDome> (); 
		current = domeSetup.selectedHotstop; 
		current.GetComponent<generalHotspot> ().radius++; 
		myint = current.GetComponent<generalHotspot> ().radius;     
	//	textsize.text = myint.ToString ();    
		current.transform.localScale = new Vector3 ((float)myint / 10, (float)myint / 10, (float)myint / 10);    
	}

	  
	public void down(){ 
		domeSetup = GameObject.FindObjectOfType<SetupDome> ();   
		current = domeSetup.selectedHotstop;
		current.GetComponent<generalHotspot> ().radius--; 
		myint = current.GetComponent<generalHotspot> ().radius;   
	//	size.text = myint.ToString ();     
		current.transform.localScale = new Vector3 ((float)myint / 10, (float)myint / 10, (float)myint / 10);
	} 

	public void textdown(){ 
		domeSetup = GameObject.FindObjectOfType<SetupDome> ();   
		current = domeSetup.selectedHotstop;   
		current.GetComponent<generalHotspot> ().radius--;    
		myint = current.GetComponent<generalHotspot> ().radius;       
	//	textsize.text = myinttext.ToString ();     
		current.transform.localScale = new Vector3 ((float)myint / 10, (float)myint / 10, (float)myint / 10);   
	}   
		 
}
  