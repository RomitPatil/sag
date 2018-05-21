using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownSelection : MonoBehaviour {
	public GameObject ActiveHotspot;
	public string NavigateToScene;
	// Use this for initialization  
	void Start () {
		
	}
	public void DropdownInput (int Input){
		Debug.Log ("Option selected : " + Input);
//		NavigateToScene = gameObject.GetComponent<Dropdown> ().options [Input].text;
//
//		NewActiveHotspot.SelectedHotspot.GetComponent<NewActiveHotspot> ().NavigateToScene =NavigateToScene;
//		NewActiveHotspot.SelectedHotspot.GetComponent<NewActiveHotspot> ().DropdownValue =Input;

		ActiveHotspot = NewHotspot.SelectedHotspot;
		NavigateToScene = gameObject.GetComponent<Dropdown> ().options [gameObject.GetComponent<Dropdown> ().value].text;
		if (NewHotspot.SelectedHotspot != null) {
			NewHotspot.SelectedHotspot.GetComponent<NewHotspot> ().DropdownValue = gameObject.GetComponent<Dropdown> ().value;
			NewHotspot.SelectedHotspot.GetComponent<NewHotspot> ().NavigateToScene = NavigateToScene;
		}
	}
	void Update (){
	}
	// Update is called once per frame

}
