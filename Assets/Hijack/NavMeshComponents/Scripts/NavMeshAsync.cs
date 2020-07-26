using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;
[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshAsync : MonoBehaviour {
	NavMeshSurface nms;
	public Image i;
	public GameObject text;
	AsyncOperation ao;
	public UnityEvent OnNavBake;

	// Use this for initialization
	void Start () {
		nms = GetComponent<NavMeshSurface> ();
	}
	
	public void BuildNavMeshAsync(){
		StartCoroutine (BuildNavmesh (nms));
	}

	bool loading=false;
	void Update(){
		if (loading) {
			i.fillAmount = (1 - ao.progress);
		}
	}

	// called by startcoroutine whenever you want to build the navmesh
	IEnumerator BuildNavmesh(NavMeshSurface surface)
	{
		// get the data for the surface
		var data = InitializeBakeData(surface);

		// start building the navmesh
		ao = surface.UpdateNavMesh(data);
		loading = true;
		// wait until the navmesh has finished baking
		yield return ao;

		Debug.Log("finished");

		// you need to save the baked data back into the surface
		surface.navMeshData = data;

		// call AddData() to finalize it
		surface.AddData();
		loading = false;
		i.fillAmount = 0;
		text.SetActive (false);
		OnNavBake.Invoke ();
	}

	// creates the navmesh data
	static NavMeshData InitializeBakeData(NavMeshSurface surface)
	{
		var emptySources = new List<NavMeshBuildSource>();
		var emptyBounds = new Bounds();

		return UnityEngine.AI.NavMeshBuilder.BuildNavMeshData(surface.GetBuildSettings(), emptySources, emptyBounds, surface.transform.position, surface.transform.rotation);
	}



}
