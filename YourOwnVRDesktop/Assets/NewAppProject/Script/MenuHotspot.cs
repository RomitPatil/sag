using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHotspot : MonoBehaviour  
{  
    public Vector3 MenuPosition;
    public string MenuFunction; 
    public GameObject MenuContainer; 
    public GameObject MenuItemPrefab;
    public AddMenuHotspot AddMenuHotspot;
    public string PreviewHotspotGameobjectName;
    public List<string> ButtonLableName = new List<string>();
    public List<string> ButtonFunction = new List<string>();
    public List<string> ButtonFunctionData = new List<string>();
    public bool visible;
    public bool Preview;
    public int BtnID;
    public bool MenuHide;  
    bool clicked;
    public InputField posx, posy, posz; 
	public Sprite hotspotSprite;
    public bool reload;
    public string BirthScene;

    void Update()
    {
        if (!Preview)
        {
            if (gameObject.transform.localPosition.z == 0) {
                gameObject.transform.localPosition = new Vector3(0, 0, 90f);
            }

            MenuPosition = transform.localPosition;
          
        }
    }
    public void ClickOnVisible() {

        if (clicked == false)
        StartCoroutine(WaitForSce());
       
    }
    IEnumerator WaitForSce() { 
        MenuHide = !MenuHide;
        MenuContainer.gameObject.transform.parent.transform.parent.gameObject.SetActive(MenuHide);
        clicked = true;
        yield return new WaitForSeconds(0);
        clicked = false;
    }     
      
    public void InitialiesItems() 
    {   
        if (!Preview)
        {
            AddMenuHotspot = GameObject.Find("AddMenuScript").GetComponent<AddMenuHotspot>();
            AddMenuHotspot.ActiveMenu = gameObject;
            AddMenuHotspot.UpdateData = false;
            MenuFunction = "MenuHotspot";
        }
        else {
            gameObject.transform.position = MenuPosition;
        }
        for (int i = 0; i < ButtonLableName.Count; i++)
        {
            Debug.Log(ButtonLableName[i]);
            GameObject menuItem = GameObject.Instantiate(MenuItemPrefab);
            menuItem.gameObject.name = "Item" + i.ToString();
            menuItem.transform.parent = MenuContainer.transform;

            menuItem.transform.localPosition = new Vector3 (0,0,0);
            menuItem.transform.localScale = new Vector3(1f, 1f, 1f); 
            menuItem.transform.localEulerAngles = Vector3.zero;
            
            menuItem.transform.GetChild(0).GetComponent<Text>().text = ButtonLableName[i];
            menuItem.transform.GetComponent<MenuFunction>().PreviewHotspotGameobjectName =PreviewHotspotGameobjectName;
            menuItem.transform.GetComponent<MenuFunction>().NavigateTo = ButtonFunctionData[i];
        }
    }
	    
    public void VisibleMenu()
    {
       
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ClickOnMenuHotspot() {
        if (AddMenuHotspot)
        {
            AddMenuHotspot.EnableMenuTemplet();
        }
        reload = true;
        if (AddMenuHotspot) {

        AddMenuHotspot.ActiveMenu = gameObject;
        }
        Debug.Log(gameObject.name + "####");
        
        StartCoroutine( PutDataIntoMenuTemplete());
    }
    IEnumerator PutDataIntoMenuTemplete()  
    {
        Debug.Log("PutDataIntoMenuTemplete");

        if (AddMenuHotspot.ActiveMenu == gameObject)
        {
            Debug.Log("****" + AddMenuHotspot.ActiveMenu.GetComponent<MenuHotspot>().BtnID.ToString() + "***" + gameObject.GetComponent<MenuHotspot>().BtnID);


            Debug.Log("Recreate");
            for (int l = 0; l < AddMenuHotspot.MenuItemContainer.transform.childCount; l++)
            {
                Debug.Log("Destroy : " + AddMenuHotspot.MenuItemContainer.transform.GetChild(l).gameObject.name);
                Destroy(AddMenuHotspot.MenuItemContainer.transform.GetChild(l).gameObject);
            }

            while (AddMenuHotspot.MenuItemContainer.transform.childCount != 0)
            {
                yield return new WaitForEndOfFrame();
            }
            if (AddMenuHotspot.MenuItemContainer.transform.childCount == 0)
            {
                Debug.Log("All ittem are clear");
                AddMenuHotspot.BtnID.text = "";
                AddMenuHotspot.itemCounter = 0;
                AddMenuHotspot.ButtonFunction.Clear();
                AddMenuHotspot.ButtonLableName.Clear();
                AddMenuHotspot.ButtonFunctionData.Clear();
                AddMenuHotspot.MenuTemplete.gameObject.SetActive(true);
                AddMenuHotspot.ApplyBtn.gameObject.SetActive(false);
                AddMenuHotspot.UpdateBtn.gameObject.SetActive(true);

                AddMenuHotspot.BtnID.text = "Btn_" + BtnID.ToString();

                AddMenuHotspot.ButtonLableName.AddRange(ButtonLableName);
                AddMenuHotspot.ButtonFunction.AddRange(ButtonFunction);
                AddMenuHotspot.ButtonFunctionData.AddRange(ButtonFunctionData);
                AddMenuHotspot.posy.text = gameObject.transform.position.y.ToString();
                AddMenuHotspot.posx.text = gameObject.transform.position.x.ToString();
                AddMenuHotspot.posz.text = gameObject.transform.position.z.ToString();
                Debug.Log("Add information");

                for (int i = 0; i < ButtonLableName.Count; i++)
                {

                    AddMenuHotspot.AddItem();

                }


                for (int j = 0; j < ButtonLableName.Count; j++)
                {
                    Debug.Log("item name" + AddMenuHotspot.MenuItemContainer.transform.GetChild(j).gameObject.name + " @@@_" + ButtonLableName[j]);
                    AddMenuHotspot.MenuItemContainer.transform.GetChild(j).GetComponent<MenuItemProperty>().recall = true;
                    AddMenuHotspot.MenuItemContainer.transform.GetChild(j).GetComponent<MenuItemProperty>().InputItemName.textComponent.text = ButtonLableName[j];
                    AddMenuHotspot.MenuItemContainer.transform.GetChild(j).GetComponent<MenuItemProperty>().ItemName = ButtonLableName[j];
                    AddMenuHotspot.MenuItemContainer.transform.GetChild(j).GetComponent<MenuItemProperty>().Function = ButtonFunction[j];
                    AddMenuHotspot.MenuItemContainer.transform.GetChild(j).GetComponent<MenuItemProperty>().FunctionData = ButtonFunctionData[j];
                    AddMenuHotspot.MenuItemContainer.transform.GetChild(j).GetComponent<MenuItemProperty>().RecallData();

                    Debug.Log(AddMenuHotspot.MenuItemContainer.transform.GetChild(j).GetComponent<MenuItemProperty>().InputItemName.gameObject.name + "  " + ButtonLableName[j]);

                }

            } 
           
        }
    }



}
