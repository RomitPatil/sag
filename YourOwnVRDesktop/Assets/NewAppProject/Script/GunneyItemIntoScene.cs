using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunneyItemIntoScene : MonoBehaviour {

	public AddGunneyBagItems GunneyDropDown;
	public TriLib.Samples.OpenFileImages AddButton ;
	public GameObject NavigationCanvas;
	// Use this for initialization
	void Start () {
		
	}

	public void AddGunneyItem (){
		for (int i = 0 ; i < AddButton.gunneyItem.Length ; i++){
			GameObject temp = AddButton.gunneyItem [i].gameObject;
			GameObject GunneyItem = GameObject.Instantiate (temp);
			GunneyItem.transform.parent = NavigationCanvas.transform;
		}
	
	}
	// Update is called once per frame
	void Update () {
		
	}
}
