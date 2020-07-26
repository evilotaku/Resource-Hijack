using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CityType { Player, AI, Robot};

public class City : MonoBehaviour {
    public Team team;
    public GameObject CityAsset;
    public float RadarDisatance;
    public int maxResources;
    public int currentResources;
    public static Material playerMaterial;
    public static Material aiMaterial;
    public static Material robotMaterial;

    // Use this for initialization
    void Start () {
        if (playerMaterial == null)
        {
            //playerMaterial = Resources.Load("Material/PlayerCity", typeof(Material)) as Material;
            playerMaterial = Resources.Load("Material/RobotCity", typeof(Material)) as Material;
        }
        if (aiMaterial == null)
        {
            //aiMaterial = Resources.Load("Material/AICity", typeof(Material)) as Material;
            aiMaterial = Resources.Load("Material/PlayerCity", typeof(Material)) as Material;
        }
        if (robotMaterial == null)
        {
            //robotMaterial = Resources.Load("Material/RobotCity", typeof(Material)) as Material;
            robotMaterial = Resources.Load("Material/AICity", typeof(Material)) as Material;
        }
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        switch (team)
        {
            case Team.Red:
                renderers[1].sharedMaterial = playerMaterial;
                break;
            case Team.Blue:
                renderers[1].sharedMaterial = aiMaterial;
                break;
            case Team.Neutral:
                renderers[1].sharedMaterial = robotMaterial;
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void OnTriggerEnter(Collider other)
    {
        var theCollingObject = other.gameObject;
        if(theCollingObject.GetComponent<bot>())
        {

        }
    }
}
