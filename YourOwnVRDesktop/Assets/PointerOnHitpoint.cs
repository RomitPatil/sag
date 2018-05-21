using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerOnHitpoint : MonoBehaviour {
    public RaycastingOnDome HitRayCast;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        gameObject.transform.position = HitRayCast.HitPos;

    }
}
