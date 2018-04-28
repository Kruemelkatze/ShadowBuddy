using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaiveLightMovement : MonoBehaviour
{

	public float Speed = 3;
	public bool DebugSpeed = false;

	public int Inverted = 1;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		//var speedX =Input.GetAxis("LightHorizontal");
		//var speedY = -Input.GetAxis("LightVertical");
		//var movement = new Vector2(speedX, speedY);
		var movement = DeadZoned();

		if (Input.GetAxis("LightInvert") > 0.5)
		{
			Inverted *= -1;
		}

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
		
		if (Input.GetKey(KeyCode.A))
		{
			movement.x = -1;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			movement.x = 1;
		}
		if (Input.GetKey(KeyCode.W))
		{
			movement.y = 1;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			movement.y = -1;
		}
		
		transform.position = transform.position + new Vector3(movement.x * Time.deltaTime *Speed, movement.y * Time.deltaTime * Speed * Inverted , speedZ * Time.deltaTime);
	}

	private Vector2 DeadZoned()
	{
		float deadzone = 0.25f;
		Vector2 stickInput = new Vector2(Input.GetAxis("LightHorizontal"), Input.GetAxis("LightVertical"));
		if(stickInput.magnitude < deadzone)
			stickInput = Vector2.zero;
		else
			stickInput = stickInput.normalized * ((stickInput.magnitude - deadzone) / (1 - deadzone));
		return stickInput;
	}
}
