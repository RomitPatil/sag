using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour {

	public GameObject cube;
	public GameObject Collider;
	public bool OnCollider;
	public bool fix;
	public LayerMask layermask;
	public LayerMask layermask2;

	public bool dots;
	public bool ActiveHotspot;

	// Use this for initialization
	void Start () {
		dots = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			RaycastHit hit;
			RaycastHit hit2;
		
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Vector3 ScreenPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//			hit2 = Physics.RaycastAll (transform.position, transform.forward , 1000f,layermask);
//			Debug.Log (hit2.Length);

			Ray ray2 = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray2, out hit2,1000f,layermask)) {
				Debug.Log (hit2.point + "hit2");

				if (hit2.collider.gameObject.tag != "ActionNev") {
					
					if (hit2.collider.gameObject.tag == "ActiveHotspot") {
						gameObject.GetComponent<MouseCamera> ().speedH = 0f;
						gameObject.GetComponent<MouseCamera> ().speedV = 0f;
						cube = hit2.collider.gameObject;
						ActiveHotspot = true;
						hit2.collider.gameObject.GetComponent<BoxCollider> ().enabled = false;
						GameObject[] Action = GameObject.FindGameObjectsWithTag ("Dot");
						for (int j = 0; j < Action.Length; j++) {

							Action [j].GetComponent<BoxCollider> ().enabled = false;

						}
					}
					if (hit2.collider.gameObject.tag == "Dot") {
						gameObject.GetComponent<MouseCamera> ().speedH = 0f;
						gameObject.GetComponent<MouseCamera> ().speedV = 0f;
						OnCollider = true;
						cube = hit2.collider.gameObject.transform.parent.gameObject;
						hit2.collider.gameObject.GetComponent<BoxCollider> ().enabled = false;
					}
				}
				Debug.Log (hit2.collider.gameObject.name);
				Debug.DrawRay (transform.position, hit2.point, Color.green);
						}

			if (Physics.Raycast (ray, out hit , 1000f,layermask2)) {
				Debug.Log (hit.point + "hit");
				Debug.Log (hit.collider.gameObject.name);
				if (hit.collider.gameObject.tag != "InfoBox") {
					if (hit.collider.gameObject.tag == "Hotspot") {
						Debug.Log ("Hotspot on Selection");
						gameObject.GetComponent<MouseCamera> ().speedH = 0f;
						gameObject.GetComponent<MouseCamera> ().speedV = 0f;
						cube = hit.collider.gameObject.transform.parent.gameObject;
						hit.collider.gameObject.GetComponent<BoxCollider> ().enabled = false;
						cube.GetComponent<HotspotControl> ().HotspotClick ();
					} 
					if (hit.collider.gameObject.tag == "Dot") {
						if (dots) {
							gameObject.GetComponent<MouseCamera> ().speedH = 0f;
							gameObject.GetComponent<MouseCamera> ().speedV = 0f;
							OnCollider = true;
							cube = hit.collider.gameObject.transform.parent.gameObject;
							hit.collider.gameObject.GetComponent<BoxCollider> ().enabled = false;
						}
					}
					if (hit.collider.gameObject.tag == "ActiveHotspot") {
						if (ActiveHotspot) {
							gameObject.GetComponent<MouseCamera> ().speedH = 0f;
							gameObject.GetComponent<MouseCamera> ().speedV = 0f;
							OnCollider = true;
							cube = hit.collider.gameObject;
							ActiveHotspot = true;
							hit.collider.gameObject.GetComponent<BoxCollider> ().enabled = false;
							GameObject[] Action = GameObject.FindGameObjectsWithTag ("Dot");
							for (int j = 0; j < Action.Length; j++) {

								Action [j].GetComponent<BoxCollider> ().enabled = false;
						
							}
							//gameObject.transform.parent = gameObject.transform.parent.transform.parent.transform.parent.transform;
						}
					}

					if (hit.collider.gameObject.tag == "ActionNev") {
						hit.collider.gameObject.transform.parent.transform.GetComponent<HotspotControl> ().HotspotClick ();
						Debug.Log ("Click on Action ");
					}


					var startPos = hit.point;
					var startRot = Quaternion.LookRotation (hit.normal);
					Debug.DrawRay (transform.position+ new Vector3(1f ,2f,0), hit.point, Color.blue);
				
					Collider.gameObject.SetActive (true);

					if (cube != null) {
						if (cube.GetComponent<HotspotControl> ()) {
							if (cube.GetComponent<HotspotControl> ().Info) {
								cube.transform.GetChild (0).gameObject.transform.localPosition = new Vector3 (0f, 0f, 12f);
								cube.transform.GetChild (1).gameObject.transform.localPosition = new Vector3 (0f, 0f, 12f);
								cube.transform.GetChild (0).gameObject.transform.localRotation = new Quaternion (0f, 180f, 0f, 0f);
								cube.transform.GetChild (1).gameObject.transform.localRotation = new Quaternion (0f, 180f, 0f, 0f);
							} else if (cube.GetComponent<HotspotControl> ().hotspot) {
								cube.transform.GetChild (0).gameObject.transform.localPosition = new Vector3 (0f, 0f, 1f);
							} else if (cube.GetComponent<HotspotControl> ().arrow) {
								cube.transform.GetChild (0).gameObject.transform.localPosition = new Vector3 (0f, 0f, 1f);
							}
						} else {
							if (cube.transform.parent.gameObject.GetComponent<HotspotControl> ()) {
								if (cube.transform.parent.gameObject.GetComponent<HotspotControl> ().ActionHotspot) {
									//cube.transform.GetChild (0).transform.localPosition = new Vector3 (0f, 0f, 0f);
								}
							}
							if (cube.GetComponent<PolygonTester> ()) {
								if (ActiveHotspot) {
									cube.transform.position = hit.point;
									cube.transform.rotation = startRot;	
								}
							}
						}
						if (cube.GetComponent<DoubleClick>()) {
							if (!cube.GetComponent<DoubleClick> ().set) {
								if (dots) {
										gameObject.GetComponent<MouseCamera> ().speedH = 0f;
										gameObject.GetComponent<MouseCamera> ().speedV = 0f;
									cube.transform.rotation = startRot;	
									cube.transform.position = hit.point;
								}
							}
						}
						}

					if (cube != null) {
						if (!cube.GetComponent<DoubleClick> ()) {
							cube.transform.position = hit.point;
							cube.transform.rotation = startRot;	
						}
					}

					}
			
				}
			}



		if (Input.GetMouseButtonUp(0)) {
			if (cube != null) {
				if (cube.GetComponent<HotspotControl> ()) {
					cube.GetComponent<HotspotControl> ().ui_Canvas_control.FixHotspot.isOn = true;
				}
					cube = null;
				gameObject.GetComponent<MouseCamera> ().speedH = 2f;
				gameObject.GetComponent<MouseCamera> ().speedV = 2f;
			}
			GameObject[] temp = GameObject.FindGameObjectsWithTag ("Hotspot");
			GameObject[] Action = GameObject.FindGameObjectsWithTag ("Dot");
			GameObject[] ActionHotspot = GameObject.FindGameObjectsWithTag ("ActiveHotspot");
			Debug.Log (Action.Length);
			for (int i = 0; i < temp.Length; i++) {

				temp[i].GetComponent<BoxCollider> ().enabled = true;
			}
			for (int j = 0; j < Action.Length; j++) {

				Action[j].GetComponent<BoxCollider> ().enabled = true;

			}
			for (int k = 0; k < ActionHotspot.Length; k++) {
				if (ActionHotspot [k].GetComponent<BoxCollider> ()) {
					ActionHotspot [k].GetComponent<BoxCollider> ().enabled = true;
				}
			}
			Debug.Log ("Fixed");


		}

		if (OnCollider) {
			Collider.SetActive (true);


		} else {
			Collider.SetActive (false);
		}
	}
}
