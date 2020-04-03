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
}
