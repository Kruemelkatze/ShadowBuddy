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
            if (!child.gameObject.CompareTag("ignore"))
            {
                levelSegmentList.Add(segment);
            }
        }

        Segments = levelSegmentList.ToArray();

        for (int i = 0; i < Segments.Length; i++)
        {
            var table = Segments[i].transform.Find("Table");
            var scaleX = table.localScale.x * (i % 2 == 0 ? -1 : 1);
            table.localScale = new Vector3(scaleX, table.localScale.y, table.localScale.z);
        }

        if (Segments.Length != 0)
        {
            CurrentSegment = Segments[0];
            Reset(false, true);
        }

        Hub.Get<AudioControl>().PlayDefaultMusic();
    }

    public void Reset(bool cameraToo = false, bool initial = false)
    {
        if (!CurrentSegment)
        {
            Debug.LogWarning("Reset without CurrentSegment called");
            return;
        }

        PlayerShadow.GetComponent<SpriteRenderer>().color = CurrentSegment.ShadowColor;
        CurrentSegment.GetComponentInChildren<EndOfLevel>().GetComponent<SpriteRenderer>().color = CurrentSegment.ShadowColor;

        PlayerLight.transform.position = CurrentSegment.InitialSunPosition + CurrentSegment.LevelOffset;
        PlayerShadow.transform.position =
            CurrentSegment.InitialPlayerPosition + CurrentSegment.LevelOffset + Vector3.up * 0.01f;

        if (cameraToo)
        {
            StartCoroutine(SlowCamera());
            Hub.Get<AudioControl>().PlaySound("amb_book_flip_3");
        }
        else if(!initial)
        {
            Hub.Get<AudioControl>().PlaySound("player_reset_1");
        }
    }

    private IEnumerator SlowCamera()
    {
        var followScript = Camera.main.gameObject.GetComponent<FollowCamera2D>();
        var oldDamp = followScript.dampTime;
        var tempDamp = oldDamp * 5;
        followScript.dampTime = tempDamp;

        var zoomOut = Camera.main.gameObject.GetComponent<ZoomOut>();
        zoomOut.DampTime = tempDamp;
        zoomOut.ZoomOutCamera = true;
        yield return new WaitForSeconds(tempDamp);
        zoomOut.ZoomOutCamera = false;
        yield return new WaitForSeconds(tempDamp);
        followScript.dampTime = oldDamp;
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