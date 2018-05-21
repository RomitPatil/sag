using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addTemplateScript : MonoBehaviour {
	 
	public GameObject template; 
	public GameObject content;  
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame  
	void Update () {
		
	}

	public void addButton(){
		GameObject temp = GameObject.Instantiate (template);  
		RectTransform rt = temp.GetComponent<RectTransform>();   
		temp.transform.parent = content.transform;  
		rt.localScale = new Vector3 (1f, 1f, 1f);        
		rt.localPosition= new Vector3(rt.position.x,rt.position.y,0f);  
	}  
}
