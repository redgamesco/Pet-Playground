#if !PLATFORM_STANDALONE
using System;
using System.Collections;
using System.Collections.Generic;
using NotificationSamples;
using UnityEditor;
using UnityEngine;

public class PushNotifs : MonoBehaviour
{
    public PushNotifsList NotifsList;
    public int NotifsToQueue = 5;
    public int NotifStaggeringMins = 30;

    public GameNotificationsManager GameNotificationsManager;

    public string PetName = "Your pet"; //swap with pet name, if available!

    public void Start()
    {
        StartCoroutine(SetupFutureNotifs());
    }

    private IEnumerator SetupFutureNotifs()
    {
        yield return StartCoroutine(GameNotificationsManager.Initialize());
        GameNotificationsManager.CancelAllNotifications();

        List<int> notifsPicked = new List<int>();
        //Try to select unique notifications, no repeats
        while (notifsPicked.Count < NotifsToQueue)
        {
            int tryPick = UnityEngine.Random.Range(0, NotifsList.PushNotifs.Count);
            if (!notifsPicked.Contains(tryPick))
            {
                notifsPicked.Add(tryPick);
            }
        }

        for (int i = 0; i < notifsPicked.Count; i++)
        {//PlayerSettings.productName
            QueueNotif("PetPlayground", String.Format(NotifsList.PushNotifs[i], PetName), DateTime.Now.AddMinutes(NotifStaggeringMins * ((i+1) * (i+1))));
        }
    }

    public void QueueNotif(string title, string body, DateTime time)
    {
        GameNotification notification = GameNotificationsManager.CreateNotification();
        if (notification == null)
        {
            return;
        }
        notification.Title = title;
        notification.Body = body;

        GameNotificationsManager.ScheduleNotification(notification, time);
        Debug.Log("[NOTIFICATION QUEUED FOR " + time.ToShortTimeString() + "] " + title + " : " + body);
    }

}
#endif