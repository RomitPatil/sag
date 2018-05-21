using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class postMessage : MonoBehaviour {
	public Text displayText;

	// Use this for initialization
	void Start () {
		string url = "http://www.romitcs40.com/unityPost.php";
		WWWForm fromDate = new WWWForm ();
		fromDate.AddField("username" , "aarlangdi");
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
