using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;using UnityEngine.Assertions.Must;
using System;using System.IO;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Web4 : MonoBehaviour
{
    public InputField If_url,If_urlimg, If_text, if_title, if_text_servic, if_id_servic;
    public Text t_add_ok, t_add, t_debugLog; //t_url;
    // Push notification
    //public InputField if_text;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetAdd(1));
        StartCoroutine(GetUrl(1));
        StartCoroutine(GetPIC("1"));
        //StartCoroutine(GetUrlImg(1));

    }

    public void ClickExit(){SceneManager.LoadScene("Web");}
    public void ClickCreateAdd(){StartCoroutine(UpdateAdd(If_url.text,If_text.text,If_urlimg.text));}

    public void Openurl(){ Application.OpenURL(PlayerPrefs.GetString("url"));}
    public void OpenHtmlPHP(){ Application.OpenURL("https://playklin.000webhostapp.com/webyk/indexSend.html");}

    public void ClickDeletServic(){StartCoroutine(DeletServic(if_id_servic.text));}

    public void ClickCreateDopServic(){StartCoroutine(CreateDopServic(if_title.text,if_text_servic.text));}

    IEnumerator CreateDopServic(string date1, string text) {
        WWWForm form = new WWWForm();
        form.AddField("date1", date1);
        form.AddField("_text", text);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreateDopServic.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log("" + www.downloadHandler.text);
        //t_news_ok.text = "OK";
        SceneManager.LoadScene("Web4");}
        }
    }

    IEnumerator DeletServic(string id_servic) {
        WWWForm form = new WWWForm();
        form.AddField("id_servic", id_servic);
        //form.AddField("_text", text);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/DeletServic.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log("" + www.downloadHandler.text);
        //t_news_ok.text = "OK";
        SceneManager.LoadScene("Web4");}
        }
    }

    IEnumerator UpdateAdd(string url1, string text1, string urlimg) {
        WWWForm form = new WWWForm();
        form.AddField("_loginadd_", 1);
        form.AddField("_urladd_", url1);form.AddField("_urlimg_", urlimg);
        form.AddField("_textadd_", text1);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/UpdateAdd.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{t_add_ok.text = "OK";SceneManager.LoadScene("Web4");}
        }
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

    IEnumerator GetUrlImg(int id_add){ 
        WWWForm form = new WWWForm(); form.AddField("_id_add_", id_add); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetUrlimg.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{//Debug.Log("получили из GetPlataIDs" + www.downloadHandler.text);
        PlayerPrefs.SetString("add_img", www.downloadHandler.text);
        //StartCoroutine(StartShow(www.downloadHandler.text));
        }}
    }

    public RawImage ttyy;
    //public GameObject butImage;
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


    // Push notification

    public InputField if_text, if_subtext, if_url;
    public void ClickPush(){StartCoroutine(PushNot("1",if_text.text,if_subtext.text,if_url.text));}

    IEnumerator PushNot(string id, string text, string subtext, string url){WWWForm form = new WWWForm(); 
        form.AddField("id", id);
        form.AddField("text", text); form.AddField("subtext", subtext); form.AddField("url", url);
        //using (UnityWebRequest www = UnityWebRequest.Post("http://p905504y.beget.tech/notification1.php",form))
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/notificationYK.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error);}else{
        //Debug.Log("" + www.downloadHandler.text);
        t_debugLog.text = www.downloadHandler.text;
        }}
    }

   
}
