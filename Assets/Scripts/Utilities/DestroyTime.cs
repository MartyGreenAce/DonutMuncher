using UnityEngine;
using System.Collections;

public class DestroyTime : MonoBehaviour
{
	public float destroyTime = 1;
		
	void Start () 
	{
		Destroy(gameObject, destroyTime);
	}
}
