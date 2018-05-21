using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class textActionHotspot : MonoBehaviour
{  

	public InputField ActionLable; 
	public InputField LableTitle_InputField, LableText_InputField;  
	public AddTextHotspot textHotspot;
	public GameObject LableBox;  
	public int ButtonID; 
	public List<Texture> SceneTexture = new List<Texture> ();
	public List<string> SceneTexturePath = new List<string> ();
	public Text Hotspot_Name;
	public static int text_number;      
	public string NavigateToScene;
	public Vector3 Position;
	public string ActionFunction;  
	public string ActionObject; 
	public string UserActionName; 
	public bool Always; 
	public bool Glance;   
	public bool ActionVisibleWhen; 
	public string ActionList;
	public string UserActionList;  
	public Vector2 LableBoxSize;   
	public List<string> SceneDropDownName;  
	public Dropdown Action_SceneList;  
	public InputField posx,posy,posz;
	public string LableTitle;
	public string LableText;  
	public GameObject navigationPanel, textPanel, actionHotspotPanel;  
	public SetupDome Dome;   
	public Toggle AlwaysToggle,glanceOver;  
	public Sprite hotspotSprite;     
	 
	void Start ()    
	{
		textHotspot = FindObjectOfType<AddTextHotspot> ();    
		ActionLable = textHotspot.ActionLable; 
		LableTitle_InputField = textHotspot.LableTitle_InputField; 
		LableText_InputField = textHotspot.LableText_InputField;   
		posx = textHotspot.posx; 
		posy = textHotspot.posy;
		posz = textHotspot.posz;  
		Hotspot_Name = textHotspot.HotspotName;   
		GameObject lable = GameObject.Instantiate (LableBox);
		lable.transform.parent = gameObject.transform;  
		lable.GetComponent<RectTransform>().localPosition = new Vector3 (0f,0f,95f);    
		GameObject.Destroy (gameObject.transform.GetChild (0).gameObject);

		ActionFunction = "Display text lable"; 
		Dome = GameObject.Find ("DomeFull").GetComponent<SetupDome>();  
		navigationPanel = textHotspot.navigationPanel;
		textPanel = textHotspot.ActionTextPanal;
		actionHotspotPanel = textHotspot.ActionHotspotPanel;  
		AlwaysToggle = textHotspot.AlwaysToggle; 
		glanceOver = textHotspot.glanceOver;  
		 
		LableTitle_InputField.text = "";   
		LableTitle_InputField.onEndEdit.AddListener (delegate {
			addLableTitle (); 
		});

		LableText_InputField.text = "";  
		LableText_InputField.onEndEdit.AddListener (delegate {
			addLableText (); 
		});   

		AlwaysToggle.isOn = false;  

		glanceOver.isOn = false; 

		AlwaysToggle.onValueChanged.AddListener (delegate {
			alwaysToggleListener();
		});
		 
		glanceOver.onValueChanged.AddListener (delegate {
			glanceListener();
		});
		  
	}    
	   
	    
	public void SetupNewActionhotspot ()
	{  
		
		Debug.Log ("setup new @");  
		// 3. Giving Button ID to ActionHotspot. 
		
		Hotspot_Name.text = "Btn ID : " + ButtonID.ToString ();  

		// 4. Give the ActionHotspot Name .   
		gameObject.name = Hotspot_Name.text;  

	} 
	  
	public void alwaysToggleListener() {
		Debug.Log ("listener is called"); 
		if (SetupDome.SelectedHotspot == gameObject) {
			if (AlwaysToggle.isOn) {
				Always = true;
			} else {
				Always = false; 
			}
		} 
	}

	public void glanceListener() {
		if (SetupDome.SelectedHotspot == gameObject) {
			if (glanceOver.isOn) {
				Glance = true;    
			} else { 
				Glance = false; 
			}  
		}
	}
	   
	 
	public void addLableTitle(){
		if(Dome.selectedHotstop==gameObject)
			LableTitle = LableTitle_InputField.text;  
	}
	 
	public void addLableText(){ 
		if(Dome.selectedHotstop==gameObject)   
			LableText = LableText_InputField.text;  
	}
	     
	// Update is called once per frame
	void Update ()
	{      
		Position = gameObject.transform.position;        
		if (SetupDome.SelectedHotspot == gameObject) {   
			if (ActionLable != null) {   		    
				if (ActionLable.text.Length != 0) {
					if (gameObject.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ()) {
						gameObject.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ().text = ActionLable.text;
						 
					}
				}
			}
		}
		 
        if (Hotspot_Name == null) {
            Hotspot_Name = GameObject.Find("Button_ID_Text").transform.GetComponent<Text>();
        }

		if(!transform.GetChild(0).transform.GetComponent<BoxCollider>().enabled) {
			getPosition ();     
		}

        if (gameObject.transform.localPosition.z != 0) {
            gameObject.transform.GetChild(0).transform.localPosition = Vector3.zero;
        }
	}
	  
	public void getPosition () 
	{
		Debug.Log ("text action 123");
        textHotspot.EnableAllHotspotTemplete();
        //AlwaysToggle.isOn = Always;    
        SetupDome.SelectedHotspot = gameObject;  
		AlwaysToggle.isOn = Always;   
		glanceOver.isOn = Glance;   
		Hotspot_Name.text = "Btn ID : " + ButtonID.ToString ();    
		navigationPanel.SetActive (false);
		actionHotspotPanel.SetActive (false);    
		textPanel.SetActive (true);  
		ActionLable.text = gameObject.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ().text;
		LableText_InputField.text = LableText;  
		LableTitle_InputField.text = LableTitle;   
	}
	  

}
