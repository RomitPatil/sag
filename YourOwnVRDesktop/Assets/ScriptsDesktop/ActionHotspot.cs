using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Step 1 - On Active Hotspot button Click . Instatiate dotPrf.
// Step 2 - dot gameobject can drag with mouse click to get position and fix on mouseUp .
// set 3 = on mouse right click ..dot gameobject postion is add to lineRender .
// set 4 - When dots are connected .. trigger panal is pop Up and select the shape of collider .
// Set 5 - Collider shape can minimize and maximize . 
// Set 6 - done button fix all thing .
// Set 7 - Prefab of Quart with scale option  and camera with icon is instatiate with + - and up down Right left .
// Set 8 - Done button read the pixels and set into the Qurt frame .. we can drag the Qurt with mouse click and fix with mouse up

public class ActionHotspot : MonoBehaviour {

	public LineRenderer LineRenderPrf;
	public GameObject dotPrf;
	public GameObject ActionCollider;
	public Mesh ColliderMesh;
	public GameObject NavigatePoint;
	public string ActionParentName;
	public Vector3 [] Dots;
	public GameObject[] CurrentDots;
	public GameObject FlatCollider;

	public Button Done;
	int i = 0;
	int j =0;
	int r = 0;
	public bool active;
	// Use this for initialization
	void Start () {
		ActionParentName = gameObject.name;
	}
	
	// Update is called once per frame
	void Update () {
		//GameObject[] dots = GameObject.FindGameObjectsWithTag ("Dot");

		DoubleClick[] temp = gameObject.GetComponentsInChildren<DoubleClick> ();
		CurrentDots = new GameObject[temp.Length]; 
		Debug.Log (gameObject.GetComponentInChildren<DoubleClick> ().gameObject.name + ":::::::" + temp.Length);
		for (int a = 0; a < temp.Length; a++) {
			CurrentDots[a] = temp [a].gameObject;
		}

		Dots = new Vector3[CurrentDots.Length];
		LineRenderPrf.positionCount = CurrentDots.Length;
		if (CurrentDots.Length != 0) {
			for (int i = 0; i < CurrentDots.Length; i++) {
				Dots [i] = CurrentDots [i].transform.localPosition;
//				Debug.Log (dots [i].transform.gameObject.transform.parent.localPosition);
				LineRenderPrf.SetPosition (i, Dots [i]);
			}

//			if (NavigatePoint.transform.GetChild(0).GetComponent<MeshCollider> ()) {
//				ColliderMesh = NavigatePoint.transform.GetChild(0).GetComponent<MeshCollider> ().sharedMesh;
//				ActionCollider.GetComponent<MeshFilter> ().sharedMesh = ColliderMesh;
//				ActionCollider.GetComponent<MeshCollider> ().sharedMesh = ColliderMesh;
//				float z = Dots [0].z;
//				Debug.Log ("Z  " + z + "    z" + Dots [0].z);
//				ActionCollider.transform.localPosition = new Vector3 (0, 0, z);
//			}
		}
		 

	}
		

	 
}
