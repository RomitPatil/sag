using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowViewControl : MonoBehaviour {

	public GameObject PhotoViewer;
	public bool Arrow;
	public DropFolder DropFolder;
	public float temp;
	bool gehight;

	// Use this for initialization
	void Start () {
		
		DropFolder = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.gameObject.GetComponent<DropFolder> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (gehight) {
			temp = gameObject.transform.parent.transform.parent.transform.GetComponent<RectTransform> ().sizeDelta.y ;
			if (temp != 0) {
				DropFolder.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (DropFolder.transform.GetComponent<RectTransform> ().sizeDelta.x, DropFolder.transform.GetComponent<RectTransform> ().sizeDelta.y + temp );

				gehight = false;
			}
		}

	}

	public void OnArrow() {
		if (Arrow) {
			Arrow = false;
			PhotoViewer.SetActive (true);


			gehight = true;

				gameObject.transform.localEulerAngles = new Vector3 (0, 0, 30f);
			//gameObject.transform.parent.transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (380, 0);
			//temp = gameObject.transform.parent.transform.parent.transform.GetComponent<RectTransform> ().sizeDelta.y;
			Debug.Log (gameObject.transform.parent.transform.parent.transform.gameObject.name + temp);
		//	DropFolder.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (DropFolder.transform.GetComponent<RectTransform> ().sizeDelta.x, DropFolder.transform.GetComponent<RectTransform> ().sizeDelta.y + temp);

			//gameObject.transform.parent.transform.parent.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, 350f);
		//	DropFolder.transform.GetComponent<DropFolder> ().width += 350f;
		//	DropFolder.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (400, DropFolder.transform.GetComponent<DropFolder> ().width);
			Debug.Log ("Arro in On");
		}

		else{
			Arrow = true;
	
			gameObject.transform.localEulerAngles = new Vector3 (0, 0, 0f);
			if (PhotoViewer.activeInHierarchy) {
				DropFolder.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (DropFolder.transform.GetComponent<RectTransform> ().sizeDelta.x, DropFolder.transform.GetComponent<RectTransform> ().sizeDelta.y - temp  );
				gameObject.transform.parent.transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, 0);
			}
			PhotoViewer.SetActive (false);
			//float CreateFolderNO = DropFolder.transform.GetChild (0).childCount;
			//float parentHight = CreateFolderNO * 50 + 50;
			//DropFolder.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (DropFolder.transform.GetComponent<RectTransform> ().sizeDelta.x, parentHight);

		//	gameObject.transform.parent.transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (380, 0);
			//gameObject.transform.parent.transform.parent.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, 0);
			//DropFolder.transform.GetComponent<DropFolder> ().width -= 150f;
		//	DropFolder.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (400, DropFolder.transform.GetComponent<DropFolder> ().width);
			Debug.Log ("Arro in Off");
		}
	
	}
}
