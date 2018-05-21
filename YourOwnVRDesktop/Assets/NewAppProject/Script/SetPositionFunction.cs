using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPositionFunction : MonoBehaviour {

	public GameObject SelectedHotspot;
	public GameObject ActiveHotspotContainer;
	public InputField Target_Pose_X;
	public InputField Target_Pose_Y;
	public InputField Radius;
	public GameObject SelectedScene;
	public bool Editing ;
	// Use this for initialization
	void Start () {
		
	}

	private void FindGameObjectbaseOfNameMatch (){
	
			ActiveHotspotContainer = GameObject.Find ("NavigationCanvas"); 
			Target_Pose_X = GameObject.Find ("Target Pos X").transform.GetChild(0).transform.GetComponent<InputField> ();
			Target_Pose_Y = GameObject.Find ("Target Pos Y").transform.GetChild(0).GetComponent<InputField> ();
			Radius = GameObject.Find ("Radius_Z").transform.GetChild(0).GetComponent<InputField> ();
		}
	
	
	
	// Update is called once per frame
	void Update () {
		
		
	}

	public void OnEnter() {
		Editing = true;
	}

	public void OnExit () {
		if (Editing){
			Debug.Log ("Left the Input field");
		OnleftInputField ();
		}
	}
	private void OnleftInputField(){

		for (int i = 0; i < ActiveHotspotContainer.transform.childCount; i++) {

			if (ActiveHotspotContainer.transform.GetChild (i).gameObject.activeInHierarchy) {
			
			}

			if (ActiveHotspotContainer.transform.GetChild (i).GetComponent<NewHotspot> ().Selected) {
				
				SelectedHotspot = ActiveHotspotContainer.transform.GetChild (i).gameObject;
				SelectedHotspot.transform.localPosition = new Vector3 (float.Parse (Target_Pose_X.text), float.Parse (Target_Pose_Y.text), float.Parse (Radius.text));
				
			}
		}

	}

}
