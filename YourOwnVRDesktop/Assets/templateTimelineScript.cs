using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class templateTimelineScript : MonoBehaviour {

	public GameObject sliderObject,sliderObject2; 
	Slider _slider,_slider2; 
	public SceneProperties sp;  
	int buttonIdIndex;  
	float duration;
	// Use this for initialization
	void Start () {
		_slider = sliderObject.transform.GetComponent<Slider> ();  
		_slider2 = sliderObject2.transform.GetComponent<Slider> (); 
		_slider.onValueChanged.AddListener (delegate {
			changeInitialTime();  
		});  
		_slider2.onValueChanged.AddListener (delegate {  
			changeFinalTime();  
		});   
	}

	public void takeInfo(SceneProperties sceneProp, int i,float dur){
		sp = sceneProp;
		buttonIdIndex = i; 
		duration = dur; 
	}

	public void changeInitialTime(){  
		sp.initialTime [buttonIdIndex] = _slider.value*duration;   
	}

	public void changeFinalTime(){ 
		sp.finalTime [buttonIdIndex] = _slider2.value*duration;     
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
