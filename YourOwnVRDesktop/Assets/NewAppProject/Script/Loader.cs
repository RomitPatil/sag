using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Loader : MonoBehaviour {

    public GameObject loadingScreen;  
    public Slider Slider;
    public Text progressText;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    public void Loading (float Progress) {

        float progressing = Mathf.Clamp01(Progress / .9f);
        Debug.Log(progressing);
        Slider.value = progressing;
        progressText.text = Progress * 100f + "%";
    }
		
	
}
