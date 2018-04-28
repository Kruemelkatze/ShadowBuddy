using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
 
public class ShadowCollision : MonoBehaviour {
   
    public GameObject[] casters;
    public List<GameObject> objInLight = new List<GameObject>();
    public List<ShadowPuppet> puppets = new List<ShadowPuppet>();
    public List<EdgeCollider2D> ecList = new List<EdgeCollider2D>();
    public GameObject edgeCollider;
 
    private Light light;
    private float a;
    private float b;
    private float c;
   
    void Start ()
    {
        casters=GameObject.FindGameObjectsWithTag("Object");
        light=gameObject.GetComponent<Light>();
    }
   
    void Update ()
    {
        CheckObjectsInLight();
    }
    void CheckObjectsInLight()
    {
        for(int i=0; i< casters.Length; i++)
        {      
            a = casters[i].transform.position.x - transform.position.x;
            b = casters[i].transform.position.z - transform.position.z;
            c = Mathf.Sqrt(Mathf.Pow(a, 2)+Mathf.Pow(b, 2));
           
            float degree = 90 - (Mathf.Acos(a/c)*Mathf.Rad2Deg);
            if(Mathf.Abs(degree) <= light.spotAngle/2)
            {
                if(objInLight.Count == 0)
                {
                    objInLight.Add(casters[i]);
                    ShadowPuppet p = ScriptableObject.CreateInstance<ShadowPuppet>();
                    p.Create(casters[i], transform.position);
                    puppets.Add(p);
                    EdgeCollider2D ec = edgeCollider.AddComponent<EdgeCollider2D>();
                    ecList.Add (ec);
                }
                else if(!objInLight.Contains(casters[i]))
                {
                    objInLight.Add(casters[i]);
                    ShadowPuppet p = ScriptableObject.CreateInstance<ShadowPuppet>();
                    p.Create(casters[i], transform.position);
                    puppets.Add(p);
                    EdgeCollider2D ec = edgeCollider.AddComponent<EdgeCollider2D>();
                    ecList.Add (ec);
                }
            }
            else if(Mathf.Abs(degree) > light.spotAngle/2)
            {
                if(objInLight.Contains(casters[i]))
                {
                    int n = objInLight.IndexOf(casters[i]);
                    print (n);
                    objInLight.RemoveAt(n);
                    puppets.RemoveAt(n);
                    Destroy(ecList[n]);
                    ecList.RemoveAt(n);
                }
            }
        }
        for(int ii=0; ii<puppets.Count; ii++)
        {
            puppets[ii].origin = transform.position;
            puppets[ii].CreateShadowCollision();
            ecList[ii].points = puppets[ii].sortedPoints;
        }
        
        
    }
}