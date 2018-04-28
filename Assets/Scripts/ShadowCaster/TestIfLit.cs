using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIfLit : MonoBehaviour
{

	public GameObject light;
	public Rigidbody rigid;

	// Use this for initialization
	void Start ()
	{
		rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		var ray = new Ray(transform.position, light.transform.position - transform.position);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{
			GetComponent<MeshRenderer>().material.color = Color.red;
			rigid.useGravity = false;
			rigid.velocity = new Vector3();
		}
		else
		{
			GetComponent<MeshRenderer>().material.color = Color.green;
			rigid.useGravity = true;
		}
		
		Debug.DrawRay(ray.origin, ray.direction, Color.red);

	}
}
