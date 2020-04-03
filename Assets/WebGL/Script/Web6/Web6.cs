using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Web6 : MonoBehaviour
{
    public InputField If_title, If_text,If_1,If_2,If_3,If_4, if_search_quiz;
    public Text t_quiz_ok, t_toggle1,t_toggle2,t_toggle3,t_toggle4, t_title, t_text, t_date, t_amaunt, t_1, t_2, t_3, t_4;

    void Start()
    {

    }

    public void ClickExit(){SceneManager.LoadScene("Web");}
    public void ClickCreateQuiz(){StartCoroutine(CreateQuiz(
        If_title.text,If_text.text,If_1.text,If_2.text,If_3.text,If_4.text,PlayerPrefs.GetString("date")));}

    public void ClicOpenQuiz(){StartCoroutine(OpenQuizTitle(if_search_quiz.text));
                               StartCoroutine(GetPEOPLchois(if_search_quiz.text));
                               StartCoroutine(GetChoisA(if_search_quiz.text,"1"));
                               StartCoroutine(GetChoisB(if_search_quiz.text,"1"));
                               StartCoroutine(GetChoisC(if_search_quiz.text,"1"));
                               StartCoroutine(GetChoisD(if_search_quiz.text,"1"));
                               }

    IEnumerator CreateQuiz(string title, string text1, string a1, string b2, string c3, string d4, string date) {
        WWWForm form = new WWWForm();
        form.AddField("_id", 2);
        form.AddField("_date_", date);
        form.AddField("_title_", title);form.AddField("_text_", text1);
        form.AddField("_a1_", a1);form.AddField("_b2_", b2);form.AddField("_c3_", c3);form.AddField("_d4_", d4);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreateQuiz.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{t_quiz_ok.text = "OK";yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Web6");
        //Debug.Log("quiz " + www.downloadHandler.text);
        }
        }
    }

  #region   OPEN QUIZ --

    IEnumerator OpenQuizTitle(string id_quiz){ 
        WWWForm form = new WWWForm(); form.AddField("_id_quiz", id_quiz); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetTitlequiz.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_title.text = "Тема: " + www.downloadHandler.text;
        StartCoroutine(GetTextquiz(if_search_quiz.text));
        StartCoroutine(GetDatequiz(if_search_quiz.text));
        StartCoroutine(GetTog1quiz(if_search_quiz.text));
        StartCoroutine(GetTog2quiz(if_search_quiz.text));
        StartCoroutine(GetTog3quiz(if_search_quiz.text));
        StartCoroutine(GetTog4quiz(if_search_quiz.text));
        //PlayerPrefs.SetString("title", www.downloadHandler.text);
        //Debug.Log("title " + www.downloadHandler.text);
        }}
    }

    IEnumerator GetTextquiz(string id_quiz){ 
        WWWForm form = new WWWForm(); form.AddField("_id_quiz", id_quiz); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetTextquiz.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_text.text = "" + www.downloadHandler.text;
        //PlayerPrefs.SetString("title", www.downloadHandler.text);
        //Debug.Log("title " + www.downloadHandler.text);
        }}
    }

    IEnumerator GetDatequiz(string id_quiz){ 
        WWWForm form = new WWWForm(); form.AddField("_id_quiz", id_quiz); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetDatequiz.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_date.text = "Дата опроса " + www.downloadHandler.text;
        //PlayerPrefs.SetString("title", www.downloadHandler.text);
        //Debug.Log("title " + www.downloadHandler.text);
        }}
    }

    IEnumerator GetTog1quiz(string id_quiz){ 
        WWWForm form = new WWWForm(); form.AddField("_id_quiz", id_quiz); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetTog1quiz.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_toggle1.text = "1.  " + www.downloadHandler.text;
        //PlayerPrefs.SetString("title", www.downloadHandler.text);
        //Debug.Log("title " + www.downloadHandler.text);
        }}
    }

    IEnumerator GetTog2quiz(string id_quiz){ 
        WWWForm form = new WWWForm(); form.AddField("_id_quiz", id_quiz); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetTog2quiz.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_toggle2.text = "2.  " + www.downloadHandler.text;
        //PlayerPrefs.SetString("title", www.downloadHandler.text);
        //Debug.Log("title " + www.downloadHandler.text);
        }}
    }

    IEnumerator GetTog3quiz(string id_quiz){ 
        WWWForm form = new WWWForm(); form.AddField("_id_quiz", id_quiz); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetTog3quiz.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_toggle3.text = "3.  " + www.downloadHandler.text;
        //PlayerPrefs.SetString("title", www.downloadHandler.text);
        //Debug.Log("title " + www.downloadHandler.text);
        }}
    }

    IEnumerator GetTog4quiz(string id_quiz){ 
        WWWForm form = new WWWForm(); form.AddField("_id_quiz", id_quiz); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetTog4quiz.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_toggle4.text = "4.  " + www.downloadHandler.text;
        //PlayerPrefs.SetString("title", www.downloadHandler.text);
        //Debug.Log("title " + www.downloadHandler.text);
        }}
    }

  #endregion

  #region   RESULT QUIZ amaunt ------

    IEnumerator GetPEOPLchois(string id_quiz){ 
        WWWForm form = new WWWForm(); form.AddField("idquiz", id_quiz); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetPEOPLchois.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{//Debug.Log("" + www.downloadHandler.text);
        t_amaunt.text = "Всего проголосовало: " + www.downloadHandler.text;
        //PlayerPrefs.SetString("add", www.downloadHandler.text);
        //Debug.Log("получили из GetPlataIDs" + www.downloadHandler.text);
        }}
    }

    IEnumerator GetChoisA(string id_quiz, string abcd){WWWForm form = new WWWForm(); form.AddField("idquiz", id_quiz); form.AddField("abcd", abcd);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetChoisA1.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_1.text = " " + www.downloadHandler.text;}}//Debug.Log("" + www.downloadHandler.text);
    }

    IEnumerator GetChoisB(string id_quiz, string abcd){WWWForm form = new WWWForm(); form.AddField("idquiz", id_quiz); form.AddField("abcd", abcd);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetChoisB2.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_2.text = " " + www.downloadHandler.text;}}//Debug.Log("" + www.downloadHandler.text);
    }

    IEnumerator GetChoisC(string id_quiz, string abcd){WWWForm form = new WWWForm(); form.AddField("idquiz", id_quiz); form.AddField("abcd", abcd);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetChoisC3.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_3.text = " " + www.downloadHandler.text;}}//Debug.Log("" + www.downloadHandler.text);
    }

    IEnumerator GetChoisD(string id_quiz, string abcd){WWWForm form = new WWWForm(); form.AddField("idquiz", id_quiz); form.AddField("abcd", abcd);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetChoisD4.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_4.text = " " + www.downloadHandler.text;}}//Debug.Log("" + www.downloadHandler.text);
    }

  #endregion

}
