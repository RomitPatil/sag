using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFirstScene : MonoBehaviour
{
    bool on;
    public int MenuCheck;
    public MenuHotspot MH;
    void OnEnable()
    {
        //StartCoroutine (wait ());
        on = true;
        MenuCheck = gameObject.transform.childCount;

    }

    void Update()
    {
        if (on)
        {
            MH = gameObject.GetComponentInChildren<MenuHotspot>();

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {


              
                for (int j = 0; j < gameObject.transform.GetChild(i).childCount; j++) {

                     gameObject.transform.GetChild(i).gameObject.SetActive(true);
                }

            }
           
          
                if (MH != null)
                {

                   
                        for (int i = 0; i < gameObject.transform.childCount; i++)
                        {
                            if (gameObject.transform.GetChild(i).GetComponent<PreviewScene>())
                            {
                                gameObject.transform.GetChild(i).gameObject.SetActive(false);
                            }
                        }

                        gameObject.transform.GetChild(0).gameObject.SetActive(true);
                on = false;
            }
                }


        if (MH != null) {

            
        }
           
        
    }

}
