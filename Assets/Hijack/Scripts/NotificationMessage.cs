using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NotificationMessage : MonoBehaviour {
    public NotificationType notificationType;
    public Player player;

    private string GetMessage()
    {
        var theMessage = "";
        switch (notificationType)
        {
            case NotificationType.YouWin:
                theMessage = "You Won " + player.name + "Great Job!!!";
                break;
            case NotificationType.YouLoose:
                theMessage = "You have falled us, we will all die shortly";
                break;
            case NotificationType.TruckCmdInititated:
                theMessage = "Yes sir, on our way";
                break;
            case NotificationType.BotHiJacked:
                theMessage = "Bot Hijacked";
                break;
            case NotificationType.Truck5Deactivated:
                theMessage = "Truck 5 reporting we are disabled, city is low on resources.";
                break;
            case NotificationType.Truck4Deactivated:
                theMessage = "Truck 4 reporting we are disabled, city is very low on resources.";
                break;
            case NotificationType.Truck3Deactivated:
                theMessage = "Truck 3 reporting we are disabled, city is critical on resources.";
                break;
            case NotificationType.Truck2Deactivated:
                theMessage = "Truck 2 reporting we are disabled, city is extremely critical on resources.";
                break;
            case NotificationType.Welcome:
                theMessage = "Welcome " + player.name + " Guide our trucks to HiJack recources and keep our race alive.";
                break;
        }
        return theMessage;
    }
}
