using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainOnS : MonoBehaviour
{
  public static string playerid = "";

    void Start () {
    // Enable line below to enable logging if you are having issues setting up OneSignal. (logLevel, visualLogLevel)
    // OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.INFO, OneSignal.LOG_LEVEL.INFO);
    // 6ddb852c-dfca-4e69-84d9-3c5d38a4b34b
    //AndroidRoman//OneSignal.StartInit("18acf8c9-f31d-485a-92ab-8d7b886be084")
    OneSignal.StartInit("6ddb852c-dfca-4e69-84d9-3c5d38a4b34b")// YK
    .HandleNotificationOpened(HandleNotificationOpened)
    .EndInit();
  
     OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
     
     // проверка
     OneSignal.subscriptionObserver += OneSignal_subscriptionObserver;
    }

    //OneSignal.subscriptionObserver += OneSignal_subscriptionObserver;

      public void OneSignal_subscriptionObserver(OSSubscriptionStateChanges stateChanges) {
      //Debug.Log("stateChanges: " + stateChanges);
      Debug.Log("stateChanges.to.userId: " + stateChanges.to.userId); playerid = stateChanges.to.userId;
      //Debug.Log("stateChanges.to.subscribed: " + stateChanges.to.subscribed);
   }

    // Gets called when the player opens the notification.
    private static void HandleNotificationOpened(OSNotificationOpenedResult result) {
    }
}
