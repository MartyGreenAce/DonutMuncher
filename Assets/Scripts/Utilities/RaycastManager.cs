using UnityEngine;
using System.Collections;

public class RaycastManager : MonoBehaviour 
{
	public static RaycastManager managerInstance;
	
	public static RaycastManager Instance()
	{
		if (managerInstance == null)
		{
			Debug.LogError("Havn't Assigned GameManager");
		}
		
		return managerInstance;
	}
		
	void Awake ()
	{
		managerInstance = this;	
	}
		
	public bool DidHitMenuButton()
	{
		Ray shotRay = Camera.main.ScreenPointToRay(Input.mousePosition);
	    RaycastHit shotRayHit;
	                        
	    if (Physics.Raycast(shotRay, out shotRayHit, 40))
        {
            return (shotRayHit.transform.tag == "MenuButton");
        }
				
		return false;
	}

	void Update () 
	{
	}
}
