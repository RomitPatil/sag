using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  
public class dragKnobScript : MonoBehaviour {

	private Vector3 dragPosition;
	private Vector3 screenPoint;
	private Vector3 offset;  
	// Use this for initialization  
	void Start () {
		
	} 
	public void OnMouseDown() {
		Debug.Log ("mouse enter");  
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		Debug.Log ("offset "+offset.x);    
	}


	public void OnMouseDrag()    
	{   
		Debug.Log ("draging");
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
		//transform.position = new Vector3 (((Input.mousePosition.x-3800)*7/50)-350, 0f, 0f);             
	} 
	
	// Update is called once per frame 
	void Update () {
		//transform.position = dragPosition; 

	}
}
