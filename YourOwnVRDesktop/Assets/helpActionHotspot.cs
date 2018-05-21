using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;  
using UnityEngine.Video; 

public class helpActionHotspot : MonoBehaviour { 
	 
	public List<string> SceneTexturePath = new List<string>();
	public List<Texture> SceneTexture = new List<Texture>();
	public List<Dropdown.OptionData> flagItems = new List<Dropdown.OptionData> ();     
	public Dropdown ActionList_DropDown;  
	public Dropdown targetObject;   
	public string ActionFunction;  
	public GameObject TextOption;    
	public GameObject VideoOption; 
	public GameObject LableBox,MediaFilePrfb;   
	public GameObject ColorGiver; 
	public GameObject NewHotspotContainer;  
	public AddHelpHotspot helpHotspot;
    public InputField xpos;
    public InputField yPos;
    public InputField zPos;
	public InputField Action_Name;   
	public InputField Lable_Text,Lable_Title;  
	public GameObject menuPanal,navigationPanal,mediaPanal,textPanal,actionPanal,objectPanal,helpPanal,timeLine;
	public string ActionName; 
	public Vector3 Position; 
	public string videoURL;    
	public string LableText,LableTitle;   
	public SetupDome Dome; 
	public VideoPlayer videoPlayer;  
	public Sprite hotspotSprite;  
	private bool text,movie;  
	public Toggle VisibleWhen;  
	public bool visibleAfter;  
	public Text Hotspot_Name;    
	public int ButtonID;    
	public Sprite mySprite;  
    // Use this for initialization       
    void Start () {
		text = false;
		movie = false; 
		helpHotspot = FindObjectOfType<AddHelpHotspot> ();  
		Action_Name = helpHotspot.ActionLable;    
		Lable_Text = helpHotspot.LableText_InputField; 
		Lable_Title = helpHotspot.LableTitle_InputField;  
		VideoOption = helpHotspot.VideoOption;
		TextOption = helpHotspot.TextOption;  
		LableBox = helpHotspot.LableBox;  
		MediaFilePrfb = helpHotspot.MediaFilePrefab;  
		targetObject = helpHotspot.targetObject;    
		menuPanal = helpHotspot.BlueHotspotPanal;
		navigationPanal = helpHotspot.yellowHotspotPanal;
		mediaPanal = helpHotspot.purpleHotspotPanal;
		textPanal = helpHotspot.ActionTextPanal; 
		actionPanal = helpHotspot.ActionHotspotPanel;
		helpPanal = helpHotspot.HelpHotspotPanal;
		timeLine = helpHotspot.timeLine;  
		ColorGiver = helpHotspot.ColorGiver;  
		NewHotspotContainer = helpHotspot.NewHotspotContainer;  
		ActionList_DropDown = helpHotspot.ActionList_DropDown;
		VisibleWhen = helpHotspot.AlwaysToggle;   
		Action_Name.text = "";     
		targetObject.value = 0;   
		VideoOption.gameObject.SetActive (false); 
		TextOption.gameObject.SetActive (false);
		Dome = GameObject.Find ("DomeFull").GetComponent<SetupDome>();   
		mySprite = helpHotspot.mySprite;  
		Lable_Text.text = "";  
		Lable_Text.onEndEdit.AddListener (delegate {
			addLableText (); 
		});   

		Lable_Title.text = "";  
		Lable_Title.onEndEdit.AddListener (delegate {
			addLableTitle ();    
		});  

		targetObject.onValueChanged.AddListener (delegate {
			Debug.Log("abc");
			if (SetupDome.SelectedHotspot == gameObject) {
				InstantiateObjects (targetObject.captionText.text);  
				ActionFunction = targetObject.captionText.text; 
				Debug.Log("bac");  
			}
		});     
		 
		Action_Name.onEndEdit.AddListener (delegate {
			if(SetupDome.SelectedHotspot == gameObject)  
				addActionText ();   
		}); 
		//InstantiateLable ();   
		VisibleWhen.onValueChanged.AddListener(delegate {
			VisibleAfter();  
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

	public void addLableText(){    
		if(Dome.selectedHotstop==gameObject)   
			LableText = Lable_Text.text;    
	}  

	public void addLableTitle(){   
		if(Dome.selectedHotstop==gameObject)   
			LableTitle = Lable_Title.text;    
	}     
	 
	void addActionText(){  
		ActionName = Action_Name.text;  
	}

	// Update is called once per frame
	void Update () { 
		if (!transform.GetChild (0).transform.GetComponent<BoxCollider> ().enabled) { 
			getPosition ();    
		} else {
			if (movie) {
				if (!transform.GetChild (0).transform.GetChild (0).transform.GetComponent<BoxCollider> ().enabled) { 
					getPosition ();      
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
		if (SetupDome.SelectedHotspot == gameObject) {   
			if (Lable_Text != null) {    		   
				if (Lable_Text.text.Length != 0) {     
					if (gameObject.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ()) {  
						gameObject.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ().text = Lable_Text.text;

					}
				}
			} 	 
		}  
	} 


	// Function of ActionHotspot Setup
	public void InstantiateObjects( string ObjectName)
	{
		switch (ObjectName)
		{
		case "Display text lable":       
			VideoOption.gameObject.SetActive (false);  
			TextOption.gameObject.SetActive (true); 
			InstantiateLable ();  
			text = true;  
			movie = false;  
			break; 			  
			 
		case "Play Move Clip":     
			VideoOption.gameObject.SetActive (true);  
			TextOption.gameObject.SetActive (false); 
			InstantiateMediaFile ();	 
			movie = true; 
			text = false;
			break;  
		}
	}
  
	public void InstantiateLable ()
	{
		GameObject lable = GameObject.Instantiate (LableBox);
		lable.transform.parent = gameObject.transform;  
		lable.transform.localPosition = Vector3.zero;    
		gameObject.transform.GetChild (1).gameObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite = mySprite;  
		GameObject.Destroy (gameObject.transform.GetChild (0).gameObject);  
		getPosition ();   
	}     
	    
	// Initalize the MediaFile  
	public void InstantiateMediaFile ()  
	{   
		GameObject MediaFilePrb = GameObject.Instantiate (MediaFilePrfb);
		MediaFilePrb.GetComponent<generalHotspot> ().enabled = false;   
		MediaFilePrb.transform.parent = gameObject.transform;   

		MediaFilePrb.transform.localPosition = new Vector3 (0,0,0);
		MediaFilePrb.transform.localScale = new Vector3(1f, 1f, 1f);   
		MediaFilePrb.transform.localEulerAngles = Vector3.zero;   
		gameObject.transform.GetChild (1).gameObject.transform.GetChild (0).transform.GetComponent<Image> ().sprite = mySprite;
		videoPlayer = gameObject.transform.GetChild (1).gameObject.GetComponent<MediaHotspot> ().videoPlayer; 
		GameObject.Destroy (gameObject.transform.GetChild (0).gameObject);   
		getPosition ();       
	}    
	   
	 
	public void SetupNewActionhotspot ()
	{  

	//	Hotspot_Name = GameObject.Find ("Button_ID_Text").transform.GetComponent<Text> ();   
		Hotspot_Name.text = "Btn ID : " + ButtonID.ToString ();  
		gameObject.name = Hotspot_Name.text;  

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
	 
	public void getPosition ()
	{ 
		Debug.Log ("random1");
		SetupDome.SelectedHotspot = gameObject;  
		Hotspot_Name.text = "Btn ID : " + ButtonID.ToString (); 
		textPanal.SetActive (false);
		navigationPanal.SetActive (false);  
		actionPanal.SetActive (false);  
		menuPanal.SetActive (false);
		mediaPanal.SetActive (false);
		helpPanal.SetActive (true);
		timeLine.SetActive (false);   
		if (text) {
			Debug.Log ("random2");
			VideoOption.gameObject.SetActive (false);  
			TextOption.gameObject.SetActive (true); 
		} 
		if (movie) {   
			Debug.Log ("random3");
			VideoOption.gameObject.SetActive (true);  
			TextOption.gameObject.SetActive (false);      
		}
		Debug.Log ("random4");   
		Lable_Text.text = LableText;   
		Lable_Title.text = LableTitle;   
  		
		Hotspot_Name.text = "Btn ID : " + ButtonID.ToString (); 
		helpHotspot.SelectedHotspot = gameObject; 
		VisibleAfterDropdownInstantiate ();  
	}

}
