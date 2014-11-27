using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public D2D_DestructibleSprite foodDestrucible;
	public Transform foodParent;
		
	public tk2dTextMesh bestScoreText;
	public tk2dTextMesh scoreText;
	private int currentScore;
		
	public string [] foodPrefabs;
		
	public float donutCompleteTime = 10;
	public tk2dUIProgressBar mainTimerProgress;
	public double mainTimer;
		
	void Start () 
	{
		bestScoreText.text = "Best " + System.Environment.NewLine + PlayerPrefs.GetInt("BestScore").ToString();
	}
	
	void Update () 
	{
		float timeRemaining = (float)(Time.time - mainTimer) / donutCompleteTime;
		mainTimerProgress.Value = 1 - timeRemaining;		
	
		if (timeRemaining > 1)
		{
			//Check if score is a new high score
			if (currentScore > PlayerPrefs.GetInt("BestScore"))
			{
				//Save new high score & update UI
				PlayerPrefs.SetInt("BestScore", currentScore);
				bestScoreText.text = "Best " + System.Environment.NewLine + currentScore;
			}
						
			Destroy(foodDestrucible.gameObject);
						
			GameObject food = (GameObject)Instantiate(Resources.Load("Foods/" + foodPrefabs[Random.Range(0, foodPrefabs.Length)]));
			food.transform.parent = foodParent;
			food.transform.localPosition = Vector3.zero;
			foodDestrucible = food.GetComponent<D2D_DestructibleSprite>();
						
			mainTimer = Time.time;
			currentScore = 0;
			scoreText.text = currentScore.ToString();
		}
				
		if (foodDestrucible.SolidPixelRatio < 0.006f)
		{
			mainTimer = Time.time;
			
			currentScore++;
			scoreText.text = currentScore.ToString();
								
			Destroy(foodDestrucible.gameObject);
						
			GameObject food = (GameObject)Instantiate(Resources.Load("Foods/" + foodPrefabs[Random.Range(0, foodPrefabs.Length)]));
			food.transform.parent = foodParent;
			food.transform.localPosition = Vector3.zero;
			foodDestrucible = food.GetComponent<D2D_DestructibleSprite>();
		}
				
		//foodPercentage.text = ((1 - foodDestrucible.SolidPixelRatio) * 100).ToString("F") + "%";
	}
}
