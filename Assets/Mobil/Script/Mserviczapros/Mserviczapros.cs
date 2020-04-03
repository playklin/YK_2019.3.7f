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
// myText.color=Color.red
public class Mserviczapros : MonoBehaviour
{
    public Text t_name, t_surname, t_phone, t_email, t_street, t_house, t_flat, t_title, t_text;
    public InputField if_text;
    public GameObject g_seccess;
    // Start is called before the first frame update
    void Start()
    {

    #region KOMPONENTY ---
        t_name = GameObject.Find("T_name").GetComponent<Text>();
        t_surname = GameObject.Find("T_surname").GetComponent<Text>(); 
        t_phone = GameObject.Find("T_phone").GetComponent<Text>(); 
        t_email = GameObject.Find("T_email").GetComponent<Text>(); 
        t_street = GameObject.Find("T_street").GetComponent<Text>(); 
        t_house = GameObject.Find("T_house").GetComponent<Text>(); 
        t_flat = GameObject.Find("T_flat").GetComponent<Text>();

        t_title = GameObject.Find("T_title").GetComponent<Text>();
        t_text = GameObject.Find("T_text").GetComponent<Text>();

        //if_text = GameObject.Find("InputF_text").GetComponent<Text>();

        t_name.text = PlayerPrefs.GetString("name");
        t_surname.text = PlayerPrefs.GetString("surname"); 
        t_phone.text = PlayerPrefs.GetString("phone"); 
        t_email.text = PlayerPrefs.GetString("email"); 
        t_street.text = "ул. " + PlayerPrefs.GetString("street"); 
        t_house.text = "д." + PlayerPrefs.GetString("house"); 
        t_flat.text = "кв." + PlayerPrefs.GetString("flat");
    #endregion

    //PlayerPrefs.SetString("id_servic", "2");
    StartCoroutine(GetTitleServic(PlayerPrefs.GetString("id_servic")));
    StartCoroutine(GetTextServic(PlayerPrefs.GetString("id_servic")));

    }

    public void ClickMdopservic(){SceneManager.LoadScene("Mdopservic");}

    IEnumerator GetTitleServic(string id){
        WWWForm form = new WWWForm(); form.AddField("id", id); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetTitleServic.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_title.text = www.downloadHandler.text;
        PlayerPrefs.SetString("title_servic", www.downloadHandler.text);
        //Debug.Log("получили из GetPlataIDs" + www.downloadHandler.text);
        }}//StartCoroutine(GetTitle(If_id_order.text));
    }

    IEnumerator GetTextServic(string id){
        WWWForm form = new WWWForm(); form.AddField("id", id); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetTextServic.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_text.text = www.downloadHandler.text;
        PlayerPrefs.SetString("text_servic", www.downloadHandler.text);
        //Debug.Log("получили из GetPlataIDs" + www.downloadHandler.text);
        }}//StartCoroutine(GetTitle(If_id_order.text));
    }

    public void sendzakaz()
    {
        MailMessage message = new MailMessage();

        message.Subject = "ЖИЛИЩНИК ЛЕФОРТОВО " + "(заказ услуги)"+ " ул." + PlayerPrefs.GetString("street") + ", " + "д." + PlayerPrefs.GetString("house") + 
                          " кв." + PlayerPrefs.GetString("flat");  // Тема письма
        message.Body = "" + PlayerPrefs.GetString("title_servic") + "\n" + "\n" + if_text.text + "\n" + "\n" + "Контакты отправителя:\n" + PlayerPrefs.GetString("surname") + " " + PlayerPrefs.GetString("name") + 
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
