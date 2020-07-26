using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class util : MonoBehaviour 
{
public static GameObject ClosestObject<T>(Vector3 origin)
{
    var gameObjects = FindObjectsOfType(typeof(T)) as GameObject[];
    return ClosestObject(origin, gameObjects);
}
 
//Returns all gameObjects with colliders within the given range of the given origin.
static GameObject ClosestObject(Vector3 origin, float range)
{
    var list = new List<GameObject>();
    Collider[] found = Physics.OverlapSphere(origin, range);
 
    foreach(var collider in found)
        list.Add(collider.gameObject);
   
    return ClosestObject(origin, list);
}
 
//Returns the closest gameObject in a given collection of gameObjects.
                       //IEnumerable so that you can pass any collection into this method. Array, List, Dictionary.Values, ect
static public GameObject ClosestObject(Vector3 origin, IEnumerable<GameObject> gameObjects)
{
    GameObject closest = null;
    float closestSqrDist = 0f;
 
    foreach(var gameObject in gameObjects) {
        float sqrDist = (gameObject.transform.position - origin).sqrMagnitude; //sqrMagnitude because it's faster to calculate than magnitude
 
        if (!closest || sqrDist < closestSqrDist) {
            closest = gameObject;
            closestSqrDist = sqrDist;
        }
    }
 
    return closest;
}


}
