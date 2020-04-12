using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.UI; using System; using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Morderchat : MonoBehaviour
{
    public InputField if_text_message;
    public Text t_facenumber, t_street, t_house, t_flat, t_id_order, t_title, t_status;
    public GameObject g_order_no;
    // Start is called before the first frame update
    void Start()
    {
       t_id_order.text = "Заявка № " + PlayerPrefs.GetString("id_order");
       StartCoroutine(GetTitle(PlayerPrefs.GetString("id_order")));
       StartCoroutine(GetStatus(PlayerPrefs.GetString("id_order")));
    }

    public void ClickSend(){StartCoroutine(GetServerDate());}
    public void ClickExit(){SceneManager.LoadScene("Morder");}

 #region не настроино !!! GET INFORMATION ORDER -----------------
    //public Text if_facenumber;
    IEnumerator OpenIdOrder(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetFacenumber.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        t_facenumber.text = "" + www.downloadHandler.text;
        //StartCoroutine(GetStreet(if_facenumber.text));
        //StartCoroutine(GetSurname(if_facenumber.text));
        //StartCoroutine(GetName(if_facenumber.text));
        //StartCoroutine(GetOtch(if_facenumber.text));
        //PlayerPrefs.SetString("order_facenumber", www.downloadHandler.text);
        }}
    }

    IEnumerator GetTitle(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_id_order_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetTitle.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        t_title.text = "Тема: " + www.downloadHandler.text;
        }}
    }

    IEnumerator GetStatus(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_id_order_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetStatus.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        t_status.text = "Статус:  " + www.downloadHandler.text;
        }}
    }

    

 #endregion



    IEnumerator CheckOrder(string idorder, string facenumber) {
        WWWForm form = new WWWForm();
        form.AddField("_idorder", idorder);
        form.AddField("_facenumber", facenumber);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CheckOrder.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//t_quiz_ok.text = "OK";yield return new WaitForSeconds(0.5f);
        //SceneManager.LoadScene("Web6");
        if(www.downloadHandler.text == "orderdone"){g_order_no.SetActive(true);}else{}
        //Debug.Log("" + www.downloadHandler.text);
        }
        }
    }

    IEnumerator CreateMessage(string id_order,string facenumber, string text1, string name, string datetime) {
        WWWForm form = new WWWForm();
        form.AddField("_id_order", id_order);form.AddField("_datetime", datetime);form.AddField("_facenumber", facenumber);
        form.AddField("_text1", text1);form.AddField("_name", name);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreateMessage.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//t_quiz_ok.text = "OK";
        StartCoroutine(EditStatusOrder(Web5.Web5idorder,"Открыта"));
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Morderchat");
        //Debug.Log(" " + www.downloadHandler.text);
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
        StartCoroutine(CreateMessage(PlayerPrefs.GetString("id_order"),PlayerPrefs.GetString("facenumber"),if_text_message.text,PlayerPrefs.GetString("name"),_timeData));
        }
    }

    // для изменения статуса в заявке
    IEnumerator EditStatusOrder(string id_order,string status) {WWWForm form = new WWWForm();
        form.AddField("id_order", id_order);form.AddField("status", status);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/Order/EditStatusOrder.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log(" " + www.downloadHandler.text);
        //yield return new WaitForSeconds(0.5f);
        //SceneManager.LoadScene("Web5chat");
        //StartCoroutine(PushNot(yk_playerid,if_message.text,"Диспетчер"));
        //Debug.Log(" " + www.downloadHandler.text);
        }}
    }
}
