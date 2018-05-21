using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateFolder : MonoBehaviour
{
    public List<string> FileVideoname = new List<string>();
    public List<string> ImageUrlPath = new List<string>();
    public List<Texture> Videoes = new List<Texture>();
    public List<Texture> photoes = new List<Texture>();
    public List<string> Filename = new List<string>();

    public GameObject FootageDragAndDrop;
    public GameObject PhotoContainer;  
    public GameObject PhotoPrfb;
    public GameObject Arrow;

    public InputField FolderName;

    public bool ImportPhotoesFolder;
    public bool ImportMovieFolder;
    public bool ImportUnityFolder;
    public bool DropFolder;
    public bool DropFiles;
    public bool Photo;
    public bool Video;
    public bool Extend;
    public bool Done;

    public int MovieFileCount;
    public int PhotoFileCount;
    public int UnityFilesCount;
    public int ModelFilesCount;
    int temp;
    public DropFolder DropFileRef;
    // Use this for initialization
    void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        DropFileRef = transform.parent.transform.parent.gameObject.GetComponent<DropFolder>();
        FootageDragAndDrop = FindObjectOfType<FootageDragAndDrop>().gameObject;
    }

    private void FindContantRowSize()
    {

        Debug.Log(" find coutant Row Size " + PhotoContainer.transform.childCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Arrow == null)
        {
            //	Arrow = gameObject.transform.GetChild (0).transform.GetChild (1).gameObject;

        }  

        FindContantRowSize();

        // We are creater tree view . of Imported 360 Image files
        if (ImportPhotoesFolder)
        {

            if (PhotoFileCount != FootageDragAndDrop.GetComponent<FootageDragAndDrop>().PhotoFiles.Count)
            {
				 
                for (int i = 0; i < FootageDragAndDrop.GetComponent<FootageDragAndDrop>().PhotoFiles.Count; i++)
                {
                    GameObject temPhoto = GameObject.Instantiate(PhotoPrfb);
                    temPhoto.transform.GetComponent<SelectFiles>().Image = true;
                    temPhoto.transform.parent = PhotoContainer.transform;
                    temPhoto.transform.localPosition = new Vector3(0, 0, 0);
                    temPhoto.transform.localScale = new Vector3(1, 1, 1);
                    temPhoto.GetComponent<RawImage>().texture = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().PhotoFiles[i].GetComponent<RawImage>().mainTexture;
                    temPhoto.transform.GetChild(0).GetComponent<InputField>().text = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().PhotoFiles[i].transform.GetChild(0).GetComponent<InputField>().text;
                    temPhoto.transform.GetChild(0).GetComponent<InputField>().textComponent.fontSize = 16;
                    temPhoto.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3(0, -35, 0);
                    temPhoto.transform.GetComponent<SelectFiles>().inPhotoViewer = false;
                    temPhoto.transform.GetComponent<SelectFiles>().scene.ImageURLPath = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().PhotoFiles[i].GetComponent<SelectFiles>().scene.ImageURLPath;
					temPhoto.transform.GetComponent<SelectFiles> ().id = FootageDragAndDrop.GetComponent<FootageDragAndDrop> ().PhotoFiles [i].GetComponent<SelectFiles> ().id;  
                    temPhoto.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 40);
                    temPhoto.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                    Debug.Log(temPhoto.transform.GetChild(2).gameObject.name + " SelectionBaseAttribute Filename ");
                    temp = i;
                }
                PhotoFileCount = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().PhotoFiles.Count;
                Debug.Log("Calling ");

                if (PhotoFileCount == temp)
                {
                    Arrow.GetComponent<ArrowViewControl>().OnArrow();
                    temp = 0;
                }
            }
            else
            {
                FootageDragAndDrop.GetComponent<FootageDragAndDrop>().PhotoFiles.Clear();
            }
        }

        // We are creater tree view . of Imported Moviw files
        if (ImportMovieFolder) 
        {  
            Debug.Log("Import Movie Folder !!!!!"); 
			Debug.Log("^^^^^^^Movie Count "+FootageDragAndDrop.GetComponent<FootageDragAndDrop>().MovieFiles.Count);
			Debug.Log("Movie File Count "+MovieFileCount);  
            if (MovieFileCount != FootageDragAndDrop.GetComponent<FootageDragAndDrop>().MovieFiles.Count)
            {
                Debug.Log("Movie Count "+FootageDragAndDrop.GetComponent<FootageDragAndDrop>().MovieFiles.Count);

                for (int i = 0; i < FootageDragAndDrop.GetComponent<FootageDragAndDrop>().MovieFiles.Count; i++)
                {
                    GameObject temPhoto = GameObject.Instantiate(PhotoPrfb);
                    temPhoto.transform.parent = PhotoContainer.transform;
                    temPhoto.transform.localPosition = new Vector3(0, 0, 0);
                    temPhoto.transform.localScale = new Vector3(1, 1, 1);    
                    temPhoto.transform.GetComponent<SelectFiles>().inPhotoViewer = true;
                    temPhoto.transform.GetComponent<SelectFiles>().Video = true;
                    temPhoto.transform.GetComponent<SelectFiles>().MediaFile = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().MovieFiles[i].GetComponent<SelectFiles>().MediaFile;
					temPhoto.transform.GetComponent<SelectFiles> ().id = FootageDragAndDrop.GetComponent<FootageDragAndDrop> ().MovieFiles[i].GetComponent<SelectFiles> ().id;  
                    temPhoto.GetComponent<RawImage>().texture = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().DefaultMovieImage;
                    temPhoto.transform.GetChild(0).GetComponent<InputField>().text = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().MovieFiles[i].transform.GetChild(0).GetComponent<InputField>().text;
                    temPhoto.transform.GetChild(0).GetComponent<InputField>().textComponent.fontSize = 16;
                    temPhoto.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3(0, -35, 0);
                    temPhoto.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 40);

                    temPhoto.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                    temPhoto.transform.GetChild(1).gameObject.SetActive(true);
                //    FootageDragAndDrop.GetComponent<FootageDragAndDrop>().MovieFiles.RemoveAt(i);
                }   
                MovieFileCount = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().MovieFiles.Count;

                if (MovieFileCount == temp)
                {
                 //   Arrow.GetComponent<ArrowViewControl>().OnArrow();
                    temp = 0;
                }
            }
            else
            {
                FootageDragAndDrop.GetComponent<FootageDragAndDrop>().MovieFiles.Clear();
            }
        }


        // We are creater tree view . of Imported Unity files
        if (ImportUnityFolder)
        {

            Debug.Log("Import Unity Folder");
            if (UnityFilesCount != FootageDragAndDrop.GetComponent<FootageDragAndDrop>().UnityFiles.Count)
            {

                UnityFilesCount = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().UnityFiles.Count;

                for (int i = 0; i < FootageDragAndDrop.GetComponent<FootageDragAndDrop>().UnityFiles.Count; i++)
                {
                    GameObject temPhoto = GameObject.Instantiate(PhotoPrfb);
                    temPhoto.transform.GetComponent<SelectFiles>().UnityObject = true;
                    temPhoto.transform.parent = PhotoContainer.transform;
                    temPhoto.transform.localPosition = new Vector3(0, 0, 0); 
                    temPhoto.transform.localScale = new Vector3(1, 1, 1);
                    temPhoto.GetComponent<RawImage>().texture = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().DefaultUnityImage;
                    temPhoto.GetComponent<SelectFiles>().UnityAssetBundle = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().UnityFiles[i];
                    temPhoto.GetComponent<SelectFiles>().UnityAssetPath = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().UnityAssetPath[i];  
					temPhoto.transform.GetComponent<SelectFiles> ().id = FootageDragAndDrop.GetComponent<FootageDragAndDrop> ().UnityFiles[i].GetComponent<SelectFiles> ().id;   
                    temPhoto.transform.GetChild(0).GetComponent<InputField>().text = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().UnityFiles[i].transform.GetChild(0).GetComponent<Text>().text;
                    temPhoto.transform.GetChild(0).GetComponent<InputField>().textComponent.fontSize = 16;
                    temPhoto.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3(0, -35, 0);
                    temPhoto.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 40);

                    temPhoto.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                }
                Debug.Log("Models Filees " + FootageDragAndDrop.GetComponent<FootageDragAndDrop>().ModelsFiles.Count);

            }

            if (ModelFilesCount != FootageDragAndDrop.GetComponent<FootageDragAndDrop>().ModelsFiles.Count)
            {
                for (int j = 0; j < PhotoContainer.transform.childCount; j++)
                {

                    GameObject.Destroy(PhotoContainer.transform.GetChild(j).gameObject);
                    UnityFilesCount = 0;
                }
                Debug.Log("Models Filees " + FootageDragAndDrop.GetComponent<FootageDragAndDrop>().ModelsFiles.Count);

                for (int i = 0; i < FootageDragAndDrop.GetComponent<FootageDragAndDrop>().ModelsFiles.Count; i++)
                {
                    GameObject temPhoto = GameObject.Instantiate(PhotoPrfb);
                    temPhoto.transform.parent = PhotoContainer.transform;
                    temPhoto.transform.GetComponent<SelectFiles>().UnityObject = true;
                    temPhoto.transform.localPosition = new Vector3(0, 0, 0);
                    temPhoto.transform.localScale = new Vector3(1, 1, 1);
                    Debug.Log("****" + i + "*****" + FootageDragAndDrop.GetComponent<FootageDragAndDrop>().ModelsFilesImage.Count);
                    temPhoto.GetComponent<RawImage>().texture = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().DefaultUnityImage;
                    temPhoto.GetComponent<SelectFiles>().UnityAssetBundle = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().ModelsFiles[i];

                    temPhoto.transform.GetChild(0).GetComponent<InputField>().text = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().ModelsFiles[i].gameObject.name;
                    temPhoto.GetComponent<SelectFiles>().UnityAssetPath = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().UnityAssetPath[i];
                    temPhoto.transform.GetChild(0).GetComponent<InputField>().textComponent.fontSize = 16;
                    temPhoto.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3(0, -35, 0);
                    temPhoto.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 40);

                    temPhoto.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                }
                ModelFilesCount = FootageDragAndDrop.GetComponent<FootageDragAndDrop>().ModelsFiles.Count;
            }
            else
            {
                FootageDragAndDrop.GetComponent<FootageDragAndDrop>().UnityFiles.Clear();
                //   Arrow.GetComponent<ArrowViewControl>().OnArrow();
            }
        }

        // Creating Folder from Drag and drop and Extend the tree view.
        if (DropFolder)
        {
            DropFolder = false;
            //	gameObject.transform.localPosition = new Vector3 (-160, 50, 0);
        }
        if (Extend)
        {
            if (Done == false)
            {

                transform.GetComponent<RectTransform>().sizeDelta = new Vector2(380, 350);
                Done = true;
            }

        }
        else if (Extend == false)

        {
            if (Done == true)
            {

                transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                Done = false;
            }
        }
    }

    // Create file into Photo and Movie Folder .
    public void Drop()
    {
        DropFolder = false;
        DropFileRef = gameObject.transform.parent.transform.parent.GetComponent<DropFolder>();

        if (DropFileRef.VideoFilter == true)
        {

            for (int i = 0; i < FileVideoname.Count; i++)
            {

                GameObject temPhoto = GameObject.Instantiate(PhotoPrfb);
                temPhoto.transform.parent = PhotoContainer.transform;
                temPhoto.transform.localPosition = new Vector3(0, 0, 0);
                temPhoto.transform.localScale = new Vector3(1, 1, 1);
                temPhoto.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                Debug.Log("Video");
                temPhoto.transform.GetChild(1).gameObject.SetActive(true);
                temPhoto.transform.GetChild(0).GetComponent<InputField>().text = FileVideoname[i];
                temPhoto.transform.GetChild(0).GetComponent<InputField>().textComponent.fontSize = 16;
                temPhoto.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3(0, -35, 0);
                temPhoto.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 40);
                temPhoto.transform.GetComponent<SelectFiles>().inPhotoViewer = true;
            }
        }
        if (DropFileRef.PhotoesFilter == true)
        {

            for (int i = 0; i < photoes.Count; i++)
            {

                GameObject temPhoto = GameObject.Instantiate(PhotoPrfb);
                temPhoto.transform.parent = PhotoContainer.transform;
                temPhoto.transform.localPosition = new Vector3(0, 0, 0);
                temPhoto.transform.localScale = new Vector3(1, 1, 1);
                temPhoto.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                Debug.Log("Photos");
                temPhoto.GetComponent<RawImage>().texture = photoes[i];
                temPhoto.transform.GetChild(0).GetComponent<InputField>().text = Filename[i];

                temPhoto.transform.GetChild(0).GetComponent<InputField>().textComponent.fontSize = 16;
                temPhoto.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3(0, -35, 0);
                temPhoto.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 40);
                temPhoto.transform.GetComponent<SelectFiles>().inPhotoViewer = true;
                temPhoto.transform.GetComponent<SelectFiles>().scene.ImageURLPath = ImageUrlPath[i];

            }
            ImageUrlPath.Clear();
        }

        DropFolder = true;
    }
}
