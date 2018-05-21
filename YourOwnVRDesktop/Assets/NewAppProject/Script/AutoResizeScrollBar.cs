using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoResizeScrollBar : MonoBehaviour {

    public GameObject ItemContainer;
    public int itemCounter;
	// Use this for initialization
	void OnEnable () {
		if (transform.localEulerAngles.y == 180 && transform.localPosition.x == -20f)
        {

         //   transform.localEulerAngles = new Vector3(0, 0, 0);
         //   transform.localPosition = new Vector3(-80, 0, 0);

        }
	}
	
	// Update is called once per frame
	void Update () {

      //  Debug.Log(transform.localEulerAngles.y + " " + transform.localPosition.x);
        if (transform.localEulerAngles.y == 180 && transform.localPosition.x == -30f)
        {

            transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.localPosition = new Vector3(-90, 0, 0);

        }

        if (itemCounter != ItemContainer.transform.childCount)
        {
            itemCounter = ItemContainer.transform.childCount;
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            for (int i = 0; i < ItemContainer.transform.childCount; i++)
            {

                Vector2 size = new Vector2(0, ItemContainer.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y + 2f);
                if (size.y <= 260)
                {
                    gameObject.GetComponent<RectTransform>().sizeDelta += size;
                }
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(ItemContainer.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.x + 10, gameObject.GetComponent<RectTransform>().sizeDelta.y);
            }
        }
	}
}
