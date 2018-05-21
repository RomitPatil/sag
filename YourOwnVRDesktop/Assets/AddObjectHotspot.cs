using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddObjectHotspot : MonoBehaviour
{

    public InputField Pos_X;
    public InputField Pos_Y;
    public InputField Pos_Z;
    public InputField ScaleFactor;
    public InputField SelectionRadius;
    public GameObject NavigationContainer;
    public SetupDome setupDome;
    public GameObject DomeCamera;
    public Text BntID;
    public Dropdown selectedModel;
    public Dropdown SelectedFunction;
    public Slider GetSliderValue;
    public Slider ScaleSlider;
    public Dropdown XYZAxies;
    public GameObject ObjectHotspot;
    public GameObject targetHotspot;
    public GameObject TargetText;
    public Toggle CanSeleletModel;
    public Dropdown targetFunction;
    public Dropdown NaviateFunctionData;
    public InputField TextBoxFunction;
    public TriLib.Samples.OpenFileImages OpenFileImage;
    public GameObject SelectedModel;
    public ObjectHotsot SelectedObjectHotspot;
    public TargetFunction SelectedTarget;
    public GameObject[] AllHotspotTemplets;
    public GameObject ActiveScene;
    public GameObject UnityModelFileContainer;
    public GameObject Target;
    public List<GameObject> UnityModels = new List<GameObject>();
    public List<string> UnityAssetPath = new List<string>();
    public GameObject UnityInstance;
    public string ModelAssetPath;
    public string TargetFunctionType;
    public string TargetFunctionData;
    int NewModleCheker;
    bool rotation;
    bool scaling;
    string SelectedAxis;
    public float ScrolFactor;
    public float Radius;
    public bool canTarget;
    public int CheckerScene;
    bool DragTaget;
    // Use this for initialization
    void Start()
    {

        Pos_X.onEndEdit.AddListener(delegate { setPosition(); });
        Pos_Y.onEndEdit.AddListener(delegate { setPosition(); });

        Pos_Z.onEndEdit.AddListener(delegate { OnSelectionRedius(); });
        selectedModel.onValueChanged.AddListener(delegate {
            OnSelectModel();
        });

        SelectedFunction.onValueChanged.AddListener(delegate {
            RotateAndScaleModel();
        });

        XYZAxies.onValueChanged.AddListener(delegate {
            SelectedAxis = XYZAxies.captionText.text.ToString();
        });

        targetFunction.onValueChanged.AddListener(delegate {

            if (targetFunction.captionText.text.ToString() == "Navigation")
            {


                TextBoxFunction.gameObject.SetActive(false);
                NaviateFunctionData.gameObject.SetActive(true);
                InsertSceneList();
            }
            if (targetFunction.captionText.text.ToString() == "Text Box")
            {
                TextBoxFunction.gameObject.SetActive(true);
                NaviateFunctionData.gameObject.SetActive(false);
            }

        });

        NaviateFunctionData.onValueChanged.AddListener(delegate {
            TargetFunctionData = NaviateFunctionData.captionText.text.ToString();
        });

        TextBoxFunction.onEndEdit.AddListener(delegate { TargetFunctionData = TextBoxFunction.text; });


        ScaleFactor.onEndEdit.AddListener(delegate { OnScaleFactor(); });

        SelectionRadius.onEndEdit.AddListener(delegate { OnSelectedModelOutline(); });
        CanSeleletModel.onValueChanged.AddListener(delegate { InitiateTarget(); });
    }

    public void OnAddObjectHotspot()
    {

        EnableMenuTemplet();
        InitiateObjectHotspot();
    }

    public void OnScaleFactor()
    {

        string m = ScaleFactor.text.ToString();

        ScrolFactor = float.Parse(m);


    }
    public void OnSelectedModelOutline()
    {
        if (CanSeleletModel.isOn)
        {

            if (SelectedModel != null)
            {

                string v = SelectionRadius.text;
                float pixel = float.Parse(v);
                GameObject Clone = SelectedModel.transform.GetChild(0).gameObject;
                Clone.transform.localScale = SelectedModel.transform.localScale;
                Clone.transform.localScale = new Vector3(Clone.transform.localScale.x + pixel, Clone.transform.localScale.y + pixel, Clone.transform.parent.localScale.z);
            }
        }
    }

    public void OnSelectionRedius()
    {
        if (SelectedModel != null)
        {
            Target = SelectedTarget.gameObject;
            string m = Pos_Z.text.ToString();

            Radius = float.Parse(m);
            Debug.Log(Radius + "???????");
            if (Radius != 0)
            {
                Target.transform.localScale = new Vector3(Radius, Radius, Radius);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        GetFunctionDataAndInitiateObject();



        if (rotation)
        {
            Debug.Log(SelectedAxis + " ????");
            if (SelectedAxis == "X")
            {
                SelectedObjectHotspot.transform.localEulerAngles = new Vector3(
                     180 * GetSliderValue.value,
                     SelectedObjectHotspot.transform.localEulerAngles.y,
                    SelectedObjectHotspot.transform.localEulerAngles.z
                 );
            }
            if (SelectedAxis == "Y")
            {
                SelectedObjectHotspot.transform.localEulerAngles = new Vector3(
                     SelectedObjectHotspot.transform.localEulerAngles.x,
                      180 * GetSliderValue.value,
                     SelectedObjectHotspot.transform.localEulerAngles.z
                  );
            }
            if (SelectedAxis == "Z")
            {
                SelectedObjectHotspot.transform.localEulerAngles = new Vector3(
                        SelectedObjectHotspot.transform.localEulerAngles.x,
                        SelectedObjectHotspot.transform.localEulerAngles.y,
                        180 * GetSliderValue.value
                    );
            }
        }

        if (scaling)
        {
            SelectedObjectHotspot.transform.localScale = new Vector3(
                                 ScrolFactor * ScaleSlider.value,
                                ScrolFactor * ScaleSlider.value,
                                ScrolFactor * ScaleSlider.value);
        }
        if (SelectedModel != null) {

            if (SelectedModel.gameObject.transform.childCount != 0) {

                SelectedModel.gameObject.transform.GetChild(0).gameObject.SetActive(CanSeleletModel.isOn);
            }
        }
    }

    public void EnableMenuTemplet()
    {

        for (int j = 0; j < AllHotspotTemplets.Length; j++)
        {

            if (AllHotspotTemplets[j].gameObject.name == "ObjectHotspotPanel")
            {

                AllHotspotTemplets[j].SetActive(true);
            }
            else
            {
                AllHotspotTemplets[j].SetActive(false);
            }
        }
    }

    public void setPosition()
    {
        EnableMenuTemplet();
        Debug.Log("Set Position is call");

        if (SelectedTarget != null)
        {
            string text = Pos_X.text;
            int x = 0;
            int.TryParse(text, out x);
            Debug.Log(x);
            SelectedTarget.transform.localPosition = new Vector3(x, SelectedTarget.transform.localPosition.y, SelectedTarget.transform.localPosition.z);
            text = Pos_Y.text;
            int y = 0;
            int.TryParse(text, out y);

            SelectedTarget.transform.localPosition = new Vector3(SelectedTarget.transform.localPosition.x, y, SelectedTarget.transform.localPosition.z);

        }
    }

    public void OnSelectModel()
    {

        var selected = selectedModel.options[selectedModel.value];
        string selectedModelName = selected.text.ToString();

        for (int i = 0; i < UnityModels.Count; i++)
        {
            Debug.Log(UnityModels[i].name + "::::");
            if (selectedModelName == UnityModels[i].name)
            {
                SelectedModel = UnityModels[i];
                UnityInstance = UnityModels[i];
                ModelAssetPath = UnityAssetPath[i];
                Debug.Log(UnityModels[i].name + "****");
            }

        }

        if (SelectedModel != null)
        {
            InitiateModele();
        }

    }

    public void InitiateModele()
    {

        if (SelectedModel != null)
        {
            if (SelectedObjectHotspot.gameObject.transform.GetChild(0).transform.childCount != 0)
            {

                GameObject.Destroy(SelectedObjectHotspot.gameObject.transform.GetChild(0).GetChild(0).gameObject);
            }

            GameObject model = GameObject.Instantiate(SelectedModel);
            model.transform.parent = SelectedObjectHotspot.gameObject.transform.GetChild(0).transform;
            SelectedObjectHotspot.GetComponent<BoxCollider>().enabled = true;

            SelectedObjectHotspot.gameObject.transform.GetChild(0).transform.gameObject.AddComponent<LookAtTarget>();
            model.transform.localPosition = Vector3.zero;
            SelectedObjectHotspot.gameObject.transform.GetChild(0).transform.gameObject.GetComponent<LookAtTarget>().enabled = false;
            model.AddComponent<FindMesh>();
            //  SelectedModel = model.GetComponent<FindMesh>().ms.gameObject;
            // SelectedObjectHotspot.SelectedModel = SelectedModel;
            model.SetActive(true);
            canTarget = true;
        }
    }

    public void InitiateObjectHotspot()
    {


        for (int i = 0; i < NavigationContainer.transform.childCount; i++)
        {
            if (NavigationContainer.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                if (NavigationContainer.transform.GetChild(i).GetComponent<SceneProperties>())
                {

                    ActiveScene = NavigationContainer.transform.GetChild(i).gameObject;
                }
            }
        }
        SetupDome.ButtonId++;
        BntID.text = "Btn ID :" + SetupDome.ButtonId.ToString();

        GameObject Object = GameObject.Instantiate(ObjectHotspot);
        Object.gameObject.name = "Btn ID :" + SetupDome.ButtonId;
        Object.gameObject.transform.parent = ActiveScene.transform;
        Object.transform.localPosition = new Vector3(0, 0, 90f);
        Object.transform.localScale = new Vector3(1f, 1f, 1f);
        SelectedObjectHotspot = Object.transform.GetComponent<ObjectHotsot>();
        Object.transform.GetComponent<ObjectHotsot>().AddObjectHotspot = gameObject.GetComponent<AddObjectHotspot>();
        setupDome.SelectFile.GetComponent<SelectFiles>().EditScene = true;
    }

    public void GetFunctionDataAndInitiateObject()
    {
        if (UnityModelFileContainer.transform.childCount != NewModleCheker)
        {


            for (int i = 0; i < UnityModelFileContainer.transform.childCount; i++)
            {

                UnityModels.Add(UnityModelFileContainer.transform.GetChild(i).GetComponent<SelectFiles>().UnityAssetBundle);
                UnityAssetPath.Add(UnityModelFileContainer.transform.GetChild(i).GetComponent<SelectFiles>().UnityAssetPath);

            }

            if (selectedModel.options.Count != UnityModelFileContainer.transform.childCount)
            {

                selectedModel.ClearOptions();
                List<Dropdown.OptionData> Flag = new List<Dropdown.OptionData>();
                var Empty = new List<Dropdown.OptionData>();
                Flag.Add(new Dropdown.OptionData());
                for (int i = 0; i < UnityModelFileContainer.transform.childCount; i++)
                {

                    var model = new Dropdown.OptionData(UnityModelFileContainer.transform.GetChild(i).GetComponent<SelectFiles>().UnityAssetBundle.gameObject.name);
                    Flag.Add(model);
                }

                selectedModel.AddOptions(Flag);

            }
            NewModleCheker = selectedModel.options.Count;

        }

    }

    public void InitiateTarget()
    {
        if (DragTaget == false)
        {
            if (canTarget)
            {
                GameObject target = GameObject.Instantiate(targetHotspot);
                target.name = "Target_" + BntID.text.ToString();
                target.transform.parent = ActiveScene.transform;

                target.transform.localPosition = new Vector3(SelectedObjectHotspot.transform.gameObject.transform.localPosition.x + 30f, SelectedObjectHotspot.transform.gameObject.transform.localPosition.y, SelectedObjectHotspot.transform.gameObject.transform.localPosition.z);
                target.transform.localScale = new Vector3(10f, 10f, 10f);
                target.AddComponent<TargetFunction>();
                target.GetComponent<TargetFunction>().NavigationCanvas = NavigationContainer;
                target.GetComponent<TargetFunction>().TargetFunctions = targetFunction;
                target.GetComponent<TargetFunction>().NavigationFunction = NaviateFunctionData;
                target.GetComponent<TargetFunction>().TextBoxFunction = TextBoxFunction;
                target.GetComponent<TargetFunction>().AddObjectHotspot = gameObject.transform.GetComponent<AddObjectHotspot>();
                target.GetComponent<TargetFunction>().ObjectHotspot = SelectedObjectHotspot;
                target.AddComponent<SelfActive>();
                SelectedTarget = target.GetComponent<TargetFunction>();
                SelectedObjectHotspot.SelectedTarget = SelectedTarget;
                DragTaget = true;
            }
        }
    }
    public void RotateAndScaleModel()
    {

        if (SelectedFunction.captionText.text.ToString() == "Rotation")
        {
            ScaleSlider.gameObject.SetActive(false);
            XYZAxies.gameObject.SetActive(true);
            GetSliderValue.gameObject.SetActive(true);
            rotation = true;
            scaling = false;
        }

        if (SelectedFunction.captionText.text.ToString() == "Scale")
        {

            XYZAxies.gameObject.SetActive(false);
            GetSliderValue.gameObject.SetActive(false);
            rotation = false;
            scaling = true;
            ScaleSlider.gameObject.SetActive(true);
        }
    }


    public void InsertSceneList()
    {
        Debug.Log("Enter on Insert");
        List<Dropdown.OptionData> SceneFlag = new List<Dropdown.OptionData>();
        SceneFlag.Clear();
        NaviateFunctionData.ClearOptions();
        for (int i = 0; i < NavigationContainer.transform.childCount; i++)
        {
            string SceneName = NavigationContainer.transform.GetChild(i).name;
            Debug.Log("Scene name : " + SceneName);
            var flagOptionNavigate = new Dropdown.OptionData(SceneName);

            SceneFlag.Add(flagOptionNavigate);

        }
        CheckerScene = NavigationContainer.transform.childCount;
        NaviateFunctionData.AddOptions(SceneFlag);
    }
}
