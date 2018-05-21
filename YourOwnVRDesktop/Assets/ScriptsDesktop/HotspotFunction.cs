using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotspotFunction : MonoBehaviour {
	public GameObject TargetCamera;
	public string NavigateScene;
	public int NavigateNo;
	public Quaternion CustomLocROt;
	public string InfoText;
	public int InfoFontSize;
	public Vector2 InfoBoxSize;
	public string HotspotType;
	public Transform ActionHotspot;
	public Vector3 ActionPos;
	public Vector3 ActionColliderPos;
	public Vector3[] ActionDots;	
	public Quaternion ActionColliderPosRot;
	public Quaternion NavigatePointRot;
	public Quaternion ActionRotation;

	public GameObject ActionCollider;
	public GameObject NavigationPoint;
	public GameObject ActionHotspotMesh;
	public LineRenderer LineRender;

	Vector3[] vertices2D;
	Vector2[] vert2D;
	// Use this for initialization
	void Start () {
		TargetCamera = Camera.main.gameObject;
		if (HotspotType == "Info") {
			gameObject.transform.GetChild (0).GetComponent<RectTransform> ().sizeDelta = InfoBoxSize;
			gameObject.transform.GetChild (1).GetComponent<RectTransform> ().sizeDelta = InfoBoxSize;
			gameObject.transform.GetChild (1).GetComponent<Text> ().text = InfoText;
			gameObject.transform.GetChild (1).GetComponent<Text> ().fontSize = InfoFontSize;

		}
		if (HotspotType == "Action") {
			CreateMesh ();
		}
	}
	public void NavegateToScene (){

		for (int i = 0; i < gameObject.transform.parent.transform.parent.childCount; i++) {
			Debug.Log (gameObject.transform.parent.transform.parent.GetChild(i).name + "  :::" + (NavigateScene +"_"+NavigateNo).ToString());
			if (gameObject.transform.parent.transform.parent.GetChild(i).name == (NavigateScene +"_"+NavigateNo).ToString()) {
				
				gameObject.transform.parent.transform.parent.GetChild(i) .gameObject.SetActive(true);
				gameObject.transform.parent.transform.parent.GetChild (i).gameObject.GetComponent<ImageName> ().SetImageOnDome ();
				TargetCamera.transform.rotation = CustomLocROt;

			}else{
				gameObject.transform.parent.transform.parent.GetChild(i) .gameObject.SetActive(false);
			}
		}

	}

	public void CreateMesh () {

		int no = ActionDots.Length;
		vertices2D = new Vector3[no];

		for (int i = 0; i < ActionDots.Length; i++) {

			vertices2D [i] = new Vector3 (ActionDots [i].x, ActionDots [i].y , ActionDots[i].z) ; 

		}
		vert2D = new Vector2[no];
		for (int j = 0; j < ActionDots.Length; j++) {
			vert2D [j] = new Vector2 (ActionDots [j].x, ActionDots [j].y);
		}
		// Use the triangulator to get indices for creating triangles
		Triangulator tr = new Triangulator (vert2D);
		int[] indices = tr.Triangulate ();

		// Create the Vector3 vertices
		Vector3[] vertices = new Vector3[vertices2D.Length ];
		for (int i = 0; i < vertices.Length; i++) {
			vertices [i] = new Vector3 (vertices2D [i].x, vertices2D [i].y, vertices2D[i].z);
		}


		// Create the mesh
		Mesh msh = new Mesh ();
		msh.Clear ();
		msh.vertices = vertices;
		msh.triangles = indices;
		msh.RecalculateNormals ();
		msh.RecalculateBounds ();

		// Set up game object with mesh;
	//	ActionHotspotMesh.AddComponent (typeof(MeshRenderer));
//		MeshFilter filter = ActionHotspotMesh.AddComponent (typeof(MeshFilter)) as MeshFilter;
//		filter.mesh = msh;


//		MeshCollider collider = ActionHotspotMesh.AddComponent (typeof(MeshCollider)) as MeshCollider;
//		collider.sharedMesh = msh;
//		collider.convex = true;



		NavigationPoint.transform.localPosition = ActionPos;
		NavigationPoint.transform.localRotation  = NavigatePointRot;
		ActionCollider.transform.localPosition = new Vector3 (0 ,0, 0);

		gameObject.transform.GetChild (0).transform.rotation = ActionRotation;

		LineRender.positionCount = ActionDots.Length;
	
		if (ActionDots.Length != 0) {
			for (int i = 0; i < ActionDots.Length; i++) {
				LineRender.SetPosition (i, ActionDots [i]);
			}
		}

		MeshFilter filter = ActionHotspotMesh.AddComponent (typeof(MeshFilter)) as MeshFilter;
		filter.mesh = msh;

		//ActionCollider.AddComponent (typeof(MeshRenderer));
		MeshFilter filter2 = ActionCollider.AddComponent (typeof(MeshFilter)) as MeshFilter;
		filter2.mesh = msh;
		//ActionCollider.AddComponent (typeof(MeshRenderer));
		MeshCollider collider  = ActionCollider.AddComponent (typeof(MeshCollider)) as MeshCollider;
		collider.sharedMesh = msh;
		//collider.convex = true;
		gameObject.transform.GetChild (0).transform.localPosition = ActionDots [0];
	}

}
