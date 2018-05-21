using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.VR;
using UnityEngine.UI;

public class PrevSaveLoad : MonoBehaviour {

	public string PreServerURL;
  
	public TriLib.Samples.PreviewLoader loaderdata;
	public string PostUrl = "?overwrite=true";
	public static string ProjectID = "";
	public  string FilePath ;  
	public string IDpath;
    public string ProjectPath;
	  
	public GameObject waitForsetupPanal; 

	public Texture DefaultTexture;

	public  bool MobileApp;
    public bool Server;
    public bool Drive;

    private void Start()
    {
        StartLoad();
    }

   public void StartLoad ()
    {
        // Creating  ID file into local space . 
        // Crrating  Sag###$ file into Local space that file holding all data of creating scene and hotspot properties.
        FilePath = Application.persistentDataPath;
        if (File.Exists(Application.persistentDataPath + "/ID.txt"))
        {
            IDpath = (Application.persistentDataPath + "/ID.txt");
        }
        else
        {

            File.Create(Application.persistentDataPath + "/ID.txt");
        IDpath = Application.persistentDataPath + "/ID.txt";
        }
        if (File.Exists(IDpath))
        {
            ProjectID = File.ReadAllText(IDpath);
            Debug.Log(IDpath + "  " + ProjectID);
           
        }
        else
        {
            File.Create(IDpath);
        }

        if (File.Exists(Application.persistentDataPath + "/Projectpath.txt"))
        {
            ProjectPath = (Application.persistentDataPath + "/Projectpath.txt");
           
        }
        else
        {

            File.Create(Application.persistentDataPath + "/Projectpath.txt");
            ProjectPath = Application.persistentDataPath + "/Projectpath.txt";
        }
        if (File.Exists(ProjectPath))
        {
            ProjectPath = File.ReadAllText(ProjectPath);
            Debug.Log(ProjectPath + "  " + ProjectID);
            PreServerURL = ProjectPath +"/";
        }
        else
        {
            File.Create(ProjectPath);
        }


        if (File.Exists(Application.persistentDataPath + "/sag###$.json"))
        {

            FilePath = Application.persistentDataPath + "/sag###$.json";
        }
        else
        {

            File.Create(Application.persistentDataPath + "/sag###$.json");
        }
       
         //   waitForsetupPanal.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "New File :" + FilePath;
      
		if (MobileApp)
        {
            waitForsetupPanal.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "/n On mobile";
            StartCoroutine (DownloadProjectdata ());

		}
	}

    
    // Enable the VR mode in Mobile APP
	IEnumerator loadDevice (string newDevice)
    {
		UnityEngine.XR.XRSettings.LoadDeviceByName (newDevice);
		yield return null;
        UnityEngine.XR.XRSettings.enabled = true;
	}

    // Download the ProjectData.json , that file having clone of our project setup data .we have data into scene property formate.
	IEnumerator DownloadProjectdata ()
    {
        string link = "";
        if (Server)
        {

		 link = PreServerURL + ProjectID + "/ProjectData.json" + PostUrl;
        }
        if (Drive) {

             link = PreServerURL  + "/ProjectData.json" ;
            Debug.Log(link);
        }
        waitForsetupPanal.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "/n " + link;

        WWW downloadProjectData =new WWW (link);
		yield return downloadProjectData;
        waitForsetupPanal.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "/n  downloading " + link;
        Debug.Log (link);
		Debug.Log (downloadProjectData.text);
        waitForsetupPanal.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "/n  downloading /n" + downloadProjectData.error;
        if (downloadProjectData.error == null || downloadProjectData.error == "")
        {  
            
            waitForsetupPanal.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "/n  download SucessFully  " + FilePath;

            File.WriteAllText (FilePath, downloadProjectData.text);
            waitForsetupPanal.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "/n  Writing ";
            yield return FilePath;
           
            Debug.Log (FilePath);
            waitForsetupPanal.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "/n  Writing SucessFully ";

	    	StartCoroutine(Load());
		}
	}
	

	public void Save ()
    {

		Debug.Log ("Save Project Data :: :::");
		StartCoroutine (Projectdata ());
	}
    
    // Create PreviewLoader Script data clone into sag###$.json file, so our data is setup automaticaly.
	IEnumerator Projectdata()
    {
     
            FilePath = Application.persistentDataPath + "/sag###$.json";
      
		string jsonstring = JsonUtility.ToJson (loaderdata);
		File.WriteAllText (FilePath, jsonstring);



        Debug.Log(FilePath);
		WWW www = new WWW ("file:///" + FilePath);

		yield return www;
        string url = "";
        if (Server) {

		 url = PreServerURL + ProjectID + "/ProjectData.json" + "?overwrite=true";
        }
        if (Drive) {
             url = PreServerURL + ProjectID + "/ProjectData.json";
        }
        
		Debug.Log (url);

        if (www.error == null || www.error == "")
        { 
			Debug.Log ("Loaded projectdata file sucessfully");
		}
        else
        {
			Debug.Log ("Open file error projectdata : " + www.error);
			yield break;
		}
        if (Server)
        {
            WWWForm postFrom = new WWWForm();

            postFrom.AddBinaryData("", www.bytes, url, "File/image/*");

            Debug.Log("Upload URL :::" + url);

            WWW upload = new WWW(url, postFrom);

            yield return upload;
            Debug.Log("upload done :" + upload.text);

            loaderdata.ProjectdataURL = url;

        }
        if (Drive) {
            loaderdata.ProjectdataURL = url;
            File.WriteAllBytes(url, www.bytes);
        }
	}

    // Reading all data from json file and setup into PreviewLoader into mobile App scene.
    // call Setup Scene .
	IEnumerator Load ()
    { 
		if (MobileApp) {
			waitForsetupPanal.SetActive (true);
		}
        Debug.Log(FilePath);
        // Reading all data from json file and setup into PreviewLoader into mobile App scene.
        string jsonString = File.ReadAllText(FilePath);
        waitForsetupPanal.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "/n  Reading data ";
        yield return jsonString;

        waitForsetupPanal.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "/n  Reading data Sucessfully ";
        Debug.Log(jsonString);
     
		JsonUtility.FromJsonOverwrite (jsonString, loaderdata);

		loaderdata.MobileApp = true;

        // call Setup Scene .
        loaderdata.StartSetup ();
	}
}
