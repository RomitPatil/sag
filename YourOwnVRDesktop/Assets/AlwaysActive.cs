using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysActive : MonoBehaviour {


	// Update is called once per frame
	void Update () {
        if (!gameObject.activeInHierarchy) {
            gameObject.SetActive(true);
        }
        gameObject.SetActive(true);

    }
}
