using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Video; 

public class timelineScript : MonoBehaviour {

	public GameObject DomeObject; 
	VideoPlayer player;   
	VideoClip klip;  
	public GameObject sliderObject,sliderObject2; 
	Slider _slider,_slider2;  
	float duration;  
	public GameObject navigationCanvas;   
	public GameObject timelineTemplate,templateParent;  
	public GameObject purpleObject;    
	public GameObject greenObject;  

	// Use this for initialization
	void Start () {    
		player = DomeObject.transform.GetComponent<VideoPlayer> ();   
		_slider = sliderObject.transform.GetComponent<Slider> ();  
		_slider2 = sliderObject2.transform.GetComponent<Slider> (); 
		_slider2.onValueChanged.AddListener (delegate {  
			changeVideoTime();  
		});      
		duration = player.frameCount / player.frameRate;    
	}   

	// Update is called once per frame  
	void Update () {            
		_slider.value = (float) player.time / duration ;                        
	}   
	   
	void changeVideoTime(){
		player.time = (double) _slider2.value * duration;             
	}

	public void playPause(){
		if (player.isPlaying)
			player.Pause ();
		else
			player.Play ();      
		 
	}  

	public void instantiateButtonTemplates() {
		for (int i = 0; i < templateParent.transform.childCount; i++) {
			Debug.Log ("Destroy");    
			Destroy(templateParent.transform.GetChild(i).gameObject);  
			for (int j = 0; j < navigationCanvas.transform.childCount; j++) {   
			SceneProperties sp = navigationCanvas.transform.GetChild (j).transform.GetComponent<SceneProperties> ();
			sp.initialTime.Clear ();     
			}
		}    
				 
		player = DomeObject.transform.GetComponent<VideoPlayer> ();     
		duration = player.frameCount / player.frameRate;   
		if (DomeObject.transform.GetComponent<VideoPlayer> ().isActiveAndEnabled) {    
			for (int i = 0; i < navigationCanvas.transform.childCount; i++) {
				if (navigationCanvas.transform.GetChild (i).gameObject.activeSelf) {
					for (int j = 0; j < navigationCanvas.transform.GetChild (i).childCount; j++) {
						GameObject temp = Instantiate (timelineTemplate);        
						temp.transform.GetChild (1).transform.GetComponent<Text> ().text = navigationCanvas.transform.GetChild (i).transform.GetChild (j).name;      
						if (navigationCanvas.transform.GetChild (i).transform.GetChild (j).GetComponent<MenuHotspot> ()) {
							temp.transform.GetChild (0).transform.GetComponent<Image> ().color = Color.blue;
							temp.transform.GetChild (2).transform.GetChild (2).transform.GetChild(0).GetComponent<Image>().color = Color.blue;
							temp.transform.GetChild (3).transform.GetChild (2).transform.GetChild(0).GetComponent<Image>().color = Color.blue;
						}
						if (navigationCanvas.transform.GetChild (i).transform.GetChild (j).GetComponent<navigateActionHotspot> ()) {
							temp.transform.GetChild (0).transform.GetComponent<Image> ().color = new Color (255f, 255f, 0f);     
							temp.transform.GetChild (2).transform.GetChild (2).transform.GetChild(0).GetComponent<Image>().color = new Color (255f, 255f, 0f);
							temp.transform.GetChild (3).transform.GetChild (2).transform.GetChild(0).GetComponent<Image>().color = new Color (255f, 255f, 0f);
						}
						if (navigationCanvas.transform.GetChild (i).transform.GetChild (j).GetComponent<MediaHotspot> ()) {
							temp.transform.GetChild (0).transform.GetComponent<Image> ().color = purpleObject.GetComponent<Image> ().color;   
							temp.transform.GetChild (2).transform.GetChild (2).transform.GetChild(0).GetComponent<Image>().color = purpleObject.GetComponent<Image> ().color;
							temp.transform.GetChild (3).transform.GetChild (2).transform.GetChild(0).GetComponent<Image>().color = purpleObject.GetComponent<Image> ().color;
						}
						if (navigationCanvas.transform.GetChild (i).transform.GetChild (j).GetComponent<textActionHotspot> ()) {
							temp.transform.GetChild (0).transform.GetComponent<Image> ().color = new Color (255f, 0f, 0f); 
							temp.transform.GetChild (2).transform.GetChild (2).transform.GetChild(0).GetComponent<Image>().color = new Color (255f, 0f, 0f);
							temp.transform.GetChild (3).transform.GetChild (2).transform.GetChild(0).GetComponent<Image>().color = new Color (255f, 0f, 0f);  
						}
						if (navigationCanvas.transform.GetChild (i).transform.GetChild (j).GetComponent<NewActionHotspot> ()) {
							temp.transform.GetChild (0).transform.GetComponent<Image> ().color = Color.green;   
							temp.transform.GetChild (2).transform.GetChild (2).transform.GetChild (0).GetComponent<Image> ().color = Color.green;
							temp.transform.GetChild (3).transform.GetChild (2).transform.GetChild(0).GetComponent<Image>().color = Color.green;  
						}
						if (navigationCanvas.transform.GetChild (i).transform.GetChild (j).GetComponent<helpActionHotspot> ()) {
							temp.transform.GetChild (0).transform.GetComponent<Image> ().color = new Color (0f, 223f, 255f);    
							temp.transform.GetChild (2).transform.GetChild (2).transform.GetChild(0).GetComponent<Image>().color = new Color (0f, 223f, 255f); 
							temp.transform.GetChild (3).transform.GetChild (2).transform.GetChild (0).GetComponent<Image>().color = new Color (0f, 223f, 255f);  
						}   
						temp.transform.parent = templateParent.transform; 
						temp.transform.localScale = new Vector3 (1f, 1f, 1f);   
						temp.GetComponent<RectTransform>().localPosition = new Vector3(0f,0f,0f);        
						SceneProperties sp = navigationCanvas.transform.GetChild (i).transform.GetComponent<SceneProperties> ();
						sp.initialTime.Add (0f);
						Debug.Log ("duration is" + duration);    
						sp.finalTime.Add (duration);      
						temp.transform.GetComponent<templateTimelineScript> ().takeInfo (sp,j,duration);    
					}
				}  
			}
		} 
	}

}  