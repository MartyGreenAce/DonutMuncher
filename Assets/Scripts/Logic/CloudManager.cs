using UnityEngine;
using System.Collections;

public class CloudManager : MonoBehaviour
{
	private double lastCloudSpawn;
		
	void Start ()
	{
		lastCloudSpawn = Time.time - 5;
	}
	
	void Update () 
	{
		if (Time.time - lastCloudSpawn > 5)		
		{
			lastCloudSpawn = Time.time;
			
			if (Random.Range(0, 100) > 50)
			{
				GameObject cloud = (GameObject)Instantiate(Resources.Load("Cloud"));
				cloud.transform.position = new Vector3(11.0f, Random.Range(-2.0f, 5.0f), -1.6f);
				cloud.GetComponent<Cloud>().moveSpeed = -1.5f;
			}
			else
			{
				GameObject cloud = (GameObject)Instantiate(Resources.Load("Cloud"));
				cloud.transform.position = new Vector3(-11.0f, Random.Range(-2.0f, 5.0f), -1.5f);
				cloud.GetComponent<Cloud>().moveSpeed = 1.5f;
			}
		}
	}
}
