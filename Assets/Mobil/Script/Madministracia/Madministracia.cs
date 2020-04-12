using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.UI; using System; using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Madministracia : MonoBehaviour
{
    public InputField if_id_order, if_title, if_text;
    public GameObject g_defolt_order;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClickCreateOrder(){StartCoroutine(GetServerDate());}
    public void ClickM3(){SceneManager.LoadScene("M3");}
    public void ClickOpenDefoltOrder(){g_defolt_order.SetActive(true);}
    public void ClickOtmenaCreateOrder(){g_defolt_order.SetActive(false);}

    IEnumerator CreateOrder(string facenumber, string street,string house,string name,string surname,string title, string text1, string datetime) {
        WWWForm form = new WWWForm();
        form.AddField("_facenumber", facenumber);form.AddField("_text1", text1);form.AddField("_name", name);form.AddField("_surname", surname);
        form.AddField("_title", title);form.AddField("_datetime", datetime);form.AddField("_house", house);form.AddField("_street", street);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreateAdm.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//t_order_ok.text = "Заявка отправлена.";
        //Debug.Log("" + www.downloadHandler.text);
        //yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Madministracia");}
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
        //PlayerPrefs.SetString("date", words[0]);
        //_data.text = words[0];
        //setting current time
        //t_date.text = words[0];
        //string _currentTime = words[1];
        StartCoroutine(CreateOrder(PlayerPrefs.GetString("facenumber"),PlayerPrefs.GetString("street"),PlayerPrefs.GetString("house"),PlayerPrefs.GetString("name"),PlayerPrefs.GetString("surname"),if_title.text,if_text.text,_timeData));
        }
    }
}
