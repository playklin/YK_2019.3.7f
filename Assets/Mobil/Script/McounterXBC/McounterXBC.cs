using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;

public class McounterXBC : MonoBehaviour
{
    public InputField if_countXBC;
    public Text t_countlastXBC, t_date, t_facenumber;
    // Start is called before the first frame update
    void Start()
    {
        t_facenumber.text = "л/сч " + PlayerPrefs.GetString("facenumber");
        StartCoroutine(GetLastXBC(PlayerPrefs.GetString("facenumber")));
        StartCoroutine(GetServerDate());
    }

    public void ClickMcounter(){SceneManager.LoadScene("Mcounter");}
    public void ClickSendcountXBC(){StartCoroutine(EditLastXBC(PlayerPrefs.GetString("facenumber"),if_countXBC.text));}

    IEnumerator GetLastXBC(string facenumber){
        WWWForm form = new WWWForm(); form.AddField("_facenumber", facenumber); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetLastXBC.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_countlastXBC.text = www.downloadHandler.text;
        //PlayerPrefs.SetString("title_order", www.downloadHandler.text);
        //Debug.Log("получили из GetPlataIDs" + www.downloadHandler.text);
        }}//StartCoroutine(GetTitle(If_id_order.text));
    }

    IEnumerator EditLastXBC(string facenumber, string countXBC){
        WWWForm form = new WWWForm(); form.AddField("_facenumber", facenumber);form.AddField("_countXBC", countXBC);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/EditLastXBC.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{//t_countlastXBC.text = www.downloadHandler.text;
        yield return new WaitForSeconds(0.5f);
        //SceneManager.LoadScene("McounterXBC");
        //PlayerPrefs.SetString("title_order", www.downloadHandler.text);
        //Debug.Log("получили из GetPlataIDs" + www.downloadHandler.text);
        }}//StartCoroutine(GetTitle(If_id_order.text));
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
        //PlayerPrefs.SetString("date", words[0]);
        t_date.text = "Показания на " + words[0] + " г.";
        //setting current time
        //t_date.text = words[0];
        //string _currentTime = words[1];
        //StartCoroutine(CreateMessage(PlayerPrefs.GetString("id_order"),PlayerPrefs.GetString("facenumber"),if_text_message.text,PlayerPrefs.GetString("name"),_timeData));
        }
    }

    public GameObject g_seccess;
    public void ClickCloseSeccess(){//g_seccess.SetActive(false);
    SceneManager.LoadScene("McounterXBC");}

    public void sendEMAILcountXBC()
    {
        MailMessage message = new MailMessage();

        message.Subject = "ЖИЛИЩНИК ЛЕФОРТОВО " + "(ХВС)"+ " ул." + PlayerPrefs.GetString("street") + ", " + "д." + PlayerPrefs.GetString("house") + 
                          " кв." + PlayerPrefs.GetString("flat") + ", л/с " + PlayerPrefs.GetString("facenumber");  // Тема письма
        message.Body = "" + "Счётчик № " + PlayerPrefs.GetString("XBC") + "\n" + "\n" + if_countXBC.text + "\n" + "\n" + "Контакты отправителя:\n" + PlayerPrefs.GetString("surname") + " " + PlayerPrefs.GetString("name") + 
        "\n" + "Телефон: " + PlayerPrefs.GetString("phone") + "\n Почта: " + PlayerPrefs.GetString("email");

        //message.From = new MailAddress("app@velgapark.ru");
        message.From = new MailAddress("demo.app.test@yandex.ru");
        message.To.Add("demo.app.test@yandex.ru");
        //message.To.Add(PlayerPrefs.GetString("email"));
        //message.To.Add("playklin@gmail.com));

        message.BodyEncoding = System.Text.Encoding.UTF8;

        SmtpClient client = new SmtpClient();
        client.Host = "smtp.yandex.ru";
        client.Port = 25;
        client.Credentials = new NetworkCredential(message.From.Address, "1416Zxcv");
        client.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
         delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
         { return true; };
        client.Send(message);
        // ------------
        g_seccess.SetActive(true);

    }

}
