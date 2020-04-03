using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.UI; using System; using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;
using System.Linq;

public class Mquizchois : MonoBehaviour
{
    public Text t_quiz_ok, t_toggle1,t_toggle2,t_toggle3,t_toggle4, t_title, t_text, t_date;
    public GameObject g_b_send, g_quiz_done;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckQuiz(PlayerPrefs.GetString("id_quiz"),PlayerPrefs.GetString("facenumber")));
        //StartCoroutine(GetChoisQuizA1(PlayerPrefs.GetString("id_quiz"),PlayerPrefs.GetString("facenumber")));
        StartCoroutine(OpenQuizTitle(PlayerPrefs.GetString("id_quiz")));
    }

    public void ClickMquiz(){SceneManager.LoadScene("Mquiz");}

  #region TOGGEL -----------

    ToggleGroup toggleGroupInstance;

    public Toggle currentSelection{
        get { return toggleGroupInstance.ActiveToggles ().FirstOrDefault ();}
    }
    // НЕ РАБОТАЕТ для изменения выбора из скрипта 
    public void SelectToggle(int id){
        var toggles = toggleGroupInstance.GetComponentsInChildren<Toggle>();
        toggles [id].isOn = true;}
    ////------------------------------------------

    public void ClickSend(){
        toggleGroupInstance = GetComponent<ToggleGroup>();
        //Debug.Log("First selected " + currentSelection.name);
        if(currentSelection.name == "Toggle1"){StartCoroutine(CreateQuizChois(PlayerPrefs.GetString("id_quiz"),PlayerPrefs.GetString("facenumber"),"1","0","0","0"));}
        if(currentSelection.name == "Toggle2"){StartCoroutine(CreateQuizChois(PlayerPrefs.GetString("id_quiz"),PlayerPrefs.GetString("facenumber"),"0","1","0","0"));}
        if(currentSelection.name == "Toggle3"){StartCoroutine(CreateQuizChois(PlayerPrefs.GetString("id_quiz"),PlayerPrefs.GetString("facenumber"),"0","0","1","0"));}
        if(currentSelection.name == "Toggle4"){StartCoroutine(CreateQuizChois(PlayerPrefs.GetString("id_quiz"),PlayerPrefs.GetString("facenumber"),"0","0","0","1"));}
    }


 #endregion

  #region SEND QUIZ CHOIS -- CHECK QUIZ --

    IEnumerator CreateQuizChois(string idquiz, string facenumber, string a1, string b2, string c3, string d4) {
        WWWForm form = new WWWForm();
        form.AddField("_idquiz", idquiz);
        form.AddField("_facenumber", facenumber);
        //form.AddField("_title_", title);form.AddField("_text_", text1);
        form.AddField("_a1", a1);form.AddField("_b2", b2);form.AddField("_c3", c3);form.AddField("_d4", d4);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreateQuizChois.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{g_quiz_done.SetActive(true);g_b_send.SetActive(false);
        //t_quiz_ok.text = "OK";yield return new WaitForSeconds(0.5f);//SceneManager.LoadScene("Web6");
        //Debug.Log("quizchois " + www.downloadHandler.text);
        }
        }
    }

    IEnumerator CheckQuiz(string idquiz, string facenumber) {
        WWWForm form = new WWWForm();
        form.AddField("_idquiz", idquiz);
        form.AddField("_facenumber", facenumber);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CheckQuiz.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//t_quiz_ok.text = "OK";yield return new WaitForSeconds(0.5f);
        //SceneManager.LoadScene("Web6");
        if(www.downloadHandler.text == "quizdone"){g_quiz_done.SetActive(true);}else{g_b_send.SetActive(true);}
        //Debug.Log("" + www.downloadHandler.text);
        }
        }
    }
    // НЕ ИСПОЛЬЗУЕМ
    IEnumerator GetChoisQuizA1(string idquiz, string facenumber) {
        WWWForm form = new WWWForm();
        form.AddField("_idquiz", idquiz);
        form.AddField("_facenumber", facenumber);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetChoisQuizA1.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log("" + www.downloadHandler.text);
            if(www.downloadHandler.text == "1"){
                //SelectToggle(2);
            }
        }
        }
    }

  #endregion

  #region   OPEN QUIZ --

    IEnumerator OpenQuizTitle(string id_quiz){ 
        WWWForm form = new WWWForm(); form.AddField("_id_quiz", id_quiz); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetTitlequiz.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_title.text = "Тема: " + www.downloadHandler.text;
        StartCoroutine(GetTextquiz(PlayerPrefs.GetString("id_quiz")));
        StartCoroutine(GetDatequiz(PlayerPrefs.GetString("id_quiz")));
        StartCoroutine(GetTog1quiz(PlayerPrefs.GetString("id_quiz")));
        StartCoroutine(GetTog2quiz(PlayerPrefs.GetString("id_quiz")));
        StartCoroutine(GetTog3quiz(PlayerPrefs.GetString("id_quiz")));
        StartCoroutine(GetTog4quiz(PlayerPrefs.GetString("id_quiz")));
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
        else{t_toggle1.text = "  " + www.downloadHandler.text;
        //PlayerPrefs.SetString("title", www.downloadHandler.text);
        //Debug.Log("title " + www.downloadHandler.text);
        }}
    }

    IEnumerator GetTog2quiz(string id_quiz){ 
        WWWForm form = new WWWForm(); form.AddField("_id_quiz", id_quiz); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetTog2quiz.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_toggle2.text = "  " + www.downloadHandler.text;
        //PlayerPrefs.SetString("title", www.downloadHandler.text);
        //Debug.Log("title " + www.downloadHandler.text);
        }}
    }

    IEnumerator GetTog3quiz(string id_quiz){ 
        WWWForm form = new WWWForm(); form.AddField("_id_quiz", id_quiz); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetTog3quiz.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_toggle3.text = "  " + www.downloadHandler.text;
        //PlayerPrefs.SetString("title", www.downloadHandler.text);
        //Debug.Log("title " + www.downloadHandler.text);
        }}
    }

    IEnumerator GetTog4quiz(string id_quiz){ 
        WWWForm form = new WWWForm(); form.AddField("_id_quiz", id_quiz); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetTog4quiz.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_toggle4.text = "  " + www.downloadHandler.text;
        //PlayerPrefs.SetString("title", www.downloadHandler.text);
        //Debug.Log("title " + www.downloadHandler.text);
        }}
    }

  #endregion

}
