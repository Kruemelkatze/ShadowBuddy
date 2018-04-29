using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Help : MonoBehaviour {

	// Use this for initialization
	private void Awake()
	{
		var joystickNames = Input.GetJoystickNames();
		var useJoystick = joystickNames.Length > 0;

		foreach (Transform child in transform)
		{
			if (useJoystick)
			{
				if (child.gameObject.name.Contains("joystick"))
				{
					child.gameObject.SetActive(true);
				}
			}
			else
			{
				if (child.gameObject.name.Contains("keyboard"))
				{
					child.gameObject.SetActive(true);
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
