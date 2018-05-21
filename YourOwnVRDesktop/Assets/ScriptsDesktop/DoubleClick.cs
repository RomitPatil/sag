using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClick : MonoBehaviour {
	public GameObject dotPrf;
	public GameObject Parent; 
	public GameObject dot;
	public int i = 0;
	public int j =0;
	public bool Active;
	public bool Move;
	public bool set;
	public bool Selected;
	public static bool firstdot;
	void Start() {
		Active = true;
		gameObject.transform.parent.gameObject.GetComponent<HotspotControl> ().HotspotClick ();

		if (gameObject.transform.GetChild (0).transform.localPosition.z != 1) {
			Move = true;
			gameObject.transform.GetChild (0).transform.localPosition = new Vector3 (0, 0, -1);
		} else {
			// For new Instatiation of Action hotstop
			Camera.main.GetComponent<Raycasting> ().dots = true;
		}
	} 

	public void DoubleClik (){
		StartCoroutine (doubleClick ());
	}



	IEnumerator doubleClick () {
		Debug.Log ("Click");
		Selected = true;
		gameObject.transform.GetChild (0).GetComponent<BoxCollider> ().enabled = true;
		gameObject.transform.parent.gameObject.GetComponent<HotspotControl> ().HotspotClick ();
		i ++;

		if (Active) {
			if (i == 2) {
				Vector3 pos;
				Quaternion rot;
				Debug.Log ("gameObject.transform.childCount" + (gameObject.transform.localPosition.x +"  " + gameObject.transform.localPosition.y +" "+ gameObject.transform.localPosition.z));
				pos = new Vector3 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z );
				rot = new Quaternion (gameObject.transform.localRotation.x, gameObject.transform.localRotation.y, gameObject.transform.localRotation.z ,gameObject.transform.localRotation.w);

				dot = GameObject.Instantiate (dotPrf);
				GameObject parent = GameObject.FindObjectOfType<ActionHotspot> ().gameObject;
		
				dot.transform.parent = parent.transform;
				dot.transform.localPosition = pos;
				dot.transform.localRotation = rot;
				//dot.transform.position += new Vector3 (0f , -10f , 0);
				//dotPrf.transform.GetChild (0).transform.localPosition = new Vector3(0 ,3, 1f);
				Active = false;
			
				if (Move) {
					gameObject.transform.GetChild (0).localPosition = new Vector3 (0, 0, 0);
					dot.transform.GetChild (0).localPosition = new Vector3 (0, 0, -1);

				} else {
					gameObject.transform.GetChild (0).localPosition = new Vector3 (0, 0, 2f);
					gameObject.transform.GetChild (0).transform.localRotation = new Quaternion (0, 0, 0, 0);
				}
			}
		}
		yield return new WaitForSeconds (0.5f);
		i = 0;
	}

	void Update ()
	{
		if (gameObject.transform.parent.GetComponent<ActionHotspot> ().CurrentDots.Length > 2) {
			Debug.Log ("lenght : " + gameObject.transform.parent.GetComponent<ActionHotspot> ().CurrentDots.Length);
			gameObject.transform.parent.GetComponent<ActionHotspot> ().CurrentDots [0].gameObject.transform.GetChild (0).transform.localPosition = new Vector3(0, 0, -2f);
		}
		
		if (Input.GetMouseButtonUp (0)) {
			Selected = false;
		}
		if (!Input.GetMouseButton (0)) {
			Selected = false;


			dotPrf.transform.GetChild (0).transform.localScale = new Vector3 (1, 1, 1f);

		} 

	
	}




}

