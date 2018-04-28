using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{

	private Camera _cam;
	public bool ZoomOutCamera = false;

	public float InitialFOV;
	public float ZoomoutFOV;

	public float DampTime;

	private float _current;
	// Use this for initialization
	void Start ()
	{
		_cam = GetComponent<Camera>();
		InitialFOV = _cam.fieldOfView;
		_current = InitialFOV;
	}
	
	// Update is called once per frame
	void Update ()
	{
		var target = ZoomOutCamera ? ZoomoutFOV : InitialFOV;
		_cam.fieldOfView = Mathf.SmoothDamp(_cam.fieldOfView, target, ref _current, DampTime);
		
	}
}
