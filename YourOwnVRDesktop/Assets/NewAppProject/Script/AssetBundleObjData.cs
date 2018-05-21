using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetBundleObjData : MonoBehaviour {

	public string ObjectName;
	public Vector3 ObjectPostion;

	public Text NameText;
	public Text ObjectDataText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		NameText.text = ObjectName.ToString ();
		ObjectDataText.text = ObjectPostion.ToString ();
	}
}
