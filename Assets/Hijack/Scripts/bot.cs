using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class bot : MonoBehaviour 
{
	public Team team = Team.Neutral;
    public int speed;
	public resource resource;
	private NavMeshAgent agent;
	private bool loaded = false;
	private bool hasTarget = false;
	private System.Random random;
	private int supplyindex;
	//public List<GameObject> supplyPool;
	
	

	// Use this for initialization
	void Start () 
	{				
		agent = GetComponent<NavMeshAgent>();
		StartCoroutine(FindResource());		
	}

	void Update()
	{
		if(!hasTarget || loaded) return;
		//print("Distance Remaining: " + Vector3.Distance(transform.position, ResourceSpawner.SupplyList[supplyindex].transform.position).ToString());						
		if(Vector3.Distance(transform.position, agent.destination ) < 10.0f)
		{
			print("Supply Dropped Got");
			resource = ResourceSpawner.SupplyList[supplyindex].GetComponent<resource>();
			Destroy(ResourceSpawner.SupplyList[supplyindex]);
			ResourceSpawner.SupplyList.RemoveAt(supplyindex);
			ReturnResource();
		}		
	}
	


	public void CallFindResource()
	{
		StartCoroutine(FindResource());
	}

	public IEnumerator FindResource()
	{			
		yield return new WaitUntil(()=>agent.isOnNavMesh);
		random = new System.Random();
		supplyindex = random.Next(ResourceSpawner.SupplyList.Count);
		while (true)
		{
			if(hasTarget == true) yield break;
			print("Finding New Supply Drop");			
			if(agent.SetDestination(ResourceSpawner.SupplyList[supplyindex].transform.position))
			{
				hasTarget = true;
				print("Path Status: " + agent.path.status.ToString());
				print("Supply Drop Found");
				//yield return new WaitWhile(() => agent.remainingDistance == 0);  
				
			}
			else
			{
					print("Supply is active but unable to set destination");
			}
			yield return null;				
		}				
			
	}

	void ReturnResource()
	{
		print("Returning to base!");
		loaded = true;
		agent.SetDestination(ClosestTeamCity().transform.position);
		if(agent.remainingDistance < 1.0f)
		{
			agent.isStopped = true;
			hasTarget = false;
			loaded = false;
		}
	}

	public GameObject ClosestTeamCity()
	{
    City[] cities = FindObjectsOfType(typeof(City)) as City[];
	City[] teamObjs = Array.FindAll(cities, x => x.team == team);
	GameObject[] objs = new GameObject[teamObjs.Length];
	for(int i = 0; i < teamObjs.Length; i++)
	{
		objs[i] = teamObjs[i].gameObject;
	}
    return util.ClosestObject(transform.position, objs);
	}

	
}
