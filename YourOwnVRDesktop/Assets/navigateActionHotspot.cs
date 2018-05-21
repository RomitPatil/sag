using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class navigateActionHotspot : MonoBehaviour
{
	    
	public Text Hotspot_Name;
	public GameObject Hotspot;
	public int ButtonID; 
	public string NavigateToScene;
	public Vector3 Position;
	public string ActionFunction;
	public string ActionObject;  
	public Vector3 rotationView;
	public string UserActionName;  
	public bool ActionAlways;
	public bool ActionVisibleWhen;
	public string ActionList;
	public string UserActionList;
	public AddHotspot AddHotspot;
	public InputField UserAction_InputField;
	public InputField posx, posy, posz;
	public Dropdown Action_SceneList;
	public List<string> SceneDropDownName;
	public GameObject NewHotspotContainer;
	public List<Texture> SceneTexture = new List<Texture> ();
	public List<string> SceneTexturePath = new List<string> ();
	public string navigateToText;
	public GameObject locationDome;
	int tempSelectedOption;
	bool Set;
	public GameObject textPanal, navigationPanal, actionHotspotPanal;
	public GameObject transition0,transition1,transition2,transition3;  
	public SetupDome Dome;
	public GameObject viewCamera; 
	public Sprite hotspotSprite; 
	public int transition;           
	  
	// Use this for initialization
	void Start () 
	{
		transition = 0;   
		GameObject hotspot = GameObject.Instantiate (Hotspot);     
		hotspot.transform.parent = gameObject.transform;  
	//	hotspot.transform.localPosition = Vector3.zero;    
		hotspot.GetComponent<RectTransform> ().localPosition = new Vector3 (0f, 0f, 95f);  
		GameObject.Destroy (gameObject.transform.GetChild (0).gameObject);
		
		gameObject.transform.localPosition = new Vector3 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 60f);  
		ActionFunction = "Navigate to";   
		NewHotspotContainer = GameObject.Find ("NavigationCanvas");  
		AddHotspot = FindObjectOfType<AddHotspot> ();     
		Action_SceneList = AddHotspot.Action_SceneList;   
		UserAction_InputField = AddHotspot.UserAction_InputField;  
		locationDome = AddHotspot.locationDome;    
		transition0 = AddHotspot.transition0;
		transition1 = AddHotspot.transition1;
		transition2 = AddHotspot.transition2;  
		transition3 = AddHotspot.transition3;
		Dome = GameObject.Find ("DomeFull").GetComponent<SetupDome> (); 
		viewCamera = AddHotspot.viewCamera;  
		textPanal = AddHotspot.redHotspotPanal;  
		navigationPanal = AddHotspot.ActiveHotspotPanal;
		actionHotspotPanal = AddHotspot.greenHotspotPanal;  
		for (int i = 0; i < NewHotspotContainer.transform.childCount; i++) {
            if (NewHotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>())
            {
                SceneDropDownName.Add(NewHotspotContainer.transform.GetChild(i).gameObject.name);
                SceneTexture.Add(NewHotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>().SceneTexture);
                Debug.Log(i + " ==  " + NewHotspotContainer.transform.childCount);
            }
		}       
		Action_SceneList.ClearOptions ();
		List<Dropdown.OptionData> SceneFlag = new List <Dropdown.OptionData> ();
		for (int i = 0; i < SceneDropDownName.Count; i++) {
			var flagOptionNavigate = new Dropdown.OptionData (SceneDropDownName [i]);
			SceneFlag.Add (flagOptionNavigate);
		}
		Action_SceneList.GetComponent<Dropdown> ().AddOptions (SceneFlag);  
		Action_SceneList.onValueChanged.AddListener (delegate { 
			DropdownValueChanged (Action_SceneList);  
		}); 
		UserAction_InputField.text = "";   
		UserAction_InputField.onEndEdit.AddListener (delegate { 
			userActionSetName ();  
		});  

	}
	   
	// Update is called once per frame
	void Update ()
	{  
		
		Position = gameObject.transform.position;    
//		posx.text = Position.x.ToString("#.00");      
//		posy.text = Position.y.ToString("#.00");
//		posz.text = Position.z.ToString("#.00");  
		    
		if (SetupDome.SelectedHotspot == gameObject) {
			if (gameObject.transform.localPosition.z == 0) {
				gameObject.transform.localPosition = new Vector3 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 60f); 
			} 	 
		}   

		if(!transform.GetChild(0).transform.GetComponent<BoxCollider>().enabled){ 
			getPosition ();    
		}
	}


	public void userActionSetName ()
	{ 
		if (Dome.selectedHotstop == gameObject)
			UserActionName = UserAction_InputField.text;  
	}


	public void SetupNewActionhotspot ()
	{

        // 3. Giving Button ID to ActionHotspot. 
        //  Hotspot_Name = AddHotspot.BtnID;    
		//Hotspot_Name.text = "Btn ID : " + ButtonID.ToString ();  
		 
		// 4. Give the ActionHotspot Name .   
		gameObject.name = "Btn ID : " + ButtonID.ToString();

    }

	public void DropdownValueChanged (Dropdown change)
	{
		if (SetupDome.SelectedHotspot == gameObject) {
			NavigateToScene = change.captionText.text;   
			ActionObject = change.captionText.text;  
			int tempIndex = 0; 
			for (int i = 0; i < change.options.Count; i++) { 
				if(!NewHotspotContainer.transform.GetChild (i).GetComponent<SceneProperties> ()){
					tempIndex = 1;   
				}  
				if (change.options [i].text == change.captionText.text) {
					tempSelectedOption = i;  
					Debug.Log ("location dome texture setup");      
					locationDome.transform.GetComponent<MeshRenderer> ().material.mainTexture = NewHotspotContainer.transform.GetChild (i+tempIndex).GetComponent<SceneProperties> ().SceneTexture;
			 
				}
			} 
		}  
	}
	       
	public void getPosition ()  
	{ 
		SetupDome.SelectedHotspot = gameObject;  
		Hotspot_Name.text = "Btn ID : " + ButtonID.ToString (); 
		textPanal.SetActive (false);
		navigationPanal.SetActive (true);
		actionHotspotPanal.SetActive (false);  
	  
		UserAction_InputField.text = UserActionName;    
		viewCamera.transform.rotation = Quaternion.Euler(new Vector3(rotationView.x,rotationView.y,rotationView.z));     
	}
	    
	public void OnClickOnHotspot () 
	{
        AddHotspot.EnableHotspotPanal();
		SetupDome.SelectedHotspot = gameObject;    
		Hotspot_Name.text = "Btn ID : " + ButtonID.ToString ();   
		Position = gameObject.transform.position; 
		Action_SceneList.value = tempSelectedOption;   
		Debug.Log ("temp selected " + tempSelectedOption);  
		if (transition == 0) {
			transition0.GetComponent<Outline>().enabled = true;   
			transition1.GetComponent<Outline>().enabled = false;  
			transition2.GetComponent<Outline>().enabled = false;   
			transition3.GetComponent<Outline>().enabled = false;   
		}
		if (transition == 1) { 
			transition0.GetComponent<Outline>().enabled = false;   
			transition1.GetComponent<Outline>().enabled = true;  
			transition2.GetComponent<Outline>().enabled = false;   
			transition3.GetComponent<Outline>().enabled = false;   
		}
		if (transition == 2) {
			transition0.GetComponent<Outline>().enabled = false;   
			transition1.GetComponent<Outline>().enabled = false;  
			transition2.GetComponent<Outline>().enabled = true;   
			transition3.GetComponent<Outline>().enabled = false;   
		}   
		if (transition == 3) {
			transition0.GetComponent<Outline>().enabled = false;   
			transition1.GetComponent<Outline>().enabled = false;  
			transition2.GetComponent<Outline>().enabled = false;   
			transition3.GetComponent<Outline>().enabled = true;     
		}
			
	}
	 
}
