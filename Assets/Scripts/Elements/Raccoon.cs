using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raccoon : MonoBehaviour
{

	public float MinusX = 3;
	public float PlusX = 3;

	private Vector3 _startPosition;

	public int Direction = 1;

	private float _initalX;

	public float Speed = 1;
	public float SinSpeed = 1;
	
	// Use this for initialization
	void Start ()
	{
		_initalX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
		var moveX = 0f;
		var posX = transform.position.x;
		
		switch( Direction )
		{
			case -1:
				// Moving Left
				if( posX > (-MinusX + _initalX))
				{
					moveX = -1.0f;
				}
				else
				{
					// Hit left boundary, change direction
					Direction = 1;
				}
				break;
     
			case 1:
				// Moving Right
				if( posX < (PlusX + _initalX) )
				{
					moveX = 1.0f;
				}
				else
				{
					// Hit right boundary, change direction
					Direction = -1;
				}
				break;
		}

		transform.position += new Vector3(moveX * Speed * Time.deltaTime, Mathf.Sin(2*Time.time) * SinSpeed * Time.deltaTime, 0);
	}
}
