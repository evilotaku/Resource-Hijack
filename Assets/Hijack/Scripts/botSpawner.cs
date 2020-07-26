using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botSpawner : MonoBehaviour 
{
	public GameObject bot;
	public static List<GameObject> botList;
	public City[] citylist;
	public GameObject ground;
	public int numberOfBots; // number of objects to place
	private int currentBots; // number of placed objects
	public System.Random rand;


	void Start () 
	{
		botList = new List<GameObject>();
		rand = new System.Random();
		SpawnBots ();
	}

	public void SpawnBots()
	{
		ground = GameObject.Find("Ground");
		for (int i = 0; i < numberOfBots; i++)
		{
			Spawn();
		}		

		//ActivateBots();	
	}
	public void Spawn()
	{
		var index = rand.Next(citylist.Length);		
		var city = citylist[index].gameObject;
		float angle = rand.Next(360) * Mathf.PI * 2;
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 100; // original line
        var newBot = Instantiate(bot ,city.transform.position + pos, Quaternion.identity);
		newBot.transform.SetParent(GameObject.Find("Bots").transform);
		botList.Add(newBot);
	}

	public void ActivateBots()
	{
		foreach (var bot in botList)
		{
			bot.GetComponent<bot>().CallFindResource();
		}	
	}

	
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
