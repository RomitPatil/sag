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

namespace TriLib  
{
    namespace Samples
    {
        [RequireComponent(typeof(Button))]
        public class OpenFileImages : MonoBehaviour
        {

            public FootageDragAndDrop FootageDragAndDrop;
            public UICanvasControl ui_Canvas_control;
            public OpenProjectData openProjectData;
            public CustomLocation CustomLocation;
            public SceneProperties[] UnityScene;
            public GameObject FootageObjectPrfb;
            public GameObject FootageContainer;
            public AddProject AddProject;


            public List<GameObject> unitySceneObject = new List<GameObject>();
            public List<GameObject> unityAssetObject = new List<GameObject>();
            public List<Texture> ModaleFileImage = new List<Texture>();
            public List<GameObject> objObject = new List<GameObject>();
            public List<string> CacheMovieFiles = new List<string>();
            public List<string> CacheImageFiles = new List<string>();
            public List<string> CacheUnityFiles = new List<string>();
            public List<string> CacheModelFiles = new List<string>();
            public List<string> CacheModelImage = new List<string>();
            public List<string> Texture_URL = new List<string>();

            public GameObject[] gunneyItem;
            public GameObject ObjObject;

            public string ExtentionName = "";
            public string currentFolderPath;
            public string ProjectFileName;
            public string fileName = "";
            public string URL;
            public List<Sprite> yourSprite = new List<Sprite>();
            int yourIndex;
            Sprite sprite;
            public Texture2D yourTexture;
            public Texture2D ModelAsset;
            public Texture2D DefaultUnityImage;
            public Texture2D DefaultMovieImage;

            public bool UnityScene_Import;
			int id;  
            [System.Serializable]
            public struct SceneProperties
            {
                public string name;
                public Texture texture;
                public GameObject item;
            }


            WWW loadUM;

            void Start()
            {
                FootageDragAndDrop = FindObjectOfType<FootageDragAndDrop>(); 
                var button = GetComponent<Button>();
				id = 0;  
                button.onClick.AddListener(OnClick);
            }

            //  click to import file from file explorer
            public void OnClick()
            {

                var extensions = new[]  
                {
                    new ExtensionFilter("Image Files And Video Files", "png", "jpg", "mp4", "jpeg" ,"MP4","fbx","FBX","*"),
                };

                // Get the path of selected file.
                var paths = StandaloneFileBrowser.OpenFilePanel("", "", extensions, true);  

                if (paths.Length > 0)
                {

                    string name = ProjectFileName + "Data";


                    Debug.Log(AddProject.CurrentFolderPath + "/" + name);
                    string path = AddProject.CurrentFolderPath + "/" + name;

                    Debug.Log(path);
                    var folder = Directory.CreateDirectory(AddProject.CurrentFolderPath + "/" + name);
                    currentFolderPath = path;

                    for (int i = 0; i < paths.Length; i++)
                    {
                        Texture_URL.Add(new System.Uri(paths[i]).AbsoluteUri);
                        Debug.Log("File namr : " + paths[i]);
						string nameOfFile = GetFileName(paths[i]);  
						id++;
						StartCoroutine(OutputRoutine(new System.Uri(paths[i]).AbsoluteUri,nameOfFile,id));      
                    }
                } 
            }

            public void loadExistingFile(string path, string name, bool UnityImage)
            {
                Debug.Log(path + ";;;;;;;;");
				string[] parts = path.Split('.');
				if (parts.Length > 0)
				{
					ExtentionName = parts[parts.Length - 1];
				}
				else
				{
					ExtentionName = path; 
				}    
				id++;
				StartCoroutine(OutputRoutineTwo(path, name, UnityImage,ExtentionName,id));  
            }    

			public IEnumerator OutputRoutineTwo(string url, string name, bool UnityImage,string ExtentionName,int id)
            {  
				
                var loader = new WWW(url);  
                yield return loader;

                GetFileName(url);   
                Debug.Log("file name is " + url);

                FootageDragAndDrop.fileUrl.Add(url);

                CreateFolder[] createFolders = FindObjectsOfType<CreateFolder>();
                byte[] ImageFile = loader.bytes;
                Debug.Log("file name time -1 " + fileName);
                Texture2D Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
                if (Tex2D.LoadImage(ImageFile))
                    yourTexture = Tex2D;
                sprite = Sprite.Create(yourTexture, new Rect(0, 0, yourTexture.width, yourTexture.height), new Vector2(0, 0), 100f);
                yourSprite.Add(sprite);

                if (UnityImage == false) 
                { 

					if (ExtentionName == "png" || ExtentionName == "jpg" || ExtentionName == "PNG" || ExtentionName == "jpeg"|| ExtentionName == "JPG" || ExtentionName == "JPEG")
                    {
						Debug.Log ("&&&@" + ExtentionName + url);   
                        CacheImageFiles.Add(url);
                        var load = new WWW(CacheImageFiles[CacheImageFiles.Count - 1]);
                        Debug.Log("file name time 0 " + fileName);
                        yield return load;
                        Debug.Log("file name time 1 " + fileName);

                        if (load.error == null)
                        {
                            int i = 0;
                            FootageDragAndDrop.FileName.Add(name);
                            GameObject footag = GameObject.Instantiate(FootageObjectPrfb);
                            footag.transform.parent = FootageContainer.transform;
                            footag.transform.localPosition = new Vector3(0, 0, 0);
                            footag.transform.localScale = new Vector3(1, 1, 1);
                            Debug.Log("Sucess Image Asset");
                            footag.transform.GetChild(0).GetComponent<InputField>().text = name;
                            Debug.Log("file name time n " + name);
                            i++;
                            footag.transform.GetComponent<RawImage>().texture = load.texture;
                            FootageDragAndDrop.PhotoFiles.Add(footag);
                            footag.transform.GetComponent<SelectFiles>().inPhotoViewer = false;
                            footag.transform.GetComponent<SelectFiles>().Image = true;
							footag.transform.GetComponent<SelectFiles> ().id = id;  
                            string remove = CacheImageFiles[CacheImageFiles.Count - 1];
                            string FIlepath = remove.Replace(Path.GetFileName(remove), "");
                            Debug.Log(FIlepath + "^^^^");
                            footag.transform.GetComponent<SelectFiles>().scene.ImageURLPath = FIlepath + name;
                            Debug.Log(CacheImageFiles[CacheImageFiles.Count - 1] + "******");
                        }
                        else
                        {
                            Debug.Log("Error" + load.error);
                        }

                    }
                }

                if (UnityImage == false)
                {
					if ((ExtentionName == "mp4" || ExtentionName == "MP4"))
                    {
						Debug.Log ("&&&@" + ExtentionName + url); 
                        CacheMovieFiles.Add(url);
                        var loadMovie = new WWW(CacheMovieFiles[CacheMovieFiles.Count - 1]);
                        Debug.Log("file name time 0 " + fileName);
                        yield return loadMovie;
                        Debug.Log("file name time 1 " + fileName);

                        if (loadMovie.error == null)
                        {
                            int i = 0;
                            FootageDragAndDrop.FileName.Add(name);
                            GameObject footag = GameObject.Instantiate(FootageObjectPrfb);
                            footag.transform.parent = FootageContainer.transform;
                            footag.transform.localPosition = new Vector3(0, 0, 0);
                            footag.transform.localScale = new Vector3(1, 1, 1);
                            Debug.Log("Sucess Image Asset");
                            footag.transform.GetChild(0).GetComponent<InputField>().text = name;
                            Debug.Log("file name time n " + name);
                            i++;
                            footag.transform.GetComponent<RawImage>().texture = DefaultMovieImage;
                            FootageDragAndDrop.DefaultMovieImage = DefaultMovieImage;
                            FootageDragAndDrop.MovieFiles.Add(footag);
                            footag.transform.GetComponent<SelectFiles>().inPhotoViewer = false;
                            footag.transform.GetComponent<SelectFiles>().Video = true;
							footag.transform.GetComponent<SelectFiles> ().id = id;  
                            string remove = CacheMovieFiles[CacheMovieFiles.Count - 1];
                            string FIlepath = remove.Replace(Path.GetFileName(remove), "");
                            Debug.Log(FIlepath + "^^^");
                            footag.transform.GetComponent<SelectFiles>().MediaFile = FIlepath + name;
                            Debug.Log(CacheMovieFiles[CacheMovieFiles.Count - 1] + "******") ; 
							  
                        }
                        else  
                        {
                            Debug.Log("Error" + loadMovie.error);
                        }

                    }
                }

                if (UnityImage)
                {
                    Debug.Log(ExtentionName);

                    if (ExtentionName == "fbx" || ExtentionName == "obj" || ExtentionName == "FBX")
                    {
                        Debug.Log(url + "*****");  
						yield return StartCoroutine(objLoading(url,fileName,id));     
						  
                    }

                }
            }
			 
            // load file from path and creat file according to file extention and setup Ui of the file.
			public IEnumerator OutputRoutine(string url,string nameOfFile,int id)     

            {
				  
                Debug.Log("URL ::" + url);
                URL = url;

                var loader = new WWW(url);
                yield return loader;

                GetFileName(url);

                GetFileExtantion(nameOfFile); 

                FootageDragAndDrop.fileUrl.Add(url);

				string temp1;
				string[] parts = url.Split('/');   
				if (parts.Length > 0)
				{
					temp1 = parts[parts.Length - 2];  
				}
				else
				{
					temp1 = ""; 
				}


                CreateFolder[] createFolders = FindObjectsOfType<CreateFolder>();


                if (ExtentionName == "mp4" || ExtentionName == "MP4")
                {
                    Debug.Log("Video");
					 
                    byte[] moveFile = loader.bytes;

                    if (!Directory.Exists(currentFolderPath + "/" + "VideoFile"))
                    {
                        Debug.Log("Video folder" + currentFolderPath + "/" + "VideoFile");
                        var folder = Directory.CreateDirectory(currentFolderPath + "/" + "VideoFile");
                    }



                    File.WriteAllBytes(currentFolderPath + "/" + "VideoFile" + "/" + nameOfFile, moveFile);

                    string path = new System.Uri(currentFolderPath + "/" + "VideoFile" + "/" + nameOfFile).AbsoluteUri;

                    Debug.Log("path :" + path);


                    CacheMovieFiles.Add(path);


                    var load = new WWW(CacheMovieFiles[CacheMovieFiles.Count - 1]);
					temp1 = CacheMovieFiles [CacheMovieFiles.Count - 1];   
                    yield return load;
					  
                    if (load.error == null)
                    {
                        Debug.Log("Sucess Movie Asset");  
                        FootageDragAndDrop.FileName.Add(nameOfFile);
                        GameObject footag = GameObject.Instantiate(FootageObjectPrfb);
                        footag.transform.parent = FootageContainer.transform;
                        footag.transform.localPosition = new Vector3(0, 0, 0);
                        footag.transform.localScale = new Vector3(1, 1, 1);

                        footag.transform.GetChild(0).GetComponent<InputField>().text = nameOfFile; 
                        footag.transform.GetChild(1).gameObject.SetActive(true);

                        footag.transform.GetComponent<SelectFiles>().inPhotoViewer = false;
                        footag.transform.GetComponent<SelectFiles>().Video = true;
						footag.transform.GetComponent<SelectFiles>().MediaFile = temp1;    
						footag.transform.GetComponent<SelectFiles> ().id = id;  
							//CacheMovieFiles[CacheMovieFiles.Count - 1];  
                        FootageDragAndDrop.MovieFiles.Add(footag);
                    }

                }

                else if (ExtentionName == "png" || ExtentionName == "jpg" ||
                    ExtentionName == "PNG" || ExtentionName == "JPG" ||
					ExtentionName == "JPEG"||ExtentionName == "jpeg")   
                {

                    Debug.Log("Image");

                    byte[] ImageFile = loader.bytes;

                    if (!Directory.Exists(currentFolderPath + "/" + "ImageFile"))
                    {
                        Debug.Log("Image folder" + currentFolderPath + "/" + "ImageFile");
                        var folder = Directory.CreateDirectory(currentFolderPath + "/" + "ImageFile");
                    }

                    File.WriteAllBytes((currentFolderPath + "/" + "ImageFile" + "/" + nameOfFile), ImageFile); 

                    string path = new System.Uri(currentFolderPath + "/" + "ImageFile" + "/" + nameOfFile).AbsoluteUri;

                    Debug.Log("Image Path : " + path);

                    CacheImageFiles.Add(path);

                    var load = new WWW(CacheImageFiles[CacheImageFiles.Count - 1]);

                    yield return load;

                    if (load.error == null)
                    {
                        FootageDragAndDrop.FileName.Add(nameOfFile); 
                        GameObject footag = GameObject.Instantiate(FootageObjectPrfb);
                        footag.transform.parent = FootageContainer.transform;
                        footag.transform.localPosition = new Vector3(0, 0, 0);
                        footag.transform.localScale = new Vector3(1, 1, 1);
                        Debug.Log("Sucess Image Asset");
                        footag.transform.GetChild(0).GetComponent<InputField>().text = nameOfFile; 
                        footag.transform.GetComponent<RawImage>().texture = load.texture;
                        FootageDragAndDrop.PhotoFiles.Add(footag);
                        footag.transform.GetComponent<SelectFiles>().inPhotoViewer = false;
                        footag.transform.GetComponent<SelectFiles>().Image = true;
						footag.transform.GetComponent<SelectFiles> ().id = id;
                        footag.transform.GetComponent<SelectFiles>().scene.ImageURLPath = CacheImageFiles[CacheImageFiles.Count - 1];
                    }
                    else
                    {
                        Debug.Log("Error" + load.error); 
                    }

                }
                else if (ExtentionName != "png" || ExtentionName != "jpg" ||
                    ExtentionName != "PNG" || ExtentionName != "JPG" ||
                    ExtentionName != "JPEG" || ExtentionName != "mp4" || ExtentionName != "MP4")
                {

                    Debug.Log("Unity Asset");

                    byte[] UnityFile = loader.bytes;

                    if (!Directory.Exists(currentFolderPath + "/" + "UnityFile"))
                    {
                        Debug.Log("Unity folder" + currentFolderPath + "/" + "UnityFile");
                        var folder = Directory.CreateDirectory(currentFolderPath + "/" + "UnityFile");
                    }
                    Debug.Log(currentFolderPath + "/" + "UnityFile" + "/" + nameOfFile);
                    string path = currentFolderPath + "/" + "UnityFile" + "/" + nameOfFile; 

                    Debug.Log(currentFolderPath + "/" + "UnityFile" + "/" + nameOfFile); 

                    Debug.Log("Extention name is " + ExtentionName);

                    if (ExtentionName == "obj" || ExtentionName == "fbx" || ExtentionName == "OBJ" || ExtentionName == "FBX")  
                    {

                        Debug.Log("???Path of 3d Model:" + path);  
                        File.WriteAllBytes(path, UnityFile);

						yield return StartCoroutine(objLoading(path,nameOfFile,id));  
						 
                        CacheModelFiles.Add(path);

                        FootageDragAndDrop.UnityAssetPath.Add(path);  
                    }
                    else
                    {
                        CacheUnityFiles.Add(path);

                        FootageDragAndDrop.UnityAssetPath.Add(path);

                        var load = new WWW("file:///" + CacheUnityFiles[CacheUnityFiles.Count - 1]);

                        yield return load;

                        if (load.error == null)
                        {
                            Debug.Log("Sucess UnityAsset Bundle");
                            Debug.Log(load.error);

                            AssetBundle unityBundle = new AssetBundle();

                            unityBundle = load.assetBundle;

                            Debug.Log(unityBundle.name);

                            GameObject request = null;

                            if (unityBundle.name == "unityitem")
                            {
                                request = unityBundle.LoadAsset<GameObject>("UnityAssetItem");
                                yield return request;
                            }

                            if (unityBundle.name == "unityscene")
                            {
                                request = unityBundle.LoadAsset<GameObject>("UnityAssetScene");
                                yield return request;
                            }

                            string name = unityBundle.name;

                            Debug.Log("Unity bundle name :::::" + name);

                            if (load.error == null)
                            {
                                GameObject temp = request as GameObject;
                                temp.name = name;

                                if (temp != null)
                                {
                                    Debug.Log("temp gameobject name  " + temp.name);

                                    if (temp.gameObject.name == "unityscene")
                                    {
                                        unitySceneObject.Add(temp);  

                                        SceneFromAssetBundle();
                                    }
                                    else
                                    {
                                        unityAssetObject.Add(temp);
                                        ItemFromAssetBundle();
                                    }
                                }
                                unityBundle.Unload(false);
                            }
                        }

                        Debug.Log("Loader " + loader.error);
                    }

                }
            }

            // Load Unity Asset data and creat file.and add that object into gunney bag.
            void ItemFromAssetBundle()
            {

                gunneyItem = new GameObject[unityAssetObject[unityAssetObject.Count - 1].transform.childCount];

                for (int i = 0; i < unityAssetObject[unityAssetObject.Count - 1].transform.childCount; i++)
                {
                    GameObject footag = GameObject.Instantiate(FootageObjectPrfb);

                    footag.GetComponent<SelectFiles>().UnityAssetBundle = unityAssetObject[unityAssetObject.Count - 1];
                    footag.GetComponent<SelectFiles>().UnityObject = true;
                    footag.GetComponent<SelectFiles>().UnityAssetPath = CacheUnityFiles[CacheModelFiles.Count - 1];

                    footag.transform.parent = FootageContainer.transform;
                    footag.transform.localPosition = new Vector3(0, 0, 0);
                    footag.transform.localScale = new Vector3(1, 1, 1);

                    gunneyItem[i] = unityAssetObject[unityAssetObject.Count - 1].transform.GetChild(i).gameObject;
                    footag.transform.GetChild(0).GetComponent<InputField>().text = gunneyItem[i].gameObject.transform.GetChild(0).GetComponent<Text>().text;
                    footag.transform.GetComponent<RawImage>().texture = gunneyItem[i].gameObject.transform.GetComponent<Image>().mainTexture;
                    FootageDragAndDrop.FileName.Add(gunneyItem[i].gameObject.transform.GetChild(0).GetComponent<Text>().text);
                    FootageDragAndDrop.UnityFiles.Add(gunneyItem[i]);
                }

            }

            // load unity scene and create a object of unity asset 
            void SceneFromAssetBundle()
            {
				  
                UnityScene = new SceneProperties[UnityScene.Length + 1];

                Debug.Log("UnityScene ::  " + UnityScene.Length + ":: " + unitySceneObject[unitySceneObject.Count - 1].transform.GetChild(2).transform.GetChild(0).gameObject.name);

                UnityScene[UnityScene.Length - 1].name = unitySceneObject[unitySceneObject.Count - 1].transform.GetChild(0).transform.GetComponent<Text>().text;

                if (unitySceneObject[unitySceneObject.Count - 1].transform.GetChild(1).transform.GetComponent<Image>())
                {
                    UnityScene[UnityScene.Length - 1].texture = unitySceneObject[unitySceneObject.Count - 1].transform.GetChild(1).transform.GetComponent<Image>().mainTexture;
                }

                UnityScene[UnityScene.Length - 1].item = unitySceneObject[unitySceneObject.Count - 1].transform.GetChild(2).transform.GetChild(0).gameObject;

                Debug.Log("Scene from Asset Bundle");

                GameObject footag = GameObject.Instantiate(FootageObjectPrfb);

                footag.GetComponent<SelectFiles>().UnitySceneBundle = unitySceneObject[unitySceneObject.Count - 1];
                footag.GetComponent<SelectFiles>().UnityObject = true;
                footag.transform.parent = FootageContainer.transform;
                footag.transform.localPosition = new Vector3(0, 0, 0);
                footag.transform.localScale = new Vector3(1, 1, 1);
                footag.transform.GetChild(0).GetComponent<InputField>().text = UnityScene[UnityScene.Length - 1].name;
                footag.transform.GetComponent<RawImage>().texture = UnityScene[UnityScene.Length - 1].texture;

            }

            // Loading fbx and obj 3d models at runtime and add to GunnyBag and create a unity file
			IEnumerator objLoading(string url,string nameOfFile,int id) 
            {  
                Debug.Log(url);
                string path = url.Replace("file:///", "");
                path = url.Replace("file://", "");
                Debug.Log(url);
                path = path.Replace("%20", "");
                Debug.Log(url);
                Debug.Log("Obj is found" + path);  

                var assetLoader = new AssetLoader();

                Debug.Log("1");
                var loadedGameObject = assetLoader.LoadFromFile(path);

                Debug.Log("2");
                yield return loadedGameObject;

                CacheModelFiles.Add(path); 
                loadedGameObject.gameObject.name = nameOfFile;
                unityAssetObject.Add(loadedGameObject);
                objObject.Add(loadedGameObject);
                Debug.Log("%!^loaded"+loadedGameObject.gameObject.name);    
                ObjObject = loadedGameObject;
                FootageDragAndDrop.ModelsFiles.Add(loadedGameObject);
                FootageDragAndDrop.UnityAssetPath.Add(path);

                if (objObject != null)
                {
                    GameObject footag = GameObject.Instantiate(FootageObjectPrfb);

                    footag.transform.parent = FootageContainer.transform;
                    footag.transform.localPosition = new Vector3(0, 0, 0);
                    footag.transform.localScale = new Vector3(1, 1, 1);
                    footag.transform.GetChild(0).GetComponent<InputField>().text = nameOfFile; 
                    footag.GetComponent<SelectFiles>().UnityObject = true;
					footag.transform.GetComponent<SelectFiles> ().id = id;   
                    FootageDragAndDrop.DefaultUnityImage = DefaultUnityImage;
                    Debug.Log(ModaleFileImage.Count);
                    footag.transform.GetComponent<RawImage>().texture = DefaultUnityImage;
                    GameObject.Find("Unity3D_Project").gameObject.GetComponent<HideTheObjects>().hideObjects();
					  
                }
				   
            }  
            // Load Image for 3d Model desply and add the Image in unity File 
            IEnumerator ChooseImageOf3dModel()
            {

                var extensions = new[]
                {
                    new ExtensionFilter("Image Files for 3d Modale ", "png", "jpg", "jpeg" ),
                };

                var paths = StandaloneFileBrowser.OpenFilePanel("", "", extensions, false);
                Debug.Log("Model Image Path " + paths[0]);

                WWW ModeleImage = new WWW(paths[0]);
                yield return ModeleImage;

                byte[] modeleFile = ModeleImage.bytes;
                string path = currentFolderPath + "/" + "UnityFile" + "/" + Path.GetFileNameWithoutExtension(fileName) + ".jpg";

                Debug.Log("Path of Image Model:" + path);

                File.WriteAllBytes(path, modeleFile);
                Debug.Log(currentFolderPath + "/" + "UnityFile" + "/" + fileName);

                Texture2D T = ModeleImage.texture;
                ModaleFileImage.Add(T);

                GameObject footag = GameObject.Instantiate(FootageObjectPrfb);
                CacheModelImage.Add(path);


                footag.transform.parent = FootageContainer.transform;
                footag.transform.localPosition = new Vector3(0, 0, 0);
                footag.transform.localScale = new Vector3(1, 1, 1);
                footag.transform.GetChild(0).GetComponent<InputField>().text = fileName;
                footag.GetComponent<SelectFiles>().UnityObject = true;

                Debug.Log(ModaleFileImage.Count);
                footag.transform.GetComponent<RawImage>().texture = ModaleFileImage[ModaleFileImage.Count - 1];

                FootageDragAndDrop.ModelsFilesImage.Add(T);

                GameObject.Find("Unity3D_Project").gameObject.GetComponent<HideTheObjects>().hideObjects();
            }

            // Get file Name 
            private string GetFileName(string hrefLink)
            {
                string[] parts = hrefLink.Split('/');


                if (parts.Length > 0)
                    fileName = parts[parts.Length - 1];
                else
                    fileName = hrefLink;


                fileName = fileName.Replace("%", "_");
                return fileName;
            }

            // Get File Extention
            private string GetFileExtantion(string fileName)
            {

                string[] parts = fileName.Split('.');
                if (parts.Length > 0)
                {
                    ExtentionName = parts[parts.Length - 1];
                }
                else
                {
                    ExtentionName = fileName;
                }
                return ExtentionName; 
            }

            // Loading data from Cache.
            public void LoadData()
            {

                for (int i = 0; i < CacheMovieFiles.Count; i++)
                {
                    StartCoroutine(loadingMovie(CacheMovieFiles[i]));

                }
                for (int i = 0; i < CacheImageFiles.Count; i++)
                {
                    StartCoroutine(loadingImage(CacheImageFiles[i]));
                }

                for (int i = 0; i < CacheUnityFiles.Count; i++)
                {
                    StartCoroutine(loadingUnityAsset(CacheUnityFiles[i]));
                }


                for (int i = 0; i < CacheModelFiles.Count; i++)
                {
                    StartCoroutine(loadingModelAsset(CacheModelFiles[i], CacheModelImage[i]));
                }

            }

            // Loading Movie file data and create a file
            IEnumerator loadingMovie(string url)
            {

                var loadData = new WWW(url);

                yield return loadData;

                if (loadData.error == "")
                {

                    GameObject footag = GameObject.Instantiate(FootageObjectPrfb);
                    footag.transform.parent = FootageContainer.transform;
                    footag.transform.localPosition = new Vector3(0, 0, 0);
                    footag.transform.localScale = new Vector3(1, 1, 1);
                    Debug.Log("Sucess Movie Asset");

                    GetFileName(url);

                    GetFileExtantion(fileName);

                    footag.transform.GetChild(0).GetComponent<InputField>().text = fileName;
                    footag.transform.GetChild(1).gameObject.SetActive(true);
                    footag.transform.GetComponent<RawImage>().enabled = false;
                    footag.transform.GetComponent<SelectFiles>().inPhotoViewer = false;
                    FootageDragAndDrop.MovieFiles.Add(footag);

                }
                else
                {
                    Debug.Log("Movie Load :" + loadData.error);
                }

            }

            // Loading Image data and create file
            IEnumerator loadingImage(string url)
            {

                var loadData = new WWW(url);
                yield return loadData;
                if (loadData.error == "")
                {

                    GameObject footag = GameObject.Instantiate(FootageObjectPrfb);
                    footag.transform.parent = FootageContainer.transform;
                    footag.transform.localPosition = new Vector3(0, 0, 0);
                    footag.transform.localScale = new Vector3(1, 1, 1);
                    Debug.Log("Sucess Image Asset");

                    Debug.Log("Sucess Image Asset");
                    GetFileName(url);
                    GetFileExtantion(fileName);
                    footag.transform.GetChild(0).GetComponent<InputField>().text = fileName;
                    footag.transform.GetComponent<RawImage>().texture = loadData.texture;
                    footag.transform.GetComponent<SelectFiles>().scene.ImageURLPath = url;
                    FootageDragAndDrop.PhotoFiles.Add(footag);
                    footag.transform.GetComponent<SelectFiles>().inPhotoViewer = false;

                }
                else
                {
                    Debug.Log("Image Load :" + loadData.error);
                }


            }

            // Loading unity asset data and create file
            IEnumerator loadingModelAsset(string url, string url2)
            {
                var assetLoader = new AssetLoader();

                ;
                var loadData = assetLoader.LoadFromFile(url);
                yield return loadData;

                if (loadData != null)
                {

                    GameObject footag = GameObject.Instantiate(FootageObjectPrfb);
                    footag.transform.parent = FootageContainer.transform;
                    footag.transform.localPosition = new Vector3(0, 0, 0);
                    footag.transform.localScale = new Vector3(1, 1, 1);
                    Debug.Log("Sucess Imaeg Asset");

                    Debug.Log("Sucess Image Asset");
                    GetFileName(url);
                    GetFileExtantion(fileName);
                    footag.transform.GetChild(0).GetComponent<InputField>().text = loadData.name;
                    WWW loadImage = new WWW("file:///" + url2);
                    yield return loadImage;
                    footag.transform.GetComponent<RawImage>().texture = loadImage.texture;
                    FootageDragAndDrop.ModelsFiles.Add(loadData);
                    FootageDragAndDrop.ModelsFilesImage.Add(loadImage.texture);
                    footag.transform.GetComponent<SelectFiles>().inPhotoViewer = false;

                }
                else
                {
                    Debug.Log("Image Load :Error !!!");
                }

            }

            // Loading 3d medale data and create a file
            IEnumerator loadingUnityAsset(string url)
            {

                var loadData = new WWW(url);
                yield return loadData;
				 

                if (loadData.error == "")
                {


                    Debug.Log("Sucess Unity Asset");
                    Debug.Log("Sucess UnityAsset Bundle");


                    GetFileName(url);
                    GetFileExtantion(fileName);

                    if (ExtentionName != "fbx" || ExtentionName != "obj")
                    {
                        AssetBundle unityBundle = loadData.assetBundle;
                        AssetBundleRequest request = unityBundle.LoadAssetAsync<GameObject>("UnityAssetBundle");
                        yield return request;
                        if (loadData.error == "")
                        {
                            GameObject temp = request.asset as GameObject;
                            unityAssetObject.Add(temp);

                            gunneyItem = new GameObject[unityAssetObject[unityAssetObject.Count - 1].transform.childCount];
                            for (int i = 0; i < unityAssetObject[unityAssetObject.Count - 1].transform.childCount; i++)
                            {

                                GameObject footag = GameObject.Instantiate(FootageObjectPrfb);
                                footag.GetComponent<SelectFiles>().UnityAssetBundle = temp;
                                footag.transform.parent = FootageContainer.transform;
                                footag.transform.localPosition = new Vector3(0, 0, 0);
                                footag.transform.localScale = new Vector3(1, 1, 1);

                                footag.transform.GetChild(3).gameObject.SetActive(true);
                                Debug.Log("Sucess Unity Asset");
                                Debug.Log("Sucess UnityAsset Bundle");
                                gunneyItem[i] = unityAssetObject[unityAssetObject.Count - 1].transform.GetChild(i).gameObject;
                                FootageDragAndDrop.UnityFiles.Add(gunneyItem[i]);
                                FootageDragAndDrop.UnityAssetPath.Add(CacheModelFiles[i]);

                                gunneyItem[i] = unityAssetObject[unityAssetObject.Count - 1].transform.GetChild(i).gameObject;
                                footag.transform.GetChild(0).GetComponent<Text>().text = gunneyItem[i].gameObject.transform.GetChild(0).GetComponent<Text>().text;
                                footag.transform.GetComponent<RawImage>().texture = gunneyItem[i].gameObject.transform.GetComponent<Image>().mainTexture;
                                FootageDragAndDrop.FileName.Add(gunneyItem[i].gameObject.transform.GetChild(0).GetComponent<Text>().text);
                                GetFileExtantion(fileName);
                            }


                        }
                        else
                        {
                            Debug.Log("Loader " + loadData.error);
                        }
                    }
                    else
                    {


                    }
                }
            }
        }

    }

}

