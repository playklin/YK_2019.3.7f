using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Web7 : MonoBehaviour
{
    public InputField if_date, if_street,if_house, if_text, if_num_notif_del;
    public Text t_news_ok;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClickExit(){SceneManager.LoadScene("Web");}
    public void ClickCreateNotif(){StartCoroutine(CreateNotif(if_date.text,if_street.text,if_house.text,if_text.text));}
    public void ClickDeletNotif(){StartCoroutine(DeletNotif(if_num_notif_del.text));}

    IEnumerator CreateNotif(string date, string street,string house, string text) {
        WWWForm form = new WWWForm();
        form.AddField("date", date);form.AddField("house", house);
        form.AddField("street", street);form.AddField("text", text);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreateNotif.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{t_news_ok.text = "OK";SceneManager.LoadScene("Web7");}
        }
    }

    IEnumerator DeletNotif(string id) {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        //form.AddField("_text", text);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/DeletNotif.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log("" + www.downloadHandler.text);
        //t_news_ok.text = "OK";
        SceneManager.LoadScene("Web7");}
        }
    }

     // Push notification

    //public InputField if_text;// if_subtext;
    public void ClickPush(){StartCoroutine(PushNot(if_text.text,"Уведомление по улице: " + if_street.text,""));}

    IEnumerator PushNot(string text, string subtext, string url){WWWForm form = new WWWForm(); 
        //form.AddField("id", id);
        form.AddField("text", text); form.AddField("subtext", subtext); form.AddField("url", url);
        //using (UnityWebRequest www = UnityWebRequest.Post("http://p905504y.beget.tech/notification1.php",form))
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/notificationYK.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error);}else{
        //Debug.Log("" + www.downloadHandler.text);
        //t_debugLog.text = www.downloadHandler.text;
        }}
    }
}
