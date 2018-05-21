using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Steps : MonoBehaviour {

	public Button FirstStep;
	public Button SecoundStep;
	public Button ThirdStep;

	public GameObject FirstStepPanel;
	public GameObject SecoudStepPanel;
	public GameObject ThirdSteppanal;
	public GameObject DomeSetup;
	public SetupDome setupDome;  
	public GameObject PreviewSetup;
	public GameObject NavigationCanvas;

    
	// Use this for initialization
	void Start () {
		
	}

	public void OnFirstStep() {
	
		FirstStepPanel.SetActive (true);
		SecoudStepPanel.SetActive (false);
		ThirdSteppanal.SetActive (false);
		FirstStep.transform.GetComponent<Outline> ().enabled = true;
		SecoundStep.transform.GetComponent<Outline> ().enabled = false;
		ThirdStep.transform.GetComponent<Outline> ().enabled = false;
		PreviewSetup.SetActive (false);
		DomeSetup.SetActive (false);
	}
	public void OnSceoundStep () {
	

		FirstStepPanel.SetActive (true);
		SecoudStepPanel.SetActive (true);
		ThirdSteppanal.SetActive (false);
		FirstStep.transform.GetComponent<Outline> ().enabled = false;
		SecoundStep.transform.GetComponent<Outline> ().enabled = true;
		ThirdStep.transform.GetComponent<Outline> ().enabled = false;
		PreviewSetup.SetActive (false);
		for (int i = 0; i < PreviewSetup.transform.GetChild (0).childCount; i++) {
			GameObject.Destroy (PreviewSetup.transform.GetChild (0).GetChild (i).gameObject);
		}
		PreviewSetup.transform.GetChild (1).GetChild(1). GetComponent<RaycastingOnDome> ().enabled = false;
		PreviewSetup.transform.GetChild (1).GetChild(1). GetComponent<MouseCameraDraging> ().enabled = false;
		PreviewSetup.transform.GetChild (1).GetChild (2).GetComponent<MeshRenderer> ().material.mainTexture = PreviewSetup.GetComponent<PrevSaveLoad> ().DefaultTexture;
		NavigationCanvas.SetActive (true);
        if (DomeSetup.transform.GetChild(3).GetComponent<SetupDome>().GlobalMenuHotspot != null)
        {
            DomeSetup.transform.GetChild(3).GetComponent<SetupDome>().GlobalMenuHotspot.transform.parent = NavigationCanvas.transform;
          
        }
		DomeSetup.SetActive (true);
	} 
	public void OnThirdStep () {
	
		FirstStep.transform.GetComponent<Outline> ().enabled = false;
		SecoundStep.transform.GetComponent<Outline> ().enabled = false;
		ThirdStep.transform.GetComponent<Outline> ().enabled = true;
		ThirdSteppanal.SetActive (true); 
		//NavigationCanvas.SetActive (false);
		setupDome.DataOnPreviewLoader ();

	}
	// Update is called once per frame
	void Update () {
		
	}
}
