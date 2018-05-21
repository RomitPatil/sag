using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMesh : MonoBehaviour
{
    public MeshFilter ms;
    private object Selection;
    public Material OUTLINE;
    // Use this for initialization

    private void Update()
    {
        if (ms == null)
        {

            ms = gameObject.transform.GetComponentInChildren<MeshFilter>();
            gameObject.transform.parent.GetComponent<MeshRenderer>().enabled = false;



            if (!ms.gameObject.transform.GetComponent<BoxCollider>())
            {
                ms.gameObject.transform.gameObject.AddComponent<BoxCollider>();
            }

            OUTLINE = Resources.Load("PrefabForPreview/OUTLINE") as Material;

            ms.gameObject.tag = "ObjectModel";
            ms.gameObject.layer = 8;
            gameObject.transform.parent.parent.GetComponent<MeshRenderer>().enabled = false;
            gameObject.transform.parent.parent.GetComponent<BoxCollider>().enabled = false;
            if (!ms.gameObject.GetComponent<BoxCollider>())
            {

                ms.gameObject.AddComponent<BoxCollider>();
            }
            if (!ms.gameObject.GetComponent<SelfActive>())
            {

                ms.gameObject.AddComponent<SelfActive>();
            }
            if (!ms.gameObject.GetComponent<ParentObjectHotspot>())
            {

                ms.gameObject.AddComponent<ParentObjectHotspot>();
            }
            ObjectHotsot OH = gameObject.transform.parent.GetComponentInParent<ObjectHotsot>();
            OH.SelectedModel = ms.gameObject;
            OH.AddObjectHotspot.SelectedModel = ms.gameObject;
            if (ms.gameObject.transform.childCount == 0)
            {
                GameObject clone = ms.gameObject;

                clone.name = "clone";
                GameObject ModeleClone = GameObject.Instantiate(clone);
                ModeleClone.name = "ModeleClone";
                ModeleClone.transform.parent = ms.gameObject.transform;
                ModeleClone.transform.localPosition = Vector3.zero;
                ModeleClone.transform.localRotation = ms.gameObject.transform.localRotation;
                ModeleClone.transform.localScale = new Vector3(1f, 1f, 1f);
                ModeleClone.transform.gameObject.GetComponent<MeshRenderer>().material = OUTLINE;
                ModeleClone.transform.GetComponent<BoxCollider>().enabled = false;
                ModeleClone.transform.GetComponent<SelfActive>().enabled = false;
                ModeleClone.tag = "Untagged";
                ModeleClone.layer = 0;
            }
        }
        //  gameObject.GetComponent<MeshCollider>().sharedMesh = ms.mesh;

    }
    public void ModelOutLine(float Radius, Vector3 LocalScale)
    {

        StartCoroutine(modelOutLine(Radius, LocalScale));
    }

    IEnumerator modelOutLine(float Radius, Vector3 LocalScale)
    {

        yield return new WaitForSeconds(1f);
        if (ms.gameObject.transform.childCount == 0)
        {
            GameObject clone = ms.gameObject;

            clone.name = "clone";
            GameObject ModeleClone = GameObject.Instantiate(clone);
            ModeleClone.name = "ModeleClone";
            ModeleClone.transform.parent = ms.gameObject.transform;
            ModeleClone.transform.localPosition = Vector3.zero;
            ModeleClone.transform.localRotation = ms.gameObject.transform.localRotation;
            ModeleClone.transform.localScale = new Vector3(LocalScale.x + Radius, LocalScale.y + Radius, LocalScale.z);
            ModeleClone.transform.gameObject.GetComponent<MeshRenderer>().material = OUTLINE;
            ModeleClone.transform.GetComponent<BoxCollider>().enabled = false;
            ModeleClone.transform.GetComponent<SelfActive>().enabled = false;
            ModeleClone.tag = "Untagged";

            ModeleClone.layer = 8;
        }
    }
}

