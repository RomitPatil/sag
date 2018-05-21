using System.Collections;
using System.Collections.Generic;
using TriLib.Samples;
using UnityEngine;
using UnityEngine.Video;
  
public class MediaFullScrenController : MonoBehaviour {

    public AddMediaHotspot AddMediaHotspot;
    public VideoPlayer FullVideo;
    public bool toggle;

   

    public void Toggle()
    {

        toggle = !toggle;

        if (toggle)
        {
            if (AddMediaHotspot != null)
            {
                AddMediaHotspot.SelectedMediaHotspot.FullScreenVideoPlayer.Play();

            }
            else {
                FullVideo.Play();

            }   
        }
        else {
            if (AddMediaHotspot != null)
            {
                AddMediaHotspot.SelectedMediaHotspot.FullScreenVideoPlayer.Pause();

            }
            else
            {
                FullVideo.Pause();

            }
        }
    }

    public void CloseFullScreen()
    {
        if (AddMediaHotspot != null)
        {
            AddMediaHotspot.FullSceenVideoUI.SetActive(false);
            AddMediaHotspot.SelectedMediaHotspot.FullScreenVideoPlayer.gameObject.SetActive(false);
            AddMediaHotspot.SelectedMediaHotspot.FullScreenVideoPlayer.Stop();
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        else {

            FullVideo.Stop();
            FullVideo.gameObject.SetActive(false);
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
