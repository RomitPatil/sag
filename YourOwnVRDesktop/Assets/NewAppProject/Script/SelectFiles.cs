using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SelectFiles : MonoBehaviour {

    // loaded Assetbundle object.  
    public GameObject UnityAssetBundle;

    // List of 3d models and unity File Object
	public List<GameObject> GunneyBagItems = new List<GameObject> ();

    //List for Storing Selected File 
	public List<SelectFiles> Selectedfiles = new List<SelectFiles> ();

    // Unity asset Object holding on this File.
    public GameObject UnitySceneBundle;

    // DropFolder in Scene, Drop that gameobject into referance.
	public GameObject DropFolder;

    // PtohoPrbf in Scene, Drop that gameobject into referance.
    public GameObject PtohoPrbf;

    // Unity 3d Object having it's Image. 
    public Texture UnityAssetTexture;

    // FootageDragDrop having all referance of files and file path.
	public FootageDragAndDrop FootageDragAndDrop;

    // Holding the UnityAsset Path.
    public string UnityAssetPath ;  

    // Extention and File name
    public string ExtentionName;
	public string FileSceneName;
	public string MediaFile;

    // variable the File type and is Edit or not and Selected or not. 
	public bool Selected;
	public bool EditScene;
	public bool Image;
	public bool Video;
	public bool UnityObject;
	public int id;  
    // Scene Property Struture.
    [System.Serializable]
	public struct Container {public string SceneTitle; public Texture sceneTexture; string DoneImageName; public string ImageURLPath;  public Hotspot[] hotspots; }

    // Hotspot Propertly Structure.
    public struct Hotspot
	{
		public string typeName; 
		public string hotspotsName;
		public Transform hotspots;
		public Vector3 hotspotPosition;
		public Quaternion hotspotRotation;
		public int NavigateNo;
		public string NavigateScene; 
		public Quaternion CustomLocationRot;
		public Vector2 InfoBoxSize;
		public string InfoText;
		public int InfoFontSize;
		public Transform ActionHotspot;
		public Vector3 ActionHotspotPos;  
		public Quaternion ActionHotspotRot; 
		public Vector3 NavigatePoint;
		public Vector3 ActionColliderPos;
		public Quaternion ActionColliderPosRot;
		public Quaternion NavigatePointRot;
		public Quaternion ActionRotation;
		public Vector3[] ActionDots;
		public GameObject DragThumbnil;
      
	}

	public Container scene;
	public SetupDome Dome;

    public bool Drag;
	public bool inPhotoViewer;
	public bool Imported;

    public int clickcount;


    void Start ()
    {
		 
		Imported = true; 

		FootageDragAndDrop = FindObjectOfType<FootageDragAndDrop> ();
		GetFileExtantion(gameObject.transform.GetChild (0).GetComponent<InputField>().text);

		FileSceneName = gameObject.transform.GetChild (0).transform.GetComponent<InputField> ().text;  

	}

    // 1. click to Image .
    // Click counter Increment .
    // If Image then. 
    // Setup Drag to Scene Method .
    // Setup Drop to scene Method .
    // Call SelectFile method .

    // 2. If unity object 
    // Put the Selected gameject on Dome.SelectedFile.
    // Adding asset from gunney bag.
    // put texture into selected file.

    // 3. If videp file 
     // Put the Selected gameject on Dome.SelectedFile. 
     // Call Selected hotspot LoadmediaFile method.

   // 4. Make counter 0;

    public void click ()
    {
		   
        // 1. click to Image .
        clickcount += 1;

		if (clickcount == 1)
        {
			Debug.Log ("clicked");  
			if (Dome != null)
            {
                Dome.SelectFile = gameObject;
                // If Image then. 
                if (Image)
                {
                 
                    DragToScene();
					  
                    // Setup Drop to scene Method .
                    Dome.DropToScene ();
					EditScene = true;  
                    //Call SelectFile method .
              //      SelectFile();
				} 

                // 2. If unity object 
                if (UnityObject)
                {
					
					if (Dome.SelectFile != null)
                    {
                        Debug.Log("???");
                        // Put the Selected gameject on Dome.SelectedFile.
                        if (Imported ) { 
                 
                            // Adding asset from gunney bag.
                            Dome.SelectFile.GetComponent<SelectFiles> ().GunneyBagItems.Add (UnityAssetBundle);

                            // put texture into selected file.
                            Dome.SelectFile.GetComponent<SelectFiles>().UnityAssetTexture = gameObject.GetComponent<RawImage>().texture;
							Imported = false;
						}
					}

				}

                // 3. If videp file 
                if (Video)
                {	
					     
                    // Put the Selected gameject on Dome.SelectedFile. 
                    Dome.SelectFile = gameObject;

					Dome.setVideoOnDome (MediaFile);  
					EditScene = true;     
					//Call SelectFile method . 
					//SelectFile();

					/*
                            if (SetupDome.SelectedHotspot != null)
                            {
                                // Call Selected hotspot LoadmediaFile method.
                                SetupDome.SelectedHotspot.GetComponent<NewActionHotspot>().LoadMediaFile();
                                Imported = false;
                            }
                            */   
                        
				}
			}

		}

        // 4. Make counter 0;
        clickcount = 0;
		}
			

    // Unselect the file .
    // 1. Check for selected files 
    // 2. Disable the selction UI
    // 3. Clear the SelectedObject
    // 4. Clear the SelectedURl;
    // 5. Select the multiple File with shift.

	public void UnSelectFile () {
		 
		for (int i = 0; i < FootageDragAndDrop.SeletedObject.Count; i++) {
			Debug.Log (FootageDragAndDrop.SeletedObject.Count);
			FootageDragAndDrop.SeletedObject [i].gameObject.transform.GetChild (2).gameObject.SetActive (false);
			FootageDragAndDrop.SeletedObject [i].gameObject.transform.GetComponent<SelectFiles>().Selected = false;
			FootageDragAndDrop.SeletedObject [i].gameObject.transform.GetComponent<Outline> ().enabled = false;
		}

		FootageDragAndDrop.SeletedObject.Clear();
		FootageDragAndDrop.SelecteURL.Clear ();

	//	FootageDragAndDrop.cursor.setMouse ();
	}

    // 1. Check if selected and couter is 1 then first Unselect that file.
    // 2. Unselect the UI.
    // 3. if not selected the Enable the selected UI.
    // 4. Select the multiple File with shift.
    public void SelectFile() {

		Debug.Log ("Select file");

        // 1. Check if selected and couter is 1 then first Unselect that file.
        if (Selected)
        {
			if (FootageDragAndDrop.SeletedObject.Count == 1)
            {
				UnSelectFile ();
			} 

            // 2. Unselect the UI.
            Selected = false;
			gameObject.transform.GetChild (2).gameObject.SetActive (Selected);
			gameObject.GetComponent<Outline> ().enabled = Selected;
			FootageDragAndDrop.SeletedObject.Remove (gameObject);
			FootageDragAndDrop.SelecteURL.Remove (scene.ImageURLPath);
		}
        // 3. if not selected the Enable the selected UI.
        else if (Selected == false)
        {

			if (FootageDragAndDrop.SeletedObject.Count == 0)
            {
			
				Selected = true;
				FootageDragAndDrop.cursor.setHand ();
				gameObject.transform.GetChild (2).gameObject.SetActive (Selected);

				if (inPhotoViewer == true)
                {
					gameObject.transform.GetChild (2).gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (50, 50);

                    for (int i = 0; i < FootageDragAndDrop.SeletedObject.Count; i++)
                    {
						FootageDragAndDrop.SeletedObject [i].transform.GetChild (2).gameObject.SetActive (false);
						FootageDragAndDrop.SeletedObject [i].gameObject.transform.GetComponent<SelectFiles> ().Selected = false;
						FootageDragAndDrop.SeletedObject [i].gameObject.transform.GetComponent<Outline> ().enabled = false;
					}

					FootageDragAndDrop.SeletedObject.Clear ();  
				}


				gameObject.transform.GetChild (2).gameObject.SetActive (Selected);
				gameObject.GetComponent<Outline> ().enabled = Selected;
				gameObject.transform.GetChild (2).gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (50, 50);
				gameObject.transform.GetChild (2).gameObject.transform.GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (gameObject.GetComponent<RectTransform> ().sizeDelta.x, gameObject.GetComponent<RectTransform> ().sizeDelta.y);

                FootageDragAndDrop.SeletedObject.Add (gameObject);
				FootageDragAndDrop.SelecteURL.Add (scene.ImageURLPath);
			}

            // 4. Select the multiple File with shift.
            else if (Input.GetKey (KeyCode.LeftShift))
            {
				
				Selected = true;
				FootageDragAndDrop.cursor.setHand ();
				gameObject.transform.GetChild (2).gameObject.SetActive (Selected);

				if (inPhotoViewer == true)
                {
					gameObject.transform.GetChild (2).gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (50, 50);

                    for (int i = 0; i < FootageDragAndDrop.SeletedObject.Count; i++)
                    {
						FootageDragAndDrop.SeletedObject [i].transform.GetChild (2).gameObject.SetActive (false);
						FootageDragAndDrop.SeletedObject [i].gameObject.transform.GetComponent<SelectFiles>().Selected = false;
						FootageDragAndDrop.SeletedObject [i].gameObject.transform.GetComponent<Outline> ().enabled = false;
					}

					FootageDragAndDrop.SeletedObject.Clear ();
				}

				gameObject.transform.GetChild (2).gameObject.SetActive (Selected);
				gameObject.GetComponent<Outline> ().enabled = Selected; 
				gameObject.transform.GetChild (2).gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (50, 50);
				gameObject.transform.GetChild (2).gameObject.transform.GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (gameObject.GetComponent<RectTransform> ().sizeDelta.x, gameObject.GetComponent<RectTransform> ().sizeDelta.y);


				FootageDragAndDrop.SeletedObject.Add (gameObject);
				FootageDragAndDrop.SelecteURL.Add (scene.ImageURLPath);

			}
            else
            { 
                // If Shift key is not press and click on any file , then all file are Unselected.
                UnSelectFile();
			}
		}
	}


    //Drage file to Dome.
     // 1. Cursor setDrag Method is called.
     // 2. Get All selected file texture and filenmae in FootageDragAndDrop
	public void DragFiles (){

		Debug.Log ("Drag");

        // 1. Cursor setDrag Method is called.
        FootageDragAndDrop.cursor.setDrag ();

        if (Drag) {

            // 2. Get All selected file texture and filenmae in FootageDragAndDrop
            for (int j = 0; j < Selectedfiles.Count; j++)
            {
		
				FootageDragAndDrop.SelectedTexture.Add (transform.GetComponent<RawImage> ().mainTexture);
				FootageDragAndDrop.SelectedFileName.Add (transform.GetChild (0).GetComponent<Text> ().text);
			}
			Drag = false;
		} 
	}


    // Drag to Scene 
         // Cursor Drag method is called.
         // ScenedragPanal is Enable (SceneDrag Panal is Area where our Drag and Drop Trigger is called)
         // If it is Image then put that file inti Dome SelecteFile.
         
	public void DragToScene() {

        // Cursor Drag method is called.
        FootageDragAndDrop.cursor.setDrag ();

		if (Dome != null) {

            // If it is Image then put that file inti Dome SelecteFile.
            if (inPhotoViewer == true) {

				Debug.Log ("Drag into Scene");

                // ScenedragPanal is Enable (SceneDrag Panal is Area where our Drag and Drop Trigger is called)
                Dome.SceneDragPanel.SetActive (true);

                Dome.SelectFile = gameObject; 
		
			}
		}
	}

    // Method for Filter Extention from file path or File Name.
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
	
	
	void Update ()
    {

        if (Dome == null)
        {
            Dome = GameObject.FindObjectOfType<SetupDome>();
        }

		if (Selected == true) {
			gameObject.transform.GetChild (4).gameObject.SetActive (false);  
		} else {
			gameObject.transform.GetChild (4).gameObject.SetActive (true);    
		}
      
	
	}
}
