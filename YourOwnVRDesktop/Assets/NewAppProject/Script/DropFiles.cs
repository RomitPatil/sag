using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropFiles : MonoBehaviour {
	public GameObject PhotoPrfb;
	public InputField FolderName;
	public GameObject PhotoContainer;
	public FootageDragAndDrop FootageDragAndDrop;
	public GameObject arrow;
	// Use this for initialization
	void Start () {
		FootageDragAndDrop = GameObject.Find ("FootageViewer").GetComponent<FootageDragAndDrop> ();
	}

	public void Dropfiles() {
		if (FolderName.text != "Imported Photoes" ||FolderName.text != "imported photoes" || FolderName.text != "Imported photoes" || FolderName.text != "Imported photoes")
		{
			Debug.Log (FootageDragAndDrop.gameObject.name);
		for (int i =0 ;  i < FootageDragAndDrop.SeletedObject.Count ; i++ ){
		GameObject temp = GameObject.Instantiate (PhotoPrfb);
				temp.transform.GetComponent<SelectFiles> ().Image = true;
		temp.transform.parent = PhotoContainer.transform;
			temp.transform.localPosition = new Vector3 (0, 0, 0);
			temp.transform.localScale = new Vector3 (1, 1, 1);
			temp.GetComponent<RawImage> ().texture = FootageDragAndDrop.SeletedObject [i].GetComponent<RawImage> ().mainTexture;
				temp.GetComponent<SelectFiles> ().inPhotoViewer = true;
				Debug.Log ( FootageDragAndDrop.PhotoFiles [i].GetComponent<SelectFiles> ().scene.ImageURLPath);
				temp.GetComponent<SelectFiles> ().scene.ImageURLPath = FootageDragAndDrop.SeletedObject [i].GetComponent<SelectFiles> ().scene.ImageURLPath;
				temp.transform.GetChild (0).GetComponent<InputField> ().text = FootageDragAndDrop.SeletedObject [i].transform.GetChild (0).GetComponent<InputField> ().text;
				temp.transform.GetChild (0).GetComponent<InputField> ().textComponent.fontSize = 16;
				temp.transform.GetChild (0).GetComponent<RectTransform> ().localPosition = new Vector3 (0, -35, 0);		
				temp.transform.GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (200, 40);		
			}
		if (FootageDragAndDrop.SeletedObject.Count != 0) {
			FootageDragAndDrop.SeletedObject [0].gameObject.GetComponent<SelectFiles> ().inPhotoViewer = false;

			//FootageDragAndDrop.SeletedObject [0].gameObject.GetComponent<SelectFiles> ().UnSelectFile ();
		}
		if (FootageDragAndDrop.SeletedObject.Count != 0) {
                Debug.Log("Calling ");
			arrow.GetComponent<ArrowViewControl> ().OnArrow ();
		}

		FootageDragAndDrop.cursor.SetMouse ();
//		DropFolder TemDropFolder =	arrow.GetComponent<ArrowViewControl> ().DropFolder;
//		TemDropFolder.setScroll = true;
	}
	}
	// Update is called once per frame
	void Update () {
		if (arrow == null) {
			arrow = gameObject.transform.parent.transform.GetChild (0).transform.GetChild (1).gameObject;
		}
		FootageDragAndDrop = GameObject.Find ("FootageViewer").GetComponent<FootageDragAndDrop> ();
	}
}
