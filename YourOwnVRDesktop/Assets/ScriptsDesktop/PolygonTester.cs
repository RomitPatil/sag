using UnityEngine;
using System.Collections.Generic;
public class PolygonTester : MonoBehaviour {
	public Vector3[] vertices2D;
	public Vector2[] vert2D;
	public List<Transform> dotsPositions = new List<Transform>();
	public Vector3 centroidPoint;
	public GameObject ActionHotsotParent;
	public GameObject[] currentDots;
	public GameObject ActionCollider;
	public GameObject ActiveHotspotMesh;
	public bool Set;
//	public PolygonCollider2D PolygonCollider;

	public void CreateMesh () {
		gameObject.transform.GetChild (0).gameObject.SetActive (true);
		dotsPositions.Clear ();
		//GameObject [] dots = GameObject.FindGameObjectsWithTag("Dot");

		GameObject parent = ActionHotsotParent.transform.parent.gameObject;

		DoubleClick[] temp = parent.GetComponentsInChildren<DoubleClick> ();

		currentDots = new GameObject[temp.Length];
		for (int a = 0; a < currentDots.Length ; a++) {
			currentDots [a] = temp [a].gameObject; 
		}

		GameObject [] dots = currentDots;
		vertices2D = new Vector3[dots.Length];
		Debug.Log (dots.Length);

		for (int j = 0; j < dots.Length; j++) {
			
			dotsPositions.Add (dots[j].transform);
		}
		
		for (int i = 0; i < dots.Length; i++) {
		
			vertices2D [i] = new Vector3 (dots [i].transform.localPosition.x, dots [i].transform.localPosition.y ,dots [i].transform.localPosition.z);
		

		}


		Vector2[] vertices2d = new Vector2[vertices2D.Length];
		for (int i=0; i<vertices2D.Length; i++) {
			vertices2d[i] = new Vector2(vertices2D[i].x, vertices2D[i].y);
		}

		// Use the triangulator to get indices for creating triangles
		Triangulator tr = new Triangulator(vertices2d);
		int[] indices = tr.Triangulate();

		// Create the Vector3 vertices
		Vector3[] vertices = new Vector3[vertices2D.Length];
		for (int i=0; i<vertices.Length; i++) {
			vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y,  vertices2D[i].z);
		}




		// Create the mesh
		Mesh msh = new Mesh();
		msh.Clear ();
		msh.vertices = vertices;
		msh.triangles = indices;
		msh.RecalculateNormals();
		msh.RecalculateBounds();

		// Set up game object with mesh;
//		gameObject.AddComponent(typeof(MeshRenderer));
//		MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
//		filter.mesh = msh;
//
//		MeshCollider collider = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
//		collider.sharedMesh = msh;
//		collider.convex = true;



		
			MeshFilter filter2 = ActiveHotspotMesh.AddComponent(typeof(MeshFilter)) as MeshFilter;
			filter2.mesh = msh;

		

			MeshFilter filter = ActionCollider.AddComponent(typeof(MeshFilter)) as MeshFilter;
			MeshCollider collider = ActionCollider.AddComponent(typeof(MeshCollider)) as MeshCollider;
			
		filter.mesh = ActiveHotspotMesh.GetComponent<MeshFilter> ().mesh;
		collider.sharedMesh = ActiveHotspotMesh.GetComponent<MeshFilter> ().mesh;

			
		
		Set = true;

		Camera.main.GetComponent<Raycasting> ().dots = false;
		Camera.main.GetComponent<Raycasting> ().ActiveHotspot = true;

		currentDots [0].transform.GetChild (0).GetChild (0).gameObject.SetActive (false);
		ActiveHotspotMesh.transform.localScale = new Vector3 (1, 1, 1);
		ActionCollider.transform.localPosition = new Vector3 (0, 0, 0f);

		for (int j = 0; j < currentDots.Length; j++) {
			currentDots [j].gameObject.transform.localPosition = new Vector3 (currentDots [j].gameObject.transform.localPosition.x, currentDots [j].gameObject.transform.localPosition.y, currentDots [j].gameObject.transform.localPosition.z);

		}
	}
	public void FixActiveHotspot () {
//		for (int i = 0; i < currentDots.Length; i++) {
//			currentDots [i].GetComponent<DoubleClick> ().set = true;
//		}

		Camera.main.GetComponent<Raycasting> ().dots = true;
		Camera.main.GetComponent<Raycasting> ().ActiveHotspot = false;
		gameObject.transform.GetChild (0).gameObject.SetActive (false);
		ActionCollider.GetComponent<MeshCollider> ().enabled = false;
		ActiveHotspotMesh.GetComponent<MeshCollider> ().enabled = false;
		currentDots [0].transform.GetChild (0).GetChild (0).gameObject.SetActive (false);
		ActionHotsotParent.transform.parent.gameObject.GetComponent<HotspotControl> ().HotspotClick ();

	}
		

	void Update (){




	}

}