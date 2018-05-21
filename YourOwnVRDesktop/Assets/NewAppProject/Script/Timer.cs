using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Image fillImg;
    public float timeAmt = 10;
    public float time;
    public Text TimeText;
	// Use this for initialization
	void Start () {
        time = timeAmt;
	}
	
	// Update is called once per frame
	void Update () {

        if (time > 0) {
            time -= Time.deltaTime;
            fillImg.fillAmount = time / timeAmt;
            TimeText.text = time.ToString("F");
        }
	}
}
