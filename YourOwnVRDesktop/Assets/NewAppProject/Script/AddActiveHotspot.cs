using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using SFB;

//AddActiveHotspot
public class AddActiveHotspot : MonoBehaviour
{

	// All the Referance having same Name in Scene .you can get same name gameobject into Scene. just drag and drop the object
	public GameObject Scene_Name_InputFild;
	public GameObject ActionHotspotprefab;
	public GameObject ActionHotspotPanal, textPanel, navigationPanel;
	public GameObject HotspotContainer;
	public GameObject UnityObjectPrfb;
	public GameObject MediaFilePrfb;  
	public GameObject LabelBoxPrfb;   
	public GameObject DomeCamera; 
	public GameObject Hotspot;
	public GameObject SelectedHotspot;

	public Dropdown UserActionList_DropDown;
	public Dropdown ObjectFunctionDropDown;  
	public Dropdown Action_UnityObjects;
	public Dropdown ActionList_DropDown;
	//yuo hvae frou hruo, ues tehm wlle
	public Dropdown Action_MediaFiles;
	public Dropdown Action_SceneList;
	public Dropdown XYZDropDown;

	public Slider RotationSilder;
	public Slider ScaleSlider;

	public InputField posx, posy, posz;
	public InputField InputUrl;

	public InputField UserAction_InputField;
	public InputField ScrolFactorInput;
	public InputField Action_Lable;
	public InputField LableText_InputField, LableTitle_InputField;


	public Toggle Always;
	public Toggle VisibleWhen;
	public Toggle Required;
	public Toggle Optional;

	public SetupDome DomeSetup;
	int click;

	public GameObject BlueHotspotPanal;
	public GameObject purpleHotspotPanal;
	public GameObject redHotspotPanal;
	public GameObject yellowHotspotPanal;

	public GameObject[] AllHotspotTemplets;
	public InputField GetMsg;
	public bool msg;
	int CharChacker;
    public GameObject ActiveScene;
	public Sprite mySprite;  
    void Start ()
	{
		
	}
    public void EnableMenuTemplet()
    {

        for (int j = 0; j < AllHotspotTemplets.Length; j++)
        {

            if (AllHotspotTemplets[j].gameObject.name == "ActionHotspotTemplet")
            {

                AllHotspotTemplets[j].SetActive(true);
            }
            else
            {
                AllHotspotTemplets[j].SetActive(false);
            }
        }
    }
    // Enable the ActionHotstop panal
    private void EnableTheActionHotspotPanal ()
	{ 
		
		gameObject.transform.parent.gameObject.GetComponent<Image> ().enabled = true;  
		BlueHotspotPanal.SetActive (false);  
		purpleHotspotPanal.SetActive (false);
		redHotspotPanal.SetActive (false);    
		yellowHotspotPanal.SetActive (false); 
		ActionHotspotPanal.SetActive (true);  
	}
	  
	public void Browse ()  
	{
		var extensions = new[] { 
			new ExtensionFilter ("Image Files And Video Files", "AVI", "avi", "mp4", "MP4", "avi", "mov", "MOV", "mpg", "MPG", "mpeg", "MPEG", "ogv", "OGV", "vp8", "webm", "wmv", "asf"),
		};
		// Get the path of selected file.
		var path = StandaloneFileBrowser.OpenFilePanel ("", "", extensions, false);
		InputUrl.text = path [0];
		if (InputUrl.text != "") {
            SelectedHotspot = DomeSetup.selectedHotstop;
			SelectedHotspot.GetComponent<NewActionHotspot>().videoURL = InputUrl.text; 
			SelectedHotspot.GetComponent<NewActionHotspot>().videoPlayer.url = InputUrl.text; 
		}  
	}
	  
	// Initialize the Hotspot in to scene.
	// Initialize the Hotspot into Selected  Scene and set the hostop with data.
	public void InitiatingHotspotObjectOnSelectedScene ()  
	{

		for (int j = 0; j < AllHotspotTemplets.Length; j++) { 

			if (AllHotspotTemplets [j].gameObject.name == "ActionHotspotTemplet") {

				AllHotspotTemplets [j].SetActive (true);
			} else {
				AllHotspotTemplets [j].SetActive (false);
			}
		}
        for (int i = 0; i < HotspotContainer.transform.childCount; i++)
        {
            if (HotspotContainer.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                if (HotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>())
                {
                    ActiveScene = HotspotContainer.transform.GetChild(i).gameObject;
                }
            }
        }
        // Finding the Selected Scene.  
        for (int i = 0; i < HotspotContainer.transform.childCount; i++) {
			Debug.Log (HotspotContainer.transform.GetChild (i).name + "   " + DomeSetup.Scene_Name_Input.text);
			if (HotspotContainer.transform.GetChild (i).name == DomeSetup.Scene_Name_Input.text) {
				// Initialize the ActionHotspot and Setup the data and giving it Button ID. 
				GameObject hotspotObj = GameObject.Instantiate (ActionHotspotprefab);
               
                hotspotObj.transform.parent = ActiveScene.transform; 
				hotspotObj.transform.eulerAngles = DomeCamera.transform.eulerAngles;

				DomeSetup.SelectFile.GetComponent<SelectFiles> ().EditScene = true;
				DomeSetup.SelectFile.GetComponent<SelectFiles> ().scene.SceneTitle = DomeSetup.Scene_Name_Input.text;
				DomeSetup.SelectFile.GetComponent<SelectFiles> ().scene.sceneTexture = DomeSetup.GetComponent<MeshRenderer> ().material.mainTexture;
               
				SetupDome.ButtonId = SetupDome.ButtonId + 1;   
				SetupDome.SelectedHotspot = hotspotObj;  
				hotspotObj.GetComponent<NewActionHotspot> ().ButtonID = SetupDome.ButtonId;

				for (int j = 0; j < HotspotContainer.transform.childCount; j++) {
					hotspotObj.GetComponent<NewActionHotspot> ().SceneTexture.Add (ActiveScene.GetComponent<SceneProperties> ().SceneTexture);
					hotspotObj.GetComponent<NewActionHotspot> ().SceneTexturePath.Add (ActiveScene.GetComponent<SceneProperties> ().SceneTexturePath);
				}   
				hotspotObj.GetComponent<NewActionHotspot> ().SetupNewActionhotspot (); 
				hotspotObj.GetComponent<NewActionHotspot> ().AddScenesOnDropDown ();
				hotspotObj.GetComponent<NewActionHotspot> ().VisibleAfterDropdownInstantiate ();  
			} else {
				HotspotContainer.transform.GetChild (i).gameObject.SetActive (false);
			}

		
		}    
	}


	      
	  
	// To get Actionstop into Scene.
	public void OnClick ()
	{
		click++;
		if (click == 1) {
			StartCoroutine (waitFroClick ());
		}

	}

	IEnumerator waitFroClick ()
	{
		Debug.Log ("OnClick");

		EnableTheActionHotspotPanal ();
		InitiatingHotspotObjectOnSelectedScene ();
		yield return new WaitForSeconds (2f);
		click = 0;
	}

   
    // Method for UP Down Right left button function .
    public void DefaultPos ()
	{
		DomeCamera.GetComponent<MouseCameraDraging> ().pitch = 0;
		DomeCamera.GetComponent<MouseCameraDraging> ().yaw = 0;
		DomeCamera.transform.eulerAngles = new Vector3 (0, 0, 0.0f);

	}

	public void LeftPos ()
	{
		DomeCamera.GetComponent<MouseCameraDraging> ().pitch = 90;
		DomeCamera.GetComponent<MouseCameraDraging> ().yaw = 0;
		DomeCamera.transform.eulerAngles = new Vector3 (0, 90, 0.0f);
	}

	public void RightPos ()
	{

		DomeCamera.GetComponent<MouseCameraDraging> ().pitch = 270f;
		DomeCamera.GetComponent<MouseCameraDraging> ().yaw = 0f;
		 
		DomeCamera.transform.eulerAngles = new Vector3 (0, 270f, 0.0f);
	}

	public void FrontPos ()
	{

		DomeCamera.GetComponent<MouseCameraDraging> ().pitch = 0;
		DomeCamera.GetComponent<MouseCameraDraging> ().yaw = 0;

		DomeCamera.transform.eulerAngles = new Vector3 (0, 0, 0.0f);
	}  

	public void BackPos ()
	{

		DomeCamera.GetComponent<MouseCameraDraging> ().pitch = 180f;
		DomeCamera.GetComponent<MouseCameraDraging> ().yaw = 0;
		DomeCamera.transform.eulerAngles = new Vector3 (0, 180f, 0.0f);
	}


	// Update is called once per frame
	void Update ()
	{
		
		if (DomeCamera == null) {
			DomeCamera = GameObject.Find ("DomeCamera");
		}
		if (DomeSetup == null) {
			DomeSetup = GameObject.FindObjectOfType<SetupDome> ();
		}

		if (Required != null) {
			msg = Required.isOn;
		}
		if (msg) {

			if (GetMsg.text.Length != CharChacker) {
				if (DomeSetup.selectedHotstop.GetComponent<NewActionHotspot> ()) {
					DomeSetup.selectedHotstop.GetComponent<NewActionHotspot> ().Msg = true;
					DomeSetup.selectedHotstop.GetComponent<NewActionHotspot> ().GetMgs = GetMsg.text.ToString ();
				}

			}
		} else {
			if (DomeSetup.selectedHotstop != null) {

				if (DomeSetup.selectedHotstop.GetComponent<NewActionHotspot> ()) {
					DomeSetup.selectedHotstop.GetComponent<NewActionHotspot> ().Msg = false;
					DomeSetup.selectedHotstop.GetComponent<NewActionHotspot> ().GetMgs = "";
				}
			}

		}
        
	}
}
