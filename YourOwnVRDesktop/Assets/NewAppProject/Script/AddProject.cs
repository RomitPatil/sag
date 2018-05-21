using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using System.IO;
using System;

[RequireComponent(typeof(Button))]
public class AddProject : MonoBehaviour
{
    //for saving data only project path files is used
    public string fileName;
    public string CurrentFolderPath;
    public string CurrentFilePath;  
    public GameObject SecoundPanal;
    public string ProjectPathFiles;
    public OpenProjectData OpenProjectData;
    public ProjectData projectData;
    public SaveLoadData LoadData;
    public TriLib.Samples.OpenFileImages openFileImage;
	int count=0;  
    // Use this for initialization
    void Start()
    {
        Debug.Log("persistent data path " + Application.persistentDataPath);
        LoadData = FindObjectOfType<SaveLoadData>();

        ProjectPathFiles = Path.Combine(Application.dataPath, "ProjectPaths");

        Debug.Log(ProjectPathFiles);
        if (!File.Exists(ProjectPathFiles))
        {
            File.Create(Path.Combine(Application.dataPath, "ProjectPaths"));
            Debug.Log(Path.Combine(Application.dataPath, "ProjectPaths"));
        }
    }

    public void OnAddProject()
    {

        var path = StandaloneFileBrowser.SaveFilePanel("Save Project", "", "", "");
        path = path.Replace(@"\", "/");


        if (path.Length > 0)
        {
            var folder = Directory.CreateDirectory(path);
            CurrentFolderPath = path;
            fileName = Path.GetFileName(path);
            Debug.Log("filename is " + fileName);

            CurrentFilePath = (path + "/");
            Debug.Log(CurrentFilePath + "Current File path");


            if (File.Exists(ProjectPathFiles))
            {

                var ProjectPathsDataFiles = CurrentFolderPath + "$";
                Debug.Log(ProjectPathsDataFiles);
                OpenProjectData.OpenProjectsDataPaths = (ProjectPathsDataFiles);
                string LastWrite = File.ReadAllText(ProjectPathFiles);
                string JsonString = ProjectPathsDataFiles;
                File.WriteAllText(ProjectPathFiles, JsonString + LastWrite);

                Debug.Log("File is exist");

            }

            SecoundPanal.SetActive(true);
            Debug.Log("Add new project");
        }
        LoadData.AutoSaveing();
    }

    public void LastWorkedProject()
    {

        string PathsString = File.ReadAllText(ProjectPathFiles);
        string[] ProjectsPath = PathsString.Split('$');
        if (ProjectsPath.Length != 0)
        {

            OpenProjectData.ProjectsPaths.AddRange(ProjectsPath);

            Debug.Log("Last projet path " + ProjectsPath[0] + "/ProjectData.json");
            if (File.Exists(ProjectsPath[0] + "/ProjectData.json"))
            {
                string jsonString = File.ReadAllText(ProjectsPath[0] + "/ProjectData.json");
                JsonUtility.FromJsonOverwrite(jsonString, projectData);
                CurrentFolderPath = ProjectsPath[0];
                //	LoadData.Load ();  
                SecoundPanal.SetActive(true);
            }
            else
            {
                CurrentFolderPath = ProjectsPath[0];
                //	LoadData.Load ();  
                SecoundPanal.SetActive(true);
                Debug.Log("Project has not saved any data");

            }
            if (!Directory.Exists(ProjectsPath[0] + "/Data/ImageFile"))
            {
                Directory.CreateDirectory(ProjectsPath[0] + "/Data/ImageFile");
            }
            DirectoryInfo d = new DirectoryInfo(ProjectsPath[0] + "/Data/ImageFile");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files    
            string str = "";
            foreach (FileInfo file in Files)
            {
				Debug.Log ("NumberS" + count);
				count++;  
                str = str + ", " + file.Name;
                Debug.Log("name is " + file.Name);
                Debug.Log("path is " + ProjectsPath[0] + "/Data/ImageFile/" + file.Name);
                openFileImage.loadExistingFile("file://" + ProjectsPath[0] + "/Data/ImageFile/" + file.Name, file.Name, false);
            }

            if (!Directory.Exists(ProjectsPath[0] + "/Data/VideoFile"))
            {
                Directory.CreateDirectory(ProjectsPath[0] + "/Data/VideoFile");
            }
            DirectoryInfo m = new DirectoryInfo(ProjectsPath[0] + "/Data/VideoFile");//Assuming Test is your Folder
            FileInfo[] FilesM = m.GetFiles("*.*"); //Getting Text files    
            string strM = "";
            foreach (FileInfo fileM in FilesM)
            {
				Debug.Log ("NumberM" + count);  
				count++;
                strM = strM + ", " + fileM.Name;
                Debug.Log("name is " + fileM.Name);
                Debug.Log("path is " + ProjectsPath[0] + "/Data/VideoFile/" + fileM.Name);
                openFileImage.loadExistingFile("file://" + ProjectsPath[0] + "/Data/VideoFile/" + fileM.Name, fileM.Name, false);
            }
            if (!Directory.Exists(ProjectsPath[0] + "/Data/UnityFile"))
            {
                Directory.CreateDirectory(ProjectsPath[0] + "/Data/UnityFile");
            }
            DirectoryInfo u = new DirectoryInfo(ProjectsPath[0] + "/Data/UnityFile");//Assuming Test is your Folder
            FileInfo[] FilesU = u.GetFiles("*.*"); //Getting Text files    
            string strU = "";
            foreach (FileInfo fileU in FilesU)
            {
                strU = strU + ", " + fileU.Name;
                Debug.Log("name is " + fileU.Name);
                Debug.Log("path is " + ProjectsPath[0] + "/Data/UnityFile/" + fileU.Name);

                openFileImage.loadExistingFile("file://" + ProjectsPath[0] + "/Data/UnityFile/" + fileU.Name, fileU.Name, true);
            }

            Debug.Log("Project path is " + ProjectsPath[0]);
        }
    }
	  
    public void SecoundLastWorkedProject()
    {
        string jsonString = File.ReadAllText(ProjectPathFiles);
        //JsonUtility.FromJsonOverwrite (jsonString, OpenProjectData);
        string[] ProjectsPath = jsonString.Split('$');

        string getFirstProjectPath = OpenProjectData.OpenProjectsDataPaths;

        string[] ProjectPath = getFirstProjectPath.Split('$');
        Debug.Log("First Project path  " + ProjectPath.Length);

        CurrentFolderPath = ProjectsPath[1];
        LoadData.Load();

        DirectoryInfo d = new DirectoryInfo(ProjectsPath[0] + "/Data/ImageFile");//Assuming Test is your Folder
        FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files    
        string str = "";
        foreach (FileInfo file in Files)
        {
            str = str + ", " + file.Name;
            Debug.Log("name is " + file.Name);
            Debug.Log("path is " + ProjectsPath[0] + "/Data/ImageFile/" + file.Name);
            openFileImage.loadExistingFile("file://" + ProjectsPath[0] + "/Data/ImageFile/" + file.Name, file.Name, false);
        }


        DirectoryInfo m = new DirectoryInfo(ProjectsPath[0] + "/Data/MovieFile");//Assuming Test is your Folder
        FileInfo[] FilesM = m.GetFiles("*.*"); //Getting Text files    
        string strM = "";
        foreach (FileInfo fileM in FilesM)
        {
            strM = strM + ", " + fileM.Name;
            Debug.Log("name is " + fileM.Name);
            Debug.Log("path is " + ProjectsPath[0] + "/Data/MovieFile/" + fileM.Name);
            openFileImage.loadExistingFile("file://" + ProjectsPath[0] + "/Data/MovieFile/" + fileM.Name, fileM.Name, false);
        }

        DirectoryInfo u = new DirectoryInfo(ProjectsPath[0] + "/Data/UnityFile");//Assuming Test is your Folder
        FileInfo[] FilesU = u.GetFiles("*.*"); //Getting Text files    
        string strU = "";
        foreach (FileInfo fileU in FilesU)
        {
            strU = strU + ", " + fileU.Name;
            Debug.Log("name is " + fileU.Name);
            Debug.Log("path is " + ProjectsPath[0] + "/Data/UnityFile/" + fileU.Name);
            openFileImage.loadExistingFile("file://" + ProjectsPath[0] + "/Data/UnityFile/" + fileU.Name, fileU.Name, true);
        }

        Debug.Log("Project path is " + ProjectsPath[0]);
    }

    public void ThirdLastWorkedProject()
    {
        string jsonString = File.ReadAllText(ProjectPathFiles);
        //JsonUtility.FromJsonOverwrite (jsonString, OpenProjectData);  
        string[] ProjectsPath = jsonString.Split('$');

        string getFirstProjectPath = OpenProjectData.OpenProjectsDataPaths;

        string[] ProjectPath = getFirstProjectPath.Split('$');
        Debug.Log("First Project path  " + ProjectsPath[2]);

        CurrentFolderPath = ProjectsPath[2];
        LoadData.Load();

        DirectoryInfo d = new DirectoryInfo(ProjectsPath[0] + "/Data/ImageFile");//Assuming Test is your Folder
        FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files    
        string str = "";
        foreach (FileInfo file in Files)
        {
            str = str + ", " + file.Name;
            Debug.Log("name is " + file.Name);
            Debug.Log("path is " + ProjectsPath[0] + "/Data/ImageFile/" + file.Name);
            openFileImage.loadExistingFile("file://" + ProjectsPath[0] + "/Data/ImageFile/" + file.Name, file.Name, false);
        }


        DirectoryInfo m = new DirectoryInfo(ProjectsPath[0] + "/Data/MovieFile");//Assuming Test is your Folder
        FileInfo[] FilesM = m.GetFiles("*.*"); //Getting Text files    
        string strM = "";
        foreach (FileInfo fileM in FilesM)
        {
            strM = strM + ", " + fileM.Name;
            Debug.Log("name is " + fileM.Name);
            Debug.Log("path is " + ProjectsPath[0] + "/Data/MovieFile/" + fileM.Name);
            openFileImage.loadExistingFile("file://" + ProjectsPath[0] + "/Data/MovieFile/" + fileM.Name, fileM.Name, false);
        }

        DirectoryInfo u = new DirectoryInfo(ProjectsPath[0] + "/Data/UnityFile");//Assuming Test is your Folder
        FileInfo[] FilesU = u.GetFiles("*.*"); //Getting Text files    
        string strU = "";
        foreach (FileInfo fileU in FilesU)
        {
            strU = strU + ", " + fileU.Name;
            Debug.Log("name is " + fileU.Name);
            Debug.Log("path is " + ProjectsPath[0] + "/Data/UnityFile/" + fileU.Name);
            openFileImage.loadExistingFile("file://" + ProjectsPath[0] + "/Data/UnityFile/" + fileU.Name, fileU.Name, true);
        }

        Debug.Log("Project path is " + ProjectsPath[0]);
    }

    public void BrowseAllProject()
    {

    }

    void Update()
    {
        //		Debug.Log (" ProjectPathFiles " + ProjectPathFiles);
    }
}
