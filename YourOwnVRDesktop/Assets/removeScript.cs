using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeScript : MonoBehaviour {

	public GameObject Dome; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Remove(){
		Destroy (Dome.GetComponent<SetupDome> ().selectedHotstop);      
	}

}
  