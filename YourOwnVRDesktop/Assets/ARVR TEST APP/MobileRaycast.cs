using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileRaycast : MonoBehaviour {

	public GameObject cube;
	public LayerMask layermask;
	public Image FanSlider;
	public Image AlarmSlider;
	public Image LaptopSlider;
	public bool Targeted;
	public float fillTime = 2f;
	public float timer;
	public float Fixtimer;
	public bool gazedAt;
	private Coroutine fillBarRoutine;
	public GameObject[] SelectedObjects;
	public GameObject[] ActionNevObject;
	public GameObject[] LineRenderObjects;
	// Use this for initialization
	void Start () {

		ActionNevObject = GameObject.FindGameObjectsWithTag ("ActionNev");
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Vector3 ScreenPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if (Physics.Raycast (transform.position, transform.forward ,out hit , 1000f)) {
			//Debug.Log (hit.point + "hit");
			Debug.DrawRay (transform.position, hit.point, Color.green);
			Debug.Log (hit.collider.gameObject.name);
			if (hit.collider.gameObject.tag == "SelectObject") {

				cube = hit.collider.gameObject;
				string name = cube.gameObject.name;
				
					for (int i = 0; i < SelectedObjects.Length; i++) {
					if (SelectedObjects [i].gameObject.name != name) {
						SelectedObjects [i].GetComponent<BoxCollider> ().enabled = false;
					}
					}


				}
			if (hit.collider.gameObject.tag == "ActionNev") {

				hit.collider.gameObject.GetComponent<MeshRenderer> ().enabled = true;
				hit.collider.gameObject.transform.parent.transform.GetChild (1).GetComponent<LineRenderer> ().enabled = true;
			
			}

			if (hit.collider.gameObject.tag == "MobileCollider") {
			
				for (int i = 0; i < ActionNevObject.Length; i++) {
					ActionNevObject [i].GetComponent<MeshRenderer> ().enabled = false;
					LineRenderObjects [i].GetComponent<LineRenderer> ().enabled = false;
				}
			}

			if (cube != null) {
				cube.GetComponent<BoxCollider> ().enabled = false;
			} else {
			}		if (Targeted) {
				cube.transform.position = hit.point;
				//cube.transform.rotation = Quaternion.LookRotation (hit.normal);
			}
	}
		if (Targeted){
			Vector3 startPoint = cube.transform.position;
			Vector3 EndPoint = new Vector3(0,0,0);
			float distance = 0;

			Fixtimer += Time.deltaTime;
			if (Fixtimer < 5) {
				int temNO=0;
				if (Fixtimer > 3) {
					Debug.Log ("Enter");

					distance = Vector3.Distance (startPoint, EndPoint);
					temNO = (int)distance;
					Debug.Log ("distance" + distance);
				}
					if (Fixtimer > 4) {
						int no =(int) distance;
						if (no == temNO) {
						string name = cube.gameObject.name;
						Targeted = false;
						cube = null;
					
							Debug.Log ("null");
							Fixtimer = 0;
						for (int i = 0; i < SelectedObjects.Length; i++) {
							
							if (SelectedObjects [i].gameObject.name != name) {
								SelectedObjects [i].GetComponent<BoxCollider> ().enabled = true;
							}
							}
						}
				}
				}
			}
		ActionNevObject = GameObject.FindGameObjectsWithTag ("ActionNev"); 

		LineRenderObjects = GameObject.FindGameObjectsWithTag ("LineRender");


}

	public void PointerEnter () {
	
		gazedAt = true;
		for (int i = 0; i < SelectedObjects.Length; i++) {
			SelectedObjects [i].GetComponent<BoxCollider> ().enabled = true;
		}

		fillBarRoutine = StartCoroutine (FillBar());

	
	}
	public void PointExit() {
	
		gazedAt = true;

		if (fillBarRoutine != null) {
			StopCoroutine (fillBarRoutine);
		}

		timer = 0f;
		if (cube != null) {
			if (cube.gameObject.name == "Fan") {
		
				FanSlider.fillAmount = 0f;
			}
			if (cube.gameObject.name == "Laptop") {
				LaptopSlider.fillAmount = 0f;
			}
			if (cube.gameObject.name == "Alarm") {
				AlarmSlider.fillAmount = 0f;
			}
		}
	}

	private IEnumerator FillBar() {
		timer = 0f;
		while (timer < fillTime) {
			timer += Time.deltaTime;

			if (cube.gameObject.name == "Fan") {
				FanSlider.fillAmount = timer / fillTime;
			}
			if (cube.gameObject.name == "Laptop") {
				LaptopSlider.fillAmount = timer / fillTime;
			}
			if (cube.gameObject.name == "Alarm") {
				AlarmSlider.fillAmount = timer / fillTime;
			}
			if (timer > 2f) {
				Targeted = true;
			}
					yield return null;					
				if (gazedAt)					
					continue;					
				timer = 0f;				
				FanSlider.fillAmount = 0f;				
				LaptopSlider.fillAmount = 0f;				
				AlarmSlider.fillAmount = 0f;	
				yield break;
		}



}
}