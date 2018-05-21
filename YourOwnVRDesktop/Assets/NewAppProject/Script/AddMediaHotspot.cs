using SFB;
using UnityEngine;
using UnityEngine.UI;
using System;
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
        public class AddMediaHotspot : MonoBehaviour
        {
            public GameObject NavigationCanvas;
            public GameObject MediaHotspotTemplet;
            public Text BtnID; 
            public InputField InputUrl; 
            public InputField Pos_X;
            public InputField Pos_Y; 
            public InputField Pos_Z;
            public GameObject MediaHotspotPrbf;
            public GameObject ActiveScene; 
            public MediaHotspot SelectedMediaHotspot;  
            public GameObject DomeCamera;
            public SetupDome setupDome;
            public bool Potrate;   
            public bool Lanscape; 
            public bool FullScreen;
			public GameObject landscape, portait, inscene, fullscreen;  		   
            public GameObject FullSceenVideoUI;
			public Dropdown targetAction;  
            public GameObject[] AllHotspotTemplets;  

            public void Start()
            {
                Pos_X.onEndEdit.AddListener(delegate {setPosition(); });
                Pos_Y.onEndEdit.AddListener(delegate { setPosition(); });

            }

            public void Browse() {
				if (setupDome.selectedHotstop.GetComponent<MediaHotspot>().BrowseImage == false) {
					var extensions = new[] {
						new ExtensionFilter ("Video Files", "AVI", "avi", "mp4", "MP4", "avi", "mov", "MOV", "mpg", "MPG", "mpeg", "MPEG", "ogv", "OGV", "vp8", "webm", "wmv", "asf"),
					};

					// Get the path of selected file.
					var path = StandaloneFileBrowser.OpenFilePanel ("", "", extensions, false);

					InputUrl.text = path [0];
					if (InputUrl.text != "") { 
						SelectedMediaHotspot.videoURL = InputUrl.text;
						SelectedMediaHotspot.videoPlayer.url = InputUrl.text;
					}  
				} else {  
					var extensions = new[] {  
						new ExtensionFilter ("Image Files", "Jpg","Jpeg","Png"),  
					};

					// Get the path of selected file.
					var path = StandaloneFileBrowser.OpenFilePanel ("", "", extensions, false);  
					string url = path [0];  
					InputUrl.text = path [0];  
					StartCoroutine (loadTexture(url));  

					   
				}  
            }

			public IEnumerator loadTexture(string url){  
				var loader = new WWW(url);   
				yield return loader; 
				setupDome.selectedHotstop.transform.GetChild (0).transform.GetChild (1).transform.GetChild (2).GetComponent<MeshRenderer> ().material.mainTexture = loader.texture;   
				setupDome.selectedHotstop.GetComponent<MediaHotspot> ().ImageTexture = loader.texture;   
				 
			}

		

			public void EnableMediaHotspot() {
				 
                for (int j = 0; j < AllHotspotTemplets.Length; j++)
                {

                    if (AllHotspotTemplets[j].gameObject.name == "MediaHotspotPanel")
                    {

                        AllHotspotTemplets[j].SetActive(true);
                    }
                    else
                    {
                        AllHotspotTemplets[j].SetActive(false);
                    }
                }

            }

            public void Initiate()
            {
                EnableMediaHotspot();

                for (int i = 0; i < NavigationCanvas.transform.childCount; i++)
                {
                    if (NavigationCanvas.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        if (NavigationCanvas.transform.GetChild(i).GetComponent<SceneProperties>())
                        {
                            ActiveScene = NavigationCanvas.transform.GetChild(i).gameObject;
                        }
                    }
                }
                SetupDome.ButtonId++; 
                BtnID.text = "Btn ID :" + SetupDome.ButtonId;
                
                GameObject mediaHostspot = GameObject.Instantiate(MediaHotspotPrbf);
                mediaHostspot.name = "Btn_ID :" + SetupDome.ButtonId;
                mediaHostspot.transform.parent = ActiveScene.transform;
                mediaHostspot.transform.localPosition = Vector3.zero;
                mediaHostspot.transform.localScale = new Vector3(1, 1, 1);
                mediaHostspot.transform.eulerAngles = DomeCamera.transform.eulerAngles;
                mediaHostspot.GetComponent<MediaHotspot>().posx = Pos_X;
                mediaHostspot.GetComponent<MediaHotspot>().posy = Pos_Y;
                mediaHostspot.GetComponent<MediaHotspot>().posz = Pos_Z;
                mediaHostspot.GetComponent<MediaHotspot>().videoURL = InputUrl.text.ToString();
                mediaHostspot.GetComponent<MediaHotspot>().Potrate = Potrate;
                mediaHostspot.GetComponent<MediaHotspot>().landscape = Lanscape;
                mediaHostspot.GetComponent<MediaHotspot>().fullScreen = FullScreen;
                mediaHostspot.GetComponent<MediaHotspot>().setupDome = setupDome ;
                mediaHostspot.GetComponent<MediaHotspot>().NavigationCanvas = NavigationCanvas;
                mediaHostspot.GetComponent<MediaHotspot>().Btn_ID = SetupDome.ButtonId;
                SelectedMediaHotspot = mediaHostspot.GetComponent<MediaHotspot>();
               

                setupDome.SelectFile.GetComponent<SelectFiles>().EditScene = true;

            }  
            public void InitiateMediaHotspot()
            {
                Initiate();
            }

            public void PotrateMode() 
            {
                Potrate = true;
                Lanscape = false;
                FullScreen = false;
                SelectedMediaHotspot.changeAspetRatio = true;
                SelectedMediaHotspot.Potrate = Potrate;
                SelectedMediaHotspot.landscape = Lanscape;
                SelectedMediaHotspot.fullScreen = FullScreen;
				portait.GetComponent<Outline> ().enabled = true;
				landscape.GetComponent<Outline> ().enabled = false;
				fullscreen.GetComponent<Outline> ().enabled = false;
				inscene.GetComponent<Outline> ().enabled = true;
            } 

            public void LandscapeMode()
            {
                Lanscape = true;
                Potrate = false;
                FullScreen = false;  

                SelectedMediaHotspot.changeAspetRatio = true;

                SelectedMediaHotspot.Potrate = Potrate;
                SelectedMediaHotspot.landscape = Lanscape;
                SelectedMediaHotspot.fullScreen = FullScreen;
				portait.GetComponent<Outline> ().enabled = false;
				landscape.GetComponent<Outline> ().enabled = true;
				fullscreen.GetComponent<Outline> ().enabled = false;
				inscene.GetComponent<Outline> ().enabled = true;
            }
			 
			public void InscereenMode(){  
				FullScreen = false;
				SelectedMediaHotspot.fullScreen = FullScreen;  
				fullscreen.GetComponent<Outline> ().enabled = false;
				inscene.GetComponent<Outline> ().enabled = true; 
			}

            public void FullScreenMode()
            {
                FullScreen = true;  
                Potrate = false;
                Lanscape = false;

                SelectedMediaHotspot.changeAspetRatio = true;
                SelectedMediaHotspot.Potrate = Potrate;
                SelectedMediaHotspot.landscape = Lanscape;
                SelectedMediaHotspot.fullScreen = FullScreen;
				portait.GetComponent<Outline> ().enabled = false;
				landscape.GetComponent<Outline> ().enabled = false;
				fullscreen.GetComponent<Outline> ().enabled = true;
				inscene.GetComponent<Outline> ().enabled = false; 
            }

            public void setPosition()
            {
                Debug.Log("Set Position is call");
                if (SelectedMediaHotspot != null) {
                    string text = Pos_X.text;
                    int x = 0;
                    int.TryParse(text, out x);
                    Debug.Log(x);
                    SelectedMediaHotspot.transform.localPosition = new Vector3(x, SelectedMediaHotspot.transform.localPosition.y, SelectedMediaHotspot.transform.localPosition.z);
                    text = Pos_Y.text;
                    int y = 0;
                    int.TryParse(text, out y);

                    SelectedMediaHotspot.transform.localPosition = new Vector3(SelectedMediaHotspot.transform.localPosition.x, y, SelectedMediaHotspot.transform.localPosition.z);

                }
            }
        }  
		 
    }

   
}  