using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Stealth : MonoBehaviour {
	
	public GameObject[] casters;
	public List<GameObject> objInLight = new List<GameObject>();
	public List<Puppet> puppets = new List<Puppet>();
	LightChecker lC = new LightChecker();
	private Light light;
	private float a;
	private float b;
	private float c;

	bool test;
	GameObject tmp;
	
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
			if(lC.CheckForInLight(casters[i].transform.position, light))
			{
				if(!objInLight.Contains(casters[i]))
				{
					objInLight.Add(casters[i]);
					Puppet p = new Puppet();
					p.Create(casters[i], transform.position);
					puppets.Add(p);
				}
			}
			else
			{
				if(objInLight.Contains(casters[i]))
				{
					int n = objInLight.IndexOf(casters[i]);
					objInLight.RemoveAt(n);
					puppets.RemoveAt(n);
				}
			}
		}
		int k = puppets.Count-1;
		
		for(int ii=0; ii<puppets.Count; k = ii++)
		{
			puppets[ii].origin = transform.position;
			puppets[ii].rayLength = light.range;
			puppets[ii].GetShadowBounds();
				for(int j=0; j < puppets[k].pointsList.Count; j++)
				{

					if(Area.ContainsPoint(puppets[ii].pointsList.ToArray(), puppets[k].pointsList[j]))
					{
						Debug.Log("Shadows are touching!");
					}
//					else
//					{
//						Debug.Log("outside "+ii+" / "+k+" / "+j);
//					}
				}
		}

	}
}