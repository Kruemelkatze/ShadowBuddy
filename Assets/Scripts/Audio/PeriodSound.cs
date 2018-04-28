using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PeriodSound : MonoBehaviour
{

	public string[] AudioClipNames;

	public float PeriodMin = 7f;
	public float PeriodMax = 15f;

	private void Start()
	{
		Invoke(nameof(PlayRandomSoundInInterval), Random.Range(PeriodMin, PeriodMax));
	}

	void PlayRandomSoundInInterval()
	{
		int idx = Random.Range(0, AudioClipNames.Length);
		var key = AudioClipNames[idx];
		Hub.Get<AudioControl>().PlaySound(key);
		
		Invoke(nameof(PlayRandomSoundInInterval), Random.Range(PeriodMin, PeriodMax));
	}
}
