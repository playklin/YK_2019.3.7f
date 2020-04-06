using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.UI;
using System;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;
// для почты
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;

public class WebAdmChat : MonoBehaviour
{
    public InputField if_message;
    public Text t_id_yk_adm;
    // Сообщения почтой
    public static string yk_email = "";
    public static string yk_facenumber = "defROMA";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetEmail(yk_facenumber));
        t_id_yk_adm.text = "Обращение № " + SpisokAdmWs.id_yk_adm;
        //Debug.Log(SpisokAdmWs.id_yk_adm);
    }

    public void ClickSend(){StartCoroutine(GetServerDate());}
    public void ClickExit(){SceneManager.LoadScene("WebAdm");}

    IEnumerator CreateMessage(string id_order,string facenumber, string text1, string surname,string name, string datetime) {
        WWWForm form = new WWWForm();
        form.AddField("_id_order", id_order);form.AddField("_datetime", datetime);form.AddField("_facenumber", facenumber);
        form.AddField("_text1", text1);form.AddField("_name", name);form.AddField("_surname", surname);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreateMessageAdmChat.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log(" " + www.downloadHandler.text);
        //yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("WebAdmChat");
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
        StartCoroutine(CreateMessage(SpisokAdmWs.id_yk_adm,"0",if_message.text," ","Администрация",_timeData));
        }
    }

    public void sendEMAILchat()
    {
        MailMessage message = new MailMessage();

        message.Subject = "ЖИЛИЩНИК ЛЕФОРТОВО " + "(Администрация)";  // Тема письма
        message.Body = "" + "Администрация ответила на ваше сообщение " + "\n" + "\n" + "Зайдите в приложение" + "\n" + "\n" + "";

        message.From = new MailAddress("demo.app.test@yandex.ru");
        message.To.Add("demo.app.test@yandex.ru");
        message.To.Add(yk_email);
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
        //g_seccess.SetActive(true);

    }

    // получения почты
    IEnumerator GetEmail(string facenumber) {WWWForm form = new WWWForm();
        form.AddField("_facenumber_", facenumber);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetEmail.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log(" " + www.downloadHandler.text);
        yk_email = www.downloadHandler.text;
        //yield return new WaitForSeconds(0.5f);
        //SceneManager.LoadScene("Web5chat");
        //Debug.Log(" " + www.downloadHandler.text);
        }}
    }
}
