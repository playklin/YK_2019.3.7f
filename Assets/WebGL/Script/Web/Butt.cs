using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using SimpleJSON;
using UnityEngine.Networking;

public class Butt : MonoBehaviour
{
    public GameObject g_add, g_order, g_promo;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetServerDate());
        if(Weblog.pass == "789"){g_promo.SetActive(false);g_add.SetActive(false);g_order.SetActive(false);}else{g_add.SetActive(true);g_order.SetActive(true);}
        if(Weblog.pass == "123"){g_add.SetActive(true);g_order.SetActive(true);}
    }

    public void ClickAll(){SceneManager.LoadScene("Web1");}
    public void ClickNotif(){SceneManager.LoadScene("Web7");}
    public void ClickNews(){SceneManager.LoadScene("Web3");}
    public void ClickAdd(){SceneManager.LoadScene("Web4");}
    public void ClickCreateOrder(){SceneManager.LoadScene("Web5");}//создать заявку
    public void ClickCreateQuiz(){SceneManager.LoadScene("Web6");}//создать опрос
    public void ClickPromo(){SceneManager.LoadScene("WebPromo");}//создать опрос
    public void ClickAdm(){SceneManager.LoadScene("WebAdm");}//создать опрос
    public void ClickExit(){SceneManager.LoadScene("Weblog");}//создать опрос
    public void ClickTEST(){SceneManager.LoadScene("WebTEST");}//создать опрос

    public IEnumerator GetServerDate()
    {   UnityWebRequest www = UnityWebRequest.Get("https://playklin.000webhostapp.com/yk/GetServerDate.php");
        yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) {Debug.Log(www.error);} else
        {//Debug.Log(www.downloadHandler.text);
        string _timeData = www.downloadHandler.text;
        string[] words = _timeData.Split(' ');    
        //timerTestLabel.text = www.text;
        //Debug.Log ("The date is : " + words[0]);
        //Debug.Log ("The time is : " + words[1]);
        PlayerPrefs.SetString("date", words[0]);
        //_data.text = words[0];
        //setting current time
        //t_date.text = words[0];
        //string _currentTime = words[1];
        }
    }

}
