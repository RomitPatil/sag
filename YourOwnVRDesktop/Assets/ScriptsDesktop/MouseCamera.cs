using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour {
	 
	public float speedH = 0f;
	public float speedV = 0f;
	public  float yaw = 0.0f;
	public float pitch = 0.0f;
	public int i;
	public GameObject InsideCollider;
	public bool set;
	public bool preview;
	// Use this for initialization
	void Start () {
		i = 0;
	}

	public IEnumerator moveScene(){


		yield return new WaitForEndOfFrame ();

		yaw -= speedH * Input.GetAxis ("Mouse X");
		pitch += speedV * Input.GetAxis ("Mouse Y");
	
		if (set) {
			pitch = transform.localRotation.eulerAngles.x;
			yaw = transform.localRotation.eulerAngles.y;
			Debug.Log (transform.localRotation.eulerAngles.y + " " + transform.localRotation.eulerAngles.x);
			set = false;
		}
			
		transform.eulerAngles = new Vector3 (pitch, yaw, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetMouseButton (0)) {
			
			StartCoroutine (moveScene ());

		
		} 
		
		if (preview) {
			
			RaycastHit hit;
			RaycastHit hit2;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Input.GetMouseButtonDown (0)) {
				
				if (Physics.Raycast (ray, out hit)) {
					Debug.DrawRay (transform.position, hit.point, Color.blue);
					Debug.Log (hit.collider.gameObject.name);
					if (hit.collider.gameObject.tag == "Hotspot") {
						Debug.Log ("Click on Hotspot");
						StopCoroutine (moveScene ());
						set = true;
						hit.collider.gameObject.transform.parent.gameObject.GetComponent<HotspotFunction> ().NavegateToScene ();
						gameObject.transform.rotation = hit.collider.gameObject.transform.parent.gameObject.GetComponent<HotspotFunction> ().CustomLocROt;
					}
					if (hit.collider.gameObject.tag == "ActionNev") {
						Debug.Log ("Click on ActionNev");
						StopCoroutine (moveScene ());
						hit.collider.gameObject.transform.parent.gameObject.GetComponent<HotspotFunction> ().NavegateToScene ();
					
					}
						

		
				}
	

			}
		}
	}
}
