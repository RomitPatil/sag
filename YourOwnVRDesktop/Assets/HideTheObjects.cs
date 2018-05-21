using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTheObjects : MonoBehaviour {
    public List <GameObject>  HideGameObjects = new List<GameObject>();
   public int objectCount;
	// Use this for initialization
	void Start () {
		
	}
    // this is for hiding the extra gameobject into scene we can not destroy that object , they holding the refrances .
    public  void hideObjects() {
        Transform[] hideGameObjects = GameObject.FindObjectsOfType<Transform>();

        if (hideGameObjects.Length != objectCount)
        {

            for (int i = 0; i < hideGameObjects.Length; i++)
            {
                Debug.Log("Running");
                if (hideGameObjects[i].transform.parent == null)
                {
                    if (hideGameObjects[i].gameObject.name != "Unity3D_Project")
                    {
                        if (hideGameObjects[i].gameObject.name != "PreviewSatup")
                        {
                            if (!HideGameObjects.Contains(hideGameObjects[i].gameObject))
                            {
                                HideGameObjects.Add(hideGameObjects[i].gameObject);
                            }
                        }
                    }
                }
				  
            }
			  
        }

        if (HideGameObjects.Count != objectCount)
        {
            Debug.Log(HideGameObjects.Count + "||" + objectCount);
            for (int i = 0; i < HideGameObjects.Count; i++)
            {
                Debug.Log(HideGameObjects[i].gameObject.name);

                HideGameObjects[i].gameObject.SetActive(false);

                objectCount += i;
            }
        }
		 
    }
	// Update is called once per frame
	void Update () {
		  

       
	}
}
