using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPlayerRigid : MonoBehaviour {
	public float GravityScale = 1;
	public bool DebugStuff = false;

	public float Speed = 3;

	public bool Sticked = false;
	public bool PreviousLit = false;
	
	public float MaxDropSpeed = 5;
	public float JumpForce = 10;

	private static float JumpTreshold = 0.5f;

	private TestIfLit _testIfLid;
	private SpriteRenderer _spriteRenderer;
	private Rigidbody _rigid;
	
	// Use this for initialization
	void Start()
	{
		_testIfLid = GetComponent<TestIfLit>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_rigid = GetComponent<Rigidbody>();

	}
	
	void LateUpdate()
	{
		float speedX = Input.GetAxis("ShadowHorizontal") * Speed * 100 * Time.deltaTime;

		float verticalVelocity = Math.Min(_rigid.velocity.z, MaxDropSpeed);
		bool lit = _testIfLid.Lit;


		if (lit)
		{
			_rigid.velocity = new Vector3(speedX, 0, verticalVelocity);
			_rigid.useGravity = true;
		}
		
		if (PreviousLit && !lit)
		{
			Sticked = true;
			_rigid.velocity = Vector3.zero;
			_rigid.useGravity = false;
		}
		
		
		float jumpAxis = Input.GetAxis("ShadowJump");
		bool Jump = jumpAxis > JumpTreshold;
		
		if (!lit && Sticked && Jump)
		{
			float h = _rigid.velocity.x;
			_rigid.velocity = new Vector3(h, 0, -JumpForce);
			StartCoroutine(SetNotStickedafterTime());
			_rigid.useGravity = true;
		}
		
		if (DebugStuff)
		{
			Debug.Log($"Jump Axis: " + jumpAxis);
			Debug.Log($"ShadowHorizontal: " + speedX);
		}

		PreviousLit = lit;
	}

	private IEnumerator SetNotStickedafterTime()
	{
		yield return new WaitForSeconds(0.2f);
		if (_testIfLid.Lit)
		{
			Sticked = false;
		}
	}
}
