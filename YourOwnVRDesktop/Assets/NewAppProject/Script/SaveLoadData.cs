using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveLoadData : MonoBehaviour {

	public ProjectData projectData;
	public AddProject addproject ;
	public TriLib.Samples.OpenFileImages openFileImages;
	public GameObject secoundPanel; 
	public string FilePath;


	public void Save () {
		projectData.SaveProjectData ();
		string jsonString = JsonUtility.ToJson (projectData);
		FilePath = (addproject.CurrentFolderPath + "/ProjectData.json");
		Debug.Log ("FilePath... " + FilePath);
		File.WriteAllText (FilePath, jsonString);
		Debug.Log ("Project Save");
	}
	 
	//data on load 
	//onSaveLoad load method loads the data  
	public void Load () {
		secoundPanel.gameObject.SetActive (true);
		openFileImages.CacheImageFiles = projectData.ImageAssetPath;
		openFileImages.CacheMovieFiles = projectData.MovieAssetPath;
		openFileImages.CacheUnityFiles = projectData.UnityAssetPath;
		openFileImages.CacheModelImage = projectData.ModelImagePath;
		openFileImages.CacheModelFiles = projectData.ModelAssetPath;
		openFileImages.ProjectFileName = projectData.ProjectName;
		openFileImages.LoadData ();
		AutoSaveing ();
	}
	//autosave() 
	IEnumerator AutoSave () {
		int i = 0;
		Debug.Log ("Auto saving...");
		yield return new WaitForSeconds (5000f);

		projectData.SaveProjectData ();
		string jsonString = JsonUtility.ToJson (projectData);
		FilePath = (addproject.CurrentFolderPath + "/ProjectData.json");
		File.WriteAllText (FilePath, jsonString);
		Debug.Log ("Project Save" + addproject.CurrentFolderPath + "/ProjectData.json");
		StartCoroutine (AutoSave ());
	}
	 
	public void AutoSaveing(){
		StartCoroutine (AutoSave ());
	}

}
