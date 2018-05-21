using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;
using SFB;
using System;

// TriLib is use for 3d Model importing
namespace TriLib
{  
    namespace Samples  
    {
        public class PreviewLoader : MonoBehaviour {

            public List<string> PostSceneTexturepath = new List<string>();
            public List<string> PreSceneTexturepath = new List<string>();  
            public List<string> UnityAssetFilepath = new List<string>();
            public List<Texture> PreloadedTexture = new List<Texture>();
            public List<string> PostUnityAssetath = new List<string>();
            public List<string> SenceTexturePath = new List<string>();
            public List<string> PreUnityAssetath = new List<string>();
            public List<Texture> Scenetexture = new List<Texture>();
			 
            public string ProjectdataURL;  
			public GameObject finalPreviewCamera;  

            [System.Serializable]
            public struct container  
            { 
                public string SceneName;
				public Texture SceneTexture;   
                public Vector3 SceanPos;
				public bool Video;   
				public string url;  
                public hotspotContainer[] HotspotData;
				public List<float> initialTime;
				public List<float> finalTime;     
            }
            [System.Serializable]
            public struct hotspotContainer
            {
                public string hotspotName;  
                public Vector3 Position;
                public string NagivateToScene;
                public string ActionFunction;
                public string ActionObject;
                public string UserActionName;
                public Vector3 rotationView;
                public string UserActionList;
                public string ActionList;
                public Vector2 LableBoxSize;
                public bool Always;
                public bool VisibleWhen;
                public GameObject UnityObjectInstance;
                public string UnityAssetPath;
                public Texture UnityModelTexture;
                public Vector3 UnityModelScale;
                public Vector3 UnityModelRot;
                public string LableTitle;
                public string LableText;
                public bool required;
                public bool visibleAfter;
                public int ActionNumber;
                public bool Msg;
                public string GetMsg;
				public Texture ImageTexture;  
                //Object hotspot data

                public Vector3 TargetPosition;
                public Vector3 ModelPosition;
                public Vector3 ModelScale;
                public Vector3 ModelRotation;
                public string ConnectTarget;
                public GameObject Target;
              
                public float Radius;
                public string Targetfunction;
                public string TargetfunctionData;
                public float Z_pos;
                public string ModelAssetPath;

                //MenuHotspot data

                public Vector3 MenuPosition;
                public string[] MenuItemNames;
                public string[] MenuItemFunctions;
                public string[] MenuItemFunctionData;
                public string MenuFunction;

                // MediaHotspot Data

                public string VideoURL;
                public bool Landscape;
                public bool Potrate;
                public bool FullSceen;
				public bool Action;
				public bool Help; 
	 			
				public int transition;  
           	    // Start up the Preview
                // Enable the WaitForSetupPanal
            }
            public container[] ScenesData;	   
            public GameObject ScenePrafab;
            public GameObject HotspotPrafab;
            public GameObject NavigationCanvasPrv;
            public GameObject Camera;
            public GameObject Dome;
            public GameObject WaitForSetupPanal;

            public bool MobileApp;
            public bool loadFromPreTexture;
            public bool ReloadChecker;

            public Texture DefaultTxture;
       
            public Slider LoadingSlider;
            public Image ProjectLoaderImage;


            public GameObject LoadingAsset;

            public void StartSetup() {

                WaitForSetupPanal = GameObject.Find("WaitForSetupPanal");
                WaitForSetupPanal.SetActive(true);    
				  
                if (MobileApp)
                {
                    NavigationCanvasPrv = GameObject.Find("NavigationCanvasPrv");
                    WaitForSetupPanal.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "/n  Start UP ";

                    StartCoroutine(wait());
                }
               
            }  

            // Put Texture to Dome 
            // Get all texture from the Path.
            IEnumerator wait()
            {
				 
                yield return new WaitForSeconds(3f);
                Dome = null;
                yield return Dome;
                Dome.GetComponent<MeshRenderer>().material.mainTexture = DefaultTxture;

                Gettexture();
            }

            // Update is called once per frame
            void Update() {

                // That Code for Moblie Scene if MobileApp is true .
                if (MobileApp) {
                    if (Camera == null)
                    {
                        Camera = GameObject.Find("DomeCamera");
                    }
                    if (NavigationCanvasPrv == null)
                    {
                        NavigationCanvasPrv = GameObject.Find("NavigationCanvasPrv");
                    }
                    if (Dome == null ) {
                        Dome = GameObject.Find("DomePreview");
                    }

                    if (MobileApp)
                    {

                        if (LoadingSlider == null)
                        {
							if (GameObject.Find ("LoadingSlider")) {
								LoadingSlider = GameObject.Find ("LoadingSlider").GetComponent<Slider> ();
							}  
                        }

                        if (ProjectLoaderImage == null)
                        {
							if(GameObject.Find("ProjectLoaderImage")){
                            	ProjectLoaderImage = GameObject.Find("ProjectLoaderImage").GetComponent<Image>();
							}  
                        }  
                    }

                }


            }

            // 1. First check in Run on mobile device . 
            // 2. Find the file in local Space.
            // 3. Load data From Server or Load Data From Local Space.
            public void Gettexture()
            {
                Debug.Log("gettecture ");
                Scenetexture.Clear();
                if (ProjectLoaderImage != null)
                {
                    ProjectLoaderImage.transform.GetChild(0).GetComponent<Text>().text += SenceTexturePath.Count;
                }

                // 1. First check in Run on mobile device .  
                if (MobileApp)
                {  

                    // 2. Find the file in local Space.
                    for (int i = 0; i < SenceTexturePath.Count; i++)   
                    {
                        string FindFile = Path.GetFileName(SenceTexturePath[i]).Replace("?overwrite=true", "");
                        Debug.Log("Finded File Name " + FindFile);

                        FindFile = Application.persistentDataPath + "/Images/" + FindFile;

                        if (!File.Exists(Application.persistentDataPath + "/Images"))
                        {
                            Directory.CreateDirectory(Application.persistentDataPath + "/Images");
                        }
                        else
                        {
                            WaitForSetupPanal.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "/n   " + FindFile;
                            if (File.Exists(FindFile))
                            {
                                ReloadChecker = true;
                                Debug.Log("I found " + FindFile);
                            }
                            else
                            {
                                ReloadChecker = false;
                                Debug.Log("I not found " + FindFile);
                            }
                        }
                    }

                    // 3. Load data From Server or Load Data From Local Space.
                    if (ReloadChecker == false)
                    {
                        Debug.Log("Load From Cache");

                
                        StartCoroutine(loadformCache());
                    }
                    else {

                        Debug.Log("Load From Local");
                        StartCoroutine(LoadfromLocal());
                    }

                }
                else 
                {
                    StartCoroutine(loadformCache());
					//StartCoroutine(SetupPrview());
                }

            }  

            // 1. Loop from the SceneTexturePath .
            // 2. Download From WWW.
            // 3. Save to Local device Space.
            // 4. Check all Texture is Downloaded 
            // 5. Setup Scene. 
            IEnumerator loadformCache()
            { 
				
				for (int i = 0; i < SenceTexturePath.Count; i++) {
					Debug.Log ("number of runs"); 
					// 2. Download From WWW.
					if (SenceTexturePath [i] != "") {   
						Debug.Log ("number of runs actual");      

						WWW load = new WWW (SenceTexturePath [i]);
						Debug.Log ("temporary 2"); 
						while (!load.isDone) {  
							if (LoadingSlider != null) {
								LoadingSlider.transform.GetChild (0).GetComponent<Text> ().text = Path.GetFileName (SenceTexturePath [i]).Replace ("?overwrite=true", "");
								float progress = Mathf.Clamp01 (load.progress) / 0.9f;

								Debug.Log ("Downloading : " + progress + " % ");
								LoadingSlider.value = progress;
							}
							yield return null;
						}
						yield return load;
						// 3. Save to Local device Space.
						if (load.error == null) {
							string Localfilepath = Application.persistentDataPath + "/Images";
							if (!File.Exists (Localfilepath)) {
								Debug.Log ("! Localfilepath" + Localfilepath);
								Directory.CreateDirectory (Localfilepath);
								Localfilepath = Localfilepath + "/" + Path.GetFileName (SenceTexturePath [i]).Replace ("?overwrite=true", "");
								File.WriteAllBytes (Localfilepath, load.bytes);
							} else {
								File.WriteAllBytes (Localfilepath, load.bytes);
							}

							Debug.Log (Localfilepath);

							WWW reload = new WWW (Localfilepath);

							Debug.Log ("Reloading : " + reload.progress + " %");
							yield return reload;

							if (load.error == null) {
								Texture texture = load.texture;
								yield return texture;
								Scenetexture.Add (texture);
								  
								if (ProjectLoaderImage != null) {
	 								ProjectLoaderImage.fillAmount += 1 / SenceTexturePath.Count;
									ProjectLoaderImage.transform.GetChild (1).GetComponent<Text> ().text += 1f;
								}
							}
						}  
					}
					if (SenceTexturePath [i] == "") {
						Scenetexture.Add (null);   
					}
					// 4. Check all Texture is Downloaded   
					if (SenceTexturePath.Count == Scenetexture.Count) {
						Debug.Log ("Setup preview");
						// 5. Setup Scene.
						StartCoroutine (SetupPrview ());  
					}  
				}
				  
            }   


            //Load data from Local Device space. 
            public IEnumerator LoadfromLocal() {

                for (int i = 0; i < SenceTexturePath.Count; i++) {
                    string path = Path.GetFileName(SenceTexturePath[i]).Replace("?overwrite=true", "");
                    Debug.Log("Reload Path File Name " + path);
                    path = Application.persistentDataPath + "/Images/" + path;

                    Debug.Log("Local File reload" + path);
                    WWW reload = new WWW(path);
                    while (!reload.isDone)
                    {
                        if (LoadingSlider != null)
                        {
                            LoadingSlider.transform.GetChild(0).GetComponent<Text>().text = Path.GetFileName(SenceTexturePath[i]).Replace("?overwrite=true", "");
                            float progress = Mathf.Clamp01(reload.progress / .9f);
                            Debug.Log("Reloading : " + progress + " % ");
                        }
                        
                        yield return null;
                    }
                    yield return reload ;

                    if (reload.error == null)
                    {
                        Texture texture = reload.texture;
                        yield return texture;
                        Scenetexture.Add(texture);

                        if (ProjectLoaderImage != null)
                        {
                            ProjectLoaderImage.fillAmount += 1 / SenceTexturePath.Count;
                            ProjectLoaderImage.transform.GetChild(1).GetComponent<Text>().text += 1f;
                        }
                    }
                    if (SenceTexturePath.Count == Scenetexture.Count)
                    {
                        Debug.Log("Setup preview");

                        StartCoroutine(SetupPrview());
                    }
                }
            }

            // Destroy All the Gameobject from "NavigationCanvasPrv" : that is canvas in 3d space.
            // Then we load Prefab from Resource folder .
            // Scene Setup according to SceneData .
            // Start the VR mode.
            public IEnumerator SetupPrview()
            {
                  yield return 0;

                // Destroy All the Gameobject from "NavigationCanvasPrv" : that is canvas in 3d space.
                for (int i = 0; i < NavigationCanvasPrv.transform.childCount; i++)
                {
                    GameObject.Destroy(NavigationCanvasPrv.transform.GetChild(i).gameObject);
                }

                // Then we load Prefab from Resource folder .
                ScenePrafab = Resources.Load("PrefabForPreview/SceneObjectPrv 1") as GameObject;
                HotspotPrafab = Resources.Load("PrefabForPreview/ActionObjPrv") as GameObject;

                // Scene Setup according to SceneData .
                for (int i = 0; i < ScenesData.Length; i++)
                {
					Debug.Log ("inside setup preview");  
                    GameObject scen = GameObject.Instantiate(ScenePrafab);
                    
                    scen.transform.parent = NavigationCanvasPrv.transform;
                   
                    scen.gameObject.name = ScenesData[i].SceneName;
                     
                    scen.GetComponent<PreviewScene>().sceneName = ScenesData[i].SceneName;
                    scen.GetComponent<PreviewScene>().camPos = ScenesData[i].SceanPos;   
					scen.GetComponent<PreviewScene> ().video = ScenesData [i].Video; 
					scen.GetComponent<PreviewScene> ().initialTime = ScenesData [i].initialTime;
					scen.GetComponent<PreviewScene> ().finalTime = ScenesData [i].finalTime;
                    if (scen.GetComponent<PreviewScene>().video) {

                    }
                    scen.GetComponent<PreviewScene>().url = ScenesData[i].url;
                    if (ScenesData[i].Video == true)
                    {
                        scen.GetComponent<PreviewScene>().url = ScenesData[i].url;
                    }
					Debug.Log ("scene texture is "+Scenetexture [i]);   
					scen.GetComponent<PreviewScene> ().SceneTexture = Scenetexture [i];      
					  
					 
                    for (int j = 0; j < ScenesData[i].HotspotData.Length; j++) 
                    { 
                        GameObject hotsp = GameObject.Instantiate(HotspotPrafab);
                        hotsp.transform.parent = scen.transform;
                        hotsp.gameObject.name = ScenesData[i].HotspotData[j].hotspotName;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().hotspotName = ScenesData[i].HotspotData[j].hotspotName;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().NavigateToScene = ScenesData[i].HotspotData[j].NagivateToScene;    
                        hotsp.gameObject.GetComponent<PreviewHotspot>().UserActionName = ScenesData[i].HotspotData[j].UserActionName;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().ActionFunction = ScenesData[i].HotspotData[j].ActionFunction;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().ActionObject = ScenesData[i].HotspotData[j].ActionObject;
						hotsp.gameObject.GetComponent<PreviewHotspot>().rotationView = ScenesData[i].HotspotData[j].rotationView;  
                        hotsp.gameObject.GetComponent<PreviewHotspot>().Always = ScenesData[i].HotspotData[j].Always;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().VisibleWhen = ScenesData[i].HotspotData[j].VisibleWhen;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().UserActionList = ScenesData[i].HotspotData[j].UserActionList;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().ActionList = ScenesData[i].HotspotData[j].ActionList;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().LableBoxSize = ScenesData[i].HotspotData[j].LableBoxSize;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().UnityAssetpath = ScenesData[i].HotspotData[j].UnityAssetPath;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().UnityModleScale = ScenesData[i].HotspotData[j].UnityModelScale;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().UnityModleRot = ScenesData[i].HotspotData[j].UnityModelRot;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().MobileApp = MobileApp;
						hotsp.gameObject.GetComponent<PreviewHotspot> ().LableTitle = ScenesData [i].HotspotData [j].LableTitle;  
						hotsp.gameObject.GetComponent<PreviewHotspot> ().LableText = ScenesData [i].HotspotData [j].LableText;  
						hotsp.gameObject.GetComponent<PreviewHotspot> ().visibleAfter = ScenesData [i].HotspotData [j].visibleAfter;  
						hotsp.gameObject.GetComponent<PreviewHotspot> ().required = ScenesData [i].HotspotData [j].required;    
                        hotsp.gameObject.GetComponent<PreviewHotspot> ().ActionNumber = ScenesData [i].HotspotData [j].ActionNumber;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().Msg = ScenesData[i].HotspotData[j].Msg;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().GetMsg = ScenesData[i].HotspotData[j].GetMsg;  
						hotsp.gameObject.GetComponent<PreviewHotspot>().ActionButton = ScenesData[i].HotspotData[j].Action;
						hotsp.gameObject.GetComponent<PreviewHotspot>().Help = ScenesData[i].HotspotData[j].Help;   
                        hotsp.gameObject.GetComponent<PreviewHotspot>().MenuItemNames = ScenesData[i].HotspotData[j].MenuItemNames;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().MenuItemFunctions = ScenesData[i].HotspotData[j].MenuItemFunctions;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().MenuItemFunctionData = ScenesData[i].HotspotData[j].MenuItemFunctionData;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().MenuFunction = ScenesData[i].HotspotData[j].MenuFunction;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().MenuPosition = ScenesData[i].HotspotData[j].MenuPosition;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().MenuPosition = ScenesData[i].HotspotData[j].MenuPosition;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().Position = ScenesData[i].HotspotData[j].Position;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().VideoURL = ScenesData[i].HotspotData[j].VideoURL;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().Potrate = ScenesData[i].HotspotData[j].Potrate;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().Landscape = ScenesData[i].HotspotData[j].Landscape;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().FullSceen = ScenesData[i].HotspotData[j].FullSceen;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().ActionFunction = ScenesData[i].HotspotData[j].ActionFunction;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().TargetPosition = ScenesData[i].HotspotData[j].TargetPosition;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().ModelPosition = ScenesData[i].HotspotData[j].ModelPosition;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().ModelAssetPath = ScenesData[i].HotspotData[j].ModelAssetPath;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().Radius = ScenesData[i].HotspotData[j].Radius;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().Z_pos = ScenesData[i].HotspotData[j].Z_pos;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().Targetfunction = ScenesData[i].HotspotData[j].Targetfunction;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().TargetfunctionData = ScenesData[i].HotspotData[j].TargetfunctionData;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().UnityObjectInstance = ScenesData[i].HotspotData[j].UnityObjectInstance;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().ModelScale = ScenesData[i].HotspotData[j].ModelScale;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().ModelRotation = ScenesData[i].HotspotData[j].ModelRotation;
						hotsp.gameObject.GetComponent<PreviewHotspot>().transition = ScenesData[i].HotspotData[j].transition;     
						hotsp.gameObject.GetComponent<PreviewHotspot>().ImageTexture = ScenesData[i].HotspotData[j].ImageTexture;
                        hotsp.gameObject.GetComponent<PreviewHotspot>().ConnectTarget = ScenesData[i].HotspotData[j].ConnectTarget;

                        if (MobileApp) {
							hotsp.gameObject.GetComponent<PreviewHotspot> ().LoadingAsset = LoadingAsset;   
						}
                        Vector3 pos = new Vector3();    
                        if (ScenesData[i].HotspotData[j].Position.z == 0)
                        {
                            pos = new Vector3(ScenesData[i].HotspotData[j].Position.x, ScenesData[i].HotspotData[j].Position.y, 90);
                        } else
                        {
                            pos = ScenesData[i].HotspotData[j].Position;
                        }
                        hotsp.gameObject.GetComponent<PreviewHotspot>().Position = pos;

                    }
                } 
                
                for (int i = 0; i < NavigationCanvasPrv.transform.childCount; i++)
                {
                    Debug.Log(i); 
                    if (i == 0)
                    {
                         
                        Dome.GetComponent<MeshRenderer>().material.mainTexture = NavigationCanvasPrv.transform.GetChild(0).GetComponent<PreviewScene>().SceneTexture;

                    } else
                    {
                        NavigationCanvasPrv.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
                NavigationCanvasPrv.GetComponent<OnFirstScene>().enabled = true;
                if (MobileApp)
                {
                    WaitForSetupPanal = GameObject.Find("WaitForSetupPanal");
                    WaitForSetupPanal.SetActive(false);
                    StartCoroutine(loadDevice("Cardboard"));
                }
            }
            IEnumerator loadDevice(string newDevice)
            {
                UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
                yield return null;
                UnityEngine.XR.XRSettings.enabled = true;
            }
        }
    }
}