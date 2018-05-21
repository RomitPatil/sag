using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraDraging : MonoBehaviour {

	public float speedH = 0f;
	public float speedV = 0f;
	public  float yaw = 0.0f;
	public float pitch = 0.0f;
	public bool set;
	// Use this for initialization

	public Transform target;
	void Start () {
		
	}

    // Drag camera from mouse Click
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

	public void SetCamera() {
		//StartCoroutine (moveScene ());
	}
	// Update is called once per frame
	void Update () {
		
		if (gameObject.GetComponent<RaycastingOnDome> ().CameraDrag == false) {
			Debug.Log ("Dont Move");
			speedH = 0;
			speedV = 0;
		} if (gameObject.GetComponent<RaycastingOnDome>().CameraDrag == true) {
			if (gameObject.GetComponent<RaycastingOnDome> ().ActiveHotspot == false) {
				speedH = 5f;
				speedV = 5f;
			}
			if (Input.GetMouseButton (0)) {
				//StartCoroutine (moveScene ());

				transform.LookAt (target);
				transform.Rotate (new Vector3 (Input.GetAxis ("Mouse Y") * speedH, -Input.GetAxis ("Mouse X") * speedV , 0));
				pitch = transform.rotation.eulerAngles.x;
				yaw = transform.rotation.eulerAngles.y;
				transform.rotation = Quaternion.Euler (pitch, yaw, 0);

			}
		}

	

	}
}
