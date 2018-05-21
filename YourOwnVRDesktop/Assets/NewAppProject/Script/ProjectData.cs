using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectData : MonoBehaviour {

	// Open File Image data
	// ADD Project Data
	public TriLib.Samples.OpenFileImages OpenFileImage;
	public AddProject AddProject;
	public string FileName;
	public string ProjectName;
	public string ProjectFolderPath;
	public string ProjectFilePath;  

	public List<string> ImageAssetPath = new List<string> ();
	public List <string> MovieAssetPath = new List<string>();
	public List <string> UnityAssetPath = new List<string>();
	public List <string> ModelAssetPath = new List<string> ();
	public List <string> ModelImagePath = new List<string> ();

	public int temporaryInt;  

	void Start () {

	}

	void Update () {
		if (AddProject == null) {
			AddProject = FindObjectOfType<AddProject> ();
		}
		if (OpenFileImage == null) {
			OpenFileImage = FindObjectOfType<TriLib.Samples.OpenFileImages> ();
		}
	}
	
	public void SaveProjectData (){
//		Debug.Log (AddProject.fileName);
//		Debug.Log (AddProject.CurrentFolderPath);
//		Debug.Log (AddProject.CurrentFilePath);
		ProjectName = AddProject.fileName;
		ProjectFolderPath = AddProject.CurrentFolderPath;
		ProjectFilePath = AddProject.CurrentFilePath;
		ImageAssetPath = OpenFileImage.CacheImageFiles;
		MovieAssetPath = OpenFileImage.CacheMovieFiles;
		UnityAssetPath = OpenFileImage.CacheUnityFiles;
		ModelAssetPath = OpenFileImage.CacheModelFiles;
		ModelImagePath = OpenFileImage.CacheModelImage;

		Debug.Log ("Project data is saved");
	}
}