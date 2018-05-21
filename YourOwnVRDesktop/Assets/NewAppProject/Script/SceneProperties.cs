using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Video;  
 
public class SceneProperties : MonoBehaviour {

	public string SceneName;
	public Texture SceneTexture; 
	public GameObject Scene_Name_Input;  
	public string SceneTexturePath;
	public Vector3 CamPos;
	public GameObject NavigationListContainer;
	public GameObject DomeFull;  
	public GameObject PreviewLoader;
	public Dropdown GunnyBag;
	public List <GameObject> GunneyBagItem = new List<GameObject>();
	public bool video;   
	public string url; 
	public string ExtentionName;   
	public List<Dropdown.OptionData> flagItems = new List <Dropdown.OptionData> (); 
	public List<string> MediaFileUrl = new List<string>();
    public AddActiveHotspot AddActiveHotspotScript;
	public VideoPlayer player;  

    [System.Serializable]
	public struct Hotspottypes   
	{
		public ActiveHotspot[] ActiveHotspotsContainer;
	}
	[System.Serializable]
	public struct ActiveHotspot
	{
		public string HotspotName;
		public NewHotspot ActiveHotspotScriptRef;
		public Vector3 Position ;
		public string TargetCompletion;
		public string WebLink;
	}  


	public List<float> initialTime = new List<float>();
	public List<float> finalTime = new List<float>();   

	public Hotspottypes ActionHotspot;  

	public int ActionHotspotCount;
	public GameObject DomeCamera;
	bool Enter;
	public bool AddTexture;
	void OnEnable () {
		Enter = true;
		if (DomeCamera == null) {
			DomeCamera = GameObject.Find ("DomeCamera");
		}
		if (DomeFull == null) {
			DomeFull = GameObject.Find ("DomeFull");
		}
		if (PreviewLoader == null) {
			PreviewLoader = GameObject.Find ("Singleton");
		}
		if (GunnyBag == null) {
			GunnyBag = GameObject.Find ("Gunny_Bag").GetComponent<Dropdown>();
		}

		AddTexture = true; 
		     
        if (gameObject.transform.childCount != 0) 
        {
			if(gameObject.transform.GetChild(0).gameObject.GetComponent<NewActionHotspot>())
            gameObject.transform.GetChild(0).gameObject.GetComponent<NewActionHotspot>().OnClickOnActionHotspot();
			if(gameObject.transform.GetChild(0).gameObject.GetComponent<navigateActionHotspot>())   
			gameObject.transform.GetChild(0).gameObject.GetComponent<navigateActionHotspot>().OnClickOnHotspot();

        }    
	}
	// Use this for initialization
	void Start () {

	//	Scene_Name_Input = GameObject.Find ("Scene_Name_Input").gameObject; 

		AddTexture = true;
		GetFileExtantion(SceneName);   
		if (ExtentionName == "mp4" || ExtentionName == "MP4") {
			video = true;      
		}

		player = DomeFull.GetComponent<VideoPlayer> ();   

	}

	void FixedUpdate() { 
		for (int i = 0; i < initialTime.Count; i++) {   
			if (initialTime[i] > player.time) {
				transform.GetChild (i).gameObject.SetActive (false);   
			}   
			if ((initialTime[i] < player.time)&&(finalTime[i] > player.time)) { 
				transform.GetChild (i).gameObject.SetActive (true);     
			}  
			if (finalTime[i] < player.time) {      
				transform.GetChild (i).gameObject.SetActive (false);     
			}
		}
	}

    // Update is called once per frame
    void Update() {
        if (AddActiveHotspotScript == null) { 
        AddActiveHotspotScript = FindObjectOfType<AddActiveHotspot>();
            Scene_Name_Input = AddActiveHotspotScript.Scene_Name_InputFild;
    }
        if (Scene_Name_Input == null) {
			Scene_Name_Input = AddActiveHotspotScript.Scene_Name_InputFild;
        }
		if (PreviewLoader == null) {
			PreviewLoader = GameObject.Find ("Singleton");
		}
		if (AddTexture) {
			AddTexture = false;
			//PreviewLoader.GetComponent<PreviewLoader> ().PreloadedTexture.Add (SceneTexture);
		}

		if (gameObject.transform.childCount != 0) {
			Scene_Name_Input.gameObject.SetActive (true);
		} else {
			Scene_Name_Input.gameObject.SetActive (false);
		}

		HotspotInfo ();
		if (Scene_Name_Input != null) {
			if (Scene_Name_Input.GetComponent<InputField>().text != gameObject.name) {
				SceneName = Scene_Name_Input.GetComponent<InputField>().text;
				for (int i = 0; i < gameObject.transform.parent.childCount; i++) {
					if (gameObject.transform.parent.GetChild (i).gameObject.name == SceneName) {
						SceneName = gameObject.transform.parent.GetChild (i).gameObject.name + "1";
					}
			
				}
				gameObject.name = SceneName;
                if (gameObject.transform.GetChild(0).GetComponent<NewHotspot>())
                {
                    gameObject.transform.GetChild(0).GetComponent<NewHotspot>().AddScenesOnDropDown();
                }
                if (gameObject.transform.GetChild(0).GetComponent<NewActionHotspot>())
                {
                    gameObject.transform.GetChild(0).GetComponent<NewActionHotspot>().AddScenesOnDropDown();
                }

                DomeFull.GetComponent<SetupDome> ().SelectFile.GetComponent<SelectFiles> ().FileSceneName = Scene_Name_Input.GetComponent<InputField>().text;
			}
		}
		if (DomeCamera == null) {
			DomeCamera = GameObject.Find ("DomeCamera");
		}
		if (NavigationListContainer == null) {
			NavigationListContainer = GameObject.Find ("NavigateListContainer");
		}
		if (NavigationListContainer != null) {
			for (int J = 0; J < (NavigationListContainer.transform.childCount - 1); J++) {
					

				Debug.Log (NavigationListContainer.transform.childCount + "::::" + gameObject.transform.parent.childCount);
				NavigationListContainer.transform.GetChild (J+1).GetChild (3).GetComponent<RawImage> ().texture = gameObject.transform.parent.GetChild(J).GetComponent<SceneProperties>().SceneTexture;


			}
		}

		if (DomeCamera != null) {
			if (Enter) {
				DomeCamera.GetComponent<MouseCameraDraging> ().pitch = 0;
				DomeCamera.GetComponent<MouseCameraDraging> ().yaw = 0;
				DomeCamera.transform.localEulerAngles = CamPos;
				Enter = false;
			}
		}

		if (GunnyBag.options.Count != DomeFull.GetComponent<SetupDome> ().SelectFile.GetComponent<SelectFiles> ().GunneyBagItems.Count){
			GunnyBag.options.Clear ();
			GunneyBagItem.Clear ();
			flagItems.Clear ();
		for (int i = 0; i < DomeFull.GetComponent<SetupDome> ().SelectFile.GetComponent<SelectFiles> ().GunneyBagItems.Count; i++) {
			Debug.Log (DomeFull.GetComponent<SetupDome> ().SelectFile.GetComponent<SelectFiles> ().GunneyBagItems[i].gameObject.name);
			var flagOption = new Dropdown.OptionData (DomeFull.GetComponent<SetupDome> ().SelectFile.GetComponent<SelectFiles> ().GunneyBagItems [i].gameObject.name);
			flagItems.Add (flagOption);
				GunneyBagItem.Add (DomeFull.GetComponent<SetupDome> ().SelectFile.GetComponent<SelectFiles> ().GunneyBagItems[i]);
              
		}
			Debug.Log (GunnyBag.options.Count +"!==" + DomeFull.GetComponent<SetupDome> ().SelectFile.GetComponent<SelectFiles> ().GunneyBagItems.Count );
			GunnyBag.AddOptions (flagItems);
		}
	}

	public void HotspotInfo () {
		if ( ActionHotspotCount != transform.childCount) {
			ActionHotspotCount = transform.childCount; 
			ActionHotspot.ActiveHotspotsContainer = new ActiveHotspot[ ActionHotspotCount];
			for (int i = 0; i < transform.childCount; i++) {
				ActionHotspot.ActiveHotspotsContainer [i].HotspotName = transform.GetChild (i).gameObject.name;
				ActionHotspot.ActiveHotspotsContainer [i].Position = transform.GetChild (i).gameObject.transform.position;

		
			}
		}
	}

	private string GetFileExtantion(string fileName) 
	{

		string[] parts = fileName.Split ('.');
		if (parts.Length > 0) {
			ExtentionName = parts [parts.Length - 1];
		} else {
			ExtentionName = fileName;
		}
		return ExtentionName;
	}  
}
