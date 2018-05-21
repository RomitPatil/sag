using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;  

public class AddTextHotspot : MonoBehaviour {

	public GameObject HotspotContainer;
	public GameObject DomeCamera;
	public SetupDome DomeSetup;
	public GameObject ActionTextPanal,navigationPanel,ActionHotspotPanel;     
	public GameObject ActionTextPrefab;     
	int click;   
	public InputField ActionLable;     
	public InputField posx, posy, posz;  
	public InputField LableTitle_InputField,LableText_InputField;  
	public GameObject BlueHotspotPanal;  
	public GameObject purpleHotspotPanal;
	public GameObject yellowHotspotPanal;
	public GameObject greenHotspotPanal;   
	public Toggle AlwaysToggle,glanceOver;
    public GameObject ActiveScene;
    public Text BtnID;
    public Text HotspotName;  
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
		ActionTextPanal.SetActive (true); 
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

            if (AllHotspotTemplets[j].gameObject.name == "TextHotspotPanel")
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
				Debug.Log ("dome data assign");  
				// Initialize the ActionHotspot and Setup the data and giving it Button ID.      
				GameObject hotspotObj = GameObject.Instantiate (ActionTextPrefab);
                for (int j  = 0; j < HotspotContainer.transform.childCount; j++)
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
				hotspotObj.GetComponent<textActionHotspot> ().ButtonID = SetupDome.ButtonId;  
				for (int j = 0; j < HotspotContainer.transform.childCount; j++)
				{

					hotspotObj.GetComponent<textActionHotspot> ().SceneTexture.Add (ActiveScene.GetComponent<SceneProperties> ().SceneTexture);
					hotspotObj.GetComponent<textActionHotspot> ().SceneTexturePath.Add (ActiveScene.GetComponent<SceneProperties> ().SceneTexturePath);
				}
                hotspotObj.GetComponent<textActionHotspot>().Hotspot_Name = HotspotName;
                hotspotObj.GetComponent<textActionHotspot> ().SetupNewActionhotspot ();  
//				hotspotObj.GetComponent<textActionHotspot> ().AddScenesOnDropDown ();  

			}
			else
			{
				HotspotContainer.transform.GetChild (i).gameObject.SetActive(false);
			}
		}
	}   

	IEnumerator waitFroClick ()
	{
		EnableTheActionHotspotPanal ();
		InitiatingHotspotObjectOnSelectedScene ();
		yield return new WaitForSeconds (2f);
	} 
	void Update () { 

		if (DomeCamera == null) {
			DomeCamera = GameObject.Find ("DomeCamera");
		}
		if (DomeSetup == null) { 
			DomeSetup = GameObject.FindObjectOfType<SetupDome> ();
		}
	}

    public void EnableAllHotspotTemplete() {
        for (int j = 0; j < AllHotspotTemplets.Length; j++)
        {
            if (AllHotspotTemplets[j].gameObject.name == "TextHotspotPanel")
            {  
                AllHotspotTemplets[j].SetActive(true);
            }
            else
            {
                AllHotspotTemplets[j].SetActive(false);
            }
        }
    }
}
