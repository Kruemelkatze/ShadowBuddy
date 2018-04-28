using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class ShadowPlayer : MonoBehaviour
{
    public float GravityScale = 1;
    public bool DebugStuff = false;

    public float Speed = 3;

    public bool Sticked = false;
    public bool PreviousLit = false;

    private static float JumpTreshold = 0.5f;



    private TestIfLit _testIfLid;
    private SpriteRenderer _spriteRenderer;

    // Use this for initialization
    void Start()
    {
        _testIfLid = GetComponent<TestIfLit>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        Vector3 gravity = Vector3.forward * Physics.gravity.magnitude * GravityScale;
        float jumpAxis = Input.GetAxis("ShadowJump");
        float speedX = Input.GetAxis("ShadowHorizontal");
        bool Jump = jumpAxis > JumpTreshold;
        
        bool lit = _testIfLid.Lit;
        if (PreviousLit && !lit)
        {
            Sticked = true;
        }
        
        if (lit)
        {
            transform.position += gravity * Time.deltaTime;
            transform.position += Vector3.right * Speed * speedX * Time.deltaTime;
        }
        else if(Sticked && Jump)
        {
            Sticked = false;
            
        }
        

        if (DebugStuff)
        {
            Debug.Log($"Jump Axis: " + jumpAxis);
            Debug.Log($"ShadowHorizontal: " + speedX);
        }

        PreviousLit = lit;
    }
}