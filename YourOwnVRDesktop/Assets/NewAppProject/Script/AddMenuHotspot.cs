using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMenuHotspot : MonoBehaviour {

    public GameObject NevigationCanvas;
    public GameObject MenuHotspotPrefab;
    public GameObject MenuItemContainer;
    public GameObject MenuItemPrefab;
    public GameObject MenuTemplete;
    public GameObject ActiveMenu;
	 
    public SetupDome setupDome;
    public TriLib.Samples.OpenFileImages OpenFileImage;

    public List<Dropdown> ButtonsFunctionDropDown = new List<Dropdown>();
    public List <string> ButtonLableName = new List<string> ();
    public List<string> ButtonFunction = new List<string>();
    public List<string> ButtonFunctionData = new List<string>();
    public Text BtnID;
    public Button ApplyBtn;
    public Button UpdateBtn;
    public Button PreviewBtn;
    public SetupDome DomeSetup;
    public GameObject DomeCamera;
    public GameObject ActiveScene ;
    public int itemCounter;
    public bool UpdateData;
    public GameObject[] AllHotspotTemplets;
    public List<MenuHotspot> MenuHotspotsAll = new List<MenuHotspot>();

    public int SceneCount;

    public InputField posx, posy, posz;

    public void EnableMenuTemplet() {

        for (int j = 0; j < AllHotspotTemplets.Length; j++)
        {

            if (AllHotspotTemplets[j].gameObject.name == "MenuTemplet")
            {

                AllHotspotTemplets[j].SetActive(true);
            }
            else
            {
                AllHotspotTemplets[j].SetActive(false);
            }
        }
    }

    public void OnClick()
    {
        EnableMenuTemplet();

        MenuTemplete.SetActive(true);
      
        PreviewBtn.interactable = false;
        ApplyBtn.interactable = false;
        clearData();
    }

    public void AddItem()
    {
       
        GameObject MenuItem = GameObject.Instantiate(MenuItemPrefab);
        MenuItem.gameObject.name = itemCounter.ToString();
        MenuItem.transform.parent = MenuItemContainer.transform;
        MenuItem.transform.localScale = new Vector3(1f,1f,1f);
        MenuItem.transform.localPosition = Vector3.zero;
        MenuItem.transform.GetChild(0).GetComponent<InputField>().text = "";
        MenuItem.transform.GetChild(1).GetComponent<Dropdown>().value = 0;
       // MenuItem.transform.GetChild(2).GetComponent<Dropdown>().ClearOptions();

        ButtonsFunctionDropDown.Add(MenuItem.transform.GetChild(1).GetComponent<Dropdown>());
        string itemName = MenuItem.transform.gameObject.name;

      //  Debug.Log("Item Name : " + itemName);
        int index = System.Int32.Parse(itemName);
      //  Debug.Log(index);

    

        itemCounter++;

        ApplyBtn.interactable = true;
        PreviewBtn.interactable = true;
    }

   
    public void PulltheDatafromItems()
    {  

        ApplyBtn.gameObject.SetActive(false);
        UpdateBtn.gameObject.SetActive(true);
        UpdateBtn.interactable = true; 
        ButtonFunction.Clear();
        ButtonLableName.Clear();
        ButtonFunctionData.Clear();

        for (int i = 0; i < NevigationCanvas.transform.childCount; i++) {
            if (NevigationCanvas.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                if (NevigationCanvas.transform.GetChild(i).GetComponent<SceneProperties>())
                {
                ActiveScene = NevigationCanvas.transform.GetChild(i).gameObject;

                }

            }
        }
        for (int i = 0; i < MenuItemContainer.transform.childCount; i++)
        {
            
            ButtonLableName.Add(MenuItemContainer.transform.GetChild(i).GetChild(0).GetComponent<InputField>().text);
            ButtonFunction.Add(MenuItemContainer.transform.GetChild(i).GetChild(1).GetComponent<Dropdown>().captionText.text);
            ButtonFunctionData.Add(MenuItemContainer.transform.GetChild(i).GetChild(2).GetComponent<Dropdown>().captionText.text);
        }

        InitiateMenuHotspot();
    }

    private void Update()
    {

        if (itemCounter == 0) 
        {

            ApplyBtn.interactable = false;
            PreviewBtn.interactable = false;
        }

        if (OpenFileImage == null) {
            OpenFileImage = GameObject.Find("OpenFileImages").GetComponent<TriLib.Samples.OpenFileImages>();
        }

      

    }
    public void UpdateingData()
    {
        UpdateData = true;

    }
    public void InitiateMenuHotspot() {

        if (UpdateData == false)
        {
            SetupDome.ButtonId++;
           
            GameObject MenuHotspot = GameObject.Instantiate(MenuHotspotPrefab);
            MenuHotspot.gameObject.name = "Btn ID :" + SetupDome.ButtonId;
            MenuHotspot.transform.parent = ActiveScene.transform;
            MenuHotspot.transform.eulerAngles = DomeCamera.transform.eulerAngles;
            MenuHotspot.GetComponent<MenuHotspot>().BtnID = SetupDome.ButtonId;
            MenuHotspot.GetComponent<MenuHotspot>().posx = posx;  
            MenuHotspot.GetComponent<MenuHotspot>().posy = posy;
            MenuHotspot.GetComponent<MenuHotspot>().posz = posz;
            MenuHotspot.transform.localPosition = new Vector3(0, 0, 0);
            MenuHotspot.transform.localScale = new Vector3(1f, 1f, 1f);
            MenuHotspot.GetComponent<MenuHotspot>().ButtonLableName.AddRange(ButtonLableName);
            MenuHotspot.GetComponent<MenuHotspot>().ButtonFunction.AddRange(ButtonFunction);
            MenuHotspot.GetComponent<MenuHotspot>().ButtonFunctionData.AddRange(ButtonFunctionData);

            MenuHotspot.GetComponent<MenuHotspot>().InitialiesItems();
			BtnID.text = "Btn ID :" + SetupDome.ButtonId.ToString();     
			  
            DomeSetup.SelectFile.GetComponent<SelectFiles>().EditScene = true;
            DomeSetup.GlobalMenuHotspot = MenuHotspot;
            MenuHotspot.GetComponent<MenuHotspot>().BirthScene = ActiveScene.gameObject.name;
            MenuHotspot.gameObject.transform.parent = MenuHotspot.gameObject.transform.parent.transform.gameObject.transform.parent.transform;
            MenuHotspot.gameObject.transform.localPosition = Vector3.zero;
            gameObject.GetComponent<Button>().enabled = false;
            MenuHotspot.AddComponent<AlwaysActive>();
        }
        else
        {
            ActiveMenu.GetComponent<MenuHotspot>().ButtonLableName.Clear();
            ActiveMenu.GetComponent<MenuHotspot>().ButtonFunction.Clear();
            ActiveMenu.GetComponent<MenuHotspot>().ButtonFunctionData.Clear();

            for (int i = 0; i < ActiveMenu.GetComponent<MenuHotspot>().MenuContainer.transform.childCount; i++) {
                Destroy(ActiveMenu.GetComponent<MenuHotspot>().MenuContainer.transform.GetChild(i).gameObject);
            }


                ActiveMenu.GetComponent<MenuHotspot>().ButtonLableName.AddRange(ButtonLableName);
                ActiveMenu.GetComponent<MenuHotspot>().ButtonFunction.AddRange(ButtonFunction);
                ActiveMenu.GetComponent<MenuHotspot>().ButtonFunctionData.AddRange(ButtonFunctionData);

            ActiveMenu.GetComponent<MenuHotspot>().InitialiesItems();
        }

      
    }

    public void OnDisable()
    {

        clearData();
    }

    public void clearData() {

        ActiveScene = null;
        itemCounter = 0;  
        ButtonFunction.Clear();
        ButtonFunctionData.Clear();
        ButtonLableName.Clear();
        ButtonsFunctionDropDown.Clear();

        ApplyBtn.gameObject.SetActive(true);
        UpdateBtn.gameObject.SetActive(false);

        for (int i = 0; i < MenuItemContainer.transform.childCount; i++)
        {

            Destroy(MenuItemContainer.transform.GetChild(i).gameObject);
        }
			
        gameObject.GetComponent<Button>().interactable = true;   
    }

}
