using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.VR;


public class ProjectIDStatic : MonoBehaviour {



	public bool setProjectID;
	public InputField InputID;
    public InputField ServerPath;

	public string ProjectString;
	public string Pathw;
    public string Projectpath;
	// Use this for initialization
	void Start () {
		StartCoroutine (loadDevice ("Cardboard"));
	
        // we create a file for Save Project ID
		Pathw =  Application.persistentDataPath + "/ID.txt";
        Projectpath = Application.persistentDataPath + "/Projectpath.txt";
        Debug.Log (Projectpath);
        Debug.Log(Pathw);
        if (!File.Exists(Pathw))
        {
            File.Create(Pathw);
        }
        else {

            InputID.text = File.ReadAllText(Pathw);
        }
        if (!File.Exists(Projectpath))
        {
            File.Create(Projectpath);
        }
       

    }

	IEnumerator loadDevice (string newDevice){
        UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
		yield return null;
        UnityEngine.XR.XRSettings.enabled = false;
	}

    // Write the ID into filepath and open the Mobile VR Scene
	public void GetProjectId (){

		ProjectString = InputID.text;
        Projectpath = ServerPath.text;
        if (Projectpath != "" && ProjectString != "")
        {

            File.WriteAllText(Pathw, ProjectString);

            SceneManager.LoadScene("VRAR TEST APP");
        }
	}

    public void LoadProjectDataPath() {

        var extensions = new[]
             {
                    new SFB.ExtensionFilter("*"),
                };

        // Get the path of selected file.
        var paths = SFB.StandaloneFileBrowser.OpenFolderPanel("", "", false);
        File.WriteAllText(Projectpath, paths[0]);

        SceneManager.LoadScene("VRAR TEST APP");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
