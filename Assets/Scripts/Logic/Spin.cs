using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour 
{
	public static float spinSpeed = 2;
		
	void Start () 
	{
	}
	
	void Update ()
	{
		transform.RotateAround(new Vector3(0, 0, 1), spinSpeed * Time.deltaTime);
	}
}
