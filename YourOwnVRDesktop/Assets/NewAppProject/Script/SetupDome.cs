using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SetupDome : MonoBehaviour
{
    public GameObject HotspotContainer;
    public List<GameObject> EditScenes = new List<GameObject>();
    public InputField Scene_Name_Input;
    public string Scene_Name;
    public GameObject SelectFile;
    public GameObject SceneDragPanel;
    public GameObject DomeCamera;
    public GameObject SceneObjectPrb;

    public TriLib.Samples.PreviewLoader PreviewLoader;
    public GameObject PrevireObject;
    public GameObject Hotspottemplet;
    public GameObject HotspotTemplete;
    public GameObject CenteralPanal;
    public HandleCursor cursor;
    string ExtentionName;
    public string Url;

    public int buttonID;
    public static int ButtonId;
    public static List<string> UserActionName = new List<string>();
    public static List<string> ActionFunction = new List<string>();
    public static List<string> ActionObject = new List<string>();

    public static GameObject SelectedHotspot;
    public GameObject GlobalMenuHotspot;

    public GameObject selectedHotstop;
    public List<string> userActionName = new List<string>();
    public List<string> actionFunction = new List<string>();
    public List<string> actionObject = new List<string>();

    public List<bool> VisibleWhen = new List<bool>();
    public List<bool> Always = new List<bool>();
    public GameObject UnityobjectInstance;
    VideoPlayer player;
    public GameObject[] AllHotspotTemplets;
    public AddMenuHotspot AddMenuHotspotRF;
    public GameObject CenterBottomPanal;
    public static bool Windows;
    public static bool Mac;
    public bool windows;
    public bool mac;
    // Use this for initialization
    void Start()
    {
        Scene_Name_Input = GameObject.Find("Scene_Name_Input").GetComponent<InputField>();
        player = gameObject.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame 
    void Update()
    {

        // Staice data into Public variable. 
        selectedHotstop = SelectedHotspot;
        userActionName = UserActionName;
        actionObject = ActionObject;
        actionFunction = ActionFunction;
        buttonID = ButtonId;

        Windows = windows;
        Mac = mac;
        if (HotspotContainer == null)
        {

            HotspotContainer = GameObject.Find("NavigationCanvas");
        }
        if (GlobalMenuHotspot != null)
        {
            GlobalMenuHotspot.SetActive(true);
        }
    }

    // If file is not Edited then create a scene and if file is Edited then put the Scene data to dome .
    private void NewSceneAndTextureOnDomeAndNameFile()
    {
        /*		for (int i = 0; i < HotspotContainer.transform.childCount; i++) {
                    if (HotspotContainer.transform.GetChild (i).gameObject.transform.childCount == 0) {
                        GameObject.Destroy (HotspotContainer.transform.GetChild (i).gameObject);
                    }
                }  

            */
        if (SelectFile.GetComponent<SelectFiles>().EditScene == false)
        {

            EditScenes.Add(SelectFile);
            GameObject sceneObj = GameObject.Instantiate(SceneObjectPrb);
            sceneObj.transform.parent = HotspotContainer.transform;
            sceneObj.transform.position = Vector3.zero;
            sceneObj.name = SelectFile.transform.GetChild(0).GetComponent<InputField>().text;
            SelectFile.transform.GetComponent<SelectFiles>().FileSceneName = SelectFile.transform.GetChild(0).GetComponent<InputField>().text;
            sceneObj.GetComponent<SceneProperties>().SceneTexture = SelectFile.GetComponent<RawImage>().mainTexture;
            sceneObj.GetComponent<SceneProperties>().SceneTexturePath = SelectFile.GetComponent<SelectFiles>().scene.ImageURLPath;
            if (SetupDome.Windows)
            {
                Url = Url.Replace("file:///", "");
                Url = Url.Replace("file://", "");
                sceneObj.GetComponent<SceneProperties>().url = Url;

            }
            else
            {
                sceneObj.GetComponent<SceneProperties>().url = Url;
            }
            Scene_Name_Input.text = SelectFile.transform.GetChild(0).GetComponent<InputField>().text;
            sceneObj.GetComponent<SceneProperties>().SceneName = Scene_Name_Input.text;
            Hotspottemplet.SetActive(true);
         
        }
        Debug.Log(SelectFile.GetComponent<SelectFiles>().FileSceneName);
        Scene_Name_Input.text = SelectFile.GetComponent<SelectFiles>().FileSceneName;
        gameObject.transform.GetComponent<MeshRenderer>().material.mainTexture = SelectFile.GetComponent<RawImage>().mainTexture;

        SceneDragPanel.SetActive(false);
        gameObject.transform.localScale = new Vector3(5, 5, 5);
        DomeCamera.SetActive(true);
        DomeCamera.transform.localPosition = Vector3.zero;
        DomeCamera.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    // Hide other scene and hotspot which are not selected.
    private void HideOtherSceneHotspots()
    {
        EnableMenuTemplet();
        for (int i = 0; i < HotspotContainer.transform.childCount; i++)
        {
            Debug.Log(HotspotContainer.transform.GetChild(i).gameObject.name + " :: " + SelectFile.GetComponent<SelectFiles>().FileSceneName);
            if (HotspotContainer.transform.GetChild(i).gameObject.name != SelectFile.GetComponent<SelectFiles>().FileSceneName)
            {
                HotspotContainer.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }

    // Visible the Selected scene .
    private void VisibleEditedFile()
    {

        if (SelectFile.GetComponent<SelectFiles>().EditScene == true)
        {
            for (int i = 0; i < HotspotContainer.transform.childCount; i++)
            {
                Debug.Log("Edited File ");
                if (HotspotContainer.transform.GetChild(i).gameObject.name == SelectFile.GetComponent<SelectFiles>().FileSceneName)
                {
                    Debug.Log(HotspotContainer.transform.GetChild(i).gameObject.name + "  " + Scene_Name_Input.text);
                    HotspotContainer.transform.GetChild(i).gameObject.SetActive(true);
                    DomeCamera.transform.localPosition = Vector3.zero;
                    DomeCamera.transform.localRotation = new Quaternion(0, 0, 0, 0);
                }
            }
        }
    }

    // Scene setup
    public void DropToScene()
    {
        Debug.Log("Drop into Scene");
        CenterBottomPanal.SetActive(true);
        cursor.SetMouse();

        player.enabled = false;

        Hotspottemplet.transform.GetChild(0).gameObject.SetActive(false);

        NewSceneAndTextureOnDomeAndNameFile();

        HideOtherSceneHotspots();

        VisibleEditedFile();



    }


    public void setVideoOnDome(string mediaFile)
    {

        player.enabled = true;
        Url = mediaFile;
        if (SetupDome.Windows)
        {
            mediaFile = mediaFile.Replace("file:///", "");
            mediaFile  = mediaFile.Replace("file://", "");
            player.url = mediaFile;
        }
        else
        {
            player.url = Url;
        }
        NewSceneAndTextureOnDomeAndNameFile();
        HideOtherSceneHotspots();
        VisibleEditedFile();

    }


    // Get all scenes and hotspot data into SceneData
    public void DataOnPreviewLoader()
    {
        if (GlobalMenuHotspot != null)
        {
            for (int v = 0; v < HotspotContainer.transform.childCount; v++)
            {
                if (GlobalMenuHotspot.GetComponent<MenuHotspot>().BirthScene == HotspotContainer.transform.GetChild(v).gameObject.name)
                {
                    GlobalMenuHotspot.transform.parent = HotspotContainer.transform.GetChild(v).gameObject.transform;
                    
                }
            }
        }

        if (HotspotContainer.transform.childCount != 0)
        {
            Debug.Log("Enter on the Loader");
            PreviewLoader.SenceTexturePath.Clear();
            PreviewLoader.PreSceneTexturepath.Clear();
            PreviewLoader.PostSceneTexturepath.Clear();
            PreviewLoader.Scenetexture.Clear();

            PreviewLoader.ScenesData = new TriLib.Samples.PreviewLoader.container[0];
            PreviewLoader.ScenesData = new TriLib.Samples.PreviewLoader.container[HotspotContainer.transform.childCount];
            for (int i = 0; i < HotspotContainer.transform.childCount; i++)
            {
                if (HotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>()) { 
                PreviewLoader.SenceTexturePath.Add(HotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>().SceneTexturePath);
                //PreviewLoader.PreloadedTexture.Add (HotspotContainer.transform.GetChild (i).GetComponent<SceneProperties> ().SceneTexture);
                }
            }

            for (int i = 0; i < PreviewLoader.SenceTexturePath.Count; i++)
            {

                PreviewLoader.ScenesData[i].SceneName = HotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>().SceneName;
                PreviewLoader.ScenesData[i].Video = HotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>().video;
                PreviewLoader.ScenesData[i].url = HotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>().url;
                PreviewLoader.ScenesData[i].SceanPos = HotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>().CamPos;
                PreviewLoader.ScenesData[i].initialTime = HotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>().initialTime;
                PreviewLoader.ScenesData[i].finalTime = HotspotContainer.transform.GetChild(i).GetComponent<SceneProperties>().finalTime;

                PreviewLoader.ScenesData[i].HotspotData = new TriLib.Samples.PreviewLoader.hotspotContainer[HotspotContainer.transform.GetChild(i).childCount];

                Debug.Log("Count 2 is " + HotspotContainer.transform.GetChild(i).childCount);
                for (int j = 0; j < HotspotContainer.transform.GetChild(i).childCount; j++)
                {

                    Debug.Log(HotspotContainer.transform.GetChild(i).childCount);
                    //	PreviewLoader.ScenesData [i].HotspotData = new PreviewLoader.hotspotContainer[ActiveHotspotContainer.transform.GetChild (i).childCount];
                    Debug.Log(PreviewLoader.ScenesData[i].HotspotData.Length);

                    //previewLoader.ScenesData [i].HotspotData = new PreviewLoader.hotspotContainer[ActiveHotspotContainer.transform.GetChild (i).childCount];
                    PreviewLoader.ScenesData[i].HotspotData[j].hotspotName = HotspotContainer.transform.GetChild(i).GetChild(j).gameObject.name;
                    if (HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewHotspot>())
                    {
                        PreviewLoader.ScenesData[i].HotspotData[j].NagivateToScene = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewHotspot>().NavigateToScene;
                        PreviewLoader.ScenesData[i].HotspotData[j].Position = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewHotspot>().Position;
                    }

                    if (HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>())
                    {

                        PreviewLoader.ScenesData[i].HotspotData[j].NagivateToScene = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().NavigateToScene;
                        PreviewLoader.ScenesData[i].HotspotData[j].Position = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().Position;
                        PreviewLoader.ScenesData[i].HotspotData[j].ActionFunction = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().ActionFunction;
                        PreviewLoader.ScenesData[i].HotspotData[j].ActionObject = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().ActionObject;
                        PreviewLoader.ScenesData[i].HotspotData[j].UserActionName = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().UserActionName;
                        PreviewLoader.ScenesData[i].HotspotData[j].Always = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().ActionAlways;
                        PreviewLoader.ScenesData[i].HotspotData[j].VisibleWhen = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().ActionVisibleWhen;
                        PreviewLoader.ScenesData[i].HotspotData[j].UnityObjectInstance = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().UnityObjectInstance;
                        PreviewLoader.ScenesData[i].HotspotData[j].ActionList = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().ActionList;
                        PreviewLoader.ScenesData[i].HotspotData[j].UserActionList = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().UserActionList;
                        PreviewLoader.ScenesData[i].HotspotData[j].LableBoxSize = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().LableBoxSize;
                        PreviewLoader.ScenesData[i].HotspotData[j].UnityAssetPath = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().UnityObjectPaths;
                        PreviewLoader.ScenesData[i].HotspotData[j].UnityModelScale = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().UnityModelScale;
                        PreviewLoader.ScenesData[i].HotspotData[j].UnityModelRot = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().UnityModelRot;
                        PreviewLoader.ScenesData[i].HotspotData[j].UnityModelTexture = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().UnityModelTexture;
                        PreviewLoader.ScenesData[i].HotspotData[j].LableText = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().LableText;
                        PreviewLoader.ScenesData[i].HotspotData[j].LableTitle = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().LableTitle;
                        PreviewLoader.ScenesData[i].HotspotData[j].visibleAfter = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().visibleAfter;
                        PreviewLoader.ScenesData[i].HotspotData[j].required = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().required;
                        PreviewLoader.ScenesData[i].HotspotData[j].ActionNumber = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().ActionNumber;
                        PreviewLoader.ScenesData[i].HotspotData[j].Msg = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().Msg;
                        PreviewLoader.ScenesData[i].HotspotData[j].GetMsg = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().GetMgs;
                        PreviewLoader.ScenesData[i].HotspotData[j].VideoURL = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<NewActionHotspot>().videoURL;
                        PreviewLoader.ScenesData[i].HotspotData[j].Landscape = true;
                        PreviewLoader.ScenesData[i].HotspotData[j].Action = true;
                    }

                    if (HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<navigateActionHotspot>())
                    {

                        PreviewLoader.ScenesData[i].HotspotData[j].NagivateToScene = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<navigateActionHotspot>().NavigateToScene;
                        PreviewLoader.ScenesData[i].HotspotData[j].Position = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<navigateActionHotspot>().Position;
                        PreviewLoader.ScenesData[i].HotspotData[j].ActionFunction = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<navigateActionHotspot>().ActionFunction;
                        PreviewLoader.ScenesData[i].HotspotData[j].ActionObject = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<navigateActionHotspot>().ActionObject;
                        PreviewLoader.ScenesData[i].HotspotData[j].rotationView = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<navigateActionHotspot>().rotationView;
                        PreviewLoader.ScenesData[i].HotspotData[j].UserActionName = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<navigateActionHotspot>().UserActionName;
                        PreviewLoader.ScenesData[i].HotspotData[j].Always = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<navigateActionHotspot>().ActionAlways;
                        PreviewLoader.ScenesData[i].HotspotData[j].VisibleWhen = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<navigateActionHotspot>().ActionVisibleWhen;
                        PreviewLoader.ScenesData[i].HotspotData[j].ActionList = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<navigateActionHotspot>().ActionList;
                        PreviewLoader.ScenesData[i].HotspotData[j].UserActionList = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<navigateActionHotspot>().UserActionList;
                        PreviewLoader.ScenesData[i].HotspotData[j].transition = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<navigateActionHotspot>().transition;
                    }

                    if (HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<textActionHotspot>())
                    {

                        PreviewLoader.ScenesData[i].HotspotData[j].NagivateToScene = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<textActionHotspot>().NavigateToScene;
                        PreviewLoader.ScenesData[i].HotspotData[j].Position = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<textActionHotspot>().Position;
                        PreviewLoader.ScenesData[i].HotspotData[j].ActionFunction = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<textActionHotspot>().ActionFunction;
                        PreviewLoader.ScenesData[i].HotspotData[j].ActionObject = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<textActionHotspot>().ActionObject;
                        PreviewLoader.ScenesData[i].HotspotData[j].UserActionName = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<textActionHotspot>().UserActionName;
                        PreviewLoader.ScenesData[i].HotspotData[j].Always = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<textActionHotspot>().Always;
                        PreviewLoader.ScenesData[i].HotspotData[j].VisibleWhen = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<textActionHotspot>().ActionVisibleWhen;
                        PreviewLoader.ScenesData[i].HotspotData[j].ActionList = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<textActionHotspot>().ActionList;
                        PreviewLoader.ScenesData[i].HotspotData[j].UserActionList = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<textActionHotspot>().UserActionList;
                        PreviewLoader.ScenesData[i].HotspotData[j].LableBoxSize = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<textActionHotspot>().LableBoxSize;
                        PreviewLoader.ScenesData[i].HotspotData[j].LableText = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<textActionHotspot>().LableText;
                        PreviewLoader.ScenesData[i].HotspotData[j].LableTitle = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<textActionHotspot>().LableTitle;
                    }
                    if (HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MenuHotspot>())
                    {
                        PreviewLoader.ScenesData[i].HotspotData[j].MenuFunction = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MenuHotspot>().MenuFunction;
                        PreviewLoader.ScenesData[i].HotspotData[j].MenuPosition = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MenuHotspot>().MenuPosition;
                        PreviewLoader.ScenesData[i].HotspotData[j].MenuItemNames = new string[HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MenuHotspot>().ButtonLableName.Count];
                        PreviewLoader.ScenesData[i].HotspotData[j].MenuItemFunctions = new string[HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MenuHotspot>().ButtonFunction.Count];
                        PreviewLoader.ScenesData[i].HotspotData[j].MenuItemFunctionData = new string[HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MenuHotspot>().ButtonFunctionData.Count];
                        PreviewLoader.ScenesData[i].HotspotData[j].ActionFunction = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MenuHotspot>().MenuFunction;
                        for (int k = 0; k < HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MenuHotspot>().ButtonLableName.Count; k++)
                        {
                            Debug.Log(HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MenuHotspot>().ButtonLableName.Count);
                            Debug.Log(HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MenuHotspot>().ButtonLableName[k]);
                            PreviewLoader.ScenesData[i].HotspotData[j].MenuItemNames[k] = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MenuHotspot>().ButtonLableName[k];
                            PreviewLoader.ScenesData[i].HotspotData[j].MenuItemFunctions[k] = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MenuHotspot>().ButtonFunction[k];
                            PreviewLoader.ScenesData[i].HotspotData[j].MenuItemFunctionData[k] = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MenuHotspot>().ButtonFunctionData[k];

                        }

                    }

                    if (HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MediaHotspot>())
                    {

                        PreviewLoader.ScenesData[i].HotspotData[j].Position = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MediaHotspot>().Position;
                        PreviewLoader.ScenesData[i].HotspotData[j].VideoURL = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MediaHotspot>().videoURL;
                        PreviewLoader.ScenesData[i].HotspotData[j].Potrate = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MediaHotspot>().Potrate;
                        PreviewLoader.ScenesData[i].HotspotData[j].Landscape = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MediaHotspot>().landscape;
                        PreviewLoader.ScenesData[i].HotspotData[j].FullSceen = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MediaHotspot>().fullScreen;

                        PreviewLoader.ScenesData[i].HotspotData[j].ActionFunction = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MediaHotspot>().ActionFunction;
                        PreviewLoader.ScenesData[i].HotspotData[j].ImageTexture = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<MediaHotspot>().ImageTexture;

                    }

                    if (HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<helpActionHotspot>())
                    {

                        PreviewLoader.ScenesData[i].HotspotData[j].Position = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<helpActionHotspot>().Position;
                        PreviewLoader.ScenesData[i].HotspotData[j].VideoURL = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<helpActionHotspot>().videoURL;
                        PreviewLoader.ScenesData[i].HotspotData[j].LableText = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<helpActionHotspot>().LableText;
                        PreviewLoader.ScenesData[i].HotspotData[j].LableTitle = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<helpActionHotspot>().LableTitle;
                        PreviewLoader.ScenesData[i].HotspotData[j].ActionFunction = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<helpActionHotspot>().ActionFunction;
                        PreviewLoader.ScenesData[i].HotspotData[j].Landscape = true;
                        PreviewLoader.ScenesData[i].HotspotData[j].Help = true;
                    }

                    if (HotspotContainer.transform.GetChild(i).GetChild(j).transform.GetComponent<ObjectHotsot>())
                    {

                        PreviewLoader.ScenesData[i].HotspotData[j].TargetPosition = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<ObjectHotsot>().TargetPosition;
                        PreviewLoader.ScenesData[i].HotspotData[j].ModelPosition = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<ObjectHotsot>().ModelPosition;
                        PreviewLoader.ScenesData[i].HotspotData[j].UnityObjectInstance = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<ObjectHotsot>().UnityObjectInstance;
                        PreviewLoader.ScenesData[i].HotspotData[j].Targetfunction = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<ObjectHotsot>().TargetFunctionType;
                        PreviewLoader.ScenesData[i].HotspotData[j].TargetfunctionData = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<ObjectHotsot>().TargetFunctionData;
                        PreviewLoader.ScenesData[i].HotspotData[j].ModelAssetPath = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<ObjectHotsot>().ModelAssetPath;
                        PreviewLoader.ScenesData[i].HotspotData[j].Z_pos = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<ObjectHotsot>().Z_pos;
                        PreviewLoader.ScenesData[i].HotspotData[j].Radius = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<ObjectHotsot>().Radius;
                        PreviewLoader.ScenesData[i].HotspotData[j].ActionFunction = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<ObjectHotsot>().ActionFunction;
                        PreviewLoader.ScenesData[i].HotspotData[j].ModelScale = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<ObjectHotsot>().ModelScale;
                        PreviewLoader.ScenesData[i].HotspotData[j].ModelRotation = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<ObjectHotsot>().ModelRotation;
                        PreviewLoader.ScenesData[i].HotspotData[j].ConnectTarget = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<ObjectHotsot>().ConnectTarget;
                    }
                    if (HotspotContainer.transform.GetChild(i).GetChild(j).transform.GetComponent<TargetFunction>())
                    {

                        PreviewLoader.ScenesData[i].HotspotData[j].ActionFunction = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<TargetFunction>().ActionFunction;
                        PreviewLoader.ScenesData[i].HotspotData[j].TargetfunctionData = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<TargetFunction>().TargetFunctionObject;
                        PreviewLoader.ScenesData[i].HotspotData[j].Targetfunction = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<TargetFunction>().SelectedTargetFunction;
                        PreviewLoader.ScenesData[i].HotspotData[j].Position = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<TargetFunction>().position;
                        PreviewLoader.ScenesData[i].HotspotData[j].Z_pos = HotspotContainer.transform.GetChild(i).GetChild(j).GetComponent<TargetFunction>().Z_pos;
                    }
                }

            }

        }

        PreviewLoader.Gettexture();
        //	PreviewLoader.SetupPrview (); 
        HotspotContainer.SetActive(false);
        PrevireObject.SetActive(true);
        PrevireObject.transform.GetChild(1).GetChild(1).GetComponent<RaycastingOnDome>().enabled = true;
        PrevireObject.transform.GetChild(1).GetChild(1).GetComponent<MouseCameraDraging>().enabled = true;
        DomeCamera.transform.parent.gameObject.SetActive(false);

    }

    public void EnableMenuTemplet()
    {

        for (int j = 0; j < AllHotspotTemplets.Length; j++)
        {

            if (AllHotspotTemplets[j].gameObject.name == "")
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
