using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MediaHotspot : MonoBehaviour {

    public Vector3 Position;
    public bool Potrate;
    public bool landscape;
    public bool fullScreen;
    public GameObject NavigationCanvas;
    public TriLib.Samples.AddMediaHotspot addMediaHotspot;
    public SetupDome setupDome;
    public VideoPlayer videoPlayer;
    public VideoPlayer FullScreenVideoPlayer;
    public string videoURL;
    public GameObject VideoMesh;
    public GameObject MediaContainer;
    public Image FrameOutLine;  
    public bool hide;  
    public bool changeAspetRatio;
    public bool clicked;
    public string ActionFunction;  
    public int Btn_ID;
    public bool Preview; 
    public GameObject FullScreenVideoUIPrv;
	public float scaleX,scaleY;  
	public GameObject[] AllHotspotTemplets;
	public Sprite hotspotSprite; 
	public Dropdown targetAction; 
	public bool BrowseImage;  
	public Texture ImageTexture;  

    public InputField posx, posy, posz;
    // Use this for initialization
    void Start () {
            ActionFunction = "MediaHotspot";
            addMediaHotspot = GameObject.Find("AddMediaHotspot").GetComponent<TriLib.Samples.AddMediaHotspot>();
            addMediaHotspot.LandscapeMode();
			targetAction = addMediaHotspot.targetAction; 	  

			targetAction.onValueChanged.AddListener (delegate { 
			if (SetupDome.SelectedHotspot == gameObject) {
			//	InstantiateObjects (targetObject.captionText.text);  
				if(targetAction.captionText.text == "Image"){
						BrowseImage = true;    
					}
				else{
					BrowseImage = false;  
				} 
				}  
			});       
    }  
	 
    public void OnClickHide()
    {
        if (clicked == false)
        {
            StartCoroutine(WaitForSce()); 
        }
        
    }
    public void OnClickMediaHotspot()  
    {
       
        addMediaHotspot.SelectedMediaHotspot = gameObject.GetComponent<MediaHotspot>();
        addMediaHotspot.BtnID.text = "Btn ID :" + Btn_ID;     
        addMediaHotspot.Pos_X.text = Position.x.ToString();
        addMediaHotspot.Pos_Y.text = Position.y.ToString();
        addMediaHotspot.InputUrl.text = videoURL;  
        addMediaHotspot.Potrate = Potrate; 
        addMediaHotspot.Lanscape = landscape;
        addMediaHotspot.FullScreen = fullScreen;
        if (gameObject.transform.localPosition.z != 0) {
            gameObject.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
        }
		if (Potrate)
			addMediaHotspot.PotrateMode ();

		if (landscape)
			addMediaHotspot.LandscapeMode ();    
		  
		AllHotspotTemplets = addMediaHotspot.AllHotspotTemplets; 
		if (gameObject.transform.parent.gameObject.GetComponent<helpActionHotspot> ()) {
			for (int j = 0; j < AllHotspotTemplets.Length; j++)
			{  
				if (AllHotspotTemplets[j].gameObject.name == "HelpPanel")
				{

					AllHotspotTemplets[j].SetActive(true);
				}  
				else
				{
					AllHotspotTemplets[j].SetActive(false);
				}
			}  
		}
		else if (gameObject.transform.parent.gameObject.GetComponent<NewActionHotspot> ()) {  
			for (int j = 0; j < AllHotspotTemplets.Length; j++)
			{

				if (AllHotspotTemplets[j].gameObject.name == "ActionHotspotTemplet") 
				{

					AllHotspotTemplets[j].SetActive(true);
				}
				else
				{
					AllHotspotTemplets[j].SetActive(false);
				}
			}
		}
		else{  
			addMediaHotspot.EnableMediaHotspot ();  
		}

    }

    IEnumerator WaitForSce()
    {
        hide = !hide;
        MediaContainer.SetActive(hide);
        changeAspetRatio = true;
        clicked = true;
        yield return new WaitForSeconds(0.2f);
        clicked = false;
    }

    // Update is called once per frame
    void Update ()
    {

        Position = gameObject.transform.position;

        if (!Preview)
        {
            if (addMediaHotspot == null)
            {

                addMediaHotspot = GameObject.Find("AddMediaHotspot").GetComponent<TriLib.Samples.AddMediaHotspot>();
            }
        }
        else {
        //    FullScreenVideoUIPrv = GameObject.Find("FullScreenPreView").gameObject;
        }


        if (changeAspetRatio)
        {
            if (Potrate)
            {
                changeAspetRatio = false;
                VideoMesh.SetActive(true);
                FullScreenVideoPlayer.Stop();
                FrameOutLine.gameObject.SetActive(true);
                FullScreenVideoPlayer.gameObject.SetActive(false);
                VideoMesh.gameObject.SetActive(true);
                VideoMesh.gameObject.transform.localScale = new Vector3(80f, 100f, 1f);  
				VideoMesh.gameObject.transform.localPosition = new Vector3(0f, 0f, 1f);  
				FrameOutLine.rectTransform.sizeDelta = new Vector2(80+5f, 100+5f); 
				FrameOutLine.rectTransform.localPosition = new Vector3(0f, 0f, 1f);        
                FrameOutLine.GetComponent<BoxCollider>().size = new Vector3(80f, 100f, 4f);
                videoPlayer.url = videoURL;
            }
			   
            if (landscape)  
            {
                changeAspetRatio = false; 
                VideoMesh.SetActive(true);
                FullScreenVideoPlayer.Stop();
                FrameOutLine.gameObject.SetActive(true);
                FullScreenVideoPlayer.gameObject.SetActive(false);
                VideoMesh.gameObject.SetActive(true);
                VideoMesh.gameObject.transform.localScale = new Vector3(140f, 75f,1f); 
				FrameOutLine.rectTransform.sizeDelta = new Vector2(140f+5f, 75f+5f);   
                FrameOutLine.GetComponent<BoxCollider>().size = new Vector3(145f, 75f, 4f);
                if (videoURL != "")
                {
                videoPlayer.url = videoURL;  

                }
            }

            if (fullScreen)  
            {
                videoPlayer.Stop();
               
                FrameOutLine.gameObject.SetActive(false);
                VideoMesh.gameObject.SetActive(false);
                changeAspetRatio = false;

                if (!Preview)
                {
                    videoPlayer.playOnAwake = false;
                    videoPlayer.waitForFirstFrame = false;
                    FullScreenVideoUIPrv = addMediaHotspot.FullSceenVideoUI;
                    FullScreenVideoUIPrv.SetActive(true);
                    if (Application.platform == RuntimePlatform.OSXPlayer) {
                        FullScreenVideoUIPrv.GetComponent<Camera>().rect = new Rect(0.266f, 0.42f, 0.465f, 0.42f);
                    }
                    if (Application.platform == RuntimePlatform.WindowsPlayer)
                    {
                        FullScreenVideoUIPrv.GetComponent<Camera>().rect = new Rect(0.247f, 0.41f, 0.4699f, 0.38f);
                    }

					  
                }
                else {
                    VideoMesh.SetActive(false);
                    MediaContainer.SetActive(true);
                    FullScreenVideoPlayer.Stop();
                    videoPlayer.Stop();
                    FullScreenVideoPlayer.playOnAwake = false;
                    FullScreenVideoPlayer.waitForFirstFrame = false;
                    FullScreenVideoPlayer.GetComponent<Camera>().rect = new Rect(0, 0.05f, 0.83f, 0.83f);
                    FullScreenVideoUIPrv.transform.GetChild(0).gameObject.SetActive(true);
                    FullScreenVideoUIPrv.transform.GetChild(0).GetChild(0).GetComponent<MediaFullScrenController>().FullVideo = FullScreenVideoPlayer;
                }

                FullScreenVideoPlayer.gameObject.SetActive(true);
                FullScreenVideoPlayer.url = videoURL;
                

            }

        }
          
	}
   
}
