using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerLight;
    public GameObject PlayerShadow;

    public LevelSegment[] Segments;
    public LevelSegment CurrentSegment;

    void Awake()
    {
        Hub.Register(this);
        PlayerLight = GameObject.Find("PlayerLight");
        PlayerShadow = GameObject.Find("PlayerShadow");
    }

    // Use this for initialization
    void Start()
    {
        if (Segments.Length != 0)
        {
            CurrentSegment = Segments[0];
            Reset();
        }
    }
    
    public void Reset(bool cameraToo = false)
    {
        if (!CurrentSegment)
        {
            Debug.LogWarning("Reset without CurrentSegment called");
            return;
        }
        
        PlayerLight.transform.position = CurrentSegment.InitialSunPosition + CurrentSegment.LevelOffset;
        PlayerShadow.transform.position = CurrentSegment.InitialSunPosition + CurrentSegment.LevelOffset;
		
        if (cameraToo)
        {
            Camera.main.transform.position = CurrentSegment.InitialCameraPosition + CurrentSegment.LevelOffset;
        }
    }

    public void EndOfLevelReached()
    {
		
    }
}