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

public class M1 : MonoBehaviour
{
    //public Text t_facenumber, t_street, t_house, t_flat, t_nachisl, t_add;
    public Text t_error_code;
    public InputField if_email, if_code;
    public GameObject g_inet_no, g_enter_code;

    void Start()
    {

    }

    public void ClickUpdateEmail(){
        if(if_code.text == "4239"){ 
        StartCoroutine(EditEmail(PlayerPrefs.GetString("facenumber"),if_email.text));}else{
           t_error_code.text = "Код не верный !";
        }
    }
    public void ClickNOinternet(){g_inet_no.SetActive(false);}
    public void ClickCloseEnterCode(){g_enter_code.SetActive(false);}

    IEnumerator EditEmail(string facenumber,string email){
        WWWForm form = new WWWForm(); form.AddField("_facenumber", facenumber);
        form.AddField("_email", email);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/EditEmail.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); g_inet_no.SetActive(true);}
        else{//Debug.Log("" + www.downloadHandler.text);
        //g_enter_code.SetActive(true);
        StartCoroutine(CreatePlayerID(PlayerPrefs.GetString("facenumber"),mainOnS.playerid));
        //SceneManager.LoadScene("M2");
        }}
    }

    // sending playerid and facenumber to db
    IEnumerator CreatePlayerID(string facenumber,string playerid){WWWForm form = new WWWForm(); 
        form.AddField("facenumber", facenumber);form.AddField("playerid", playerid);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreatePlayeridYKos.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error);// g_inet_no.SetActive(true);
        }else{//Debug.Log("playerid: " + www.downloadHandler.text);
        SceneManager.LoadScene("M2");
        }}
    }


    public void sendEmail()
    {
        MailMessage message = new MailMessage();

        message.Subject = "ЖИЛИЩНИК ЛЕФОРТОВО ";  // Тема письма
        message.Body = "Если вы получили это письмо, значит почта указана верно." + "\n" +"Для завершения регистрации введи код в приложение."+ "\n"+"Ваш код: 4239";

        message.From = new MailAddress("demo.app.test@yandex.ru");
        message.To.Add("demo.app.test@yandex.ru");
        message.To.Add(if_email.text);
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
        PlayerPrefs.SetString("email", if_email.text);
        g_enter_code.SetActive(true);

    }


}
