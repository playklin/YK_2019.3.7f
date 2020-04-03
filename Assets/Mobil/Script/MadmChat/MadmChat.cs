using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.UI; using System; using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class MadmChat : MonoBehaviour
{
    public InputField if_text_message;
    public Text t_id_adm;
    // Start is called before the first frame update
    void Start()
    {
        t_id_adm.text = "Обращение № " + PlayerPrefs.GetString("id_adm");
    }

    public void ClickSend(){StartCoroutine(GetServerDate());}
    public void ClickExit(){SceneManager.LoadScene("Madministracia");}

    IEnumerator CreateMessage(string id_order,string facenumber, string text1,string surname, string name, string datetime) {
        WWWForm form = new WWWForm();
        form.AddField("_id_order", id_order);form.AddField("_datetime", datetime);form.AddField("_facenumber", facenumber);
        form.AddField("_text1", text1);form.AddField("_name", name);form.AddField("_surname", surname);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreateMessageAdmChat.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//t_quiz_ok.text = "OK";
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MadmChat");
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
        StartCoroutine(CreateMessage(PlayerPrefs.GetString("id_adm"),PlayerPrefs.GetString("facenumber"),if_text_message.text,PlayerPrefs.GetString("surname"),PlayerPrefs.GetString("name"),_timeData));
        }
    }
}
