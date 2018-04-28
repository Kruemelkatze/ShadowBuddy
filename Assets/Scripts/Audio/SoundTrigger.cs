using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{

	public string Sound;

	public float Volume = 1;

	private void OnTriggerEnter(Collider other)
	{
		Hub.Get<AudioControl>().PlaySound(Sound, Volume);
	}
}
