using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class WebPromo : MonoBehaviour
{
    public InputField if_date, if_street,if_house,if_title, if_text, if_num_notif_del;
    public Text t_news_ok;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClickExit(){SceneManager.LoadScene("Web");}
    public void ClickCreate(){StartCoroutine(Create(if_date.text,if_street.text,if_house.text,if_title.text,if_text.text));}
    public void ClickDeletPromo(){StartCoroutine(DeletPromo(if_num_notif_del.text));}

    IEnumerator Create (string date, string street,string house,string title, string text) {
        WWWForm form = new WWWForm();
        form.AddField("date", date);form.AddField("house", house);
        form.AddField("street", street);form.AddField("text", text);form.AddField("title", title);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreatePromo.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{t_news_ok.text = "OK";SceneManager.LoadScene("WebPromo");}
        }
    }

    IEnumerator DeletPromo(string id) {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        //form.AddField("_text", text);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/DeletPromo.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log("" + www.downloadHandler.text);
        //t_news_ok.text = "OK";
        SceneManager.LoadScene("WebPromo");}
        }
    }
}
