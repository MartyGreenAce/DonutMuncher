using UnityEngine;
using System.Collections;
using System;

public class ButtonGoToScene : MonoBehaviour 
{
	public ButtonHandler buttonHandler;
    public string sceneName;
		
	void Start () 
	{
        buttonHandler.buttonReleaseEvent += new ButtonHandlerRelease(GoToScene);
	}
		
	void GoToScene(object sender, EventArgs e)
	{
        Application.LoadLevel(sceneName);
	}
	
	void Update () 
	{
	}
}
