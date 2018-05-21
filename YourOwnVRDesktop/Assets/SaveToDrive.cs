using SFB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveToDrive : MonoBehaviour {

    public string LocalSavePath;
    public PostScript PostScriptRF;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void LocalDiskPath() {

        var extensions = new[]
               {
                    new ExtensionFilter("*"),
                };

        // Get the path of selected file.
        var paths = SFB.StandaloneFileBrowser.OpenFolderPanel("", "", false);
        LocalSavePath = paths[0];
        PostScriptRF.DriveURL = LocalSavePath;
        PostScriptRF.SaveLocationPath = LocalSavePath;
        PostScriptRF.Drive = true;
        PostScriptRF.SaveClick();

    }
}
