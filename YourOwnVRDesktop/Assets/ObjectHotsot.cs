using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHotsot : MonoBehaviour {

    public AddObjectHotspot AddObjectHotspot;
    public TargetFunction SelectedTarget;
    public GameObject SelectedModel;
    public Vector3 ModelPosition;
    public Vector3 TargetPosition;
    public string TargetFunctionType;
    public string TargetFunctionData;
    public string ModelFunction;
    public float Z_pos;
    public float Radius;
    public string ActionFunction;
    public GameObject UnityObjectInstance;
    public GameObject Target;
    public string ModelAssetPath;
    public Vector3 ModelScale;
    public Vector3 ModelRotation;
    public bool Preview;
    public string ConnectTarget;
    bool hide;
	// Use this for initialization
	void Start ()
    {
        ActionFunction = "ObjectHotsot";

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (AddObjectHotspot == null)
        {

            AddObjectHotspot = GameObject.FindObjectOfType<AddObjectHotspot>();

        }
        else {
            string z = AddObjectHotspot.Pos_Z.text.ToString();
            if (z != "") {

                Z_pos = float.Parse(z);
            }
            string r = AddObjectHotspot.SelectionRadius.text.ToString();
            if (r != "") {

            Radius = float.Parse(r);
            }
            
            ModelFunction = AddObjectHotspot.SelectedFunction.captionText.text;

            ModelPosition = gameObject.transform.localPosition;
            if (SelectedTarget != null) {

            TargetPosition = SelectedTarget.gameObject.transform.localPosition;
                SelectedTarget.Z_pos = Z_pos;
                ConnectTarget = SelectedTarget.gameObject.name ;
            }
            UnityObjectInstance = AddObjectHotspot.UnityInstance;
            ModelAssetPath = AddObjectHotspot.ModelAssetPath;
        }
        
            ModelScale = gameObject.transform.localScale;
            ModelRotation = gameObject.transform.localEulerAngles;
        if (Preview) {
            if (Target == null)
            {
                if (ConnectTarget != "")
                {
                    Target = GameObject.Find(ConnectTarget).gameObject;
                    Target.gameObject.SetActive(false);
                  
                }
            }

            if (hide == false)
            {
                if (SelectedModel != null)
                {
                    SelectedModel.transform.GetChild(0).gameObject.SetActive(false);
                    hide = true;
                }
            }

        }
	}

    public void ClickOnhotspot()
    {
        AddObjectHotspot.SelectedModel = SelectedModel;
        AddObjectHotspot.SelectedObjectHotspot = gameObject.GetComponent<ObjectHotsot>();
        AddObjectHotspot.SelectedTarget = SelectedTarget;
        AddObjectHotspot.Pos_Z.text = Z_pos.ToString();
        AddObjectHotspot.SelectionRadius.text = Radius.ToString();

        for (int i = 0; i < AddObjectHotspot.targetFunction.options.Count; i++) {

            if (TargetFunctionType == AddObjectHotspot.targetFunction.options[i].text) {
                AddObjectHotspot.targetFunction.value = i;
            }
        }

        for (int i = 0; i < AddObjectHotspot.NaviateFunctionData.options.Count; i++)
        {

            if (TargetFunctionData == AddObjectHotspot.NaviateFunctionData.options[i].text)
            {
                AddObjectHotspot.NaviateFunctionData.value = i;
            }
        }

        for (int i = 0; i < AddObjectHotspot.SelectedFunction.options.Count; i++)
        {

            if (ModelFunction == AddObjectHotspot.SelectedFunction.options[i].text)
            {
                AddObjectHotspot.SelectedFunction.value = i;
            }
        }
        AddObjectHotspot.EnableMenuTemplet();

    }

    public void SetupModel()
    {
        
        GameObject model = GameObject.Instantiate(UnityObjectInstance);
        model.AddComponent<FindMesh>();
        model.transform.parent = gameObject.transform.GetChild(0).transform;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.transform.parent.transform.localPosition = ModelPosition;
        gameObject.transform.parent.transform.localScale = ModelScale;
        gameObject.transform.parent.transform.localEulerAngles = ModelRotation;
        model.gameObject.SetActive(true);

       

        model.GetComponent<FindMesh>().ModelOutLine(Radius, ModelScale);
           
    }
    



}
