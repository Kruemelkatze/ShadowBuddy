using System;
using System.Collections;
using System.Collections.Generic;
using Cam;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerLight;
    public GameObject PlayerShadow;

    public LevelSegment[] Segments;
    public LevelSegment CurrentSegment;

    // Use this for initialization
    void Start()
    {
        Hub.Register(this);
        PlayerLight = GameObject.Find("PlayerLight");
        PlayerShadow = GameObject.Find("PlayerShadow");

        var levelSegmentList = new List<LevelSegment>();
        foreach (Transform child in GameObject.Find("Segments").transform)
        {
            var segment = child.gameObject.GetComponent<LevelSegment>();
            if (child.gameObject.tag != "ignore")
            {
                levelSegmentList.Add(segment);
            }
        }

        Segments = levelSegmentList.ToArray();

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
        PlayerShadow.transform.position =
            CurrentSegment.InitialPlayerPosition + CurrentSegment.LevelOffset + Vector3.up * 0.01f;

        if (cameraToo)
        {
            StartCoroutine(SlowCamera());
        }
    }

    private IEnumerator SlowCamera()
    {
        var followScript = Camera.main.gameObject.GetComponent<FollowCamera2D>();
        var oldDamp = followScript.dampTime;
        var tempDamp = oldDamp * 5;
        followScript.dampTime = tempDamp * 1.5f;

        var zoomOut = Camera.main.gameObject.GetComponent<ZoomOut>();
        zoomOut.DampTime = tempDamp;
        zoomOut.ZoomOutCamera = true;
        yield return new WaitForSeconds(tempDamp);
        followScript.dampTime = oldDamp;
        zoomOut.ZoomOutCamera = false;
    }

    public void EndOfLevelReached()
    {
        var index = Array.FindIndex(Segments, (e) => e == CurrentSegment);
        if (index == Segments.Length - 1)
        {
            Debug.LogWarning("Last Segment Overcome!");
        }
        else
        {
            CurrentSegment = Segments[index + 1];
            Reset(true);
        }
    }
}