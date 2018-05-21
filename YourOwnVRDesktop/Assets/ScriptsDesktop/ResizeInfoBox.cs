using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeInfoBox : MonoBehaviour {
	public Image imageSize;
	public Text ResizeText;
	public bool UpButtonClick;
	public bool DownButtonClick;
	public bool RightButtonClick;
	public bool LeftButtonClick;
	public bool DoneButtonClick;

	public Button UpBTN;
	public Button DownBTN;
	public Button RightBTN;
	public Button LeftBTN;
	public Button Done;

	public bool Mediaplay;

	public Color SelectedColor;
	public Color defaultColor;

	public GameObject DomeCamera;
	// Use this for initialization
	void Start () {

		DomeCamera = GameObject.Find ("DomeCamera");
		if (!Mediaplay) {
			SelectedColor = Color.blue;
			defaultColor = Color.white;

			ResizeText.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta.x * 2f, imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta.y * 2f);

		//	gameObject.transform.localPosition = new Vector3 (0, 0, 60f);
		} 
	}
	
	// Update is called once per frame
	void Update () {


		DomeCamera = GameObject.Find ("DomeCamera");
		//gameObject.transform.localPosition = new Vector3 (0, 0, 0);
//		if (Input.GetMouseButtonUp (0)) {
//			DoneButton ();
//		}

//		if (Input.GetMouseButton(0)) {
//			if(Input.GetAxis("Mouse X")<0){
//				//Code for action on mouse moving left
//				print("Mouse moved left");
//			}else if(Input.GetAxis("Mouse X")>0){
//				//Code for action on mouse moving right
//				print("Mouse moved right");
//			}
//
//			if(Input.GetAxis("Mouse Y")<0){
//				//Code for action on mouse moving Up
//				print("Mouse moved Down");
//			}else if(Input.GetAxis("Mouse Y")>0){
//				//Code for action on mouse moving Down
//				print("Mouse moved Up");
//			}
//		}

//		if (UpButtonClick) {
//			Camera.main.GetComponent<Raycasting> ().enabled = false;
//			Camera.main.GetComponent<MouseCamera> ().enabled = false;
//			UpBTN.gameObject.GetComponent<Image> ().color = SelectedColor;
//			ResizeHeight ();
//			Done.gameObject.SetActive (true);
//		} else {
////			UpBTN.gameObject.GetComponent<Image> ().color = defaultColor;
//		}
//		if (DownButtonClick) {
////			DomeCamera.GetComponent<Raycasting> ().enabled = false;
////			DomeCamera.GetComponent<MouseCamera> ().enabled = false;
//			ResizeHeight ();
//			DownBTN.gameObject.GetComponent<Image> ().color = SelectedColor;
//			Done.gameObject.SetActive (true);
//		}else {
//			DownBTN.gameObject.GetComponent<Image> ().color = defaultColor;
//		}
//		if (RightButtonClick) {
//			Camera.main.GetComponent<Raycasting> ().enabled = false;
//			Camera.main.GetComponent<MouseCamera> ().enabled = false;
//			ResizeWidth();
//			RightBTN.gameObject.GetComponent<Image> ().color = SelectedColor;
//			Done.gameObject.SetActive (true);
//		}else {
//			
//			RightBTN.gameObject.GetComponent<Image> ().color = defaultColor;
//		}
//		if (LeftButtonClick) {
//			Camera.main.GetComponent<Raycasting> ().enabled = false;
//			Camera.main.GetComponent<MouseCamera> ().enabled = false;
//			ResizeWidth();
//			LeftBTN.gameObject.GetComponent<Image> ().color = SelectedColor;
//			Done.gameObject.SetActive (true);
//		}else {
//			LeftBTN.gameObject.GetComponent<Image> ().color = defaultColor;
//		}
//
//		if (DoneButtonClick) {
//			Camera.main.GetComponent<Raycasting> ().enabled = true;
//			Camera.main.GetComponent<MouseCamera> ().enabled = true;
//			UpBTN.gameObject.GetComponent<Image> ().color = defaultColor;
//			DownBTN.gameObject.GetComponent<Image> ().color = defaultColor;
//			RightBTN.gameObject.GetComponent<Image> ().color = defaultColor;
//			LeftBTN.gameObject.GetComponent<Image> ().color = defaultColor;
//			Done.gameObject.SetActive (false);
//		}

			
	}

	public void UpButton () {
		
		Debug.Log ("Enter");
		UpButtonClick = true;
		DownButtonClick = false;
		LeftButtonClick = false;
		RightButtonClick = false;
		DoneButtonClick = false;
	}

	public void DownButton () {
		Debug.Log ("Down");
		UpButtonClick = false;
			DownButtonClick = true;
		LeftButtonClick= false;
		RightButtonClick = false;
		DoneButtonClick = false;
	}

	public void RightButton () {
		UpButtonClick = false;
		DownButtonClick = false;
		LeftButtonClick= false;
		RightButtonClick = true;
	}

	public void LeftButton () {
		UpButtonClick = false;
		DownButtonClick = false;
		LeftButtonClick= true;
		RightButtonClick = false;
		DoneButtonClick = false;
	}

	public void DoneButton () {
	
		UpButtonClick = false;
		DownButtonClick = false;
		LeftButtonClick= false;
		RightButtonClick = false;
		DoneButtonClick = true;
		gameObject.GetComponent<HotspotControl> ().InfoBoxSize = imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta;

	}
	public void ResetRaycast (){
		DomeCamera.GetComponent<RaycastingOnDome> ().enabled = true;
		DomeCamera.GetComponent<MouseCameraDraging> ().enabled = true;
	} 
	public void ResizeHeight() {
		DomeCamera.GetComponent<RaycastingOnDome> ().enabled = false;
		DomeCamera.GetComponent<MouseCameraDraging> ().enabled = false;
			Debug.Log ("Click");
		if (Input.GetAxis ("Mouse Y") < 0) {
			//Code for action on mouse moving Down
			print ("Mouse moved Up");
			if (!Mediaplay) {
				Debug.Log (imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta);
				imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta += new Vector2 (0f, 5f);
				ResizeText.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2( imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta.x , imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta.y *2f);
			} else {
				gameObject.transform.GetChild (2).transform.localScale += new Vector3 (gameObject.transform.GetChild (2).transform.localScale.x + 5f, gameObject.transform.GetChild (2).transform.localScale.y + 5f, gameObject.transform.GetChild (2).transform.localScale.z);

			}
		} else if (Input.GetAxis ("Mouse Y") > 0) {
			//Code for action on mouse moving Down
			print ("Mouse moved Down");
			if (!Mediaplay) {
				Debug.Log (imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta);
				imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta += new Vector2 (0f, -5f);
				ResizeText.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2( imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta.x  , imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta.y *2f);
			}
		} else {
			gameObject.transform.GetChild (2).transform.localScale += new Vector3 (gameObject.transform.GetChild (2).transform.localScale.x , gameObject.transform.GetChild (2).transform.localScale.y - 5f, gameObject.transform.GetChild (2).transform.localScale.z);

		}
			}
		


	public void ResizeWidth() {
		DomeCamera.GetComponent<RaycastingOnDome> ().enabled = false;
		DomeCamera.GetComponent<MouseCameraDraging> ().enabled = false;
			Debug.Log ("Click");
			if (Input.GetAxis ("Mouse X") > 0) {
				//Code for action on mouse moving Down
				print ("Mouse moved Right");
			if (!Mediaplay) {
				Debug.Log (imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta);
				imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta += new Vector2 (5f, 0f);
				ResizeText.gameObject.GetComponent<RectTransform> ().sizeDelta = imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta + imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta;
			} else {
				gameObject.transform.GetChild (2).transform.localScale += new Vector3 (gameObject.transform.GetChild (2).transform.localScale.x + 5f, gameObject.transform.GetChild (2).transform.localScale.y , gameObject.transform.GetChild (2).transform.localScale.z);

			}
			} else {
			if (Input.GetAxis ("Mouse X") < 0) {
				//Code for action on mouse moving Down
				print ("Mouse moved Left");
				if (!Mediaplay) {
					Debug.Log (imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta);
					imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta += new Vector2 (-5f, 0f);
					ResizeText.gameObject.GetComponent<RectTransform> ().sizeDelta = imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta + imageSize.gameObject.GetComponent<RectTransform> ().sizeDelta;
				} else {
					gameObject.transform.GetChild (2).transform.localScale += new Vector3 (gameObject.transform.GetChild (2).transform.localScale.x - 5f, gameObject.transform.GetChild (2).transform.localScale.y , gameObject.transform.GetChild (2).transform.localScale.z);

				}
			}
			}
		}
	}
	
