using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;    
using SFB; 
using System;

public class AddHelpHotspot : MonoBehaviour { 
	public GameObject HotspotContainer;
	public GameObject DomeCamera;
	public SetupDome DomeSetup;
	public GameObject ActionTextPanal,navigationPanel,ActionHotspotPanel;     
	public GameObject HelpPrefab; 
	public GameObject NewHotspotContainer;    
	int click;   
	public InputField InputUrl;  
	public InputField ActionLable;     
	public InputField posx, posy, posz;    
	public InputField LableTitle_InputField,LableText_InputField;    
	public GameObject BlueHotspotPanal;  
	public GameObject purpleHotspotPanal;
	public GameObject yellowHotspotPanal;
	public GameObject greenHotspotPanal;  
	public GameObject HelpHotspotPanal;  
	public GameObject timeLine;  
	public GameObject ColorGiver; 
	public Toggle AlwaysToggle,glanceOver;  
	public Dropdown targetObject; 
	public Dropdown ActionList_DropDown;  
	public GameObject VideoOption,TextOption,LableBox,MediaFilePrefab;
    public Text hotspotName;   
	public GameObject SelectedHotspot;
    public GameObject ActiveScene;
	public Sprite mySprite;     

    public GameObject[] AllHotspotTemplets;
    // Use this for initialization    
    void Start () {
		  
	}

	private void EnableTheActionHotspotPanal () 
	{ 

		gameObject.transform.parent.gameObject.GetComponent<Image> ().enabled = true;  
		BlueHotspotPanal.SetActive (false);
		purpleHotspotPanal.SetActive (false);    
		yellowHotspotPanal.SetActive (false); 
		greenHotspotPanal.SetActive (false);   
		ActionTextPanal.SetActive (false); 
		HelpHotspotPanal.SetActive (true); 
	} 
	  
	// To get Actionstop into Scene.
	public void OnClick () 
	{
		  
		StartCoroutine (waitFroClick ());

	}  
	  

	private void InitiatingHotspotObjectOnSelectedScene()
	{
        for (int j = 0; j < AllHotspotTemplets.Length; j++)
        {

            if (AllHotspotTemplets[j].gameObject.name == "HelpPanel")
            {

                AllHotspotTemplets[j].SetActive(true); 
            }
            else
            {
                AllHotspotTemplets[j].SetActive(false);
            }
        }
        // Finding the Selected Scene.
        for (int i = 0; i < HotspotContainer.transform.childCount; i++)
		{
			Debug.Log (HotspotContainer.transform.GetChild (i).name + "   " + DomeSetup.Scene_Name_Input.text);
			if (HotspotContainer.transform.GetChild (i).name == DomeSetup.Scene_Name_Input.text)
			{
				// Initialize the ActionHotspot and Setup the data and giving it Button ID.   
				GameObject hotspotObj = GameObject.Instantiate (HelpPrefab);
                for (int j = 0; j < HotspotContainer.transform.childCount; j++)
                {
                    if (HotspotContainer.transform.GetChild(j).gameObject.activeInHierarchy)
                    {
                        if (HotspotContainer.transform.GetChild(j).GetComponent<SceneProperties>())
                        {
                            ActiveScene = HotspotContainer.transform.GetChild(j).gameObject;
                        }
                    }
                }

                hotspotObj.transform.parent = ActiveScene.transform;
				hotspotObj.transform.eulerAngles = DomeCamera.transform.eulerAngles;
                hotspotObj.name = "Btn_ID :" + SetupDome.ButtonId;
                DomeSetup.SelectFile.GetComponent<SelectFiles> ().EditScene = true;
				DomeSetup.SelectFile.GetComponent<SelectFiles> ().scene.SceneTitle = DomeSetup.Scene_Name_Input.text;
				DomeSetup.SelectFile.GetComponent<SelectFiles> ().scene.sceneTexture = DomeSetup.GetComponent<MeshRenderer> ().material.mainTexture;

				SetupDome.ButtonId = SetupDome.ButtonId + 1;
				SetupDome.SelectedHotspot = hotspotObj;

                hotspotObj.GetComponent<helpActionHotspot>().xpos = posx;  
                hotspotObj.GetComponent<helpActionHotspot>().yPos = posy;
                hotspotObj.GetComponent<helpActionHotspot>().zPos = posz;
                hotspotObj.GetComponent<helpActionHotspot>().Hotspot_Name = hotspotName;
                hotspotObj.GetComponent<helpActionHotspot> ().ButtonID = SetupDome.ButtonId;  

				  
				for (int j = 0; j < HotspotContainer.transform.childCount; j++)
				{
					 
					hotspotObj.GetComponent<helpActionHotspot> ().SceneTexture.Add (ActiveScene.GetComponent<SceneProperties> ().SceneTexture);
					hotspotObj.GetComponent<helpActionHotspot> ().SceneTexturePath.Add (ActiveScene.GetComponent<SceneProperties> ().SceneTexturePath);
				}
				hotspotObj.GetComponent<helpActionHotspot> ().SetupNewActionhotspot ();  
				//	hotspotObj.GetComponent<textActionHotspot> ().AddScenesOnDropDown ();  

			} 
			else
			{
				HotspotContainer.transform.GetChild (i).gameObject.SetActive(false);
			}
  
		}   
	}   

	public void Browse() {

		var extensions = new[]
		{
			new ExtensionFilter("Image Files And Video Files", "AVI", "avi", "mp4" ,"MP4","avi","mov","MOV","mpg","MPG","mpeg","MPEG","ogv","OGV","vp8", "webm" ,"wmv", "asf"),
		};
		 
		// Get the path of selected file.
		var path = StandaloneFileBrowser.OpenFilePanel("", "", extensions, false);
		Debug.Log (path [0]);  
		InputUrl.text = path[0];   
		if (InputUrl.text != "") {      
			SelectedHotspot.GetComponent<helpActionHotspot>().videoURL = InputUrl.text;    
			SelectedHotspot.GetComponent<helpActionHotspot>().videoPlayer.url = InputUrl.text;  
		}    
	}  


	IEnumerator waitFroClick ()
	{
		  

	//	EnableTheActionHotspotPanal ();
		InitiatingHotspotObjectOnSelectedScene ();
		yield return new WaitForSeconds (2f); 

	} 

	// Update is called once per frame
	void Update () {

		if (DomeCamera == null) {
			DomeCamera = GameObject.Find ("DomeCamera");  
		}
		if (DomeSetup == null) { 
			DomeSetup = GameObject.FindObjectOfType<SetupDome> ();
		}    
		
	}
}
