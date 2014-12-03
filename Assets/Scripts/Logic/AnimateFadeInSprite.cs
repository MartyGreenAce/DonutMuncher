using UnityEngine;
using System.Collections;

public class AnimateFadeInSprite : MonoBehaviour 
{
	public float fadeTime;
	public bool shouldAlphaZero = true;
	private double lastTime;
	private bool isFadingOut = true;
		
	private tk2dSprite ownSprite;
		
	void Awake() 
	{
		ownSprite = GetComponent<tk2dSprite>();
				
		//Doesn't fade in at startup (used on title)
		if (shouldAlphaZero)
		{	
			lastTime = Time.time - (fadeTime * 2);
			ownSprite.color = new Color(ownSprite.color.r, ownSprite.color.g, ownSprite.color.b, 0);
		}
	}
		
	public void ActivateFade(bool fadeLevel)
	{
		isFadingOut = fadeLevel;
				
		lastTime = Time.time;
				
		if (isFadingOut)
			ownSprite.color = new Color(ownSprite.color.r, ownSprite.color.g, ownSprite.color.b, 0);
		else
			ownSprite.color = new Color(ownSprite.color.r, ownSprite.color.g, ownSprite.color.b, 1);			
	}
	
	void Update() 
	{	
		double timeSince = Time.time - lastTime;	
			
		if (timeSince / fadeTime < 1)
		{
			//Fade in/out the colour over time
			if (isFadingOut)
				ownSprite.color = new Color(ownSprite.color.r, ownSprite.color.g, ownSprite.color.b, 1 - ((float)timeSince / fadeTime));
			else
				ownSprite.color = new Color(ownSprite.color.r, ownSprite.color.g, ownSprite.color.b, (float)timeSince / fadeTime);
		}
		else
		{
			//Set the colour just incase the lerp wasn't accurate
			if (isFadingOut)
				ownSprite.color = new Color(ownSprite.color.r, ownSprite.color.g, ownSprite.color.b, 0);
			else
				ownSprite.color = new Color(ownSprite.color.r, ownSprite.color.g, ownSprite.color.b, 1);
		}
	}
}
