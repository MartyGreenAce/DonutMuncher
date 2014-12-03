using UnityEngine;
using System.Collections;
using System;

public class ButtonCharacterChange : MonoBehaviour 
{
	public ButtonHandler buttonHandler;
    
	public Player player;
    public CharacterData characterData;
    
	public bool cycleLeft;
		
	void Start () 
	{
        buttonHandler.buttonReleaseEvent += new ButtonHandlerRelease(ChangeCharacter);
	}
		
	void ChangeCharacter(object sender, EventArgs e)
	{
		if (!cycleLeft)
		{
            if (player.currentCharacterID+1 < player.characters.Count)
            {
                player.currentCharacterID++;
                player.ChangeCharacter(player.currentCharacterID);
            }
            else
            {
                player.currentCharacterID = 0;
                player.ChangeCharacter(player.currentCharacterID);
            }
		}
		else
        {
            print(player.currentCharacterID);
            
            if (player.currentCharacterID-1 >= 0)
            {
                player.currentCharacterID--;
                player.ChangeCharacter(player.currentCharacterID);
            }
            else
            {
                player.currentCharacterID = player.characters.Count-1;
                player.ChangeCharacter(player.currentCharacterID);
            }
        }
	}
	
	void Update () 
	{
	}
}
