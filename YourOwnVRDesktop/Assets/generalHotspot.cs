using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class generalHotspot : MonoBehaviour {
	  
	public Vector3 Position;  
	public InputField posx, posy, posz;   
	public AddHotspot navigationHotspot;
	public AddTextHotspot textHotspot;     
	public AddActiveHotspot actionHotspot;

    public MenuHotspot MenuHotspot;
    public MediaHotspot MediaHotspot;
    public helpActionHotspot HelpActionHotpot;
	float R;
    public GameObject[] AllHotspotTemplets;  
	public int radius;  

    void Start () {
     	

        if (gameObject.GetComponent<navigateActionHotspot> ()) {  
			navigationHotspot = FindObjectOfType<AddHotspot> ();  
			posx = navigationHotspot.posx; 
			posy = navigationHotspot.posy;  
			posz = navigationHotspot.posz;  
			   

		}
		if (gameObject.GetComponent<textActionHotspot> ()) {  
			textHotspot = FindObjectOfType<AddTextHotspot> ();  
			posx = textHotspot.posx; 
			posy = textHotspot.posy;  
			posz = textHotspot.posz;    

		} 
		if (gameObject.GetComponent<NewActionHotspot> ()) {
			actionHotspot = FindObjectOfType<AddActiveHotspot> ();   
			posx = actionHotspot.posx; 
			posy = actionHotspot.posy;  
			posz = actionHotspot.posz;
			   
		}
        if (gameObject.GetComponent<helpActionHotspot>())
        {
            HelpActionHotpot = FindObjectOfType<helpActionHotspot>();
            posx = HelpActionHotpot.xpos;
            posy = HelpActionHotpot.yPos;
			posz = HelpActionHotpot.zPos;   
        }
        if (gameObject.GetComponent<MenuHotspot>())  
        {
            MenuHotspot = FindObjectOfType<MenuHotspot>();
            posx = MenuHotspot.posx;
            posy = MenuHotspot.posy;
			posz = MenuHotspot.posz;   
        }
        if (gameObject.GetComponent<MediaHotspot>())
        {
            MediaHotspot = FindObjectOfType<MediaHotspot>();
            posx = MediaHotspot.posx;
            posy = MediaHotspot.posy;
			posz = MediaHotspot.posz;      
        }
		radius = 10;    
		posz.text = "10";    
        R = 107f;      

        
	}

	void Update () {  
		  
		if(!transform.GetChild(0).transform.GetComponent<BoxCollider>().enabled){ 
			Position = gameObject.transform.position; 
			cartesianToPolar (Position);   
		}
		if (SetupDome.SelectedHotspot == gameObject) {
			posz.text = radius.ToString ();       
		}  
	}     

	public void cartesianToPolar(Vector3 point) {  
		Vector2 polar;  

		R = Mathf.Sqrt(Mathf.Pow(point.x,2)+Mathf.Pow(point.y,2)+Mathf.Pow(point.z,2));     

		polar.x = Mathf.Asin(point.z/R);    
		polar.y = Mathf.Atan2 (point.y, point.x);  

		polar *= Mathf.Rad2Deg;    

		posx.text = polar.x.ToString ("#.00");           
		posy.text = polar.y.ToString ("#.00"); 
		SetupDome.SelectedHotspot = gameObject;    
	}

	public void polarToCartesian(){
		PolarToCartesian (new Vector2 (float.Parse(posx.text),float.Parse(posy.text)));   
	}
		
	private void PolarToCartesian(Vector2 polar)    
	{
		polar *= Mathf.Deg2Rad;    
		  
		Vector3 point;

		point.x = R * Mathf.Cos (polar.x) * Mathf.Cos (polar.y); 
		point.y = R * Mathf.Cos (polar.x) * Mathf.Sin (polar.y); 
		point.z = R * Mathf.Sin (polar.x);  

		gameObject.transform.position = point;    
		 
	} 
	  
}
