using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSceneLoaction : MonoBehaviour {
	public Quaternion CustumRot;

	public bool CustumLocation;


	void OnEnable (){
		Debug.Log ("Enable the object:");

		if (CustumLocation) {
			Quaternion temRot = CustumRot;
			Camera.main.transform.rotation = temRot;


		} else {

			//Camera.main.transform.rotation =  Quaternion.Euler(Vector3.zero);


		}
	}
	void Start () {
		
		if (CustumLocation) {
			Quaternion temRot = CustumRot;
			Camera.main.transform.rotation = temRot;

		} else {
		//	Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);

		}
	}

}
