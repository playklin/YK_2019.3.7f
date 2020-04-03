using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.UI; using System; using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Mquiz : MonoBehaviour
{
    public InputField if_id_quiz;
    public GameObject g_quiz_no;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClickM3(){SceneManager.LoadScene("M3");}
    public void ClickMquiz(){SceneManager.LoadScene("Mquiz");}
    public void ClickOpenQuiz(){PlayerPrefs.SetString("id_quiz", if_id_quiz.text);
    if(if_id_quiz.text == "" || if_id_quiz.text == "0"){}else{
    StartCoroutine(CheckQuizTRUE(PlayerPrefs.GetString("id_quiz"),PlayerPrefs.GetString("facenumber")));
    //SceneManager.LoadScene("Mquizchois");
    }}

    IEnumerator CheckQuizTRUE(string idquiz, string facenumber) {
        WWWForm form = new WWWForm();
        form.AddField("_idquiz", idquiz);
        form.AddField("_facenumber", facenumber);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CheckQuizTRUE.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log("" + www.downloadHandler.text);
        //SceneManager.LoadScene("Web6");
        if(www.downloadHandler.text == "no"){g_quiz_no.SetActive(true);}else{SceneManager.LoadScene("Mquizchois");}
        }
        }
    }
}
