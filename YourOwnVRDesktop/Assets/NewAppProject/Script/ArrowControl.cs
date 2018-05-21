using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour {

	public bool Arrow;
	public GameObject ImportedPhotoes;
	public GameObject FolderConationer;
	public float temp;
	public void OnClickArrow (){

		for (int k = 0; k < FolderConationer.transform.childCount; k++) {
		
			if (FolderConationer.transform.GetChild (k).gameObject.activeInHierarchy) {
				
			}
			if (!FolderConationer.transform.GetChild (k).gameObject.activeInHierarchy) {
				
			}

		}


	
		if (Arrow) {
			
			Arrow = false;
			gameObject.transform.localEulerAngles = new Vector3 (0, 0, 0);
			ImportedPhotoes.SetActive (false);
			Debug.Log (gameObject.transform.parent.transform.GetChild(0).transform.childCount);
		
			for (int i = 0; i < gameObject.transform.parent.transform.GetChild(0).transform.childCount; i++) {
				if (FolderConationer.transform.GetChild (i).GetComponent<CreateFolder> ()) {
					FolderConationer.transform.GetChild (i).gameObject.SetActive(false);
					temp = 50;
					gameObject.transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2(380 ,temp );
				

					Debug.Log (FolderConationer.transform.GetChild (i).gameObject.name + "Folder is Disable");

					}
					
			}
//			for (int j = 0 ; j < FolderConationer.transform.childCount ; j++){
//				FolderConationer.transform.GetChild (j).gameObject.SetActive (false);
//			}
		} else {
			Arrow = true;
			ImportedPhotoes.SetActive (true);
			gameObject.transform.localEulerAngles = new Vector3 (0, 0, 30f);
			for (int i = 0; i < gameObject.transform.parent.transform.GetChild(0).transform.childCount; i++) {
				if (FolderConationer.transform.GetChild (i).GetComponent<CreateFolder> ()) {
					FolderConationer.transform.GetChild (i).gameObject.SetActive(true);
					Debug.Log (FolderConationer.transform.GetChild (i).gameObject.name + "Folder is Find" + FolderConationer.transform.GetChild (i).gameObject.GetComponent<RectTransform>().sizeDelta.y);
					if (FolderConationer.transform.GetChild (i).gameObject.GetComponent<RectTransform> ().sizeDelta.y != 0) {
						temp = gameObject.transform.parent.transform.GetComponent<RectTransform> ().sizeDelta.y + FolderConationer.transform.GetChild (i).gameObject.GetComponent<RectTransform> ().sizeDelta.y + 50;
					} else {
						temp = gameObject.transform.parent.transform.GetComponent<RectTransform> ().sizeDelta.y + 50;
					}
					gameObject.transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2(380 ,temp );

					//gameObject.transform.parent.transform.GetComponent<DropFolder> ().width += 100f;
				//	gameObject.transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (400, gameObject.transform.parent.transform.GetComponent<DropFolder> ().width);
				}
					
			}
		}
	}
	// Use this for initialization
	void Start () {
		
		FolderConationer = gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
