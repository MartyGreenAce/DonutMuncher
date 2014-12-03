using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static GameManager managerInstance;
	
	public static GameManager Instance()
	{
		if (managerInstance == null)
		{
			Debug.LogError("Havn't Assigned GameManager");
		}
		
		return managerInstance;
	}
		
	public enum GameStates
	{
        Title,
		Game,
		GameOver
	};
		
	public GameStates currentGameState = GameStates.GameOver;
		
    //Systems
	public ScoreManager scoreManager;
	public Spin spinFood;
    
    //Food properties
    public Transform foodParent;
    private D2D_DestructibleSprite foodDestrucible;
    
    //Game timer progress
    public CircleProgressBar circleProgressBar;
    
    //Buttons etc
	public AnimateFadeInTextMesh [] fadeTexts;
	public AnimateFadeInSprite [] fadeSprites;
		
    //Game timer
    public float donutCompleteTime = 10;
    
    //Sky backdrop colours
	public Color [] backgroundColours;
    
    //Food prefab names
	public string [] foodPrefabs;
    
    //Game timer storing
	public double mainTimer;
	private double timeSinceLost;
		
	void Awake ()
	{
		managerInstance = this;	
	}
	
	void Start () 
	{	
        AddFood(false);
		UpdateFadeButtons(true, false);
	}
		
	void UpdateFadeButtons(bool activateFade, bool fadeState)
	{
		for (int i = 0; i < fadeTexts.Length; ++i)
		{
			//Fade in the UI buttons
			if (activateFade)
			{
				fadeTexts[i].ActivateFade(fadeState);
            }
						
			//Enable colliders so can press them
            if (fadeTexts[i].collider != null)
                fadeTexts[i].collider.enabled = !fadeState;
		}
				
		for (int i = 0; i < fadeSprites.Length; ++i)
		{
			//Fade in the UI buttons
			if (activateFade)
			{
				fadeSprites[i].ActivateFade(fadeState);
            }
						
			//Enable colliders so can press them
            if (fadeSprites[i].collider != null)
                fadeSprites[i].collider.enabled = !fadeState;
		}
	}
    
    void StartGame()
    {
        currentGameState = GameStates.Game;
        mainTimer = Time.time;  
        scoreManager.ResetScore();
        
        UpdateFadeButtons(true, true);
        
        Resources.UnloadUnusedAssets();
    }
    
    void EndGame()
    {
        spinFood.ResetSpeed();
        scoreManager.SaveScore();
        timeSinceLost = Time.time;
        mainTimer = Time.time;
        currentGameState = GameStates.GameOver;
        
        UpdateFadeButtons(true, false);
        
        //Change to a new sky colour
        Camera.main.backgroundColor = backgroundColours[Random.Range(0, backgroundColours.Length)];
    }

    void NextFood()
    {
        mainTimer = Time.time;           
        scoreManager.AddScore();
        spinFood.UpdateSpeed();
                     
        //Create particle when completed food
        GameObject foodParticle = (GameObject)Instantiate(Resources.Load("NewFoodParticle"));
        foodParticle.transform.position = foodParent.position;
                    
        //Spawn next food object
        AddFood(true);
    }
    
    void AddFood(bool destroyFirst)
    {
        if (destroyFirst)
            Destroy(foodDestrucible.gameObject);

        GameObject food = (GameObject)Instantiate(Resources.Load("Foods/" + foodPrefabs[Random.Range(0, foodPrefabs.Length)]));
        food.transform.parent = foodParent;
        food.transform.localPosition = Vector3.zero;
        foodDestrucible = food.GetComponent<D2D_DestructibleSprite>();
    }
	
	void Update () 
	{			
		if (currentGameState == GameStates.GameOver)
		{
			if (Input.GetMouseButtonDown(0) && 
				Time.time - timeSinceLost > 1 &&
				!RaycastManager.Instance().DidHitMenuButton())
			{
				StartGame();
			}
		}
        else if (currentGameState == GameStates.Title)
        {
            if (Input.GetMouseButtonDown(0) &&
				!RaycastManager.Instance().DidHitMenuButton())
            {
                StartGame();
            }
        }
		else if (currentGameState == GameStates.Game)
		{
			float timeRemaining = (float)(Time.time - mainTimer) / donutCompleteTime;
            
            //Update progress bar [0, 1]
            circleProgressBar.UpdateProgress(1 - timeRemaining);	

            //If time runs out then end game
			if (timeRemaining > 1)
			{
                EndGame();
			}
					
            //If has destroyed all food then create next food
			if (foodDestrucible.SolidPixelRatio < 0.002f)
			{
				NextFood();
			}
		}
	}
}
