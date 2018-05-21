using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageName : MonoBehaviour {

	public string imageURL;
	public string imageName;
	public bool Delete;
	public GameObject findDome;
	public int NavigateNo;
	public bool setImage;
	public bool AppPreview;
	// Use this for initialization
	void Start () {
		 findDome = GameObject.Find ("DomeFull");

		if (AppPreview) {
			setImage = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Delete) {
			Destroy (gameObject);
		} 
		if (setImage) {
			SetImageOnDome ();
		}

	}

	public void SetImageOnDome () {

		GameObject findDome = GameObject.Find ("DomeFull");
		for (int i =0 ; i < findDome.GetComponent<DomeImages>().Texture.Count ; i++){
			Debug.Log ("::::" +findDome.GetComponent<DomeImages> ().ImageName [i] );
			if (findDome.GetComponent<DomeImages> ().ImageName[i] == imageName) {
		
				findDome.GetComponent<MeshRenderer> ().material.mainTexture = findDome.GetComponent<DomeImages> ().Texture [i];
			}
		}
		setImage = false;
	}

	void OnDisable () {
		//findDome.GetComponent<MeshRenderer> ().material.mainTexture = null;
	}
}
