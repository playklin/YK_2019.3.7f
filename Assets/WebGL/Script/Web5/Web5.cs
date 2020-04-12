using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Web5 : MonoBehaviour
{
    //public InputField  If_id_order,if_message;
    public Text t_t;
    public Text t_count_open,t_count_work, t_count_close;
    public static string status = "Не отвеченные заявки";
    public static string exampel = "Открыта";
    public static string Web5idorder = "";
    //для появления цифр
    public static string open_n = "";
    public static string work_n = "";
    public static string close_n = "";
   
    void Start()
    {
        t_count_close.text = close_n;
        t_count_open.text = open_n;
        t_count_work.text = work_n;
        StartCoroutine(GetCOUNTykorderOpen("1"));
        StartCoroutine(GetCOUNTykorderWork("1"));
        StartCoroutine(GetCOUNTykorderClose("1"));
        t_t.text = status;
        //exampel = "tro";
    }

    public void ClickWork(){status = "В работе заявки";SceneManager.LoadScene("Web5");}
    public void ClickOpen(){status = "Не отвеченные заявки";SceneManager.LoadScene("Web5");}
    public void ClickClose(){status = "Закрытые заявки";SceneManager.LoadScene("Web5");}
    //public void ClickSend(){StartCoroutine(GetServerDate());PlayerPrefs.SetString("id_order", If_id_order.text);}
    //public void ClickExit(){SceneManager.LoadScene("Web");}
    public void ClickExit(){SceneManager.LoadScene("Weblog");}

  #region CHAT ----------

    IEnumerator GetCOUNTykorderOpen(string id) {WWWForm form = new WWWForm();form.AddField("id", id);//form.AddField("_facenumber", facenumber);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/Order/GetCOUNTykorderOpen.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log("" + www.downloadHandler.text);
        open_n = www.downloadHandler.text;
        t_count_open.text = www.downloadHandler.text;//yield return new WaitForSeconds(0.5f);//if(www.downloadHandler.text == "orderdone"){g_order_no.SetActive(true);}else{}
        }}
    }

    IEnumerator GetCOUNTykorderWork(string id) {WWWForm form = new WWWForm();form.AddField("id", id);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/Order/GetCOUNTykorderWork.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log("" + www.downloadHandler.text);
        work_n = www.downloadHandler.text;
        t_count_work.text = www.downloadHandler.text;//yield return new WaitForSeconds(0.5f);
        }}
    }

    IEnumerator GetCOUNTykorderClose(string id) {WWWForm form = new WWWForm();form.AddField("id", id);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/Order/GetCOUNTykorderClose.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log("" + www.downloadHandler.text);
        close_n = www.downloadHandler.text;
        t_count_close.text = www.downloadHandler.text;//yield return new WaitForSeconds(0.5f);
        }}
    }
/**
    IEnumerator CreateMessage(string id_order,string facenumber, string text1, string name, string datetime) {
        WWWForm form = new WWWForm();
        form.AddField("_id_order", id_order);form.AddField("_datetime", datetime);form.AddField("_facenumber", facenumber);
        form.AddField("_text1", text1);form.AddField("_name", name);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreateMessage.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{Debug.Log(" " + www.downloadHandler.text);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Web5");
        //Debug.Log(" " + www.downloadHandler.text);
        }
        }
    }

    public IEnumerator GetServerDate()
    {   UnityWebRequest www = UnityWebRequest.Get("https://playklin.000webhostapp.com/yk/GetServerDate.php");
        yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) {Debug.Log(www.error);} else
        {Debug.Log(www.downloadHandler.text);
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
        StartCoroutine(CreateMessage("20","0",if_message.text,"Бухгалтер",_timeData));
        }
    }
**/
  #endregion

}
