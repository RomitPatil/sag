using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemProperty : MonoBehaviour {

    public GameObject NavigationCanvas;
    public InputField InputItemName;
    public Dropdown ObjectFunction;
    public Dropdown NavigatetoScene_MediaFile;
    public TriLib.Samples.OpenFileImages OpenFileImage;

    public int CheckerScene;
    public int CheckerMedia;
    public string SelectedFunction;

    public string ItemName;
    public string Function;
    public string FunctionData;
    public bool recall;


    // Use this for initialization
    void Awake () {

        NavigationCanvas = GameObject.Find("NavigationCanvas");

    }
    private void Start()
    {
        NavigatetoScene_MediaFile.onValueChanged.AddListener(delegate {
            FunctionData = NavigatetoScene_MediaFile.captionText.text;
        });
    }

    // Update is called once per frame
    void Update() {

        
        
            if (OpenFileImage == null)
            {
                OpenFileImage = GameObject.Find("OpenFileImages").GetComponent<TriLib.Samples.OpenFileImages>();
            }

            // Check for Objtect Function
            if (SelectedFunction != ObjectFunction.captionText.text)
            {
                FunctionObjectSelection();
            }

            // Insert Scene list into dropDown
            if (CheckerScene != NavigationCanvas.transform.childCount)
            {
                if (ObjectFunction.captionText.text == "Navigation")
                {
                    InsertSceneList();
                }
            }

            // Insert media file list into dropdpwn
            if (CheckerMedia != OpenFileImage.CacheMovieFiles.Count)
            {
                if (ObjectFunction.captionText.text == "MediaFile")
                {
                    InsertMediaFile();
                }
                Debug.Log(ObjectFunction.captionText.text);

            }


       

    }

    public void RecallData()
    {
        Debug.Log("Recall" + FunctionData + NavigatetoScene_MediaFile.options.Count);
        if (Function == "Navigation")
        {
            InsertSceneList();
        }
        if (ItemName != "") {

            InputItemName.text = ItemName;
        }
        if (Function != "")
        {

            for (int i = 0; i < ObjectFunction.options.Count; i++) {
                if (ObjectFunction.options[i].text == Function)
                {

                ObjectFunction.options[i].text = Function;
                }
            }
        }

        
            for (int i = 0; i < NavigatetoScene_MediaFile.options.Count; i++)
            {
                    Debug.Log(NavigatetoScene_MediaFile.options[i].text + ">>>>>" + FunctionData + ">>" + i);
                if (NavigatetoScene_MediaFile.options[i].text == FunctionData)
                {

                    NavigatetoScene_MediaFile.value = i;
                }
            }
        
    }

    // Insert Media File
    public void InsertMediaFile() {

        Debug.Log("Enter on insert media file");
        NavigatetoScene_MediaFile.ClearOptions();
        List<Dropdown.OptionData> MediaFlag = new List<Dropdown.OptionData>();
        MediaFlag.Clear();

     


        for (int i = 0; i <  OpenFileImage.CacheMovieFiles.Count ; i++)
        {
            string MovieName = OpenFileImage.CacheMovieFiles[i];
            Debug.Log("Scene name : " + MovieName);
            var flagOptionNavigate = new Dropdown.OptionData(MovieName);

            MediaFlag.Add(flagOptionNavigate);


        }
        CheckerMedia = OpenFileImage.CacheMovieFiles.Count;
        NavigatetoScene_MediaFile.AddOptions(MediaFlag);
    }

    // Insert Scene Files
    public void InsertSceneList() {
        Debug.Log("Enter on Insert");
        List<Dropdown.OptionData> SceneFlag = new List<Dropdown.OptionData>();
        SceneFlag.Clear();
        NavigatetoScene_MediaFile.ClearOptions();
        for (int i = 0; i < NavigationCanvas.transform.childCount; i++)
        {
            if (NavigationCanvas.transform.GetChild(i).GetComponent<SceneProperties>())
            {
                string SceneName = NavigationCanvas.transform.GetChild(i).name;
                Debug.Log("Scene name : " + SceneName);
                var flagOptionNavigate = new Dropdown.OptionData(SceneName);

                SceneFlag.Add(flagOptionNavigate);
            }
            
        }
        CheckerScene = NavigationCanvas.transform.childCount;
        NavigatetoScene_MediaFile.AddOptions(SceneFlag);


    }

    public void FunctionObjectSelection() {

        SelectedFunction = ObjectFunction.captionText.text;
    }
}

