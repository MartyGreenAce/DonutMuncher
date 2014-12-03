using UnityEngine;
using System.Collections;
using System;

public class ButtonClickScaler : MonoBehaviour 
{
    private ButtonHandler buttonHandler;
	private Transform buttonSprite;
		
	private float scaleValue;
	private float originalScaleValue;
		
	void Awake () 
	{
        buttonHandler = GetComponent<ButtonHandler>();
        buttonSprite = transform;
        
        buttonHandler.buttonClickEvent += new ButtonHandlerClick(ScaleUp);
        buttonHandler.buttonReleaseNotHitEvent2 += new ButtonHandlerReleaseNotHit2(ScaleDown);
				
		originalScaleValue = buttonSprite.transform.localScale.x;
        scaleValue = buttonSprite.transform.localScale.x + (buttonSprite.transform.localScale.x * 0.1f);
	}
    
    void OnEnable()
    {
        buttonSprite.transform.localScale = new Vector3(originalScaleValue, originalScaleValue, originalScaleValue);
    }
		
	void ScaleUp(object sender, EventArgs e)
	{
		buttonSprite.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
	}
		
	void ScaleDown(object sender, EventArgs e)
	{
		buttonSprite.transform.localScale = new Vector3(originalScaleValue, originalScaleValue, originalScaleValue);
	}
	
	void Update () 
	{
	}
}
