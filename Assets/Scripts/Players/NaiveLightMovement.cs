using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaiveLightMovement : MonoBehaviour
{

	public float Speed = 3;
	public bool DebugSpeed = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		var speedX =Input.GetAxis("LightHorizontal");
		var speedY = -Input.GetAxis("LightVertical");

		if (DebugSpeed)
		{
					
			Debug.Log(Input.GetAxis("LightHorizontal"));
			Debug.Log(Input.GetAxis("LightVertical"));
		}

		var speedZ = 0f;
		if (Input.GetKey(KeyCode.O))
		{
			speedZ = Speed;
		} 
		else if(Input.GetKey(KeyCode.L))
		{
			speedZ = -Speed;
		}
		
		transform.position = transform.position + new Vector3(speedX * Time.deltaTime *Speed, speedY * Time.deltaTime * Speed, speedZ * Time.deltaTime);
	}
}
