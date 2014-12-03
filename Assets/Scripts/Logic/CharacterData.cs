using UnityEngine;
using System.Collections;

public class CharacterData : MonoBehaviour
{
	public Player player;
		
	void Awake () 
	{
		TextAsset t = Resources.Load("characters", typeof(TextAsset)) as TextAsset;
		string [] splitEntireFile = t.text.Split("\n"[0]);
		
		string txt = "";

		for (int s = 1; s < splitEntireFile.Length; ++s)
		{
			txt = splitEntireFile[s];
						
			int currentPlayerID = player.characters.Count;
			player.characters.Add(new Player.Character());
			
			string [] splitStr = txt.Split(","[0]);

			player.characters[currentPlayerID].hairID = int.Parse(splitStr[0]);
			player.characters[currentPlayerID].mouthID = int.Parse(splitStr[1]);
			player.characters[currentPlayerID].eyeID = int.Parse(splitStr[2]);
			player.characters[currentPlayerID].bodyID = int.Parse(splitStr[3]);
						
			string [] colourSplit = splitStr[4].Split("-"[0]);
						
			player.characters[currentPlayerID].characterColour = new Color((float)(int.Parse(colourSplit[0])) / 255.0f, (float)(int.Parse(colourSplit[1])) / 255.0f, (float)(int.Parse(colourSplit[2])) / 255.0f);
		}
	}
	
	void Update () 
	{
	}
}
