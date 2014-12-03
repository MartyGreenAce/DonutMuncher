using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    
	//Point where the eat location is
	public Transform eatPoint;
    
    [System.Serializable] 
    public class Character
    {
        public int hairID;
        public int mouthID;
        public int eyeID;
        public int bodyID;
        public Color characterColour;
    }
    
	[HideInInspector]
	public List<Character> characters = new List<Character>();
	
	//Changing sprites dynamically (animation)
	public tk2dSprite playerArmLegs;
	public tk2dSprite playerHair;
    public tk2dSprite playerMouth;
    public tk2dSprite playerEyes;
    public tk2dSprite playerBody;
		
	//Lerp length for jumping/idling
	public float idleLenght;
	public float flyLength;
		
	//Idle points that it lerps between
	public Transform idle0;
	public Transform idle1;
		
	//Track bouncing on idle
	private bool isBouncingUp = false;
	private Vector3 lastIdlePosition;
		
	//Keep track of time for lerping
	private double idleTime;
	private double flyTime;
	private double lastBiteTime;
    
	[HideInInspector]
	public int currentCharacterID;
		
	void Start () 
	{
        ChangeCharacter(0);
	}
		
    public void ChangeCharacter(int characterID)
    {
        currentCharacterID = characterID;
        
        playerEyes.SetSprite("eyes" + characters[characterID].eyeID.ToString());
        playerBody.SetSprite("body" + characters[characterID].bodyID.ToString());
        playerHair.SetSprite("haircut" + characters[characterID].hairID.ToString() + "_0"); 
        playerMouth.SetSprite("mouth" + characters[characterID].mouthID.ToString() + "_open"); 
        
        playerBody.color = characters[characterID].characterColour;
        playerArmLegs.color = characters[characterID].characterColour;
    }
		
	void Update () 
	{
		switch (currentCharacterState)
		{
			case CharacterStates.Idle:
				double timeSince = (Time.time - idleTime);
						
				if (!isBouncingUp)
				{
					//Check if is at end location yet
					if (timeSince < idleLenght)
					{
						//Lerp using time based on [0, 1]
						transform.position = Vector3.Lerp(idle0.position, idle1.position, (float)timeSince / idleLenght);
					}
					else
					{
						idleTime = Time.time;				
						isBouncingUp = true;
					}
				}
				else
				{				
					//Check if is at start location yet
					if (timeSince < idleLenght)
					{
						//Lerp using time based on [0, 1]
						transform.position = Vector3.Lerp(idle1.position, idle0.position, (float)timeSince / (idleLenght * 0.6f));
					}				
					else
					{
						idleTime = Time.time;	
						isBouncingUp = false;
					}
				}

                //Jump towards the donut
				if (GameManager.Instance().currentGameState == GameManager.GameStates.Game &&
					!RaycastManager.Instance().DidHitMenuButton())
				{
					if (Input.GetMouseButtonDown(0))
					{
                        //Set jump properties
						currentCharacterState = CharacterStates.GoingTowardsFood;
						lastIdlePosition = transform.position;
						flyTime = Time.time;
										
                        //Set hair to down and bite
                        playerHair.SetSprite("haircut" + characters[currentCharacterID].hairID.ToString() + "_0");	
                        playerMouth.SetSprite("mouth" + characters[currentCharacterID].mouthID.ToString() + "_open"); 
                        playerArmLegs.SetSprite("armLegDown");
					}
				}
			break;
						
			case CharacterStates.GoingTowardsFood:
				double timeSinceFly = (Time.time - flyTime);
					
				//Check if is at end location yet
				if (timeSinceFly < flyLength)
				{
					//Lerp using time based on [0, 1]
					transform.position = Vector3.Lerp(idle0.position, eatPoint.position, (float)timeSinceFly / flyLength);
				}
				else
				{
                    //Set fall properties
					currentCharacterState = CharacterStates.ComingBackToTrampoline;
					lastIdlePosition = transform.position;
					flyTime = Time.time;
					idleTime = Time.time; 
                    
					//Set hair to down and falling			
                    playerHair.SetSprite("haircut" + characters[currentCharacterID].hairID.ToString() + "_1");
                    playerMouth.SetSprite("mouth" + characters[currentCharacterID].mouthID.ToString() + "_close"); 
                    playerArmLegs.SetSprite("armLegUp");		
				}
			break;
						
			case CharacterStates.ComingBackToTrampoline:
				double timeSinceFlyBack = (Time.time - flyTime);
					
				//Check if is at end location yet
				if (timeSinceFlyBack < flyLength)
				{
					//Lerp using time based on [0, 1]
					transform.position = Vector3.Lerp(lastIdlePosition, idle0.position, (float)timeSinceFlyBack / flyLength);
				}
				else
				{
                    //Set to idle
					currentCharacterState = CharacterStates.Idle;
					idleTime = Time.time;	
					isBouncingUp = false;		
								
                    //Set hair to down and idle   
                    playerHair.SetSprite("haircut" + characters[currentCharacterID].hairID.ToString() + "_0"); 
                    playerMouth.SetSprite("mouth" + characters[currentCharacterID].mouthID.ToString() + "_open"); 
                    playerArmLegs.SetSprite("armLegDown");
				}
			break;
		}
	}
		
    //Once has bitten the donut will fall back down
	void OnTriggerEnter2D(Collider2D other)
	{
		//Stops user from biting alot or taking big chunk
		if (Time.time - lastBiteTime > 0.2f)
		{				
			lastBiteTime = Time.time;
						
			//Spawn bite so it removes bite from the destrucible object
			GameObject biteSmall = (GameObject)Instantiate(Resources.Load("BiteSmall"));	
			biteSmall.transform.position = eatPoint.position;

            //Set hair to up and fall          
            playerHair.SetSprite("haircut" + characters[currentCharacterID].hairID.ToString() + "_1"); 
            playerMouth.SetSprite("mouth" + characters[currentCharacterID].mouthID.ToString() + "_close"); 
            playerArmLegs.SetSprite("armLegUp");
						
            //Set to falling
			currentCharacterState = CharacterStates.ComingBackToTrampoline;
			lastIdlePosition = transform.position;
			flyTime = Time.time;
		}
	}
}
