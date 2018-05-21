using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loading : MonoBehaviour {
    public float speed;
    // Update is called once per frame
    void Update()
    {

        transform.eulerAngles += new Vector3(0, 0, 10 * speed);
    }
}
