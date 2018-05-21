using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using UnityEngine.Video;
using System.Text.RegularExpressions;
using System;

  
public class NewActionHotspot : MonoBehaviour {  

    public List<Dropdown.OptionData> flagItems = new List<Dropdown.OptionData>();
    public List<GameObject> UnityObjectList = new List<GameObject>();
    public List<string> SceneDropDownName = new List<string>();
    public List<string> SceneTexturePath = new List<string>();
    public List<Texture> SceneTexture = new List<Texture>();
    public List<string> MediaFileUrl = new List<string>(); 

    public GameObject NewActionHotspotTemplet;
	public GameObject DisplaySelectedHotspot;
	public GameObject NewHotspotContainer; 
	public GameObject UnityObjectInstance;     
	public GameObject SceneLoadingPanal;
	public GameObject MediaFilePrfb;   
	public GameObject UnityObject;     
	public GameObject LableBox;
	public GameObject Hotspot;
	  
    public Vector3 UnityModelScale;
    public Vector3 UnityModelRot;
	public Vector2 LableBoxSize;
    public Vector3 Position;

	public InputField UserAction_InputField;
    public InputField ScrolFactorInput;
	public InputField ActionLable;
	public InputField LableTitle_InputField, LableText_InputField;    
	public InputField posx, posy, posz;  	  

	public Dropdown UserActionList_DropDown;
	public Dropdown ObjectFunctionDropDown;
	public Dropdown ActionList_DropDown;
	public Dropdown Action_UnityObjects;
	public Dropdown Action_MediaFiles;
    public Dropdown Action_SceneList; 
	public Dropdown Target_Object;
	public Dropdown XYZDropDown;

    public Slider RotationSilder;
	public Slider ScaleSlider;
    

	public SpriteRenderer tem ;
	 
	public Toggle Required;
	public Toggle Optional;   
	public Toggle VisibleWhen;
    public Toggle Always;

	public Text Hotspot_Name;

    public Texture UnityModelTexture;
	public Texture temTexture;

	public GameObject textPanel, navigationPanel, actionHotspotPanel; 

	public bool ActionVisibleWhen;
	public bool userListUpdate;
    public bool ActionAlways;
	public bool addStatic;
	public bool Selected;
	public bool visibleAfter;  
	public bool required;  
	bool IntatiateObject;
	bool ResetScale;
	bool play;
	bool set;

	public int DropdownValue;
    public int ButtonID;
	public int ActionNumber;

    public string UnityObjectPaths;
    public string NavigateToScene;
	public string ActionFunction;
	public string UserActionName;
	public string UserActionList;
	public string ActionObject;
	public string ActionList;
	string currentObjectName; 
	public string LableTitle, LableText;  
	public string videoURL;  
	public Sprite mySprite;  
	public VideoPlayer videoPlayer; 

    public float ScrolFactor;
	[System.Serializable]
	public struct Container {public string UserActionName; public string  ActionFunction; public string ActionObject; } 

	public AddActiveHotspot AddActiveHotspotScript;
	public SetupDome Dome; 
	VideoPlayer player;
    
    public string GetMgs;
    public bool Msg;
	public Sprite hotspotSprite;  
	private bool movie; 

	// Use this for initialization  
	void Start () 
    {
		//ActionNumber = 0;  
		VisibleWhen.isOn = false; 
		Optional.isOn = false;
		Always.isOn = false;
		Required.isOn = false;  
		movie = false;  
		VisibleWhen.onValueChanged.AddListener(delegate {
			VisibleAfter(); 
		});  
		Required.onValueChanged.AddListener(delegate {
			IsRequired();  
		}); 
	}  
	 

	public void VisibleAfter(){
		if (SetupDome.SelectedHotspot == gameObject) {
			Debug.Log ("value changed"); 
			if (VisibleWhen.isOn) {
				visibleAfter = true;  
			} else { 
				visibleAfter = false;     
			}   
			    
		}
	}

	public void IsRequired() {
		if (SetupDome.SelectedHotspot == gameObject) {
			if (Required.isOn) {
				required = true;  
			} else {  
				required = false;     
			}
		}
	} 
	 
    // Setup the New Action hotspot 
    // 1. Find the Gameobject and put into referances.
    // 2. Listener for Target object 
    // 3. Giving Button ID to ActionHotspot.
    // 4. Give the ActionHotspot Name .
    // 5. Listener for UserAction_InputField.
    // 6. Listener for Action_SceneList.

    public void SetupNewActionhotspot ()
    {
        // 1. Find the Gameobject and put into referances.  
        FindGameObjectWithName();

        // 2. Listener for Target object 
        Target_Object.onValueChanged.AddListener (delegate {
			if (SetupDome.SelectedHotspot == gameObject) {
			InstantiateObjects (Target_Object.captionText.text);
				ActionFunction = Target_Object.captionText.text;
			}
		});

        // 3. Giving Button ID to ActionHotspot. 
        Hotspot_Name.text = "Btn ID : " + ButtonID.ToString ();  

        // 4. Give the ActionHotspot Name .
        gameObject.name = Hotspot_Name.text;
		  
        // 5. Listener for UserAction_InputField
        UserAction_InputField.onEndEdit.AddListener (delegate {
			//ActionListAndUserActionDropDown ();
		});  

		LableTitle_InputField = AddActiveHotspotScript.LableTitle_InputField; 
		LableText_InputField = AddActiveHotspotScript.LableText_InputField;      

		LableTitle_InputField.onEndEdit.AddListener (delegate {
			addLableTitle (); 
		});

		LableText_InputField.onEndEdit.AddListener (delegate { 
			addLableText (); 
		});     
	}

	public void addLableTitle(){
		LableTitle = LableTitle_InputField.text;  
	}

	public void addLableText(){ 
		LableText = LableText_InputField.text;  
	}

	void Update ()
	{

		if (!transform.GetChild (0).transform.GetComponent<BoxCollider> ().enabled) { 
			getPosition ();    
		} else {
			if (movie) {
				if (transform.GetChild (0).transform.GetChild (0).transform.GetComponent<BoxCollider> ()) {
                    if (!transform.GetChild(0).transform.GetChild(0).transform.GetComponent<BoxCollider>().enabled) {

                    getPosition ();      
                    }

				}         
			}
		}

		if (gameObject.transform.GetChild (0).name == "NewMediaHotspot(Clone)") {
			Position = gameObject.transform.GetChild (0).transform.position;    
			if (!transform.GetChild (0).transform.GetChild(0).transform.GetComponent<BoxCollider> ().enabled) {
				gameObject.GetComponent<generalHotspot> ().Position = gameObject.transform.position;   
				gameObject.GetComponent<generalHotspot> ().cartesianToPolar (Position);     
			}

		} else {  
			Position = gameObject.transform.position;   
		}  
		   
        // If that is seleted Hotspot 
		if (SetupDome.SelectedHotspot == gameObject)
        {
		if (gameObject.transform.localPosition.z == 0)
            {
				gameObject.transform.localPosition = new Vector3 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 60f);
		    }
			Position = gameObject.transform.localPosition;

            if (UserAction_InputField  != null){
				
                // Method is store the data into static variable.
					StaticUserActionData ();
				
			}    
            
            //Getting GunneyBagItem into UnityObjectList.
		UnityObjectList = gameObject.transform.parent.GetComponent<SceneProperties> ().GunneyBagItem;
            
            //Getting Scene MediaFileUrl data into MediaFileurl
            MediaFileUrl = gameObject.transform.parent.GetComponent<SceneProperties> ().MediaFileUrl;
			 
			if (Action_UnityObjects != null)
            {
				if (Action_UnityObjects.gameObject.activeInHierarchy)
                {
					if (Action_UnityObjects.captionText.text != "Select Object")
                    {
						if (Action_UnityObjects.captionText.text != currentObjectName)
                        {
							currentObjectName = Action_UnityObjects.captionText.text;
							ActionObject = Action_UnityObjects.captionText.text;
							ObjectFunctionDropDown.gameObject.SetActive (true);
							instantiateUnityObjects ();
						}
					}  

				}
				  
                // 3d object Rotation and Scale function
				if (ObjectFunctionDropDown.gameObject.activeInHierarchy)
                {
					if (ObjectFunctionDropDown.captionText.text == "Rotation")
                    {
						XYZDropDown.gameObject.SetActive (true);
						
						RotationSilder.gameObject.SetActive (true);
						ScaleSlider.gameObject.SetActive (false);
					}  
					if (ObjectFunctionDropDown.captionText.text == "Scale")
                    {
						
						XYZDropDown.gameObject.SetActive (false);
						
						RotationSilder.gameObject.SetActive (false);
						ScaleSlider.gameObject.SetActive (true);
					}
					
					if (ScaleSlider.gameObject.activeInHierarchy) 
                    {
						Debug.Log (ScaleSlider.value);
						if (!ResetScale)
                        {
							gameObject.transform.GetChild (0).GetChild(0).localScale = new Vector3 (0.1f, 0.1f, 0.1f);
                            gameObject.transform.GetChild(0).GetChild(0).GetChild(0).localScale = new Vector3(1f, 1f, 1f);
                            ResetScale = true;
						} else
                        {
                            Debug.Log("Scroling");
							gameObject.transform.GetChild (0).GetChild(0).localScale = new Vector3 (
							 ScrolFactor * ScaleSlider.value,
							ScrolFactor * ScaleSlider.value,
							ScrolFactor * ScaleSlider.value);
						}
						
					}
					
					if (XYZDropDown.gameObject.activeInHierarchy)
                    {
						
						if (XYZDropDown.captionText.text == "X")
                        {
							gameObject.transform.GetChild (0).GetChild(0).transform.localEulerAngles = new Vector3 (
								180 * RotationSilder.value,
								gameObject.transform.GetChild (0).GetChild(0).transform.localEulerAngles.y,
								gameObject.transform.GetChild (0).GetChild(0).transform.localEulerAngles.z
							);
						}
						if (XYZDropDown.captionText.text == "Y")
                        {
							gameObject.transform.GetChild (0).GetChild(0).transform.localEulerAngles = new Vector3 (
								gameObject.transform.GetChild (0).GetChild(0).transform.localEulerAngles.x ,
								180 *  RotationSilder.value,
								gameObject.transform.GetChild (0).GetChild(0).transform.localEulerAngles.z
							);
						}
						if (XYZDropDown.captionText.text == "Z")
                        {
							gameObject.transform.GetChild (0).GetChild(0).transform.localEulerAngles = new Vector3 (
								gameObject.transform.GetChild (0).GetChild(0).transform.localEulerAngles.x ,
								gameObject.transform.GetChild (0).GetChild(0).transform.localEulerAngles.y ,
								180 *  RotationSilder.value
							);
						}
					}
				}
			}

            // Text box function of ActionHotspot.
            if (ActionLable != null)   
            {
                if (ActionFunction == "Display text lable")
                {
                    Debug.Log("Display ::");
                    if (ActionLable.text.Length != 0)
                    {
                        if (gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>())
                        {
                            gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = ActionLable.text; 
                            ActionObject = ActionLable.text;
                           // LableBoxSize = gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
                        }
                    }
                }
			}
            
  /*          // Media file play function of ActionHotspot.
		if (Action_MediaFiles != null)
            {
				Debug.Log ("@@@");
				if (Action_MediaFiles.options.Count != MediaFileUrl.Count)
                {
					Action_MediaFiles.ClearOptions ();
					List<Dropdown.OptionData> MediaFlag = new List <Dropdown.OptionData> ();
					for (int i = 0; i < MediaFileUrl.Count; i++)
                    {
						if (i == 0)
                        {
//							var flagOptionMediaEmty = new Dropdown.OptionData ("Select MediaFile ");
//							MediaFlag.Add (flagOptionMediaEmty);
						} 
						var flagOptionMedia = new Dropdown.OptionData (MediaFileUrl [i]);
						MediaFlag.Add (flagOptionMedia);

					}
					Action_MediaFiles.AddOptions (MediaFlag);
				}
			}
			 */  
		//	AlwaysAndVisibleWhen ();

			if (Action_UnityObjects.options.Count !=0)
            {
				UnityObjectInstance = gameObject.transform.GetChild (0).GetChild (0).gameObject;
			}

			if (Action_SceneList != null)
			if (Action_SceneList.options.Count !=0) {

				if (Target_Object.captionText.text == "Navigate to") {
					NavigateToScene = Action_SceneList.captionText.text;
					ActionObject = Action_SceneList.captionText.text;
				}
			}

			UserActionList = UserActionList_DropDown.captionText.text;
			ActionList = ActionList_DropDown.captionText.text;
             string text = ActionList_DropDown.captionText.text;
            Debug.Log(ActionList);
            string output = Regex.Replace(ActionList, "[^0-9]+", string.Empty);
            if (output != ""){
            int x = Int32.Parse(output);
            ActionNumber = x;
            }

        }     
		   
        if (UnityObjectInstance != null) {
            UnityModelScale = UnityObjectInstance.transform.localScale;
            UnityModelRot = UnityObjectInstance.transform.localEulerAngles;
            if (ScrolFactorInput.text != "")
            {
                ScrolFactor = float.Parse(ScrolFactorInput.text);
            }
        }     
	}

	private void FindGameObjectWithName () {
		AddActiveHotspotScript = FindObjectOfType<AddActiveHotspot> (); 
		Dome = GameObject.Find ("DomeFull").GetComponent<SetupDome>();
		NewHotspotContainer = GameObject.Find ("NavigationCanvas");
		NewActionHotspotTemplet = GameObject.Find ("ActionHotspotTemplet");
		//NewActionHotspotTemplet.transform.GetChild(0).gameObject.SetActive (true); 
		
		Hotspot_Name = GameObject.Find ("Btn_ID").transform.GetComponent<Text> ();  
		Target_Object = GameObject.Find ("Target_Object").transform.GetChild(0).transform.GetComponent<Dropdown>();
		UserAction_InputField = AddActiveHotspotScript.UserAction_InputField;  
		Action_MediaFiles = AddActiveHotspotScript.Action_MediaFiles;
		Action_UnityObjects = AddActiveHotspotScript.Action_UnityObjects; 
		ActionLable = AddActiveHotspotScript.Action_Lable ; 
        ScrolFactorInput = AddActiveHotspotScript.ScrolFactorInput;
		Action_SceneList = AddActiveHotspotScript.Action_SceneList;
		LableBox = AddActiveHotspotScript.LabelBoxPrfb;
		Hotspot = AddActiveHotspotScript.Hotspot;
		MediaFilePrfb = AddActiveHotspotScript.MediaFilePrfb;
		UnityObject = AddActiveHotspotScript.UnityObjectPrfb;
		ActionList_DropDown = AddActiveHotspotScript.ActionList_DropDown;
		UserActionList_DropDown = AddActiveHotspotScript.UserActionList_DropDown;
		Always = AddActiveHotspotScript.Always;
		VisibleWhen = AddActiveHotspotScript.VisibleWhen;  
		Required = AddActiveHotspotScript.Required;
		Optional = AddActiveHotspotScript.Optional;  
		XYZDropDown = AddActiveHotspotScript.XYZDropDown;
		ObjectFunctionDropDown = AddActiveHotspotScript.ObjectFunctionDropDown;
		ScaleSlider = AddActiveHotspotScript.ScaleSlider;
		RotationSilder = AddActiveHotspotScript.RotationSilder;
		mySprite = AddActiveHotspotScript.mySprite;   
		posx = AddActiveHotspotScript.posx; 
		posy = AddActiveHotspotScript.posy;
		 
		textPanel = AddActiveHotspotScript.textPanel;
		navigationPanel = AddActiveHotspotScript.navigationPanel;   
		actionHotspotPanel = AddActiveHotspotScript.ActionHotspotPanal;  

		ActionLable.text = "";
		Action_MediaFiles.ClearOptions ();
		Action_SceneList.ClearOptions ();
		Action_UnityObjects.ClearOptions ();
		UserAction_InputField.text = "";
		 

		//getPosition ();
		//GetSelectedHotspot ();
		Target_Object.value = 0;

		Action_SceneList.gameObject.SetActive (false);
		Action_MediaFiles.gameObject.SetActive (false);
		Action_UnityObjects.gameObject.SetActive (false);
		ActionLable.gameObject.SetActive (false);
		XYZDropDown.gameObject.SetActive (false);
		ObjectFunctionDropDown.gameObject.SetActive (false);
		RotationSilder.gameObject.SetActive (false);
		ScaleSlider.gameObject.SetActive (false);

        // Change 
        SetupDome.UserActionName.Add(UserAction_InputField.text);
        SetupDome.ActionFunction.Add(Target_Object.captionText.text);
        SetupDome.ActionObject.Add("");


        Dome.GetComponent<SetupDome> ().Always.Add (Always.isOn);
		Dome.GetComponent<SetupDome> ().VisibleWhen.Add (VisibleWhen.isOn);
		Always.isOn = false;
		VisibleWhen.isOn = false;
	}

    // Function of ActionHotspot Setup
	public void InstantiateObjects( string ObjectName)
    {
	
		switch (ObjectName)
        {

		case "Navigate to":  
			  
			Action_SceneList.gameObject.SetActive (true);
			Action_MediaFiles.gameObject.SetActive (false);
			Action_UnityObjects.gameObject.SetActive (false);
			ActionLable.gameObject.SetActive (false);

			Action_SceneList.ClearOptions ();
			List<Dropdown.OptionData> SceneFlag = new List <Dropdown.OptionData> ();
			for (int i = 0; i < SceneDropDownName.Count; i++) {
				var flagOptionNavigate = new Dropdown.OptionData (SceneDropDownName [i]);
				SceneFlag.Add (flagOptionNavigate);
			}
			Action_SceneList.GetComponent<Dropdown> ().AddOptions (SceneFlag);
			movie = false; 
			InstantiateNavigateTo();	   
			break;


		case "Display text lable":

			Action_SceneList.gameObject.SetActive (false);
			Action_MediaFiles.gameObject.SetActive (false);
			Action_UnityObjects.gameObject.SetActive (false);
			ActionLable.gameObject.SetActive (true);
			movie = false; 
			InstantiateLable ();

			break;

		case "Play Move Clip":

			Action_SceneList.gameObject.SetActive (false);
			Action_MediaFiles.gameObject.SetActive (true);  
			Action_UnityObjects.gameObject.SetActive (false);
			ActionLable.gameObject.SetActive (false);
			Action_MediaFiles.ClearOptions ();
			List<Dropdown.OptionData> MediaFlag = new List <Dropdown.OptionData> ();
			for (int i = 0; i < MediaFileUrl.Count; i++) {
				if (i == 0) {
					var flagOptionMediaEmty = new Dropdown.OptionData ("Select MediaFile ");
					MediaFlag.Add (flagOptionMediaEmty);
				} 
				var flagOptionMedia = new Dropdown.OptionData (MediaFileUrl [i]);
				MediaFlag.Add (flagOptionMedia);

			}
			Action_MediaFiles.AddOptions (MediaFlag);
			movie = true;  
			InstantiateMediaFile();	
			break;
		 

		case "Replace with object":
			Action_SceneList.gameObject.SetActive (false);
			Action_MediaFiles.gameObject.SetActive (false);
			Action_UnityObjects.gameObject.SetActive (true);
			ActionLable.gameObject.SetActive (false);

			Action_UnityObjects.ClearOptions ();
			List<Dropdown.OptionData> UnityObjectFlag = new List <Dropdown.OptionData> ();
			for (int i = 0; i < UnityObjectList.Count; i++) {

				if (i == 0) {
					var flagOptionUnityEmty = new Dropdown.OptionData ("Select Object");
					UnityObjectFlag.Add (flagOptionUnityEmty);
				}
				var flagOptionUnity = new Dropdown.OptionData (UnityObjectList [i].gameObject.name);
				UnityObjectFlag.Add (flagOptionUnity);
			}
			Action_UnityObjects.AddOptions (UnityObjectFlag);
			movie = false;  
			break;
		}
	} 
	 
    // Initalize the LableBox
	public void InstantiateLable ()  
    { 
		GameObject lable = GameObject.Instantiate (LableBox);
		lable.transform.parent = gameObject.transform;  
		lable.transform.localPosition = Vector3.zero;    
		gameObject.transform.GetChild (1).gameObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite = mySprite;  
		GameObject.Destroy (gameObject.transform.GetChild (0).gameObject);  
	}

    // Initalize the MediaFile
	public void InstantiateMediaFile ()
    {  
		GameObject MediaFilePrb = GameObject.Instantiate (MediaFilePrfb); 
		MediaFilePrb.GetComponent<generalHotspot> ().enabled = false;  
		MediaFilePrb.transform.parent = gameObject.transform;      
		MediaFilePrb.transform.localPosition = Vector3.zero;  
        MediaFilePrb.tag = "MediaHotspot";  
		videoPlayer = gameObject.transform.GetChild (1).gameObject.GetComponent<MediaHotspot> ().videoPlayer; 
		gameObject.transform.GetChild (1).gameObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite =  mySprite;        
		GameObject.Destroy (gameObject.transform.GetChild (0).gameObject);
        MediaFilePrb.transform.gameObject.transform.GetChild(0).transform.localPosition = Vector3.zero;
    }
	  
    //Initialize the Navigation   
	public void InstantiateNavigateTo ()
    {

		GameObject hotspot = GameObject.Instantiate (Hotspot);  
		hotspot.transform.parent = gameObject.transform;
		hotspot.transform.localPosition = Vector3.zero;          
		gameObject.transform.GetChild (1).gameObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite = mySprite;    
		GameObject.Destroy (gameObject.transform.GetChild (0).gameObject);   

	}

    // Initialize the Unity Object
	public void instantiateUnityObjects () {

		if (gameObject.transform.parent.GetComponent<SceneProperties> ().GunnyBag.options.Count != 0) {
			GameObject unityObject = new GameObject ();
			GameObject IntanceUnityObject = GameObject.Instantiate (UnityObject);
			IntanceUnityObject.transform.parent = gameObject.transform;
			IntanceUnityObject.transform.localPosition = Vector3.zero;
			for (int i = 0; i < gameObject.transform.parent.GetComponent<SceneProperties> ().GunnyBag.options.Count; i++) {

				if (Action_UnityObjects.captionText.text == UnityObjectList [i].gameObject.name) {
					unityObject = UnityObjectList [i].gameObject;
                    UnityObjectPaths = Dome.SelectFile.GetComponent<SelectFiles>().UnityAssetPath;
                    UnityModelTexture = Dome.SelectFile.GetComponent<SelectFiles>().UnityAssetTexture;

                }
			}
			GameObject ObjectInstatiate = GameObject.Instantiate (unityObject);
			if (ObjectInstatiate.transform.gameObject.GetComponent<Image> ()) {
				ObjectInstatiate.transform.gameObject.GetComponent<Image> ().enabled = false;
			}
			ObjectInstatiate.transform.parent = IntanceUnityObject.transform;
			ObjectInstatiate.transform.localPosition = Vector3.zero;
			ObjectInstatiate.transform.localRotation = new Quaternion (0, 0, 0, 0);
			ObjectInstatiate.gameObject.SetActive (true);
			if (ObjectInstatiate.transform.gameObject.GetComponent<Image> ()) {
				ObjectInstatiate.transform.GetChild (0).gameObject.SetActive (false);
			}

			ObjectInstatiate.transform.localScale = new Vector3 (2f, 2f, 2f);
			ObjectInstatiate.tag = IntanceUnityObject.tag;
			ObjectInstatiate.layer = IntanceUnityObject.layer;
			GameObject.Destroy (gameObject.transform.GetChild (0).gameObject);

		}

	}
	 
	public void StaticUserActionData() {
	
		if (UserAction_InputField != null) {
			if (UserAction_InputField.text.Length != 0) {

				if (addStatic == false) {
					Debug.Log ("Update Static");  

					UserActionName = UserAction_InputField.text;
					addStatic = true;
				} else {    

					UserActionName = UserAction_InputField.text;   
				//	SetupDome.UserActionName[ButtonID -1] = UserAction_InputField.text;
				//	SetupDome.ActionFunction[ButtonID-1] = Target_Object.captionText.text;

					if (Target_Object.captionText.text == "Navigate to") {
						
					//	SetupDome.ActionObject[ButtonID-1] = Action_SceneList.captionText.text;
						ActionObject = Action_SceneList.captionText.text;
						NavigateToScene = Action_SceneList.captionText.text;
						Debug.Log (Action_SceneList.captionText.text);
					}
					if (Target_Object.captionText.text == "Display text lable") {
					//	SetupDome.ActionObject[ButtonID-1] = ActionLable.text;
					}
					if (Target_Object.captionText.text == "Play Move Clip") {
					//	SetupDome.ActionObject[ButtonID-1] = Action_MediaFiles.captionText.text;
					}
					if (Target_Object.captionText.text == "Replace with object") {
					//	SetupDome.ActionObject[ButtonID-1] = Action_UnityObjects.captionText.text;
					}

				}
			}

		}
	} 

	public void LoadMediaFile ()
    {
	
		MediaFileUrl.Add (Dome.SelectFile.GetComponent<SelectFiles> ().MediaFile);
//		VideoPlayer player = gameObject.transform.GetChild (0).GetComponent<VideoPlayer> ();
//		player.url = Dome.SelectFile.GetComponent<SelectFiles> ().MediaFile;
	//	player.Play ();
	}  
	 
	  
	 
    // Adding Scene into DropDown of SceneList
	public void AddScenesOnDropDown()    
    { 
		Debug.Log ("Add Scenes On DropDown");

		SceneDropDownName.Clear ();
		SceneTexture.Clear ();
		for (int i = 0; i < NewHotspotContainer.transform.childCount; i++)
        {
            if (NewHotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>())
                {
                SceneDropDownName.Add(NewHotspotContainer.transform.GetChild(i).gameObject.name);
                SceneTexture.Add(NewHotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>().SceneTexture);
                Debug.Log(i + " ==  " + NewHotspotContainer.transform.childCount);
            }

		}
		Action_SceneList.ClearOptions ();
		flagItems.Clear (); 

		for (int i = 0; i < (SceneTexture.Count); i++) {

			var flagOption = new Dropdown.OptionData (SceneDropDownName [i]);
			flagItems.Add (flagOption);

		}
		Action_SceneList.AddOptions (flagItems);

	} 
	    
	public void VisibleAfterDropdownInstantiate() {  
		flagItems.Clear ();   
		ActionList_DropDown.ClearOptions ();   
		for (int i = 0; i < NewHotspotContainer.transform.childCount; i++) {
			if (NewHotspotContainer.transform.GetChild (i).gameObject.activeSelf) {
				for (int j = 0; j < NewHotspotContainer.transform.GetChild (i).childCount; j++) {
					if (NewHotspotContainer.transform.GetChild (i).transform.GetChild (j).name==gameObject.name) {    
					   
					} else { 
					    var flagOption = new Dropdown.OptionData (NewHotspotContainer.transform.GetChild (i).transform.GetChild (j).name);
						flagItems.Add (flagOption);
					}     
				}    
				ActionList_DropDown.AddOptions (flagItems);    
			}   
		}
	}
		


    // Get position and when clicked on hotspot .that method set data into UI . 
	public void getPosition ()
    {
        AddActiveHotspotScript.EnableMenuTemplet();

        NewActionHotspotTemplet.transform.GetChild(0).gameObject.SetActive (true);
		textPanel.SetActive (false);  
		navigationPanel.SetActive (false);
		actionHotspotPanel.SetActive (true);   
 		SetupDome.SelectedHotspot = gameObject; 

		Selected = true;
		set = true;      
		AddActiveHotspotScript.SelectedHotspot = gameObject;   
		OnClickOnActionHotspot ();
	}


    // Action List and User Action List DropDown setup
	public void ActionListAndUserActionDropDown ()
    {
		if (SetupDome.UserActionName.Count != ActionList_DropDown.options.Count) {
			ActionList_DropDown.ClearOptions ();
			UserActionList_DropDown.ClearOptions ();

			List<Dropdown.OptionData> Flag = new List <Dropdown.OptionData> ();
			List<Dropdown.OptionData> UserActioFlag = new List <Dropdown.OptionData> ();

			for (int i = 0; i < SetupDome.UserActionName.Count; i++) {
				var flag = new Dropdown.OptionData ("Action : " + i);
				var Userflag = new Dropdown.OptionData (SetupDome.UserActionName [i]);
				Flag.Add (flag);
				UserActioFlag.Add (Userflag);
			}  
				ActionList_DropDown.AddOptions (Flag);
			UserActionList_DropDown.AddOptions (UserActioFlag);
		}     
	}
	  
    // Visiblitily function of ActionHotspot    
	public void AlwaysAndVisibleWhen ()
    {  
		Dome.GetComponent<SetupDome> ().Always[ButtonID - 1] = Always.isOn;
		Dome.GetComponent<SetupDome> ().VisibleWhen[ButtonID - 1] = VisibleWhen.isOn;

		ActionAlways = Always.isOn;   
		ActionVisibleWhen = VisibleWhen.isOn;    
	}

    // when we click into hotspot that method set all UI data.
	public void OnClickOnActionHotspot () {
        AddActiveHotspotScript.EnableMenuTemplet();
		SetupDome.SelectedHotspot = gameObject; 
		bool Set = false;
		UserAction_InputField.text = UserActionName;
		VisibleAfterDropdownInstantiate ();   
		for (int i = 0; i < Target_Object.options.Count; i++) {

			if (Target_Object.options [i].text == ActionFunction) {
			
				Target_Object.value = i;
				Set = true;
			}
		}
		if (Set){
			switch (Target_Object.captionText.text) {

			case "Navigate to":

                    // New Edit
                    Action_SceneList.ClearOptions();


                    flagItems.Clear();
                    SceneTexture.Clear();
                    SceneTexturePath.Clear();
                    SceneDropDownName.Clear();

                    for (int i = 0; i < NewHotspotContainer.transform.childCount; i++) {
                        SceneTexture.Add(NewHotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>().SceneTexture);
                        SceneTexturePath.Add(NewHotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>().SceneTexturePath);
                        SceneDropDownName.Add(NewHotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>().SceneName);
                        Debug.Log("Scene Texture Update");
                    }
                    for (int i = 0; i < (SceneTexture.Count); i++)
                    {

                        var flagOption = new Dropdown.OptionData(SceneDropDownName[i]);
                        flagItems.Add(flagOption);
                        Debug.Log("SceneTexture Dropdown list update");
                    }
                    Action_SceneList.AddOptions(flagItems);
                    Debug.Log(Action_SceneList.options.Count);

                    // Change end

                    for (int i = 0 ; i < Action_SceneList.options.Count; i++) {
				
					if (Action_SceneList.options [i].text == ActionObject) {
					
						Action_SceneList.value = i;
					}
				}
				Action_SceneList.gameObject.SetActive (true);
				Action_MediaFiles.gameObject.SetActive (false);
				Action_UnityObjects.gameObject.SetActive (false);
				ActionLable.gameObject.SetActive (false);

				break;

			case "Display text lable":

				ActionLable.text = ActionObject;

				Action_SceneList.gameObject.SetActive (false);
				Action_MediaFiles.gameObject.SetActive (false);
				Action_UnityObjects.gameObject.SetActive (false);
				ActionLable.gameObject.SetActive (true);
				break;
			case "Play Move Clip":

				for (int i = 0 ; i < Action_MediaFiles.options.Count; i++) {

					if (Action_MediaFiles.options [i].text == ActionObject) {

						Action_MediaFiles.value = i;
					}
				}
				Action_SceneList.gameObject.SetActive (false);
				Action_MediaFiles.gameObject.SetActive (true);
				Action_UnityObjects.gameObject.SetActive (false);
				ActionLable.gameObject.SetActive (false);
				break;
			case "Replace with object":

				for (int i = 0 ; i < Action_UnityObjects.options.Count; i++) {

					if (Action_UnityObjects.options [i].text == ActionObject) {

						Action_UnityObjects.value = i;
					}
				}
				Action_SceneList.gameObject.SetActive (false);
				Action_MediaFiles.gameObject.SetActive (false);
				Action_UnityObjects.gameObject.SetActive (true); 
				ActionLable.gameObject.SetActive (false);
				break;

			}

			for (int i = 0; i < ActionList_DropDown.options.Count; i++) {
			
				if (ActionList_DropDown.options [i].text == ActionList) {
					ActionList_DropDown.value = i;
				}
			}

			for (int i = 0; i < UserActionList_DropDown.options.Count; i++) {

				if (UserActionList_DropDown.options [i].text == UserActionList) {
					UserActionList_DropDown.value = i;
				}
			}

			Debug.Log ("visible when is "+ visibleAfter);    
			VisibleWhen.isOn = visibleAfter; 
			Required.isOn = required;   
			  
			Hotspot_Name.text = "Btn ID : " + ButtonID.ToString ();
		}
	}

}

