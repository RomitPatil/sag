using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropFolder : MonoBehaviour {

	public GameObject DropFolderPrb;
	public FootageDragAndDrop FootageDragAndDrop;
	public bool drop;
	public bool PhotoesFilter;
	public bool VideoFilter;
	public bool UnityPRFBFilder;
	public GameObject Aerro;
	public GameObject ImportFolder;
	public CreateFolder[] Folderes;
	public float width;
	public float PhotoviewWidth;
	public float yForFolder;
	public float xForFoloder;
	public CreateFolder[] FOLDERS;
	public 	DropFiles[] Photoviewer;
	public int FolderCount;
	public int PhotoViewerCount;
	public int ActiveFolder;
	public int ActiveViewer;
	public int tem = 0;
	public bool setScroll;
	// Use this for initialization
	void Start () {
		FootageDragAndDrop = FindObjectOfType<FootageDragAndDrop> ();
		setScroll = true;
	}
	
	// Update is called once per frame
	void Update () {
		ActiveFolder = 0;
		ActiveViewer = 0;
		if (FOLDERS.Length != 0){
			width = 50;
		}
			for (int j = 0; j < FOLDERS.Length; j++) {
			
				if (FOLDERS [j].gameObject.activeInHierarchy) {
					ActiveFolder++;
				}
			if (FOLDERS [j].gameObject.transform.GetChild (1).gameObject.activeInHierarchy) {
			
				ActiveViewer++;
			}

			
		}
		if (FOLDERS.Length != 0) {
			width = width + 50 * ActiveFolder;

		}

		if (ActiveViewer != 0){

				for (int i = 0; i < Photoviewer.Length; i++) {
				if (Photoviewer [i].transform.GetChild (0).transform.GetChild(0).childCount == 1) {
					tem = 50 *3;
					Photoviewer [i].transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (500, 200f);
					if (Photoviewer [i].isActiveAndEnabled) {
						Photoviewer [i].transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, 200f);
					} else {
						Photoviewer [i].transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, 0f);
					}
//					if (setScroll) {
//						Photoviewer [i].transform.GetChild (0).transform.GetChild (0).transform.GetComponent<RectTransform> ().localPosition = new Vector3 (-6.5f, 65, 0);
//						setScroll = false;
//					}
					width =Photoviewer [i].transform.GetComponent<RectTransform> ().sizeDelta.y + tem;
					Debug.Log ("Small " + width + "   " + Photoviewer [i].transform.GetChild (0).transform.GetChild(0).gameObject.name);
				}

				if (Photoviewer [i].transform.GetChild (0).transform.GetChild(0).childCount >= 1 && Photoviewer [i].transform.GetChild (0).transform.GetChild(0).childCount == 2) {
						tem = 50 *3;
					Photoviewer [i].transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (500f, 200f);
					if (Photoviewer [i].isActiveAndEnabled) {
						Photoviewer [i].transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, 200f);
					} else {
						Photoviewer [i].transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, 0f);
					}
//						if (setScroll) {
//						Photoviewer [i].transform.GetChild (0).transform.GetChild (0).transform.GetComponent<RectTransform> ().localPosition = new Vector3 (-6.5f, 65, 0);
//						setScroll = false;
					//}
					width =Photoviewer [i].transform.GetComponent<RectTransform> ().sizeDelta.y + tem;
						Debug.Log ("Small " + width + "   " + Photoviewer [i].transform.GetChild (0).transform.GetChild(0).gameObject.name);
					}
					if (Photoviewer [i].transform.GetChild (0).transform.GetChild(0).childCount >= 3 ) {
					
						tem = 50 * 5;
					Photoviewer [i].transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (500, 330f);
					if (Photoviewer [i].isActiveAndEnabled) {
						Photoviewer [i].transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0f, 330f);
					} else {
						Photoviewer [i].transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0f, 0f);
					
					}
//					if (setScroll) {
//						Photoviewer [i].transform.GetChild (0).transform.GetChild (0).transform.GetComponent<RectTransform> ().localPosition = new Vector3 (-6.5f, 120, 0);
//						setScroll = false;
//					}
						PhotoViewerCount = ActiveViewer;
					width =Photoviewer [i].transform.GetComponent<RectTransform> ().sizeDelta.y + tem;
						Debug.Log ("Larget" + width + "   " + Photoviewer [i].transform.GetChild (0).transform.GetChild(0).gameObject.name);
					}
					if (Photoviewer [i].transform.GetChild (0).transform.GetChild(0).childCount >= 5) {
						tem = 50 * 7;
					Photoviewer [i].transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (500, 470f);
					if (Photoviewer [i].isActiveAndEnabled) {
						Photoviewer [i].transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (10f, 500f);
					} else {
						Photoviewer [i].transform.parent.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, 0f);
					}
						PhotoViewerCount = ActiveViewer;
					width =Photoviewer [i].transform.GetComponent<RectTransform> ().sizeDelta.y + tem;
						Debug.Log ("Full Size" + width + "   " + Photoviewer [i].transform.GetChild (0).transform.GetChild(0).gameObject.name);
					}
				}

		}
		if (ActiveViewer == 0) {
			width = width + tem * 0;
		}
//		Debug.Log (FOLDERS.Length + "Enter");
		PhotoviewWidth = 0;
//		if (FOLDERS.Length != 0){ 
//			width = 0;
//			for (int k = 0 ; k < FOLDERS.Length ; k++ ){
//				width += 50;
//				Debug.Log (FOLDERS [k].transform.gameObject.transform.GetChild(1).gameObject.name);
//				if (FOLDERS [k].transform.gameObject.transform.GetChild (1).gameObject.activeInHierarchy) {
//					Debug.Log ("View");
//					Debug.Log (FOLDERS [k].transform.gameObject.transform.GetChild (1).gameObject.name + "View -350");
//					width -= 350;
//					//FOLDERS [k].transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (380, 350);
//					FOLDERS [k].Extend = true;
//				//	width =+ 350;
//					//PhotoviewWidth += 350;
//				} else if(!FOLDERS [k].transform.gameObject.transform.GetChild (1).gameObject.activeInHierarchy)  {
//
//				//	width += 350;
//					Debug.Log (FOLDERS [k].transform.gameObject.transform.GetChild (1).gameObject.name + "View 350");
//					FOLDERS [k].Extend = false;
//
//					//PhotoviewWidth -= 350;
//				}
//			}
//		}


	//	gameObject.transform.GetComponent<RectTransform> ().sizeDelta = new Vector3(400, width);
//		Debug.Log (width);
		FOLDERS = gameObject.GetComponentsInChildren<CreateFolder> ();
		Photoviewer = gameObject.GetComponentsInChildren<DropFiles> ();

		if (FOLDERS.Length != FolderCount){
			FolderCount = FOLDERS.Length;
			if (FolderCount > 2) {
				//width = 50;
			}
		//	width = 50;
			for (int i = 0; i < FolderCount; i++) {
				
			//	width += 50;
				xForFoloder = 40;
			  //  yForFolder -= 50;
			}

			Debug.Log (width);
		}


	}
	public void Drop ()
	{
		
		Debug.Log ("Drop" + FootageDragAndDrop.SeletedObject.Count);

		FootageDragAndDrop.cursor.SetMouse();


			if (PhotoesFilter) {

				if (FootageDragAndDrop.SeletedObject.Count != 0) {
					Debug.Log ("Photo Filter");
					GameObject temFolder = GameObject.Instantiate (DropFolderPrb);
					temFolder.transform.parent = gameObject.transform.GetChild (0).transform;
					ImportFolder.SetActive (true);
					//temFolder.transform.GetComponent<RectTransform>().TransformPoint = new Vector3 (0, 0, 0);
					temFolder.transform.localScale = new Vector3 (1, 1, 1);

				temFolder.transform.GetComponent<CreateFolder> ().FolderName.text = FootageDragAndDrop.SeletedObject [0].transform.GetChild (0).GetComponent<InputField> ().text;
			
					for (int i = 0; i < FootageDragAndDrop.SeletedObject.Count; i++) {	
						if (FootageDragAndDrop.SeletedObject [i].gameObject.GetComponent<SelectFiles> ().ExtentionName == "JPG"
						    || FootageDragAndDrop.SeletedObject [i].gameObject.GetComponent<SelectFiles> ().ExtentionName == "png"
						    || FootageDragAndDrop.SeletedObject [i].gameObject.GetComponent<SelectFiles> ().ExtentionName == "jpg"
						    || FootageDragAndDrop.SeletedObject [i].gameObject.GetComponent<SelectFiles> ().ExtentionName == "jpeg"
						    || FootageDragAndDrop.SeletedObject [i].gameObject.GetComponent<SelectFiles> ().ExtentionName == "PNG") {
						temFolder.transform.GetComponent<CreateFolder> ().Filename.Add (FootageDragAndDrop.SeletedObject [i].transform.GetChild (0).GetComponent<InputField> ().text);
							temFolder.transform.GetComponent<CreateFolder> ().photoes.Add (FootageDragAndDrop.SeletedObject [i].GetComponent<RawImage> ().mainTexture);
						temFolder.transform.GetComponent<CreateFolder> ().ImageUrlPath.Add (FootageDragAndDrop.SelecteURL [i]);
						}
					}
					for (int j = 0; j < FootageDragAndDrop.SeletedObject.Count; j++) {
					FootageDragAndDrop.SeletedObject [j].gameObject.GetComponent<SelectFiles> ().inPhotoViewer = false;
						FootageDragAndDrop.SeletedObject [j].gameObject.GetComponent<SelectFiles> ().UnSelectFile ();
					FootageDragAndDrop.SelecteURL.Clear ();
					}
					temFolder.transform.GetComponent<CreateFolder> ().Drop ();

					Aerro.transform.localEulerAngles = new Vector3 (0, 0, 30);
					Aerro.transform.GetComponent<ArrowControl> ().OnClickArrow ();

				}
			}
			if (VideoFilter) {
				ImportFolder.SetActive (true);
				Debug.Log ("Video Filter");
				if (FootageDragAndDrop.SeletedObject.Count != 0) {
					GameObject temFolder = GameObject.Instantiate (DropFolderPrb);
					temFolder.transform.parent = gameObject.transform.GetChild (0).transform;
					//temFolder.transform.GetComponent<RectTransform>().TransformPoint = new Vector3 (0, 0, 0);
					temFolder.transform.localScale = new Vector3 (1, 1, 1);

				temFolder.transform.GetComponent<CreateFolder> ().FolderName.text = FootageDragAndDrop.SeletedObject [0].transform.GetChild (0).GetComponent<InputField> ().text;

					for (int i = 0; i < FootageDragAndDrop.SeletedObject.Count; i++) {	
						if (FootageDragAndDrop.SeletedObject [i].gameObject.GetComponent<SelectFiles> ().ExtentionName == "mp4"
						    || FootageDragAndDrop.SeletedObject [i].gameObject.GetComponent<SelectFiles> ().ExtentionName == "MP4") {
						temFolder.transform.GetComponent<CreateFolder> ().FileVideoname.Add (FootageDragAndDrop.SeletedObject [i].transform.GetChild (0).GetComponent<InputField> ().text);
							temFolder.transform.GetComponent<CreateFolder> ().Videoes.Add (FootageDragAndDrop.SeletedObject [i].GetComponent<RawImage> ().mainTexture);
							FootageDragAndDrop.SeletedObject [i].gameObject.GetComponent<SelectFiles> ().UnSelectFile ();
						FootageDragAndDrop.SeletedObject [i].gameObject.GetComponent<SelectFiles> ().inPhotoViewer = false;
						}
					}
					temFolder.transform.GetComponent<CreateFolder> ().Drop ();

					Aerro.transform.localEulerAngles = new Vector3 (0, 0, 30);
					Aerro.transform.GetComponent<ArrowControl> ().OnClickArrow ();

				}
			}
			if (UnityPRFBFilder) {
							
//					if (FootageDragAndDrop.SeletedObject [i].gameObject.GetComponent<SelectFiles> ().ExtentionName != "mp4"
//						|| FootageDragAndDrop.SeletedObject [i].gameObject.GetComponent<SelectFiles> ().ExtentionName != "MP4")
//						temFolder.transform.GetComponent<CreateFolder> ().Filename.Add (FootageDragAndDrop.SeletedObject [i].transform.GetChild (0).GetComponent<Text> ().text);
//					temFolder.transform.GetComponent<CreateFolder> ().photoes.Add (FootageDragAndDrop.SeletedObject [i].GetComponent<RawImage> ().mainTexture);
//					FootageDragAndDrop.SeletedObject [i].gameObject.GetComponent<SelectFiles> ().UnSelectFile ();
			}
			
	


		}
	}



