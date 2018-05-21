using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FootageDragAndDrop : MonoBehaviour {

	public List <string> fileUrl = new List<string> ();
	public List <Texture> FileTexture = new List<Texture> ();
	public List <string> FileName = new List<string> ();
	public GameObject Container;
	 
	public GameObject ImportPhotoFolder;
	public GameObject ImportMoviesFolder;
	public GameObject ImportUnityFolder;

	public List <GameObject> SeletedObject = new List<GameObject>();
	public List<string> SelecteURL = new List<string> ();
	public List <Texture> SelectedTexture = new List <Texture>();
	public List <string> SelectedFileName = new List <string> ();

	public List <GameObject> PhotoFiles = new List<GameObject>();
	public  List<GameObject> MovieFiles = new List<GameObject>();
	public  List<GameObject> UnityFiles = new List<GameObject>();
	public List <GameObject> ModelsFiles = new List<GameObject> ();
	public List <Texture> ModelsFilesImage = new List<Texture>();

    public List<string> UnityAssetPath = new List<string>();  
	public HandleCursor cursor;
    public Texture2D DefaultUnityImage;
    public Texture2D DefaultMovieImage;
    // Use this for initialization
    void Start () {
		cursor = gameObject.transform.GetComponent<HandleCursor> ();
	}

	// Update is called once per frame
	void Update () {


		for (int i = 0; i < FileName.Count; i++) {
		
			FileName [i] = Container.transform.GetChild (i).transform.GetChild (0).GetComponent<InputField> ().text;


		}

		
	}
}
