using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentObjectHotspot : MonoBehaviour {
    public ObjectHotsot OH;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (OH == null) {

            OH = gameObject.GetComponentInParent<ObjectHotsot>();
        }
	}
}
