using UnityEngine;
using System.Collections;

public class AnimateScale : MonoBehaviour 
{
	public float startScale = 1;
	public float endScale = 2;
	public float animateSpeed = 1;
		
	void Start () 
	{
	}
	
	void Update () 
	{
		float newScale = Mathf.Lerp(startScale, endScale, 0.5f + 0.5f * Mathf.Sin(Time.time * animateSpeed));
		transform.localScale = new Vector3(newScale, newScale, newScale);
	}
}
