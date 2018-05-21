using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Text;
using UnityEngine;
using System.IO;
using System; 
using SFB;
using UnityEngine.EventSystems;  
  
namespace TriLib   
{
    namespace Samples  
    {
		public class PreviewHotspot : MonoBehaviour, TimeInputHandler , IPointerEnterHandler, IPointerExitHandler
        {
			  
            public string hotspotName;
            public string NavigateToScene;    
            public Vector3 Position; 
            public string ActionFunction;
            public string ActionObject;
            public string UserActionName;
            public string UserActionList;   
            public string ActionList;
            public Vector2 LableBoxSize;
            public bool Always;
            public bool VisibleWhen;
            public GameObject UnityObjectInstance;  
            public string UnityAssetpath;
            public Texture UnityModelTexture;
            public Vector3 UnityModleScale;
            public Vector3 UnityModleRot;
            public GameObject NavigationContainerPrv;
			public string LableText;
			public string LableTitle; 
			public Vector3 rotationView;   
			public GameObject finalPreviewCamera; 
            public GameObject currentObject;
            public bool Viewed;
            public bool MobileApp;  
			public bool visibleAfter;
			public bool required;
			public bool ActionButton;  
			public bool Help; 
            public int ActionNumber, tempIndex, makeThisVisible, tokenForClick;
            public bool Msg;
            public String GetMsg;    
			public int transition;      
			public Texture ImageTexture;  
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

            public GameObject LoadingAsset;   
			 
            // Use this for initialization
            void Start()
            {
                if (MobileApp)
                {

                    gameObject.GetComponent<BoxCollider>().enabled = true;
                }
                else {  

                    gameObject.GetComponent<BoxCollider>().enabled = false;
                }
				         
                // Hotspot setup according to type 
                NavigationContainerPrv = GameObject.Find("NavigationCanvasPrv");
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
					if (ActionFunction == "Play Move Clip")  
						ActionFunction = "MediaHotspot";      
                    if (ActionFunction == gameObject.transform.GetChild(i).gameObject.name)
                    {
                        gameObject.transform.GetChild(i).gameObject.SetActive(true);
                        if (gameObject.transform.GetChild(i).gameObject.name == "Replace with object")
                        {

                            if (UnityObjectInstance != null)
                            {  
                                StartCoroutine(LoadAsst(i));
                            }

                        }
						  
                        if (gameObject.transform.GetChild(i).gameObject.name == "Play Move Clip")
                        {

                            gameObject.transform.GetChild(i).gameObject.GetComponent<VideoPlayer>().url = ActionObject;

                        }
                        if (gameObject.transform.GetChild(i).gameObject.name == "Display text lable")
                        {

                            gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = ActionObject;
							if(ActionButton) 
								gameObject.transform.GetChild (i).gameObject.transform.GetChild (1).GetComponent<Image> ().color = Color.green; 
							if(Help)
								gameObject.transform.GetChild (i).gameObject.transform.GetChild (1).GetComponent<Image> ().color = new Color(0f,223f,255f);    
							  
                            //gameObject.transform.GetChild (i).GetComponent<RectTransform> ().sizeDelta = LableBoxSize;
                            //gameObject.transform.GetChild (i).gameObject.transform.GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (LableBoxSize.x * 2f, LableBoxSize.y * 2f);
                        }
                        if (gameObject.transform.GetChild(i).gameObject.name == "Navigate to")
                        {
                            Debug.Log("enter on Navigate to " + gameObject.transform.GetChild(i).gameObject.name);
                            NavigateToScene = ActionObject;
                            gameObject.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = UserActionName;
                            gameObject.transform.GetChild(i).GetComponent<BoxCollider>().size = new Vector3(5f, 5f, 5f);
							if(ActionButton) 
								gameObject.transform.GetChild (i).gameObject.transform.GetChild (0).GetComponent<Image> ().color = Color.green;    
							
                        }
                        if (gameObject.transform.GetChild(i).gameObject.name == "MenuHotspot")
                        {
							  
                            NavigateToScene = MenuFunction;  
                            Position = MenuPosition;

                            gameObject.transform.GetChild(i).gameObject.transform.localPosition = Vector3.zero;
                            gameObject.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(1f, 1f);
                            for (int k = 0; k < MenuItemNames.Length; k++)
                            {

                                gameObject.transform.GetChild(i).transform.GetChild(0).GetChild(0).gameObject.transform.GetComponent<MenuHotspot>().ButtonLableName.Add(MenuItemNames[k]);
                                gameObject.transform.GetChild(i).transform.GetChild(0).GetChild(0).gameObject.transform.GetComponent<MenuHotspot>().ButtonFunctionData.Add(MenuItemFunctionData[k]);
                                gameObject.transform.GetChild(i).transform.GetChild(0).GetChild(0).gameObject.transform.GetComponent<MenuHotspot>().ButtonFunction.Add(MenuItemFunctions[k]);
                                gameObject.transform.GetChild(i).transform.GetChild(0).GetChild(0).gameObject.transform.GetComponent<MenuHotspot>().PreviewHotspotGameobjectName = transform.gameObject.name;
                                // gameObject.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = UserActionName;
								   
                            }
                            gameObject.transform.GetChild(i).transform.GetChild(0).GetChild(0).gameObject.transform.GetComponent<MenuHotspot>().InitialiesItems();
                            Debug.Log(gameObject.transform.GetChild(i).gameObject.name);
                            gameObject.transform.parent = gameObject.transform.parent.gameObject.transform.parent;
                            gameObject.AddComponent<AlwaysActive>();
                            gameObject.transform.localPosition = Vector3.zero;
                        }
                        if (gameObject.transform.GetChild(i).gameObject.name == "MediaHotspot")
                        {    
                            gameObject.transform.GetChild(i).gameObject.GetComponent<MediaHotspot>().videoURL = VideoURL;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<MediaHotspot>().Potrate = Potrate;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<MediaHotspot>().landscape = Landscape;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<MediaHotspot>().fullScreen = FullSceen;
                            gameObject.transform.GetChild(i).gameObject.transform.position = Position;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<MediaHotspot>().Preview = true;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<MediaHotspot>().changeAspetRatio = false;
                            currentObject = gameObject.transform.GetChild(i).gameObject;  
                            currentObject.SetActive(true);
							currentObject.gameObject.transform.GetChild (0).transform.GetChild (1).transform.GetChild (2).GetComponent<MeshRenderer> ().material.mainTexture = ImageTexture;
							if(ActionButton) 
							gameObject.transform.GetChild (i).gameObject.transform.GetChild (0).GetComponent<Image> ().color = Color.green; 
							if(Help)
								gameObject.transform.GetChild (i).gameObject.transform.GetChild (0).GetComponent<Image> ().color = new Color(0f,223f,255f);  
							
                        }    

                        if (gameObject.transform.GetChild(i).gameObject.name == "ObjectHotsot")
                        {

                            gameObject.transform.GetChild(i).gameObject.GetComponent<ObjectHotsot>().ModelPosition = ModelPosition;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<ObjectHotsot>().ModelRotation = ModelRotation;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<ObjectHotsot>().ModelAssetPath = ModelAssetPath;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<ObjectHotsot>().UnityObjectInstance = UnityObjectInstance;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<ObjectHotsot>().Radius = Radius;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<ObjectHotsot>().ModelScale = ModelScale;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<ObjectHotsot>().ConnectTarget = ConnectTarget;
                            if (UnityObjectInstance != null)
                            {
                                gameObject.transform.GetChild(i).gameObject.GetComponent<ObjectHotsot>().SetupModel();
                            }
                             Position = ModelPosition;
                            gameObject.transform.localPosition = Position;
                            gameObject.transform.localEulerAngles = ModelRotation;


                        }
                        if (gameObject.transform.GetChild(i).gameObject.name == "Target")
                        {

                            gameObject.transform.GetChild(i).gameObject.GetComponent<TargetPreviewFunction>().TargetPosition = TargetPosition;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<TargetPreviewFunction>().TargetFunctionType = Targetfunction;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<TargetPreviewFunction>().TargetFunctionData = TargetfunctionData;
                            gameObject.transform.GetChild(i).gameObject.GetComponent<TargetPreviewFunction>().Z_pos = Z_pos;
                              


                        }
                    }
                    else if (Msg) {  
                        if (gameObject.transform.GetChild(i).gameObject.name == "DisplayMsg") {

                            gameObject.transform.GetChild(i).gameObject.SetActive(true);
                            
                        }
                    }
                    else
                    {
                        gameObject.transform.GetChild(i).gameObject.SetActive(false);
                    }

					  
                }
                if (VisibleWhen)  
                {
                    for (int i = 0; i < gameObject.transform.childCount; i++)
                    {
                        if (gameObject.transform.GetChild(i).gameObject.activeInHierarchy)
                        {
                            currentObject = gameObject.transform.GetChild(i).gameObject;
                            currentObject.SetActive(false);
                        }
                    }
                }  

                //	gameObject.transform.GetChild (0).transform.localPosition = Position;

                if (visibleAfter == true)
                {

                    for (int i = 0; i < gameObject.transform.parent.childCount; i++)
                    {
                        if (gameObject.transform.parent.GetChild(i).gameObject == gameObject)
                        {
                            tempIndex = i;
                        }
                    }
                    gameObject.transform.parent.GetChild(ActionNumber - 1).GetComponent<PreviewHotspot>().VisibleAfterDataScript(tempIndex);
                    gameObject.SetActive(false);
                }

            }

            // Load asseet 
            IEnumerator LoadAsst(int i)
            {
                string path = "";
                var assetLoader = new AssetLoader();
                if (!MobileApp)
                {
                    path = UnityAssetpath.Replace("file:///", "");
                    path = path.Replace("%20", " ");
                    Debug.Log("obj is found " + path);
                    var loadGameObject = assetLoader.LoadFromFile(path);
                    Debug.Log(Application.persistentDataPath + "/" + ActionObject);
                    yield return loadGameObject;

                    UnityObjectInstance = loadGameObject;
                }
                else
                {
                    WWW www = new WWW(UnityAssetpath); 
                    yield return www;
                    File.WriteAllBytes(Application.persistentDataPath + "/" + ActionObject, www.bytes);

                   

                    var loadGameObject = assetLoader.LoadFromFile(Application.persistentDataPath + "/" + ActionObject);
                    Debug.Log(Application.persistentDataPath + "/" + ActionObject);
                    yield return loadGameObject;

                UnityObjectInstance = loadGameObject;
                }

                

                GameObject tem = new GameObject();
                GameObject UnityModel = GameObject.Instantiate(tem);
                UnityModel.gameObject.name = "UnityModel";
                UnityModel.transform.parent = gameObject.transform.GetChild(i).gameObject.transform;
                GameObject UnityObject = GameObject.Instantiate(UnityObjectInstance);
                UnityObject.transform.parent = UnityModel.transform;
                if (!MobileApp)
                {
                    UnityModel.transform.localScale = new Vector3(1, 1, 1);
                    UnityModel.transform.localPosition = new Vector3(0, 0, 0);
                    UnityObject.transform.localPosition = Vector3.zero;
                    UnityObject.transform.localScale = new Vector3(UnityModleScale.x, UnityModleScale.y, UnityModleScale.z);
                    UnityObject.transform.localEulerAngles = new Vector3(UnityModleRot.x, UnityModleRot.y, UnityModleRot.z);
                    UnityObject.SetActive(true);

                    GameObject.Find("Unity3D_Project").gameObject.GetComponent<HideTheObjects>().hideObjects();
                    //  UnityObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                }
                else {
                    UnityModel.transform.localScale = new Vector3(1, 1, 1);
                    UnityModel.transform.localPosition = new Vector3(0, 0, 0);
                    UnityObject.transform.localPosition = Vector3.zero;
                    UnityObject.transform.localScale = new Vector3(UnityModleScale.x, UnityModleScale.y, UnityModleScale.z);
                    UnityObject.transform.localEulerAngles = new Vector3(UnityModleRot.x, UnityModleRot.y, UnityModleRot.z);
                    UnityObject.SetActive(true);

                    GameObject.Find("Unity3D_Project").gameObject.GetComponent<HideTheObjects>().hideObjects();
                    //  UnityObject.transform.GetChild(0).transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                }
              
            }

            // Update is called once per frame
            void Update()
            {
                if (Position.z != 0)
                {
                    gameObject.transform.localPosition = Position;
                }
                else {

                    gameObject.transform.localPosition = new Vector3(0,0,90f);
                }
            

                if (currentObject != null)
                {
                    if (!currentObject.activeInHierarchy)
                    {

                        for (int i = 0; i < gameObject.transform.parent.transform.childCount; i++)
                        {
                            if (UserActionList == gameObject.transform.parent.transform.GetChild(i).GetComponent<PreviewHotspot>().UserActionName)
                            {
                                if (gameObject.transform.parent.transform.GetChild(i).GetComponent<PreviewHotspot>().Viewed)
                                {
                                    currentObject.SetActive(true);
                                }
                            }

                        }
                    }
                }
            }

            // Timmer of Hotspot Click.
            public void HandleTimedInput()
            { 
                ClickOnHotspot();   
            }
           


            public void VisibleAfterDataScript(int temp)
            {
                makeThisVisible = temp;
                tokenForClick = 1;
            }


            public void OnPointerEnter(PointerEventData eventData)
			{
				Debug.Log("Pointer Enter");
               
			}  

			public void OnPointerExit(PointerEventData eventData)
			{
				Debug.Log("Pointer Exit");
                RaycastingOnDome.CurrentHoldTime = 0;
            }

			public void SendMessageTwo(){ 
				Debug.Log("Pointer Exit");    
			}

            

            public void ClickOnHotspot()  
            {
                string NavTo = NavigateToScene;   
                Debug.Log("OnCkick is call" + NavTo); 
                if (tokenForClick == 1)
                {
                    gameObject.transform.parent.GetChild(makeThisVisible).gameObject.SetActive(true);
                }
                if (ActionFunction == "Navigate to")
                    for (int i = 0; i < NavigationContainerPrv.transform.childCount; i++)
                    
					{
                        
                        Debug.Log(NavigationContainerPrv.transform.GetChild(i).gameObject.name + " :: " + NavTo);
                        Debug.Log(NavTo);
                        if (NavigationContainerPrv.transform.GetChild(i).gameObject.name == NavTo)
                        {
                            if (NavigationContainerPrv.transform.GetChild(i).GetComponent<PreviewScene>())
                            {
                                NavigationContainerPrv.transform.GetChild(i).GetComponent<PreviewScene>().transition = transition;
                                NavigationContainerPrv.transform.GetChild(i).gameObject.SetActive(true);
                                //set camera rot eulervalues to given rotationView    
                                NavigationContainerPrv.transform.GetChild(i).gameObject.GetComponent<PreviewScene>().camPos = rotationView;


                            }
                        }
                        else
                        {
                            Debug.Log(NavigationContainerPrv.transform.GetChild(i).gameObject.name + "**");
                            if (NavigationContainerPrv.transform.GetChild(i).GetComponent<PreviewScene>())
                            {
                                NavigationContainerPrv.transform.GetChild(i).gameObject.SetActive(false);
                            }
                        }

                    } 

                if (ActionFunction == "Play Move Clip")  
                {

                    currentObject.GetComponent<MediaPlaypause>().PauseAndPlatMediaFile();

                    if (currentObject.GetComponent<MediaPlaypause>().player.frame == (long)currentObject.GetComponent<MediaPlaypause>().player.frameCount)
                    {
                        Viewed = true;
                    }
                }
                if (ActionFunction == "MenuHotspot")
                {

                    for (int i = 0; i < NavigationContainerPrv.transform.childCount; i++)
                    {
						if (NavigationContainerPrv.transform.GetChild (i).GetComponent<PreviewScene> ()) {
                        Debug.Log(NavigationContainerPrv.transform.GetChild(i).gameObject.name + " :: " + NavTo);
                        Debug.Log(NavTo);
							if (NavTo == NavigationContainerPrv.transform.GetChild (i).GetComponent<PreviewScene> ().sceneName) {
								NavigationContainerPrv.transform.GetChild (i).gameObject.SetActive (true);
                                if (NavigationContainerPrv.GetComponent<OnFirstScene>().MH != null)
                                {
                                    NavigationContainerPrv.GetComponent<OnFirstScene>().MH.ClickOnVisible();
                                }
							} else {
								NavigationContainerPrv.transform.GetChild (i).gameObject.SetActive (false);
							}
						}  
                    }


                }

                if (ActionFunction == "MediaHotspot")
                {
					Debug.Log ("!!!###"); 
                    currentObject.gameObject.GetComponent<MediaHotspot>().videoURL = VideoURL;
                    currentObject.gameObject.GetComponent<MediaHotspot>().Potrate = Potrate;
                    currentObject.gameObject.GetComponent<MediaHotspot>().landscape = Landscape;
                    currentObject.gameObject.GetComponent<MediaHotspot>().fullScreen = FullSceen;
                  //  currentObject.SetActive(true);
					Debug.Log ("!!!###");    
                    currentObject.gameObject.GetComponent<MediaHotspot>().OnClickHide(); 
					Debug.Log ("name " + currentObject.gameObject.transform.GetChild (0).transform.GetChild (1).transform.GetChild (2).name);   
                }


            }
        }
    }
}
