using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCursor : MonoBehaviour {

	public Texture2D mouse;
	public Texture2D hand;
	public Texture2D grab;

	public GameObject GrabFile;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	public bool Grab;
	public bool Ungrab;
	public Camera Camera;

	public bool clicked;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



		if (Input.GetMouseButtonDown (0)) {
			clicked = true;
		} 

		if (Input.GetMouseButtonUp (0)) {
			if (!Input.GetKeyUp (KeyCode.LeftShift)) {
				SetMouse ();
			}
		}

	}
		
	public void SetMouse () {
		
			Cursor.SetCursor (mouse, hotSpot, cursorMode);
	
	}

	public void setDrag () {
		
		Cursor.SetCursor (grab, hotSpot, cursorMode);
	}


	public void setHand () {
		Cursor.SetCursor (hand, hotSpot, cursorMode);
	}

}
