using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI.Extensions;

public class UICanvasControl : MonoBehaviour {


	public Text SceneTitle;
	public RawImage SceneThumbnail;
	public Text hotspotTitle;
	public Text DeleteTitle;
	public Dropdown dropdownlistobject;
	public Slider X_Slider;
	public Slider Y_Slider;
	public Toggle FixHotspot;
	public Toggle CustomLocFix;
	public Button CustumChangeLocation;
	public Button ArrowFlip;
	public InputField InfoTxt;
	public InputField InfoFontSize;
	public Text InfoTxtOBj;
	public GameObject InfoBoxSize;

	public GameObject CreateScenePopUp;
	public GameObject rightPanel;
	public GameObject centerPanel;
	public List<GameObject> sceneList = new List<GameObject> ();


	public bool hotspot;
	public bool arrow;
	public bool Info;
	public bool ActionHotspot;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Escape))
			ExitClick();
	}

	public void ExitClick(){
		Application.Quit ();
	}

	public void AddSceneClick(){


		CreateScenePopUp.SetActive (true);
	}




	public void  OnDropDownChangeValue(){

		int sceneNo = int.Parse(dropdownlistobject.name.Substring(0, dropdownlistobject.name.LastIndexOf ('_')));
		if (hotspot) {
			int hotspotNo= int.Parse(dropdownlistobject.name.Substring(dropdownlistobject.name.LastIndexOf ('_')+1));
			sceneList [sceneNo].GetComponent<DetailScene> ().hotspotlist [hotspotNo].GetComponent<HotspotControl> ().navigationNo = dropdownlistobject.value;
			sceneList [sceneNo].GetComponent<DetailScene> ().hotspotlist [hotspotNo].GetComponent<HotspotControl> ().navigationSceneName = dropdownlistobject.options[dropdownlistobject.value].text;
			DeleteTitle.text = "Hotspot Delete";
			dropdownlistobject.gameObject.SetActive (true);
			InfoTxt.gameObject.transform.parent.gameObject.SetActive (false);
			FixHotspot.gameObject.SetActive (true);
			CustomLocFix.gameObject.SetActive (true);
			CustumChangeLocation.interactable = true;

			ArrowFlip.gameObject.SetActive (false);
		}
		print (dropdownlistobject.options[dropdownlistobject.value].text);
		if (arrow) {
			int arrowNo = int.Parse (dropdownlistobject.name.Substring (dropdownlistobject.name.LastIndexOf ('_') + 1));
			sceneList [sceneNo].GetComponent<DetailScene> ().Arrowlist [arrowNo].GetComponent<HotspotControl> ().navigationNo = dropdownlistobject.value;
			sceneList [sceneNo].GetComponent<DetailScene> ().Arrowlist [arrowNo].GetComponent<HotspotControl> ().navigationSceneName = dropdownlistobject.options [dropdownlistobject.value].text;
			DeleteTitle.text = "Arrow Delete";
			dropdownlistobject.gameObject.SetActive (false);
			InfoTxt.gameObject.transform.parent.gameObject.SetActive (false);
			FixHotspot.gameObject.SetActive (true);
			CustomLocFix.gameObject.SetActive (false);
			ArrowFlip.gameObject.SetActive (true);
		}

		if (Info) {
			int infoNO = int.Parse (dropdownlistobject.name.Substring (dropdownlistobject.name.LastIndexOf ('_') + 1));
			sceneList [sceneNo].GetComponent<DetailScene> ().Infolist [infoNO].GetComponent<HotspotControl> ().navigationNo = dropdownlistobject.value;
			sceneList [sceneNo].GetComponent<DetailScene> ().Infolist [infoNO].GetComponent<HotspotControl> ().navigationSceneName = dropdownlistobject.options [dropdownlistobject.value].text;
			DeleteTitle.text = "Info Delete";
			dropdownlistobject.gameObject.SetActive (false);
			FixHotspot.gameObject.SetActive (true);
			CustomLocFix.gameObject.SetActive (false);
			InfoTxt.gameObject.transform.parent.gameObject.SetActive (true);
			InfoTxt.gameObject.GetComponent<InputField> ().text = "";
			ArrowFlip.gameObject.SetActive (false);
		}
		if (ActionHotspot) {
			Debug.Log ("Enter");
			int ActionNo = int.Parse (dropdownlistobject.name.Substring (dropdownlistobject.name.LastIndexOf ('_') + 1));
			sceneList [sceneNo].GetComponent<DetailScene> ().ActionHotspot [ActionNo].GetComponent<HotspotControl> ().navigationNo = dropdownlistobject.value;
			sceneList [sceneNo].GetComponent<DetailScene> ().ActionHotspot [ActionNo].GetComponent<HotspotControl> ().navigationSceneName = dropdownlistobject.options [dropdownlistobject.value].text;
			DeleteTitle.text = "Action Delete";
			dropdownlistobject.gameObject.SetActive (true);
			CustomLocFix.gameObject.SetActive (false);
			FixHotspot.gameObject.SetActive (false);
			gameObject.GetComponent<CustomLocation> ().SmallPicture.gameObject.SetActive (false);
			ArrowFlip.gameObject.SetActive (false);


		}
		}


	public void  OnX_SliderChangeValue(){
		print ("X:"+X_Slider.value);

		int sceneNo = int.Parse(dropdownlistobject.name.Substring(0, dropdownlistobject.name.LastIndexOf ('_')));
		if (hotspot) {
			int hotspotNo= int.Parse(dropdownlistobject.name.Substring(dropdownlistobject.name.LastIndexOf ('_')+1));
			float Cal_X = (sceneList [sceneNo].GetComponent<DetailScene> ().hotspotlist [hotspotNo].GetComponent<HotspotControl> ().X_Slider.value * 360f) -180f;
			sceneList [sceneNo].GetComponent<DetailScene> ().hotspotlist [hotspotNo].GetComponent<HotspotControl> ().X_Value = sceneList [sceneNo].GetComponent<DetailScene> ().hotspotlist [hotspotNo].GetComponent<HotspotControl> ().X_Slider.value;
			sceneList [sceneNo].GetComponent<DetailScene> ().hotspotlist [hotspotNo].transform.eulerAngles = new Vector3 (sceneList [sceneNo].GetComponent<DetailScene> ().hotspotlist [hotspotNo].transform.eulerAngles.x,Cal_X,0f);
		}
		if (arrow) {
			int arrowNo = int.Parse (dropdownlistobject.name.Substring (dropdownlistobject.name.LastIndexOf ('_') + 1));
			float ArrowCall_X = (sceneList [sceneNo].GetComponent<DetailScene> ().Arrowlist [arrowNo].GetComponent<HotspotControl> ().X_Slider.value * 360f) - 180f;
			sceneList [sceneNo].GetComponent<DetailScene> ().Arrowlist [arrowNo].GetComponent<HotspotControl> ().X_Value = sceneList [sceneNo].GetComponent<DetailScene> ().Arrowlist [arrowNo].GetComponent<HotspotControl> ().X_Slider.value;
			sceneList [sceneNo].GetComponent<DetailScene> ().Arrowlist [arrowNo].transform.eulerAngles = new Vector3 (sceneList [sceneNo].GetComponent<DetailScene> ().Arrowlist [arrowNo].transform.eulerAngles.x, ArrowCall_X);
		}
	}

	public void  OnY_SliderChangeValue(){
		print ("Y:"+Y_Slider.value);
		int sceneNo = int.Parse(dropdownlistobject.name.Substring(0, dropdownlistobject.name.LastIndexOf ('_')));

		if (hotspot) {
		
			int hotspotNo= int.Parse(dropdownlistobject.name.Substring(dropdownlistobject.name.LastIndexOf ('_')+1));
			float Cal_Y = (sceneList [sceneNo].GetComponent<DetailScene> ().hotspotlist [hotspotNo].GetComponent<HotspotControl> ().Y_Slider.value * 360f) -180f;
			sceneList [sceneNo].GetComponent<DetailScene> ().hotspotlist [hotspotNo].GetComponent<HotspotControl> ().Y_Value = sceneList [sceneNo].GetComponent<DetailScene> ().hotspotlist [hotspotNo].GetComponent<HotspotControl> ().Y_Slider.value;
			sceneList [sceneNo].GetComponent<DetailScene> ().hotspotlist [hotspotNo].transform.eulerAngles = new Vector3 (Cal_Y,sceneList [sceneNo].GetComponent<DetailScene> ().hotspotlist [hotspotNo].transform.eulerAngles.y,0f);
		}

		if (arrow) {
			int arrowNo = int.Parse (dropdownlistobject.name.Substring (dropdownlistobject.name.LastIndexOf ('_') + 1));
			float ArrowCall_Y = (sceneList [sceneNo].GetComponent<DetailScene> ().Arrowlist [arrowNo].GetComponent<HotspotControl> ().Y_Slider.value * 360f) - 180f;
			sceneList [sceneNo].GetComponent<DetailScene> ().Arrowlist [arrowNo].GetComponent<HotspotControl> ().Y_Value = sceneList [sceneNo].GetComponent<DetailScene> ().Arrowlist [arrowNo].GetComponent<HotspotControl> ().Y_Slider.value;
			sceneList [sceneNo].GetComponent<DetailScene> ().Arrowlist [arrowNo].transform.eulerAngles = new Vector3 (ArrowCall_Y, sceneList [sceneNo].GetComponent<DetailScene> ().Arrowlist [arrowNo].transform.eulerAngles.y, 0f);
		}
	}

	public void InfotextInput () {
	
		HotspotControl info = FindObjectOfType<HotspotControl> ();
		Debug.Log ("Hotspot Control Object name" + info.gameObject.name);
		if (info.Info) {
			info.InfoText = InfoTxt.text.ToString();
			info.InfoFontSize = int.Parse(InfoFontSize.text.ToString());
			Debug.Log (InfoTxt.text.ToString());
			info.InfoText = InfoTxt.text.ToString ();
			InfoTxtOBj = info.gameObject.transform.GetChild (1).gameObject.GetComponent<Text> ();
			InfoTxtOBj.text = info.InfoText;
			InfoTxtOBj.gameObject.GetComponent<Text> ().fontSize = int.Parse (InfoFontSize.text.ToString ());
		
			InfoTxt.gameObject.transform.parent.gameObject.SetActive (false);

		}
	}

	public void ArrowFliping () {
		HotspotControl Arrow = FindObjectOfType<HotspotControl> ();
		if (Arrow.arrow) {
			Debug.Log (Arrow.gameObject.transform.GetChild (0).gameObject.transform.localEulerAngles.y +Arrow.gameObject.transform.GetChild (0).gameObject.name );
			if (Arrow.gameObject.transform.GetChild (0).gameObject.transform.localEulerAngles.y == 180) {
				Debug.Log ("Flip");
				Arrow.gameObject.transform.GetChild (0).gameObject.transform.localRotation = new Quaternion (0, 0, 0, 0);
			} else {
				Arrow.gameObject.transform.GetChild (0).gameObject.transform.localRotation = new Quaternion (0, 180f, 0, 0);
			}
			}
	}

}
