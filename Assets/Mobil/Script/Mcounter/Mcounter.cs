using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.UI; using System; using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Mcounter : MonoBehaviour
{
    public Text t_dateXBC, t_dateGBC, t_XBC, t_GBC, t_playerid;
    // Start is called before the first frame update
    void Start()
    {
        t_dateXBC.text = "Дата поверки: " + PlayerPrefs.GetString("dateXBC");
        t_dateGBC.text = "Дата поверки: " + PlayerPrefs.GetString("dateGBC");
        StartCoroutine(GetDateXBC(PlayerPrefs.GetString("facenumber")));
        StartCoroutine(GetDateGBC(PlayerPrefs.GetString("facenumber")));
        t_XBC.text = "Счётчик № " + PlayerPrefs.GetString("XBC");
        t_GBC.text = "Счётчик № " + PlayerPrefs.GetString("GBC");
        StartCoroutine(GetXBC(PlayerPrefs.GetString("facenumber")));
        StartCoroutine(GetGBC(PlayerPrefs.GetString("facenumber")));

        t_playerid.text = mainOnS.playerid;
    }

    public void ClickMcounterGBC(){SceneManager.LoadScene("McounterGBC");}
    public void ClickMcounterXBC(){SceneManager.LoadScene("McounterXBC");}
    public void ClickM3(){SceneManager.LoadScene("M3");}

    IEnumerator GetDateXBC(string id_facen){ WWWForm form = new WWWForm(); form.AddField("id", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetDateXBC.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        PlayerPrefs.SetString("dateXBC", www.downloadHandler.text);
        t_dateXBC.text = "Дата поверки: " + www.downloadHandler.text;
        }}
    }

    IEnumerator GetDateGBC(string id_facen){ WWWForm form = new WWWForm(); form.AddField("id", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetDateGBC.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        PlayerPrefs.SetString("dateGBC", www.downloadHandler.text);
        t_dateGBC.text = "Дата поверки: " + www.downloadHandler.text;
        }}
    }

    IEnumerator GetXBC(string id_facen){ WWWForm form = new WWWForm(); form.AddField("id", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetXBC.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        PlayerPrefs.SetString("XBC", www.downloadHandler.text);
        t_XBC.text = "Счётчик № " + www.downloadHandler.text;
        }}
    }

    IEnumerator GetGBC(string id_facen){ WWWForm form = new WWWForm(); form.AddField("id", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetGBC.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        PlayerPrefs.SetString("GBC", www.downloadHandler.text);
        t_GBC.text = "Счётчик № " + www.downloadHandler.text;
        }}
    }
}
