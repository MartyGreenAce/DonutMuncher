using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour 
{
	public float moveSpeed;
	public float removeTime;
		
	void Start () 
	{
		Destroy(gameObject, removeTime);
	}
	
	void Update () 
	{
		transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
	}
}
