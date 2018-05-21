using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddGunneyBagItems : MonoBehaviour {

	public TriLib.Samples.OpenFileImages AddButton;
	public Dropdown GunneyDropDown;
	public List<string> gunneyItemList = new List<string>();
	// Use this for initialization
	void Start () { 
		   
	}

	// Update is called once per frame 
	void Update () {
		if (AddButton == null) {
			AddButton = FindObjectOfType<TriLib.Samples.OpenFileImages> ();
		}

		if (GunneyDropDown.options.Count != AddButton.gunneyItem.Length) {  
		
			GunneyDropDown.options.Clear ();
			for (int i = 0; i < AddButton.gunneyItem.Length; i++) {
				gunneyItemList.Add (AddButton.gunneyItem [i].gameObject.name);
			}
			GunneyDropDown.AddOptions(gunneyItemList);
		}
	}
}
