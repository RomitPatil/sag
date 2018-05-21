using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenameFile : MonoBehaviour {

	public bool ChangingName;
	public int TimeCount;
	// Use this for initialization
	void Start () {
		ChangingName = false;
	}


	public void ChangeName () {

		TimeCount++;
		if (TimeCount == 1) {
			TimeCount = 0;
		//	StopCoroutine (Timing ());
		//	transform.GetChild (0).gameObject.SetActive (true);
			Debug.Log (transform.GetComponent<Text> ().text);
			transform.GetChild (0).GetChild(1).GetComponent<Text>().text = transform.GetComponent<Text> ().text ;
			transform.GetChild (0).GetComponent<InputField>().text = transform.GetComponent<Text> ().text ;
		//	transform.GetComponent<Text> ().enabled = false;
//			transform.GetComponent<Text> ().text = transform.GetChild (0).GetComponent<InputField> ().placeholder.GetComponent<Text>().text;
			ChangingName = true;
		}
	}
	// Update is called once per frame
	void Update () {
		SetChangeName ();
	}

	private void SetChangeName () {
	
		if (ChangingName == true) {
			
			if (Input.GetMouseButtonDown(0)) {
				Debug.Log ("Enter");
				transform.GetComponent<Text> ().text = transform.GetChild (0).GetComponent<InputField> ().text; 
				transform.GetChild (0).gameObject.SetActive(false);
			//	transform.GetComponent<Text> ().enabled = true;

			}
		}
	
	}
}
