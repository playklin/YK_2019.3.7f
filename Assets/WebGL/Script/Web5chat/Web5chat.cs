using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.UI;
using System;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Web5chat : MonoBehaviour
{
    public InputField if_message;
    public Text t_exampel;
    // Start is called before the first frame update
    void Start()
    {
        //t_exampel.text = "Заявка № " + Web5.exampel;
        //t_exampel.text = "Заявка № " 
        Debug.Log(Web5.Web5idorder);
    }

    public void ClickSend(){StartCoroutine(GetServerDate());}
    public void ClickExit(){SceneManager.LoadScene("Web5");}
    public void ClickCloseOrder(){StartCoroutine(EditStatusOrder(Web5.Web5idorder,"Закрыта"));
    //StartCoroutine(CreateMessage(Web5.Web5idorder,"0","Заявка закрыта","Бухгалтер","00.00.0000"));
    }

    IEnumerator CreateMessage(string id_order,string facenumber, string text1, string name, string datetime) {
        WWWForm form = new WWWForm();
        form.AddField("_id_order", id_order);form.AddField("_datetime", datetime);form.AddField("_facenumber", facenumber);
        form.AddField("_text1", text1);form.AddField("_name", name);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreateMessage.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log(" " + www.downloadHandler.text);
        //yield return new WaitForSeconds(0.5f);
        //SceneManager.LoadScene("Web5chat");
        StartCoroutine(EditStatusOrder(Web5.Web5idorder,"В работе"));
        }
        }
    }

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
        StartCoroutine(CreateMessage(Web5.Web5idorder,"0",if_message.text,"Бухгалтер",_timeData));
        }
    }

    // для изменения статуса в заявке
    IEnumerator EditStatusOrder(string id_order,string status) {WWWForm form = new WWWForm();
        form.AddField("id_order", id_order);form.AddField("status", status);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/Order/EditStatusOrder.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{Debug.Log(" " + www.downloadHandler.text);
        //yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Web5chat");
        //Debug.Log(" " + www.downloadHandler.text);
        }
        }
    }
}
