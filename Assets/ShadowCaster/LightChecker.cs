using UnityEngine;
using System.Collections;

[System.Serializable]
public class LightChecker 
{
	public bool CheckForInLight(Vector3 target, Light light)
	{
		Vector3 targetDir = target - light.transform.position;
		Vector3 forward = light.transform.forward;
		float angle = Vector3.Angle(targetDir, forward);
		if (angle < light.spotAngle/2 && Vector3.Distance(target, light.transform.position)<light.range)
		{
			return true;
		}
		else
			return false;
	}
}
