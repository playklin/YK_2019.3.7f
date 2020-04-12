using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Web3 : MonoBehaviour
{
    public InputField If_date, If_news;
    public Text t_news_ok;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClickExit(){SceneManager.LoadScene("Web");}
    public void ClickCreateNews(){StartCoroutine(CreateNews(If_date.text,If_news.text));}

    IEnumerator CreateNews(string date1, string new1) {
        WWWForm form = new WWWForm();
        form.AddField("date1", date1);
        form.AddField("new1", new1);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreateNews.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{t_news_ok.text = "OK";SceneManager.LoadScene("Web3");}
        }
    }

    // Push notification

    //public InputField if_text;// if_subtext;
    public void ClickPush(){StartCoroutine(PushNot(If_news.text,"Новости",""));}

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
