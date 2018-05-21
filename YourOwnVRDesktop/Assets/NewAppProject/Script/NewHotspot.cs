using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

public class NewHotspot : MonoBehaviour {

	public GameObject HotspotContainer;
	public GameObject HotspotTemplet;
	public Vector3 Position;
	public InputField Target_Pose_X;
	public InputField Target_Pose_Y;
	public InputField Radius_Z; 
	public Dropdown Target_Completion;
	public InputField Hotspot_Name;
	public bool Selected;
	public SpriteRenderer tem ;
	public string NavigateToScene;
	public List<string> SceneDropDownName = new List<string> ();
	public List<Texture> SceneTexture = new List<Texture> ();
	public List<string> SceneTexturePath = new List<string> ();
	public List<Dropdown.OptionData> flagItems = new List <Dropdown.OptionData> ();
	public Texture temTexture;
	public static GameObject SelectedHotspot;
	public GameObject DisplaySelectedHotspot;
	public GameObject SceneLoadingPanal; 
	public Toggle NavigateSet;
	public int DropdownValue;

	public bool set;
	// Use this for initialization
	void Start () {
		FindGameObjectWithName ();
	} 

	void OnEnable () {
		FindGameObjectWithName ();

	}
	void Update () {


		if (SelectedHotspot != null) {
			DisplaySelectedHotspot = SelectedHotspot;
			if (SelectedHotspot == gameObject) { 
				
				if (set) {
				//	gameObject.name =  Hotspot_Name.text ;
					Target_Completion.value = DropdownValue; 
				
					set = false;
				}
				Debug.Log ("Match objects");  
				//	UpdatingNameText ();
				//	UpdategetPostion ();
			} 
		}

		if (NavigateToScene == "") {
			
			NavigateSet.isOn = false;
			Target_Completion.captionText.text = "";
			Debug.Log (" Target_Completion.options.Count ::" + Target_Completion.options.Count);
			Target_Completion.value = Target_Completion.options.Count;

			//	Target_Completion.gameObject.transform.GetChild (0).gameObject.SetActive (false);
		} else {

			NavigateSet.isOn = true;
		}
		if (transform.localPosition.z == 0) {
			
			transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, 50f);
		}
		
		Position = transform.localPosition;
	}
	private void UpdatingSceneProperty () {
//		gameObject.transform.parent.GetComponent<SceneProperties> ().ActionHotspot.ActiveHotspotsContainer.HotspotName = Hotspot_Name.text;
//		gameObject.transform.parent.GetComponent<SceneProperties> ().ActionHotspot.ActiveHotspotsContainer.Position = transform.localPosition;
//		gameObject.transform.parent.GetComponent<SceneProperties> ().ActionHotspot.ActiveHotspotsContainer.TargetCompletion = Target_Completion.captionText.text;
//	
	}
	
	private void FindGameObjectWithName () {
		HotspotContainer = GameObject.Find ("NavigationCanvas");
		HotspotTemplet = GameObject.Find ("HotspotTemplet");
		HotspotTemplet.transform.GetChild(0).gameObject.SetActive (true);
		Target_Pose_X = GameObject.Find ("Target Pos X").transform.GetChild(0).transform.GetComponent<InputField> ();
		Target_Pose_Y = GameObject.Find ("Target Pos Y").transform.GetChild(0).GetComponent<InputField> ();
		Radius_Z = GameObject.Find ("Radius_Z").transform.GetChild(0).transform.GetComponent<InputField> ();
		Hotspot_Name = GameObject.Find ("Hotspot_Name").transform.GetChild (0).transform.GetComponent<InputField> ();
		Target_Completion = GameObject.Find ("Target_Completion").transform.GetChild(0).transform.GetComponent<Dropdown>();
		NavigateSet = GameObject.Find ("NavigationSet").gameObject.GetComponent<Toggle> ();

		getPosition ();
		GetSelectedHotspot ();
	
	}

	public void AddScenesOnDropDown(){
		Debug.Log ("Add Scenes On DropDown");

		SceneDropDownName.Clear ();
		SceneTexture.Clear ();
		for (int i = 0; i < HotspotContainer.transform.childCount; i++) {

			SceneDropDownName.Add (HotspotContainer.transform.GetChild (i).gameObject.name);
			SceneTexture.Add (HotspotContainer.transform.GetChild (i).GetComponent<SceneProperties> ().SceneTexture);
			Debug.Log (i + " ==  " + HotspotContainer.transform.childCount);
			if (i == HotspotContainer.transform.childCount -1) {
				
				SceneDropDownName.Add ("Select Navigation ");
			}
		}
		Target_Completion.ClearOptions ();
	
	//	List<Sprite> Sprit = new List<Sprite>();

	//	Sprit.Clear ();
//		for (int i = 0; i < SceneTexture.Count; i++) {
//			Texture2D temIM = SceneTexture [i] as Texture2D;
//
//
//		
//			Rect rec = new Rect(0,0,temIM.width ,temIM.height);
//			Sprite.Create (temIM, rec, new Vector2 (0, 0), 1);
//			tem.sprite = Sprite.Create (temIM, rec, new Vector2 (0, 0), .01f);
//			Sprit.Add (tem.sprite); 

	//	}
		flagItems.Clear ();
		//Target_Completion.AddOptions (SceneDropDownName);
		for (int i = 0; i < (SceneTexture.Count); i++) {
			
			var flagOption = new Dropdown.OptionData (SceneDropDownName [i]);
			flagItems.Add (flagOption);

		}
		Target_Completion.AddOptions (flagItems);
		Debug.Log (Target_Completion.options.Count);
		if (Target_Completion.options.Count >= 2) {
		//	Target_Completion.interactable = true;
		} else {
		//	Target_Completion.interactable = false; 
			 
		}

	}
	// Update is called once per frame
	private void  UpdatingNameText() {
		gameObject.name = Hotspot_Name.text;
//		if (Target_Completion.options.Count != 0) {
//			NavigateToScene = Target_Completion.options [DropdownValue].text;
//		}
	
	}

	public void OnActiveHotspotClick () {
		Debug.Log ("ON Active Hotspot Click");
		for (int i = 0; i < HotspotContainer.transform.childCount; i++) {
		 
			if (HotspotContainer.transform.GetChild (i).gameObject != gameObject) {
				HotspotContainer.transform.GetChild (i).GetComponent<NewHotspot> ().Selected = false;
			} else {
				Selected = true;
			}
		}
		transform.parent.gameObject.SetActive (true);
	}

	public void setPosition () {
		Debug.Log ("Set Position");
		//transform.localPosition = new Vector3 (int.Parse (Target_Pose_X.text), int.Parse (Target_Pose_Y.text),0);
		//transform.GetChild (0).transform.localPosition = new Vector3 (0, 0, int.Parse (Radius_Z.text));
	}
	 
	public void getPosition () {
		HotspotTemplet.transform.GetChild(0).gameObject.SetActive (true);
		Debug.Log ("Get Position");
		SelectedHotspot = gameObject;
		Selected = true;
		set = true;
		UpdategetPostion ();
	}
	public void UpdategetPostion () {
		Debug.Log ("Updateget Position");

		Radius_Z.text = transform.localPosition.z.ToString ();
		Target_Pose_X.text = transform.localPosition.x.ToString ();

		Target_Pose_Y.text = transform.localPosition.y.ToString ();
	}

	public void GetSelectedHotspot(){
	
		SelectedHotspot = gameObject;
	}
}
