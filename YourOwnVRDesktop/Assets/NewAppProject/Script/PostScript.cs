using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Text;
using UnityEngine;
using System.IO;
using System;
using SFB;

public class PostScript : MonoBehaviour {


    public List<string> PostUnityAssetUploadedFilepath = new List<string>();
    public List<string> PreUnityAssetUploadedFilepath = new List<string>();
    public List<string> UploadUnityAssetFilepath = new List<string>();
    public List<string> LocalUnityAssetFilepath = new List<string>();
    public List<string> LocalVideoURL = new List<string>();
    public List<string> LocalSceneVideoURL = new List<string>();
    public List<string> PostUploadedFilepath = new List<string>();
    public List<string> PreUploadedFilepath = new List<string>();
    public List<string> LocalFilepath = new List<string>();
    public List<string> UploadFilepath = new List<string>();

    public string ServerURL;
    public string DriveURL;
    public string ProjectDataURL;
    public string SaveLocationPath;
    public TriLib.Samples.OpenFileImages OpenFileImage;

    public GameObject ProjectIDDisplayPanal;
    public GameObject DomePreview;
    public GameObject DomeCamera;

    public PrevSaveLoad PreSaveLoad;
    string uploadedfilename = "";
    string ServerPath;
    string DrivePATH;
    string genrate;

    public Slider LoadingSlider;
    public Slider UploadSlider;

    public Image ProjectLoaderImages;
    public Image ProjectLoaderMovies;
    public Image ProjectLoaderAssets;

    public Text DisplayProjectID;
    float number;
    public bool Server;
    public bool Drive; 
    public string url = "";
    public string path = "";
    public IEnumerator UploadFileCo(string localFileName, string uploadURL)
    {


        uploadedfilename = Path.GetFileName(localFileName);
        print("Name:" + uploadedfilename);
        ServerPath = ServerURL;
       
        url = ServerPath + PrevSaveLoad.ProjectID + "/" + uploadedfilename + "?overwrite=true";
        Debug.Log(localFileName);
        if (localFileName != "")
        {
            WWW localfile = new WWW(localFileName);
            if (UploadSlider != null)
            {
                UploadSlider.value = 0;
                LoadingSlider.value = 0;
            }
            while (!localfile.isDone)
            {
                if (LoadingSlider != null)
                {
                    LoadingSlider.transform.GetChild(0).GetComponent<Text>().text = Path.GetFileName(localFileName).Replace("?overwrite=true", "");
                    float progress = localfile.progress;

                    //  Debug.Log("Downloading : " + progress + " % ");
                    LoadingSlider.value = progress;
                    LoadingSlider.transform.GetChild(1).GetComponent<Text>().text = (progress * 10).ToString() + "%";
                }
                yield return null;
            }

            yield return localfile;

            if (localfile.error == null)
            {
                Debug.Log("Loaded file sucessfully" + localFileName);
            }
            else
            {
                Debug.Log("Open file error : " + localfile.error);
                yield break;
            }

            WWWForm postFrom = new WWWForm();


            // Post the data to server 

            postFrom.AddBinaryData("", localfile.bytes, uploadedfilename, "File/image/*");


            Debug.Log("Upload URL :::" + url);
            WWW upload = new WWW(url, postFrom);

            // Uploading progress bar 
            while (!upload.isDone)
            {
                if (UploadSlider != null)
                {
                    UploadSlider.transform.GetChild(0).GetComponent<Text>().text = uploadedfilename;
                    float progress = upload.uploadProgress;
                    //   Debug.Log("Uploading  : " + progress*100 + " % ");
                    //    Debug.Log("UploadingRe  : " + progress + " % ");
                    UploadSlider.value = Mathf.Clamp01(progress) / 0.9f;
                    progress = progress * 100f;
                    progress = (int)progress;
                    UploadSlider.transform.GetChild(1).GetComponent<Text>().text = (progress).ToString() + "%";
                }
                yield return null;
            }

            yield return upload;
            Debug.Log("upload done :" + upload.text);

            //Checking from data type "Image " or " Movie " or " Asset"

            if (uploadedfilename.Contains(".jpg") || uploadedfilename.Contains(".JPG") || uploadedfilename.Contains(".png") || uploadedfilename.Contains(".PNG") || uploadedfilename.Contains(".JPEG") || uploadedfilename.Contains(".jpeg"))
            {

                // Adding path to List 

                
                UploadFilepath.Add(url);
                PreUploadedFilepath.Add(url);
                PostUploadedFilepath.Add(uploadedfilename + "?overwrite=true");

                //Progress File counter .
                if (ProjectLoaderImages != null)
                {
                    ProjectLoaderImages.fillAmount += 1 / LocalFilepath.Count;

                    string text = ProjectLoaderImages.transform.GetChild(1).GetComponent<Text>().text.ToString();

                    if (text != "")
                    {
                        number = float.Parse(text);
                    }
                    else
                    {
                        number = 0;
                    }
                    number = number + 1;
                    ProjectLoaderImages.transform.GetChild(1).GetComponent<Text>().text = number.ToString();
                    ProjectLoaderImages.transform.GetChild(0).GetComponent<Text>().text = "Total File : " + LocalFilepath.Count.ToString();
                }
            }
            else
            {
                // Assets data into PathList.

                UploadUnityAssetFilepath.Add(url);
                PreUnityAssetUploadedFilepath.Add(ServerPath);
                PostUnityAssetUploadedFilepath.Add(uploadedfilename + "?overwrite=true");

                // Assets file counter

                if (ProjectLoaderAssets != null && LocalUnityAssetFilepath.Count != 0)
                {
                    ProjectLoaderAssets.fillAmount += 1 / LocalUnityAssetFilepath.Count;
                    ProjectLoaderAssets.transform.GetChild(1).GetComponent<Text>().text += 1f;
                    ProjectLoaderAssets.transform.GetChild(0).GetComponent<Text>().text = "Total File : " + LocalFilepath.Count.ToString();
                }
            }
            if (upload.error == null)
            {


            }
            else
            {

                Debug.Log("Error during upload " + upload.error);
            }
        }
    }
    public IEnumerator DriveFileCo(string localFileName, string uploadURL)
    {
        uploadedfilename = Path.GetFileName(localFileName);
        print("Name:" + uploadedfilename);
        DrivePATH = DriveURL;
        if (!File.Exists(DrivePATH + PrevSaveLoad.ProjectID)) {
            Directory.CreateDirectory(DrivePATH + PrevSaveLoad.ProjectID);
        }
        path = DrivePATH + PrevSaveLoad.ProjectID + "/" + uploadedfilename;  
        Debug.Log(path);
        Debug.Log(uploadedfilename);
        Debug.Log(localFileName);

        if (localFileName != "")
        {
            WWW localfile = new WWW(localFileName);



            if (UploadSlider != null)
            {
                UploadSlider.value = 0;
                LoadingSlider.value = 0;
            }
            while (!localfile.isDone)
            {
                if (LoadingSlider != null)
                {
                    LoadingSlider.transform.GetChild(0).GetComponent<Text>().text = Path.GetFileName(localFileName).Replace("?overwrite=true", "");
                    float progress = localfile.progress;

                    //  Debug.Log("Downloading : " + progress + " % ");
                    LoadingSlider.value = progress;
                    LoadingSlider.transform.GetChild(1).GetComponent<Text>().text = (progress * 10).ToString() + "%";
                }
                yield return null;
            }

            yield return localfile;

            if (localfile.error == null)
            {
                Debug.Log("Loaded file sucessfully" + localFileName);
            }
            else
            {
                Debug.Log("Open file error : " + localfile.error);
                yield break;
            }
            Debug.Log(path + ">>>>>");
            File.WriteAllBytes(path, localfile.bytes);

           
            if (uploadedfilename.Contains(".jpg") || uploadedfilename.Contains(".JPG") || uploadedfilename.Contains(".png") || uploadedfilename.Contains(".PNG") || uploadedfilename.Contains(".JPEG") || uploadedfilename.Contains(".jpeg"))
            {

                // Adding path to List 


                UploadFilepath.Add(path);
                PreUploadedFilepath.Add(path);
                PostUploadedFilepath.Add(uploadedfilename);

                //Progress File counter .
                if (ProjectLoaderImages != null)
                {
                    ProjectLoaderImages.fillAmount += 1 / LocalFilepath.Count;

                    string text = ProjectLoaderImages.transform.GetChild(1).GetComponent<Text>().text.ToString();

                    if (text != "")
                    {
                        number = float.Parse(text);
                    }
                    else
                    {
                        number = 0;
                    }
                    number = number + 1;
                    ProjectLoaderImages.transform.GetChild(1).GetComponent<Text>().text = number.ToString();
                    ProjectLoaderImages.transform.GetChild(0).GetComponent<Text>().text = "Total File : " + LocalFilepath.Count.ToString();
                }
            }
            else if (uploadedfilename.Contains(".fbx") || uploadedfilename.Contains(".FBX") || uploadedfilename.Contains(".obj") )
            {
                // Assets data into PathList.

                UploadUnityAssetFilepath.Add(url);
                PreUnityAssetUploadedFilepath.Add(ServerPath);
                PostUnityAssetUploadedFilepath.Add(uploadedfilename + "?overwrite=true");

                // Assets file counter

                if (ProjectLoaderAssets != null && LocalUnityAssetFilepath.Count != 0)
                {
                    ProjectLoaderAssets.fillAmount += 1 / LocalUnityAssetFilepath.Count;
                    ProjectLoaderAssets.transform.GetChild(1).GetComponent<Text>().text += 1f;
                    ProjectLoaderAssets.transform.GetChild(0).GetComponent<Text>().text = "Total File : " + LocalFilepath.Count.ToString();
                }
            }
          
        }
    }
    // Methode attached to the Android build button .
    // 1. save the data.
    // 2. ProjectID panal Enable.
    // 3. DomeCamera Disable
    // 4. We gentrate the ID with Random Charaters and the numbers
    // 5. We replace the File formate.
    // 6. We create the file 
    // 7. We write the ProjectID .
    // 8. we start the Uploading method.

    public void SaveClick() {

        // 1. save the data.
        gameObject.GetComponent<SaveLoadData>().Save();
		 
        // 2. ProjectID panal Enable.
        ProjectIDDisplayPanal.SetActive (true); 

        // 3. DomeCamera Disable
        DomeCamera.SetActive (false);

        // 4. We gentrate the ID with Random Charaters and the numbers
        char c = (char)('A' + UnityEngine.Random.Range (0, 26));
		char c2 = (char)('A' + UnityEngine.Random.Range (0, 26));

        // 5. We replace the File formate.
        string p = gameObject.transform.GetComponent<SaveLoadData> ().FilePath;
		p = p.Replace ("ProjectData.json", "projectName.txt");

        // 6. We create the file 
        if (File.Exists (p)) {
			genrate = File.ReadAllText (p);
			Debug.Log (genrate + ":::::::::::::::::::::::::::::::::::");
			PrevSaveLoad.ProjectID = genrate;
		}else{

         // 7. We write the ProjectID .
            genrate = OpenFileImage.ProjectFileName; 
		    PrevSaveLoad.ProjectID = genrate + UnityEngine.Random.Range (1, 99).ToString () + c + UnityEngine.Random.Range (1, 9).ToString ()+ c2;
			string tem = genrate + UnityEngine.Random.Range (1, 99).ToString () + c + UnityEngine.Random.Range (1, 9).ToString () + c2;
		
			File.WriteAllText (p,tem);
	}
        // 8. we start the Uploading method.
        if (Server)
        {
            
            StartCoroutine(UploadFileINu());
        }
        if (Drive)
        {
            if (!Directory.Exists(DriveURL + "/" + PrevSaveLoad.ProjectID)) {
                Directory.CreateDirectory(DriveURL + "/" + PrevSaveLoad.ProjectID);
            }
            StartCoroutine(DriveFileINu());
        }
    }


    // 1. We clear all the older data from List.
    // 2. We are getting data from Prview Loader.
    // 3. Providing data to Uploading method and Loop it of Image.
    // 4. Getting data from PreviewLoader and store into List and provide that data to uploading method for asset.
    // 5. If our all Image and asset is Uploaded .
    // 6. Save from PreSaveloder.
    // 7. scenePreview and DomeCamera is false.
    public IEnumerator UploadFileINu()
    {

        // 1. We clear all the older data from List.
        UploadFilepath.Clear();
        PreUploadedFilepath.Clear();
        PostUploadedFilepath.Clear();
        PostUnityAssetUploadedFilepath.Clear();
        PreUnityAssetUploadedFilepath.Clear();

        // 2. We are getting data from Prview Loader.
        LocalFilepath = gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().SenceTexturePath;



        // 3. Providing data to Uploading method and Loop it of Image.
        for (int i = 0; i < LocalFilepath.Count; i++)
        {
            print("LocalFilePath:" + LocalFilepath[i]);

            // 5 LocalFilepath[i]. Image is Uploadeding  .
            if (LocalFilepath[i] != "")
            {
                yield return StartCoroutine(UploadFileCo(LocalFilepath[i], ServerURL));
                     }
           
        }
        
        // 4. Getting data from PreviewLoader and store into List and provide that data to uploading method for asset.
        for (int i = 0; i < gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.childCount; i++)
        {
            if (gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).GetComponent<PreviewScene>().url != "") {

                string LocalSceneUrl = gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).GetComponent<PreviewScene>().url;
                UploadFilepath.Add(LocalSceneUrl);
                yield return StartCoroutine(UploadFileCo(LocalSceneUrl, url));
           }

            Debug.Log("Enter on Containe");
            for (int j = 0; j < gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).transform.childCount; j++)
            {
                if (gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).transform.GetChild(j).GetComponent<TriLib.Samples.PreviewHotspot>().UnityObjectInstance != null)
                {

                    string urlAsset = gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).transform.GetChild(j).GetComponent<TriLib.Samples.PreviewHotspot>().UnityAssetpath;
                    LocalUnityAssetFilepath.Add(urlAsset);

                    Debug.Log("Enter on Containe" + urlAsset);

                    // 5.  asset is Uploaded .
                    yield return StartCoroutine(UploadFileCo(urlAsset, url));

                }
                if (gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).transform.GetChild(j).GetComponent<TriLib.Samples.PreviewHotspot>().VideoURL != "")
                {

                    string urlVideo = gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).transform.GetChild(j).GetComponent<TriLib.Samples.PreviewHotspot>().VideoURL;
                    LocalVideoURL.Add(urlVideo);

                    Debug.Log("Enter on Containe" + urlVideo);

                    // 5.  asset is Uploaded .
                    yield return StartCoroutine(UploadFileCo(urlVideo, url));

                }

              
            }


        }

            // 5. If our all Image and asset is Uploaded. 

            if (UploadFilepath.Count == LocalFilepath.Count)
            {
                Debug.Log("upload file count");
                gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().SenceTexturePath = UploadFilepath;
                gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().PreSceneTexturepath = PreUploadedFilepath;
                gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().PostSceneTexturepath = PostUploadedFilepath;
                gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().UnityAssetFilepath = UploadUnityAssetFilepath;
                gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().PreUnityAssetath = PreUnityAssetUploadedFilepath;
                gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().PostUnityAssetath = PostUnityAssetUploadedFilepath;
                DisplayProjectID.text = PrevSaveLoad.ProjectID;

                for (int i = 0; i < gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData.Length; i++)
                {
                if (gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].url != "")
                {
                    string filename = Path.GetFileName(gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].url);
                    string SUrl = ServerURL + DisplayProjectID.text + "/" + filename;
                    Debug.Log("We create the File path " + SUrl);
                    gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].url = SUrl;
                }

                for (int j = 0; j < gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData.Length; j++)
                    {

                        if (gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData[j].UnityAssetPath != "")
                        {

                            string SUrl = ServerURL + DisplayProjectID.text + "/" + gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData[j].ActionObject;
                            Debug.Log("We create the File path " + SUrl);
                            gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData[j].UnityAssetPath = SUrl;
                        }
                        if (gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData[j].VideoURL != "")
                        {
                            string filename = Path.GetFileName(gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData[j].VideoURL);
                            string SUrl = ServerURL + DisplayProjectID.text + "/" + filename;
                            Debug.Log("We create the File path " + SUrl);
                            gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData[j].VideoURL = SUrl;
                        }
                    }

                }

                // 6. Save from PreSaveloder.
                PreSaveLoad.Save();

                gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().SenceTexturePath = LocalFilepath;

                // 7. scenePreview and DomeCamera is false.
                DomePreview.SetActive(false);
                DomeCamera.SetActive(false);

                Debug.Log("Display ProjectID");

            if (Server)
            {
                Debug.Log(Application.dataPath + "/Resources/" + "PrefabForPreview /VrViewer");
                if (File.Exists(Application.dataPath + "/Resources/" + "PrefabForPreview /VrViewer.apk"))
                {
                    byte[] APKFile = File.ReadAllBytes(Application.dataPath + "/Resources/" + "PrefabForPreview /VrViewer.apk");
                    yield return APKFile;

                    string ApKExportPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/VrViewer.apk";

                    File.WriteAllBytes(ApKExportPath, APKFile);
                    yield return 0;

                }
                else
                {
                    Debug.Log("Not fount File " + Application.dataPath + "/Resources/" + "PrefabForPreview /VrViewer");
                }


                string ExportPath = Application.dataPath + "/Resources/" + "PrefabForPreview";
                var extensions = new[]
                {
            new ExtensionFilter("VR builder Apk File", "apk" ,"APK"),
            };

                // Get the path of selected file.
                StandaloneFileBrowser.OpenFilePanel("VrViewer", ExportPath, extensions, false);
            }
            if (Drive) {

                string ExportPath = SaveLocationPath;
                var extensions = new[]
                {
            new ExtensionFilter("VR builder App Data ", "" ,""),
            };

            }

                UploadSlider.transform.GetChild(0).GetComponent<Text>().text = "Completed ...";
                UploadSlider.value = 1;
            
        }
    }
    public IEnumerator DriveFileINu()
    {

        // 1. We clear all the older data from List.
        UploadFilepath.Clear();
        PreUploadedFilepath.Clear();
        PostUploadedFilepath.Clear();
        PostUnityAssetUploadedFilepath.Clear();
        PreUnityAssetUploadedFilepath.Clear(); 

        // 2. We are getting data from Prview Loader.
        LocalFilepath = gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().SenceTexturePath;
      
        // 3. Providing data to Uploading method and Loop it of Image.
        for (int i = 0; i < LocalFilepath.Count; i++)
        {
            print("LocalFilePath:" + LocalFilepath[i]);

            // 5 LocalFilepath[i]. Image is Uploadeding  .
            if (LocalFilepath[i] != "")
            {
                yield return StartCoroutine(DriveFileCo(LocalFilepath[i], DriveURL));
            }

        }

        // 4. Getting data from PreviewLoader and store into List and provide that data to uploading method for asset.
        for (int i = 0; i < gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.childCount; i++)
        {
            if (gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).GetComponent<PreviewScene>().url != "")
            {

                string LocalSceneUrl = gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).GetComponent<PreviewScene>().url;
                UploadFilepath.Add(LocalSceneUrl);
                yield return StartCoroutine(DriveFileCo(LocalSceneUrl, DriveURL));
            }

            Debug.Log("Enter on Containe");
            for (int j = 0; j < gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).transform.childCount; j++)
            {
                if (gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).transform.GetChild(j).GetComponent<TriLib.Samples.PreviewHotspot>().UnityObjectInstance != null)
                {

                    string urlAsset = gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).transform.GetChild(j).GetComponent<TriLib.Samples.PreviewHotspot>().UnityAssetpath;
                    LocalUnityAssetFilepath.Add(urlAsset);

                    Debug.Log("Enter on Containe" + urlAsset);

                    // 5.  asset is Uploaded .
                    yield return StartCoroutine(UploadFileCo(urlAsset, DriveURL));

                }
                if (gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).transform.GetChild(j).GetComponent<TriLib.Samples.PreviewHotspot>().VideoURL != "")
                {

                    string urlVideo = gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().NavigationCanvasPrv.transform.GetChild(i).transform.GetChild(j).GetComponent<TriLib.Samples.PreviewHotspot>().VideoURL;
                    LocalVideoURL.Add(urlVideo);

                    Debug.Log("Enter on Containe" + urlVideo);

                    // 5.  asset is Uploaded .
                    if (urlVideo != "")
                    {
                        yield return StartCoroutine(UploadFileCo(urlVideo, DriveURL));
                    }

                }


            }


        }

        // 5. If our all Image and asset is Uploaded. 

        if (UploadFilepath.Count == LocalFilepath.Count)
        {
            Debug.Log("upload file count");
            gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().SenceTexturePath = UploadFilepath;
            gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().PreSceneTexturepath = PreUploadedFilepath;
            gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().PostSceneTexturepath = PostUploadedFilepath;
            gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().UnityAssetFilepath = UploadUnityAssetFilepath;
            gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().PreUnityAssetath = PreUnityAssetUploadedFilepath;
            gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().PostUnityAssetath = PostUnityAssetUploadedFilepath;
            DisplayProjectID.text = PrevSaveLoad.ProjectID;

            for (int i = 0; i < gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData.Length; i++)
            {
                if (gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].url != "")
                {
                    string filename = Path.GetFileName(gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].url);
                    string SUrl = ServerURL + DisplayProjectID.text + "/" + filename;
                    Debug.Log("We create the File path " + SUrl);
                    gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].url = SUrl;
                }

                for (int j = 0; j < gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData.Length; j++)
                {

                    if (gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData[j].UnityAssetPath != "")
                    {

                        string SUrl = ServerURL + DisplayProjectID.text + "/" + gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData[j].ActionObject;
                        Debug.Log("We create the File path " + SUrl);
                        gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData[j].UnityAssetPath = SUrl;
                    }
                    if (gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData[j].VideoURL != "")
                    {
                        string filename = Path.GetFileName(gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData[j].VideoURL);
                        string SUrl = ServerURL + DisplayProjectID.text + "/" + filename;
                        Debug.Log("We create the File path " + SUrl);
                        gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().ScenesData[i].HotspotData[j].VideoURL = SUrl;
                    }
                }

            }

            // 6. Save from PreSaveloder.
            if (Drive) {
                PreSaveLoad.Drive = true;
                PreSaveLoad.Server = false; 
                PreSaveLoad.PreServerURL = DrivePATH ;
            }
            if (Server) {
                PreSaveLoad.Drive = false;
                PreSaveLoad.Server = true;
                PreSaveLoad.PreServerURL = ServerPath;
            }
            PreSaveLoad.Save();

            gameObject.transform.GetComponent<TriLib.Samples.PreviewLoader>().SenceTexturePath = LocalFilepath;

            // 7. scenePreview and DomeCamera is false.
            DomePreview.SetActive(false);
            DomeCamera.SetActive(false);

            Debug.Log("Display ProjectID");

            if (Server)
            { 
                Debug.Log(Application.dataPath + "/Resources/" + "PrefabForPreview /VrViewer");
                if (File.Exists(Application.dataPath + "/Resources/" + "PrefabForPreview /VrViewer.apk"))
                {
                    byte[] APKFile = File.ReadAllBytes(Application.dataPath + "/Resources/" + "PrefabForPreview /VrViewer.apk");
                    yield return APKFile;

                    string ApKExportPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/VrViewer.apk";

                    File.WriteAllBytes(ApKExportPath, APKFile);
                    yield return 0;

                }
                else
                {
                    Debug.Log("Not fount File " + Application.dataPath + "/Resources/" + "PrefabForPreview /VrViewer");
                }

                string ExportPath = Application.dataPath + "/Resources/" + "PrefabForPreview";
                var extensions = new[]
                {
            new ExtensionFilter("VR builder Apk File ", "apk" ,"APK"),
            };

                // Get the path of selected file.
                StandaloneFileBrowser.OpenFilePanel("VrViewer", ExportPath, extensions, false);
            }
            if (Drive)
            {

                string ExportPath = SaveLocationPath;
                var extensions = new[]
                {
            new ExtensionFilter("VR builder App Data ", "" ,""),
            };

            }

            UploadSlider.transform.GetChild(0).GetComponent<Text>().text = "Completed ...";
            UploadSlider.value = 1;

        }
    }
    // Back to Edit Mode.
    public void ok (){

		ProjectIDDisplayPanal.SetActive (false);
		DomePreview.SetActive (true);
				DomeCamera.SetActive (true);
	}



}
