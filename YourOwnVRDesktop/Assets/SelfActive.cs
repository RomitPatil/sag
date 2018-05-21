using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfActive : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonUp(0)) {

            if (gameObject.GetComponent<BoxCollider>()) {

            gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            if (gameObject.GetComponent<SphereCollider>()) {

                gameObject.GetComponent<SphereCollider>().enabled = true;
            }
        }
	}
}
