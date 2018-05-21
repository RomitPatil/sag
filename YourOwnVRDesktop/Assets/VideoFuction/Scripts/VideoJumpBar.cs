using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class VideoJumpBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public VideoController video;
	public Image bar;
	public Image Frame;
	public bool stopFrame;
	public GameObject domeFull;

	bool hovering;
	float pos;

	void Start (){

	
	}

	void Update() {
		if (video.IsPlaying) {
			Debug.Log (hovering);
		}
		if (!hovering) return;

		pos = Input.mousePosition.x / Screen.width;
		bar.fillAmount = pos;
		Frame.transform.position = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
		video.Seek (pos);


	}

	public void OnPointerEnter(PointerEventData ped) {
		hovering = true;
		Frame.gameObject.SetActive (true);
		video.Seek(pos);
		video.PauseVideo ();

	}
	
	public void OnPointerExit(PointerEventData ped) {
		bar.fillAmount = 0;
		Frame.gameObject.SetActive (false);
		hovering = false;
	
	}

	public void OnPointerClick(PointerEventData ped) {
		video.Seek(pos);
		domeFull.SetActive (true);
		stopFrame = true;
		video.PauseVideo ();
	}

}
