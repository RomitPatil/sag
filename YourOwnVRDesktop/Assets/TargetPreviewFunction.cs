using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetPreviewFunction : MonoBehaviour
{

    public Vector3 TargetPosition;
    public string TargetFunctionType;
    public string TargetFunctionData;
    public GameObject NavigationContainerPrv;
    public string NavTo;
    public Vector3 rotationView;
    public float Z_pos;
    public ObjectHotsot OH;
    public GameObject Model;
    // Use this for initialization

    private void Start()
    {
        gameObject.AddComponent<BoxCollider>();
        gameObject.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 5f);
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
    private void OnEnable()
    {
        OH = null;
        Model = null;
    }
    private void Update()
    {
        if (NavigationContainerPrv == null)
        {

            NavigationContainerPrv = gameObject.transform.parent.parent.parent.gameObject;
            gameObject.transform.parent.transform.localScale = new Vector3(1, 1, 1); new Vector3(Z_pos, Z_pos, Z_pos);
            gameObject.transform.localScale = new Vector3(Z_pos, Z_pos, Z_pos);
            gameObject.transform.parent.transform.localPosition = TargetPosition;
        }

    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "ObjectModel")
        {
            Debug.Log(collision.gameObject.name);
            if (Model == null)
            {
                Model = collision.gameObject;
                while (OH != null)
                {
                    OH = Model.GetComponentInParent<ObjectHotsot>();
                    OH.transform.localPosition = Vector3.zero;
                    OH.transform.GetChild(0).GetComponent<LookAtTarget>().enabled = true;
                }
            }
            if (TargetFunctionType == "Navigation")
            {
                NavTo = TargetFunctionData;
                for (int i = 0; i < NavigationContainerPrv.transform.childCount; i++)

                {
                    Debug.Log(NavigationContainerPrv.transform.GetChild(i).gameObject.name + " :: " + NavTo);
                    Debug.Log(NavTo);
                    if (NavTo == NavigationContainerPrv.transform.GetChild(i).GetComponent<PreviewScene>().sceneName)
                    {

                        NavigationContainerPrv.transform.GetChild(i).gameObject.SetActive(true);
                        //set camera rot eulervalues to given rotationView    
                        NavigationContainerPrv.transform.GetChild(i).gameObject.GetComponent<PreviewScene>().camPos = rotationView;
                        Model.GetComponent<ParentObjectHotspot>().OH.gameObject.transform.localPosition = Vector3.zero;
                       Model.GetComponent<ParentObjectHotspot>().OH.gameObject.transform.GetChild(0).transform.GetComponent<LookAtTarget>().enabled = true;
                        Model.GetComponent<ParentObjectHotspot>().OH.SelectedModel.transform.GetChild(0).gameObject.SetActive(false);

                    }
                    else
                    {
                        NavigationContainerPrv.transform.GetChild(i).gameObject.SetActive(false);
                    }

                }
            }
            if (TargetFunctionType == "Text Box")
            {

                GameObject DisplayText = gameObject.transform.parent.GetComponentInChildren<HideInfo>().gameObject;
                DisplayText.SetActive(true);
                DisplayText.GetComponent<HideInfo>().RequireMsg = true;
                TriLib.Samples.PreviewHotspot previewHotspot = gameObject.transform.parent.GetComponent<TriLib.Samples.PreviewHotspot>();
                previewHotspot.Always = true;
                previewHotspot.GetMsg = TargetFunctionData;

            }



        }
    }


}
