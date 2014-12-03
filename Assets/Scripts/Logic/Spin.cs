using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour 
{
	//User sets these
	public float initalSpeed = 3;
	public float maxSpeed = 10;
	public float spinIncrease = 0.25f;
		
	//Keep track of spin speeds
	private float currentSpeed = 0;
	private float currentSpeedRotate = 0;
		
	void Start () 
	{
		currentSpeed = initalSpeed;
		currentSpeedRotate = initalSpeed;
	}
		
	public void UpdateSpeed()
	{	
		//Cap the speed, so game doesn't get too hard
		if (currentSpeed < maxSpeed)
			currentSpeed += spinIncrease;
	
		//Random direction
		if (Random.Range(0, 100) > 50)
		{
			currentSpeedRotate = -currentSpeed;
		}
	}

	public void ResetSpeed()
	{
		currentSpeed = initalSpeed;	
		currentSpeedRotate = initalSpeed;
	}
	
	void Update ()
	{
		transform.RotateAround(new Vector3(0, 0, 1), currentSpeedRotate * Time.deltaTime);
	}
}
