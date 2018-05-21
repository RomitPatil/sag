using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastingOnDome : MonoBehaviour
{

    public GameObject CurrentSelectedObject;
    public GameObject Collider;
    public GameObject DragArea;

    public LayerMask layermask;
    public LayerMask layermask2;

    public bool ActiveHotspot;
    public bool OnCollider;
    public bool CameraDrag;
    public bool MobileApp;
    public bool preview;
    public bool fix;
    public bool dots;

    public Vector3 HitPos;

    public Image UITimerBar;


    public static int CurrentHoldTime;
    public int holdTo;

    float NEXTtime = 0f;
    float time = 0f;


    void Start()
    {
        dots = true;
    }

    // When we have hotspot is selected the don't want to camera Drag.
    public void CameraMomentControl()
    {
        if (ActiveHotspot)
        {
            gameObject.GetComponent<MouseCameraDraging>().speedH = 0f;
            gameObject.GetComponent<MouseCameraDraging>().speedV = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!MobileApp)
        {

            OnMouseButtonDown();
            OnMouseButtonUp();
            CameraMomentControl();

        }

        // that is used in moblie Scene.
        if (MobileApp)
        {
            mobileRaycast();
        }

        if (!preview)
        {
            if (DragArea.activeInHierarchy)
            {
                CameraDrag = false;

            }
        }
    }

    // left the mouse click.
    // Enble the box collider of current object.
    // null the currentSelectedObject.
    // ActionHotspot false.

    public void OnMouseButtonUp()
    {
        // left the mouse click.
        if (Input.GetMouseButtonUp(0))
        {

            if (CurrentSelectedObject != null)
            {
                if (CurrentSelectedObject.transform.GetComponent<SphereCollider>())
                {
                    Debug.Log(CurrentSelectedObject.name);

                    CurrentSelectedObject.transform.GetComponent<SphereCollider>().enabled = true;


                }

                if (CurrentSelectedObject != null)
                {

                    Debug.Log(CurrentSelectedObject.name);
                    if (CurrentSelectedObject.transform.GetComponent<BoxCollider>())
                    {

                        CurrentSelectedObject.transform.GetComponent<BoxCollider>().enabled = true;
                    }

                    if (CurrentSelectedObject.tag == "Object")
                    {
                        if (CurrentSelectedObject.transform.GetChild(0).GetComponent<LookAtTarget>())
                        {

                            CurrentSelectedObject.transform.GetChild(0).GetComponent<LookAtTarget>().enabled = false;
                        }
                        CurrentSelectedObject.transform.GetComponent<BoxCollider>().enabled = true;
                    }
                    if (CurrentSelectedObject.transform.childCount != 0)
                    {
                        if (CurrentSelectedObject.transform.GetChild(0).GetComponent<BoxCollider>())
                        {

                            CurrentSelectedObject.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;

                        }
                    }

                    if (CurrentSelectedObject.tag == "Object" || CurrentSelectedObject.tag == "Model")
                    {
                        if (CurrentSelectedObject.transform.GetComponent<BoxCollider>())
                        {

                            CurrentSelectedObject.transform.GetComponent<BoxCollider>().enabled = true;

                        }

                    }
                }





                // null the currentSelectedObject.
                CurrentSelectedObject = null;

                // ActionHotspot false.
                ActiveHotspot = false;

            }
            if (!preview)
            {
                DragArea.SetActive(true);
            }
            else
            {
                CameraDrag = false;
            }
        }
    }

    // When we click mouse then we cast 2 ray one is geting object on collider layer and the other layer is getting object on Action layer.

    public void OnMouseButtonDown()
    {

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            RaycastHit hit2;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 ScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Physics.Raycast(ray, out hit2, 1000f, layermask))
            {
                Debug.Log("Get point");
                Debug.Log(hit2.collider.gameObject.name);
                if (!preview)
                {
                    if (hit2.collider.gameObject.name == "DragArea")
                    {
                        DragArea.SetActive(false);
                        CameraDrag = true;

                    }
                }
                else
                {
                    if (hit2.collider.gameObject.tag == "Collider")
                    {

                        CameraDrag = true;

                    }

                }


                Debug.DrawRay(transform.position + new Vector3(1f, 2f, 0), hit2.point, Color.red);

                var startPos = hit2.point;



                if (CurrentSelectedObject != null)
                {
                    if (CurrentSelectedObject.transform.childCount != 0)
                    {
                        if (CurrentSelectedObject.transform.GetChild(0).gameObject.tag == "Hotspot")
                        {
                            if (!preview)
                            {
                                CurrentSelectedObject.transform.position = startPos;
                                CurrentSelectedObject.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
                                if (CurrentSelectedObject.GetComponent<NewActionHotspot>())
                                {
                                    CurrentSelectedObject.GetComponent<NewActionHotspot>().OnClickOnActionHotspot();
                                }
                                if (CurrentSelectedObject.GetComponent<navigateActionHotspot>())
                                {
                                    CurrentSelectedObject.GetComponent<navigateActionHotspot>().OnClickOnHotspot();
                                }
                            }
                        }

                        if (CurrentSelectedObject.transform.GetChild(0).gameObject.tag == "MenuHotspot")
                        {
                            if (!preview)
                            {
                                CurrentSelectedObject.transform.position = startPos;
                                CurrentSelectedObject.transform.gameObject.transform.GetChild(0).localPosition = Vector3.zero;
                            }
                        }

                        if (CurrentSelectedObject.transform.GetChild(0).gameObject.tag == "MediaHotspot")
                        {
                            if (!preview)
                            {
                                CurrentSelectedObject.transform.position = startPos;
                                CurrentSelectedObject.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
                            }
                        }
                    }
                    if (CurrentSelectedObject.transform.gameObject.tag == "Target")
                    {
                        if (!preview)
                        {
                            CurrentSelectedObject.transform.localPosition = startPos;
                        }
                    }

                    if (CurrentSelectedObject.transform.gameObject.tag == "Object")
                    {
                        if (!preview)
                        {
                            CurrentSelectedObject.transform.position = startPos;
                        }
                    }

                    if (CurrentSelectedObject.transform.gameObject.tag == "Model")
                    {
                        if (!preview)
                        {
                            CurrentSelectedObject.transform.position = startPos;
                        }
                    }

                    if (CurrentSelectedObject.transform.gameObject.tag == "Object")
                    {
                        if (preview)
                        {
                            CurrentSelectedObject.transform.position = startPos;
                        }
                    }

                }

            }

            // Get the hostspot to active stage. 

            if (Physics.Raycast(ray, out hit, 1000f, layermask2))
            {

                Debug.Log("Getting Hotspot object : " + hit.collider.gameObject.name);
                if (preview)
                {
                    if (hit.collider.gameObject.tag == "InfoBox")
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        hit.collider.gameObject.transform.parent.GetComponent<HideInfo>().hide = !hit.collider.gameObject.transform.parent.GetComponent<HideInfo>().hide;
                    }

                    if (hit.collider.gameObject.tag == "MenuHotspot")
                    {
                        Debug.Log(hit.collider.gameObject.name + "Function Call");

                        if (hit.collider.transform.parent.GetComponent<MenuHotspot>())
                        {

                            Debug.Log("Visible function");
                            //    hit.collider.transform.parent.GetComponent<MenuHotspot>().ClickOnVisible();

                        }
                        if (hit.collider.transform.GetComponent<MenuFunction>())
                        {
                            Debug.Log("Navigation function");
                            //     hit.collider.transform.GetComponent<MenuFunction>().ClickMenuHotspot();
                        }
                    }
                }



                if (hit.collider.gameObject.tag == "Hotspot")
                {
                    if (!preview)
                    {
                        hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                        //  hit.collider.gameObject.transform.localPosition = Vector3.zero;
                        hit.collider.gameObject.transform.parent.localPosition = new Vector3(hit.collider.gameObject.transform.parent.localPosition.x, hit.collider.gameObject.transform.parent.localPosition.y, hit.collider.gameObject.transform.parent.localPosition.z);
                        OnCollider = true;
                        CurrentSelectedObject = hit.collider.gameObject.transform.parent.gameObject;

                        ActiveHotspot = true;

                        if (CurrentSelectedObject.transform.parent.transform.GetComponent<NewActionHotspot>())
                        {
                            CurrentSelectedObject.transform.parent.transform.GetComponent<NewActionHotspot>().OnClickOnActionHotspot();
                        }

                    }
                    else
                    {
                        if (hit.collider.gameObject.tag == "Hotspot")
                        {
                            hit.collider.transform.parent.transform.GetComponent<TriLib.Samples.PreviewHotspot>().HandleTimedInput();
                        }





                    }


                }

                if (hit.collider.gameObject.tag == "MenuHotspot")
                {
                    Debug.Log("Menu :" + hit.collider.gameObject.name);
                    if (!preview)
                    {
                        hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                        //   hit.collider.gameObject.transform.localPosition = Vector3.zero;
                        hit.collider.gameObject.transform.parent.localPosition = new Vector3(hit.collider.gameObject.transform.parent.localPosition.x, hit.collider.gameObject.transform.parent.localPosition.y, hit.collider.gameObject.transform.parent.localPosition.z);
                        OnCollider = true;
                        CurrentSelectedObject = hit.collider.gameObject.transform.parent.gameObject;
                        CurrentSelectedObject.GetComponent<MenuHotspot>().ClickOnMenuHotspot();

                        ActiveHotspot = true;


                    }
                    else
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        //  hit.collider.gameObject.GetComponent<MenuHotspot>().VisibleMenu();
                    }


                }

                if (hit.collider.gameObject.tag == "MediaHotspot")
                {
                    if (!preview)
                    {
                        hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                        //   hit.collider.gameObject.transform.localPosition = Vector3.zero;
                        hit.collider.gameObject.transform.parent.localPosition = new Vector3(hit.collider.gameObject.transform.parent.localPosition.x, hit.collider.gameObject.transform.parent.localPosition.y, hit.collider.gameObject.transform.parent.localPosition.z);
                        OnCollider = true;
                        CurrentSelectedObject = hit.collider.gameObject.transform.parent.gameObject;
                        if (hit.collider.gameObject.transform.parent.gameObject.GetComponent<MediaHotspot>())
                        {
                            hit.collider.gameObject.transform.localPosition = Vector3.zero;
                            CurrentSelectedObject = hit.collider.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                        CurrentSelectedObject.transform.GetChild(0).
                                GetComponent<MediaHotspot>().OnClickMediaHotspot();
                        }
                        else {

                        CurrentSelectedObject = hit.collider.gameObject.transform.parent.gameObject;
                        CurrentSelectedObject.GetComponent<MediaHotspot>().OnClickMediaHotspot();
                        }

                        ActiveHotspot = true;


                    }
                    else
                    {
                        hit.collider.gameObject.transform.parent.GetComponent<MediaHotspot>().OnClickHide();
                    }

                }

                if (hit.collider.gameObject.tag == "MediaPlay")
                {


                    hit.collider.gameObject.transform.parent.GetComponent<MediaPlaypause>().PlayAndPause();
                }

                if (hit.collider.gameObject.tag == "MediaHide")
                {
                    if (!preview)
                    {

                        hit.collider.gameObject.transform.parent.parent.GetComponent<MediaHotspot>().OnClickHide();
                    }
                }



                if (hit.collider.gameObject.tag == "Target")
                {
                    if (!preview)
                    {
                        hit.collider.gameObject.GetComponent<SphereCollider>().enabled = false;

                        hit.collider.gameObject.transform.localPosition = new Vector3(hit.collider.gameObject.transform.localPosition.x, hit.collider.gameObject.transform.localPosition.y, hit.collider.gameObject.transform.localPosition.z);
                        OnCollider = true;
                        hit.collider.gameObject.GetComponent<TargetFunction>().OnClickTarget();
                        CurrentSelectedObject = hit.collider.gameObject;

                        ActiveHotspot = true;


                    }
                    else
                    {
                        // hit.collider.gameObject.transform.parent.GetComponent<MediaHotspot>().OnClickHide();
                    }


                }

                if (hit.collider.gameObject.tag == "Object")
                {
                    if (!preview)
                    {
                        hit.collider.gameObject.GetComponent<BoxCollider>().enabled = true;
                        //   hit.collider.gameObject.transform.localPosition = Vector3.zero;
                        hit.collider.gameObject.transform.localPosition = new Vector3(hit.collider.gameObject.transform.localPosition.x, hit.collider.gameObject.transform.localPosition.y, hit.collider.gameObject.transform.localPosition.z);
                        OnCollider = true;
                        CurrentSelectedObject = hit.collider.gameObject;
                        if (CurrentSelectedObject.GetComponent<ObjectHotsot>())
                        {
                            CurrentSelectedObject.GetComponent<ObjectHotsot>().AddObjectHotspot.EnableMenuTemplet();
                        }
                        ActiveHotspot = true;


                    }
                    else
                    {
                        // hit.collider.gameObject.transform.parent.GetComponent<MediaHotspot>().OnClickHide();
                    }


                }

                if (hit.collider.gameObject.tag == "Model")
                {
                    if (!preview)
                    {
                        if (hit.collider.gameObject.GetComponent<BoxCollider>())
                        {

                            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                        }


                        //  hit.collider.gameObject.transform.localPosition = Vector3.zero;
                        hit.collider.gameObject.transform.localPosition = new Vector3(hit.collider.gameObject.transform.localPosition.x, hit.collider.gameObject.transform.localPosition.y, hit.collider.gameObject.transform.localPosition.z);
                        OnCollider = true;
                        CurrentSelectedObject = hit.collider.gameObject;

                        ActiveHotspot = true;


                    }
                    else
                    {
                        // hit.collider.gameObject.transform.parent.GetComponent<MediaHotspot>().OnClickHide();
                    }
                }
                if (hit.collider.gameObject.tag == "ObjectModel")
                {
                    if (!preview)
                    {
                        if (hit.collider.gameObject.GetComponent<BoxCollider>())
                        {

                            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                        }
                        //    hit.collider.gameObject.transform.localPosition = Vector3.zero;
                        ObjectHotsot OH = hit.collider.gameObject.GetComponentInParent<ObjectHotsot>();

                        // hit.collider.gameObject.transform.localPosition = Vector3.zero;
                        //  OH.transform.parent.gameObject.transform.localPosition = new Vector3(hit.collider.gameObject.transform.localPosition.x, hit.collider.gameObject.transform.localPosition.y, hit.collider.gameObject.transform.localPosition.z);
                        OnCollider = true;
                        OH.ClickOnhotspot();
                        OH.transform.GetChild(0).GetComponent<LookAtTarget>().enabled = true;
                        CurrentSelectedObject = OH.transform.gameObject;

                        ActiveHotspot = true;


                    }
                    else
                    {
                        if (hit.collider.gameObject.GetComponent<BoxCollider>())
                        {

                            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                        }
                        OnCollider = true;
                        ObjectHotsot OH = hit.collider.gameObject.GetComponentInParent<ObjectHotsot>();
                        OH.Target.gameObject.SetActive(true);
                        OH.transform.GetChild(0).GetComponent<LookAtTarget>().enabled = true;
                        OH.SelectedModel.transform.GetChild(0).gameObject.SetActive(true);

                        // hit.collider.gameObject.transform.localPosition = Vector3.zero;

                        CurrentSelectedObject = OH.transform.gameObject;

                        ActiveHotspot = true;



                    }

                }

            }



            Debug.DrawRay(transform.position + new Vector3(1f, 2f, 0), hit.point, Color.blue);






        }

    }
    public void mobileRaycast()
    {
        RaycastHit hit;


        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        //Vector3 ScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000f, layermask2))
        {
            HitPos = hit.point;
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag == "Hotspot")
            {
                CurrentSelectedObject = hit.collider.gameObject;
                Debug.Log("Pointer is called");

                if (CurrentHoldTime >= 10)
                {
                    Debug.Log("?????");
                    CurrentHoldTime = 0;
                    UITimerBar.fillAmount = 0f;
                    CurrentSelectedObject.gameObject.transform.parent.transform.GetComponent<TriLib.Samples.PreviewHotspot>().HandleTimedInput();
                    CurrentSelectedObject = null;
                }
                else
                {
                    if (Time.time < NEXTtime)
                    {
                        NEXTtime += 2f;
                        CurrentHoldTime += 1;
                        UITimerBar.fillAmount += 0.10f;
                        Debug.Log("Current " + CurrentHoldTime);
                    }
                }

            }
            else
            {
                CurrentSelectedObject = null;
                CurrentHoldTime = 0;
                UITimerBar.fillAmount = 0f;
            }

        }
        Debug.DrawRay(transform.position + new Vector3(1f, 2f, 0), hit.point, Color.blue);

    }

    // Timer UI
    /*
   IEnumerator waitForhold()
   {

       
       if (CurrentSelectedObject != null)
       {
           yield return new WaitForSecondsRealtime(0.5f);

           CurrentHoldTime += 1;
           UITimerBar.fillAmount += 0.25f;
           Debug.Log("Current " + CurrentHoldTime);
           if (CurrentHoldTime == 4)
           {
               Debug.Log("Pointer is click");
               CurrentSelectedObject.gameObject.transform.parent.transform.GetComponent<TriLib.Samples.PreviewHotspot>().HandleTimedInput();
               StopCoroutine(waitForhold());
               CurrentSelectedObject = null;
               CurrentHoldTime = 0;
               UITimerBar.fillAmount = 0f;
           }
           StartCoroutine(waitForhold());
       }
       }
   */

}