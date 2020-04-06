using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.UI; using System; using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class M2 : MonoBehaviour
{
    public InputField if_phone,if_email;
    public Text t_surname, t_name, t_otch, t_facenumber, t_street, t_house, t_flat, t_nachisl, t_add;
    public GameObject g_save_ok, g_inet_no;
    public string url = "";
    
    void Start()
    {
        t_surname.text = PlayerPrefs.GetString("surname");
        t_name.text = PlayerPrefs.GetString("name");
        t_otch.text = PlayerPrefs.GetString("otch");

        t_facenumber.text = "№ " + PlayerPrefs.GetString("facenumber");
        t_street.text = PlayerPrefs.GetString("street") + " ,";
        t_house.text = "д." + PlayerPrefs.GetString("house");
        t_flat.text = "кв." + PlayerPrefs.GetString("flat");
        t_nachisl.text = PlayerPrefs.GetString("nachisl") + " руб.";

        if_phone.text = PlayerPrefs.GetString("phone");
        if_email.text = PlayerPrefs.GetString("email");

        StartCoroutine(GetAdd(1));StartCoroutine(GetUrl(1));
        StartCoroutine(GetPIC("1"));
    }

    public void Openurl(){ Application.OpenURL(PlayerPrefs.GetString("url"));}
    public void Openurlpay(){Application.OpenURL("https://www.gosuslugi.ru/10373/1");}

    public void ClickM1(){SceneManager.LoadScene("M1");}
    public void ClickM3(){SceneManager.LoadScene("M3");}
    public void ClickOk(){g_save_ok.SetActive(false);}
    public void ClickOkinet(){g_inet_no.SetActive(false);}
    public void ClickSave(){StartCoroutine(EditPhoneEmail(PlayerPrefs.GetString("facenumber"),if_phone.text,if_email.text));
    PlayerPrefs.SetString("phone", if_phone.text);PlayerPrefs.SetString("email", if_email.text);}

    IEnumerator EditPhoneEmail(string facenumber,string phone, string email){
        WWWForm form = new WWWForm(); form.AddField("_facenumber", facenumber);
        form.AddField("_phone", phone);form.AddField("_email", email);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/EditPhoneEmail.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); g_inet_no.SetActive(true);}
        else{g_save_ok.SetActive(true);
        //t_title_order.text = www.downloadHandler.text;
        //PlayerPrefs.SetString("phone", www.downloadHandler.text);
        //Debug.Log("" + www.downloadHandler.text);
        }}
    }

    IEnumerator GetAdd(int id_add){ 
        WWWForm form = new WWWForm(); form.AddField("_id_add_", id_add); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetAdd.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_add.text = www.downloadHandler.text;
        PlayerPrefs.SetString("add", www.downloadHandler.text);
        //Debug.Log("получили из GetPlataIDs" + www.downloadHandler.text);
        }}
    }

    IEnumerator GetUrl(int loginadd){ 
        WWWForm form = new WWWForm(); form.AddField("_loginadd_", loginadd); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetUrl.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{//t_url.text = www.downloadHandler.text;
        PlayerPrefs.SetString("url", www.downloadHandler.text);
        //Debug.Log("url" + www.downloadHandler.text);
        }}
    }

    public RawImage ttyy;
    public GameObject butImage;
    //public Renderer ttyy;
    private String imageString = "";
   
// выводим картинку из БД
    IEnumerator GetPIC(string picphp){ WWWForm form = new WWWForm();
        form.AddField("pic", picphp); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetIMG.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
         else{ //Debug.Log(www.downloadHandler.text);
             imageString = (www.downloadHandler.text);
             if(imageString.Length > 100){
                 byte[] Bytes = System.Convert.FromBase64String (imageString);
                 Texture2D texture = new Texture2D(1,1);
                 texture.LoadImage (Bytes);
                 //GUI.DrawTexture(new Rect(200,20,440,440), texture, ScaleMode.ScaleToFit, true, 1f);
                 //ttyy.material.mainTexture = texture;
                 ttyy.texture = texture;
                 //butImage.SetActive(true);
             }else{
                 //butImage.SetActive(false);
             }
            }
        } // correct
    }

}
