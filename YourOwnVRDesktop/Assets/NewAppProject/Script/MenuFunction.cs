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
using UnityEngine.EventSystems;

public class MenuFunction : MonoBehaviour , TimeInputHandler
{

    public string NavigateTo;
    public string PreviewHotspotGameobjectName;
    public GameObject NavigationContainerPrv;
    public TriLib.Samples.PreviewHotspot PreviewHotspot;

    public bool Preview;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (NavigationContainerPrv == null) {
            NavigationContainerPrv = GameObject.Find("NavigationCanvasPrv");
        }

        if (PreviewHotspotGameobjectName != "")
        {
            if (PreviewHotspot == null)
            {
                GameObject Pre = GameObject.Find(PreviewHotspotGameobjectName);
                PreviewHotspot = Pre.GetComponent<TriLib.Samples.PreviewHotspot>();
            }
        }
	}
    public void HandleTimedInput() {
        PreviewHotspot.NavigateToScene = NavigateTo;
        PreviewHotspot.ActionFunction = "MenuHotspot";
        PreviewHotspot.ClickOnHotspot();
        Debug.Log("M yha tk aaya||");
    }
    public void ClickMenuHotspot()
    {
        PreviewHotspot.NavigateToScene = NavigateTo;
        PreviewHotspot.ActionFunction = "MenuHotspot";
        PreviewHotspot.ClickOnHotspot();
    }

    }
