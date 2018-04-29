using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelSegment : MonoBehaviour
{
	public Vector3 LevelOffset = Vector3.zero;
	
	public Color ShadowColor = new Color(67,70,77, 255);
	
	public Vector3 InitialSunPosition = new Vector3(0, 4, 0.6f);
	public Vector3 InitialCameraPosition = new Vector3(0,5, 0.8f);
	public Vector3 InitialPlayerPosition = new Vector3(0,0, -5);

	// Use this for initialization
	void Awake ()
	{
		LevelOffset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
}
