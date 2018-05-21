using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavInfo : MonoBehaviour {

	public string NevScene;
	public string ImageName;
	public DomeImages domeImage;
	public List<GameObject> Scenes = new List<GameObject>();
	 GameObject Container;
	public GameObject NevToScene;

	// Use this for initialization
	void Start () {
		Container = GameObject.Find("NavigationCanvas");
		for (int i = 0; i < Container.transform.childCount; i++) {
			Scenes.Add(Container.transform.GetChild (i).gameObject);
			if (Scenes [i].gameObject.name == NevScene) {
				NevToScene = Scenes [i].gameObject;
			}
			if (Scenes [i].gameObject.name != gameObject.transform.parent.name) {
				Scenes [i].gameObject.SetActive (false);
			} else {
				Scenes [i].gameObject.SetActive (true);
			}
		}
		Debug.Log (Container.transform.childCount);
	
	}

	void Update () {
		
	}

	public void NevigateScene ( ) {
	
		NevToScene.gameObject.SetActive (true);
		domeImage.ActiveImage = ImageName;
		domeImage.Active = true;
		gameObject.transform.parent.gameObject.SetActive (false);

	}


}
