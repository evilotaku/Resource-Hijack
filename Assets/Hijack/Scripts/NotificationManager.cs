using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour {
    static NotificationManager instance;
    private IList<Notification> queue = new List<Notification>();

    public static NotificationManager GetInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void AddNotification(NotificationType aType, Player aPlayer)
    {

    }

}
