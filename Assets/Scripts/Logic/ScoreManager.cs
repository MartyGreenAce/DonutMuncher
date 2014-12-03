using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{			
	public tk2dTextMesh bestScoreText;
	public tk2dTextMesh scoreText;
	
	private int currentScore;
	private string scoreSaveName = "BestScore";
		
	void Start () 
	{
		//bestScoreText.text = "Best " + PlayerPrefs.GetInt(scoreSaveName).ToString();
	}
	
	public void AddScore()
	{
		currentScore++;
		UpdateScore(currentScore);		
	}
		
	public void ResetScore()
	{
		currentScore = 0;
		UpdateScore(currentScore);	
	}
		
	public void UpdateScore(int score)
	{
		//scoreText.text = score.ToString();
	}
		
	public void SaveScore()
	{
		//Check if score is a new high score
		if (currentScore > PlayerPrefs.GetInt(scoreSaveName))
		{
			//Save new high score & update UI
			PlayerPrefs.SetInt(scoreSaveName, currentScore);
			//bestScoreText.text = "Best " + currentScore;
		}
	}
	
	void Update () 
	{
	}
}
