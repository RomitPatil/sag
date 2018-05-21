using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixShapeActiveCollider : MonoBehaviour {
	public float z;
	public Material setMaterial;
	bool stop;
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		
		if (GetComponent<MeshRenderer> ()) {
			GetComponent<MeshRenderer> ().material = setMaterial;
		}
	}
}
