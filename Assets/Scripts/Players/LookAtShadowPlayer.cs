using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtShadowPlayer : MonoBehaviour
{

	public GameObject ShadowPlayer;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(ShadowPlayer.transform);	
	}
}
