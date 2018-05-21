using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pivotChage : MonoBehaviour {
	public MeshRenderer msh;
	Vector3 vector3;
	public BoxCollider boxCollider;
	// Use this for initialization
	void Start () {
		msh = GetComponent<MeshRenderer> ();
		gameObject.AddComponent<BoxCollider> ();

	}
	
	// Update is called once per frame
	void Update (){
		if (boxCollider == null) {
			boxCollider = GetComponent<BoxCollider> ();
		}
		vector3 = boxCollider.center;
		gameObject.transform.localPosition = new Vector3 (vector3.x * 1f, vector3.y * -1f, vector3.z);
	}


}
