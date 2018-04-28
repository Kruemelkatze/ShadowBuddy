using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaiveLightMovement : MonoBehaviour
{

	public float Speed = 3;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		var speedX = Math.Sign(Input.GetAxis("Horizontal"));
		var speedY = Math.Sign(Input.GetAxis("Vertical"));

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
