using UnityEngine;
using System.Collections;
using System;

public delegate void ButtonHandlerClick(object sender, EventArgs e);
public delegate void ButtonHandlerRelease(object sender, EventArgs e);
public delegate void ButtonHandlerReleaseNotHit(object sender, EventArgs e);
public delegate void ButtonHandlerReleaseNotHit2(object sender, EventArgs e);
public delegate void ButtonHandlerHolding(object sender, EventArgs e);

public class ButtonHandler : MonoBehaviour 
{
    public event ButtonHandlerClick buttonClickEvent;
    public event ButtonHandlerRelease buttonReleaseEvent;
    public event ButtonHandlerReleaseNotHit buttonReleaseNotHitEvent;
    public event ButtonHandlerReleaseNotHit2 buttonReleaseNotHitEvent2;
	public event ButtonHandlerHolding buttonHoldEvent;
		
    private Transform buttonObject;
    private Transform lastHitObject;
    
    protected virtual void OnButtonClick(EventArgs e) 
    {
        if (buttonClickEvent != null)
            buttonClickEvent (this, e);
    }
    
    protected virtual void OnButtonRelease(EventArgs e) 
    {
        if (buttonReleaseEvent != null)
            buttonReleaseEvent (this, e);
    }
    
    protected virtual void OnButtonReleaseNotHit(EventArgs e) 
    {
        if (buttonReleaseNotHitEvent != null)
            buttonReleaseNotHitEvent (this, e);
    }
    
    protected virtual void OnButtonReleaseNotHit2(EventArgs e) 
    {
        if (buttonReleaseNotHitEvent2 != null)
            buttonReleaseNotHitEvent2 (this, e);
    }
		
	protected virtual void OnButtonHold(EventArgs e) 
    {
        if (buttonHoldEvent != null)
            buttonHoldEvent (this, e);
    }
    
    void Start () 
    {
        buttonObject = transform;
    }
    
    void ButClicked(int touchIDFinger)
    {
        #if UNITY_EDITOR || UNITY_METRO || UNITY_WEBPLAYER
            if (Input.GetMouseButtonDown(0))
            {
                Ray shotRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit shotRayHit;
                                
                if (Physics.Raycast(shotRay, out shotRayHit, 40))
                {
                    Transform raycastHitObject = shotRayHit.transform;
                    lastHitObject = raycastHitObject;

                    if (raycastHitObject == buttonObject)
                    {
                        OnButtonClick(EventArgs.Empty);
                    }
                }
            }
        #else
            if (Input.touchCount > 0)
            {
                Ray shotRay = Camera.main.ScreenPointToRay(Input.GetTouch(touchIDFinger).position);
                RaycastHit shotRayHit;
                                
                if (Physics.Raycast(shotRay, out shotRayHit, 40))
                {
                    Transform raycastHitObject = shotRayHit.transform;
                    lastHitObject = raycastHitObject;
        
                    if (raycastHitObject == buttonObject)
                    {
                        OnButtonClick(EventArgs.Empty);
                    }
                }
            }
        #endif
    }
		
	void ButHold(int touchIDFinger)
    {
        #if UNITY_EDITOR || UNITY_METRO || UNITY_WEBPLAYER
            if (Input.GetMouseButton(0))
            {
                Ray shotRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit shotRayHit;
                                
                if (Physics.Raycast(shotRay, out shotRayHit, 40))
                {
                    Transform raycastHitObject = shotRayHit.transform;
                    lastHitObject = raycastHitObject;
								
                    if (raycastHitObject == buttonObject)
                    {
                        OnButtonHold(EventArgs.Empty);
                    }
                }
            }
        #else
            if (Input.touchCount > 0)
            {
                Ray shotRay = Camera.main.ScreenPointToRay(Input.GetTouch(touchIDFinger).position);
                RaycastHit shotRayHit;
                                
                if (Physics.Raycast(shotRay, out shotRayHit, 40))
                {
                    Transform raycastHitObject = shotRayHit.transform;
                    lastHitObject = raycastHitObject;
        
                    if (raycastHitObject == buttonObject)
                    {
                        OnButtonHold(EventArgs.Empty);
                    }
                }
            }
        #endif
    }
    
    void ButReleased(int touchIDFinger)
    {
#if UNITY_EDITOR || UNITY_METRO || UNITY_WEBPLAYER
            if (Input.GetMouseButtonUp(0))
            {
                Ray shotRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit shotRayHit;
                                
                if (Physics.Raycast(shotRay, out shotRayHit, 40))
                {
                    Transform raycastHitObject = shotRayHit.transform;
                
                    if (raycastHitObject == buttonObject &&
                        raycastHitObject == lastHitObject)
                    {
                        OnButtonRelease(EventArgs.Empty);
                    }
                    
                    if (raycastHitObject.name == buttonObject.name)
                    {
                        OnButtonReleaseNotHit(EventArgs.Empty);
                    }
                }
            
                OnButtonReleaseNotHit2(EventArgs.Empty);
            }
        #else
            if (Input.touchCount > 0)
            {
                Ray shotRay = Camera.main.ScreenPointToRay(Input.GetTouch(touchIDFinger).position);
                RaycastHit shotRayHit;
                                
                if (Physics.Raycast(shotRay, out shotRayHit, 40))
                {
                    Transform raycastHitObject = shotRayHit.transform;
                    
                    if (raycastHitObject == buttonObject &&
                        raycastHitObject == lastHitObject)
                    {
                        OnButtonRelease(EventArgs.Empty);
                    }
                    
                    if (raycastHitObject == buttonObject)
                    {
                        OnButtonReleaseNotHit(EventArgs.Empty);
                    }
                }
        
                OnButtonReleaseNotHit2(EventArgs.Empty);
            }
        #endif
    }

    void LateUpdate ()
    {
#if UNITY_EDITOR || UNITY_METRO || UNITY_WEBPLAYER
            if (Input.GetMouseButtonDown(0))
            {
                ButClicked(0);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ButReleased(0);
            }
				
			if (Input.GetMouseButton(0))
			{
				ButHold(0);
			}
        #else
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; ++i)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        ButClicked(i);
                    }
                    else if (Input.GetTouch(i).phase == TouchPhase.Ended)
                    {
                        ButReleased(i);
                    }
				
					if (Input.GetTouch(i).phase == TouchPhase.Moved ||
						Input.GetTouch(i).phase == TouchPhase.Stationary)
					{
						ButHold(i);
					}
                }
            }
        #endif
    }
}