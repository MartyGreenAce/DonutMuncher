using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public enum CharacterStates
	{
		Eating,
		Idle,
		GoingTowardsFood,
		ComingBackToTrampoline
	};
		
	public CharacterStates currentCharacterState = CharacterStates.Idle;
		
	public Transform eatPoint;
	public Transform foodPoint;
	
	public tk2dSpriteAnimator playerSprite;
		
	public float trampLenght;
	public float flyLength;
		
	public Transform trampolineIdle0;
	public Transform trampolineIdle1;
	private double trampolineTime;
	private bool isBouncingUp = false;
		
	private Vector3 lastTrampPosition;
	private double flyTime;
		
	private double lastBiteTime;
		
	void Start () 
	{
	}
		
	void Update () 
	{
		switch (currentCharacterState)
		{
			case CharacterStates.Idle:
				double timeSince = (Time.time - trampolineTime);
						
				if (!isBouncingUp)
				{
					//Check if is at end location yet
					if (timeSince < trampLenght)
					{
						//Lerp using time based on [0, 1]
						transform.position = Vector3.Lerp(trampolineIdle0.position, trampolineIdle1.position, (float)timeSince / trampLenght);
					}
					else
					{
						trampolineTime = Time.time;				
						isBouncingUp = true;
					}
				}
				else
				{				
					//Check if is at start location yet
					if (timeSince < trampLenght)
					{
						//Lerp using time based on [0, 1]
						transform.position = Vector3.Lerp(trampolineIdle1.position, trampolineIdle0.position, (float)timeSince / (trampLenght * 0.6f));
					}				
					else
					{
						trampolineTime = Time.time;	
						isBouncingUp = false;
					}
				}
						
				if (Input.GetMouseButtonDown(0))
				{
					currentCharacterState = CharacterStates.GoingTowardsFood;
					lastTrampPosition = transform.position;
					flyTime = Time.time;
								
					playerSprite.Play("bite");
				}
			break;
						
			case CharacterStates.GoingTowardsFood:
				double timeSinceFly = (Time.time - flyTime);
					
				//Check if is at end location yet
				if (timeSinceFly < flyLength)
				{
					//Lerp using time based on [0, 1]
					transform.position = Vector3.Lerp(trampolineIdle0.position, eatPoint.position, (float)timeSinceFly / flyLength);
				}
				else
				{
					currentCharacterState = CharacterStates.ComingBackToTrampoline;
					lastTrampPosition = transform.position;
					flyTime = Time.time;
								
					trampolineTime = Time.time;				
				}
			break;
						
			case CharacterStates.ComingBackToTrampoline:
				double timeSinceFlyBack = (Time.time - flyTime);
					
				//Check if is at end location yet
				if (timeSinceFlyBack < flyLength)
				{
					//Lerp using time based on [0, 1]
					transform.position = Vector3.Lerp(lastTrampPosition, trampolineIdle0.position, (float)timeSinceFlyBack / flyLength);
				}
				else
				{
					currentCharacterState = CharacterStates.Idle;
					trampolineTime = Time.time;	
					isBouncingUp = false;		
								
					playerSprite.Play("idle");
				}
			break;
		}
	}
		
	void OnTriggerEnter2D(Collider2D other)
	{
		if (Time.time - lastBiteTime > 0.2f)
		{				
			lastBiteTime = Time.time;
			GameObject biteSmall = (GameObject)Instantiate(Resources.Load("BiteSmall"));	
			biteSmall.transform.position = eatPoint.position;
		
			currentCharacterState = CharacterStates.ComingBackToTrampoline;
			lastTrampPosition = transform.position;
			flyTime = Time.time;
		}
	}
}
