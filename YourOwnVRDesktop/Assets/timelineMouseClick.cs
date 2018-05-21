using UnityEngine;
using System.Collections;
public class timelineMouseClick : MonoBehaviour  
{
	Vector3 newPosition;   
	void Start () {
		newPosition = transform.position;
	}
	void Update()
	{ 
	//	Debug.Log ("x :"+ newPosition.x);  
		if (Input.GetMouseButtonDown(0))
		{    
			RaycastHit hit;  
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				newPosition = hit.point;
				Debug.Log ("x :"+ newPosition.x);    
				//transform.position = newPosition;  
			}
		}
	}
}