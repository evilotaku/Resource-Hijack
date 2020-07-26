using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour {

	public GameObject ground;
	public int numberOfObjects; // number of objects to place
	private int currentObjects; // number of placed objects
	public resource[] ResourcesToSpawn; // GameObject to place
	
	public static List<GameObject> SupplyList;

	// Use this for initialization
	void Start () 
	{		
		SpawnSupply ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnSupply()
	{
		ground = GameObject.Find("Ground");
		SupplyList = new List<GameObject>();			
		StartCoroutine(Spawn());
	}

	public IEnumerator Spawn()
	{
		while(SupplyList.Count <= numberOfObjects)
		{
			// generate random x position
			float posx = Random.Range(ground.transform.position.x-(ground.transform.localScale.x/2) , ground.transform.position.x + (ground.transform.localScale.x/2));
			// generate random z position
			float posz = Random.Range(ground.transform.position.z-(ground.transform.localScale.z/2), ground.transform.position.x + (ground.transform.localScale.z/2));
			// get the terrain height at the random position
			float posy = ground.transform.position.y;
            //Get random Resounse to spawn
            int nb = Random.Range(0, 1000) % ResourcesToSpawn.Length;
            // create new gameObject on random position
            GameObject newObject = (GameObject)Instantiate(ResourcesToSpawn[nb].gameObject, new Vector3(posx, posy, posz), Quaternion.identity);
			SupplyList.Add(newObject);
			//yield return new WaitForSeconds(3.0f);
		}
		yield return null;
		
	}
	
}
