using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deletehotspot : MonoBehaviour {

	public string SceneNo;
	public string navigationSceneName;
	public UICanvasControl ui_Canvas_control;
	public GameObject NavigationCanvas;
	public GameObject ActiveHotspot;
	public Text title;
	// Use this for initialization
	void Start () {
		ui_Canvas_control = GameObject.FindObjectOfType<UICanvasControl> ();

	}
	void Update () {

	}

	public void HotspotDelete () {

		ui_Canvas_control.centerPanel.SetActive (false);
		string SeletedHotspot = ActiveHotspot.gameObject.name;


		for (int i = 0; i < NavigationCanvas.transform.childCount; i++) {
			Debug.Log (NavigationCanvas.transform.GetChild (i).gameObject.name);
			if (NavigationCanvas.transform.GetChild (i).gameObject.activeInHierarchy) {
				SceneNo = NavigationCanvas.transform.GetChild (i).gameObject.name;
				SceneNo = SceneNo.Substring (SceneNo.Length - 1);
				Debug.Log (SceneNo);
				for (int j = 0; j < NavigationCanvas.transform.GetChild (i).transform.childCount; j++) {
					
					string hotspotName = NavigationCanvas.transform.GetChild (i).transform.GetChild (j).name;
					Debug.Log (hotspotName);
					string SelectedItem = hotspotName.Substring (0, hotspotName.Length - 2);
					Debug.Log (SelectedItem + " SelectedItem");
					hotspotName = hotspotName.Substring (hotspotName.Length - 1);
					Debug.Log (hotspotName);
					hotspotName = SceneNo + "_" + hotspotName;
					Debug.Log (hotspotName + "::" + SeletedHotspot);
	
					if (hotspotName == SeletedHotspot) {
						
						string titleName = title.text.ToString ().Substring (0, title.text.ToString ().Length - 2);

						Debug.Log ("titleName" + titleName);
						if (titleName == "Arrow") {
							if (SelectedItem == "Arrow") {
								for (int k = 0 ;k< ui_Canvas_control.sceneList [int.Parse(ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Arrowlist.Count;  k++ )
								{
								//	Debug.Log(ui_Canvas_control.sceneList [int.Parse(ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Arrowlist[k].name);
								//	Debug.Log (ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Arrowlist [k].name + "::" + NavigationCanvas.transform.GetChild (i).transform.GetChild (j).gameObject.name);
									if (ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Arrowlist [k].name == NavigationCanvas.transform.GetChild (i).transform.GetChild (j).gameObject.name) {
										
										ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Arrowlist.Remove(ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Arrowlist [k]);
										GameObject.Destroy (NavigationCanvas.transform.GetChild (i).transform.GetChild (j).gameObject);
									}

								}
							//	Debug.Log ("Arrow ::" + NavigationCanvas.transform.GetChild (i).transform.GetChild (j).gameObject);
								ui_Canvas_control.hotspotTitle.text = "";
							}
						} 

						if (titleName == "Info") {
							if (SelectedItem == "Info") {
								for (int k = 0 ;k< ui_Canvas_control.sceneList [int.Parse(ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Infolist.Count;  k++ )
								{
									//	Debug.Log(ui_Canvas_control.sceneList [int.Parse(ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Arrowlist[k].name);
									//	Debug.Log (ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Arrowlist [k].name + "::" + NavigationCanvas.transform.GetChild (i).transform.GetChild (j).gameObject.name);
									if (ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Infolist [k].name == NavigationCanvas.transform.GetChild (i).transform.GetChild (j).gameObject.name) {

										ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Infolist.Remove(ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().Infolist [k]);
										GameObject.Destroy (NavigationCanvas.transform.GetChild (i).transform.GetChild (j).gameObject);
									}
								
								}
								//	Debug.Log ("Arrow ::" + NavigationCanvas.transform.GetChild (i).transform.GetChild (j).gameObject);
								ui_Canvas_control.hotspotTitle.text = "";
							}
						} 
						if(titleName == "Hotspot") {
							if (hotspotName == SeletedHotspot) {
								if (SelectedItem == "Hotspot") {
									for (int k = 0 ;k< ui_Canvas_control.sceneList [int.Parse(ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().hotspotlist.Count;  k++ )
									{
										if (ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().hotspotlist [k].name == NavigationCanvas.transform.GetChild (i).transform.GetChild (j).gameObject.name) {

											ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().hotspotlist.Remove(ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().hotspotlist [k]);
											GameObject.Destroy (NavigationCanvas.transform.GetChild (i).transform.GetChild (j).gameObject);
										}
									}

									ui_Canvas_control.hotspotTitle.text = "";
								}
							}
							GameObject tem = GameObject.FindGameObjectWithTag ("dome");
							tem.SetActive (true);
						}
						if(titleName == "Action") {
							if (hotspotName == SeletedHotspot) {
								if (SelectedItem == "Action") {
									for (int k = 0 ;k< ui_Canvas_control.sceneList [int.Parse(ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().ActionHotspot.Count;  k++ )
									{
										if (ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().ActionHotspot [k].name == NavigationCanvas.transform.GetChild (i).transform.GetChild (j).gameObject.name) {

											ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().ActionHotspot.Remove(ui_Canvas_control.sceneList [int.Parse (ui_Canvas_control.SceneTitle.name)].GetComponent<DetailScene> ().ActionHotspot [k]);
											GameObject.Destroy (NavigationCanvas.transform.GetChild (i).transform.GetChild (j).gameObject);
										}
									}

									ui_Canvas_control.hotspotTitle.text = "";
									Camera.main.GetComponent<Raycasting> ().dots = true;
									Camera.main.GetComponent<Raycasting> ().ActiveHotspot = false;
								}
							}
						}
					}
					
				
				}

			}
		}

	}
}
