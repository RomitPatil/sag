using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;


public class MediaPlaypause : MonoBehaviour {

	public bool play;
	public VideoPlayer player;
    public bool clicked;
    public GameObject playIcon;
    
    // Use this for initialization
    void Start () {
		play = true;
		 player = gameObject.GetComponent<VideoPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
	//	gameObject.transform.localPosition = new Vector3 (0, 0, 0f);
	}

    public void PlayAndPause() {
        if (clicked == false)
        {

        StartCoroutine(WaitForSce());

        }
    }
    IEnumerator WaitForSce()
    {
        
            clicked = true;
            PauseAndPlatMediaFile();
            yield return new WaitForSeconds(0.5f);
            clicked = false;
       
    }


    public void PauseAndPlatMediaFile()
    {

            play = !play;
            if (play) { 
                player.Play();
                // gameObject.transform.GetChild(0).gameObject.transform.GetComponent<Image>().enabled = false;
                Debug.Log("Play");
                //player.url = Dome.SelectFile.GetComponent<SelectFiles> ().MediaFile;
                playIcon.SetActive(false);
            }
            else
            {
                Debug.Log("Pause");
                player.Pause();
                //gameObject.transform.GetChild(0).gameObject.transform.GetComponent<Image>().enabled = true;
                //player.url = Dome.SelectFile.GetComponent<SelectFiles> ().MediaFile;
                playIcon.SetActive(true);
            }
        }
    
    
}
