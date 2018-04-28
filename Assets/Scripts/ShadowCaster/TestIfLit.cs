using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIfLit : MonoBehaviour
{
    public GameObject light;
    private Rigidbody _rigid;
    private MeshRenderer _meshRenderer;
    private SpriteRenderer _spriteRenderer;
    public LayerMask RayCastMask;

    public bool Lit;
    public bool UseRigid = false;
    public bool RendererTint = true;

    // Use this for initialization
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var origin = transform.position + new Vector3(0, 0, transform.localScale.y / 2);
        var ray = new Ray(origin, light.transform.position - origin);
        RaycastHit hit;

        Lit = !Physics.Raycast(ray, out hit, float.PositiveInfinity, RayCastMask.value);
        Debug.DrawRay(ray.origin, ray.direction * 3, Color.red);

        ProcessRigid();
        ProcessRenderer();
    }

    private void ProcessRigid()
    {
        if (!UseRigid || _rigid == null)
            return;


        if (Lit)
        {
            _rigid.useGravity = true;
        }
        else
        {
            _rigid.useGravity = false;
            _rigid.velocity = Vector3.zero;
        }
    }

    private void ProcessRenderer()
    {
        if (_meshRenderer)
        {
            if (RendererTint)
            {
                _meshRenderer.material.color = Lit ? Color.green : Color.red;
            }
        }
        else if (_spriteRenderer)
        {
            if (RendererTint)
            {
                _spriteRenderer.color = Lit ? Color.green : Color.red;
            }
        }
    }
}