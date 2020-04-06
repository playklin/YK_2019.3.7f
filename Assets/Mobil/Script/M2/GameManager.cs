using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotificationSamples;
using System;

// Если много сцен то пометьте экземпляр GameNotificationsManager как DontDestroyOnLoad

public class GameManager : MonoBehaviour
{
    /*
    [SerializeField] private GameNotificationsManager notificationsManager;
    private int notificationDelay = 5;
    private string inputtext;
 
    private void Start()
    {
        InitializeNotifications();
    }

    private void InitializeNotifications(){
        GameNotificationChannel channel = new GameNotificationChannel("mntutorial","Mob Notification Tutorial","Jast a notification");
        notificationsManager.Initialize(channel);
        //DontDestroyOnLoad(notificationsManager);
    }

    public void OnTimeInput(string text){
        if(int.TryParse(text, out int sec ))
        notificationDelay = sec;
    }

    public void TextInput(string text){
        //if(int.TryParse(text, out int sec ))
        inputtext = text;
    }

    public void CreateNotification(){
        CreateNotification("Жилищник ЛЕФОРТОВО","Показания приняты!",//inputtext, 
        DateTime.Now.AddSeconds(notificationDelay));
        //Debug.Log(inputtext+"  "+notificationDelay);
    }

    private void CreateNotification(string title, string body, DateTime time){



        IGameNotification notification = notificationsManager.CreateNotification();
        if(notification != null){
            notification.Title = title;
            notification.Body = body;
            notification.BadgeNumber = 1;
            //notification.SmallIcon = ""; // идентификатор из инспектора
            notification.DeliveryTime = time;
            notificationsManager.ScheduleNotification(notification);
        }
    }
    */
}
