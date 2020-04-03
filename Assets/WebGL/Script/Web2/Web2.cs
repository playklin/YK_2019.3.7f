using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Web2 : MonoBehaviour
{
    public Text t_facenumber, t_surname, t_nachisl_ok, t_debd_ok;
    public InputField If_facenumber, If_nachisl, If_debd;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ClickExit(){SceneManager.LoadScene("Web");}
    public void ClickSearch(){StartCoroutine(GetFacenumber(If_facenumber.text));
    StartCoroutine(GetSurname(If_facenumber.text));
    StartCoroutine(GetNachisl(If_facenumber.text));
    StartCoroutine(GetDebd(If_facenumber.text));}
    public void ClickEditnachisl(){StartCoroutine(EditNachisl(If_facenumber.text,If_nachisl.text));}
    public void ClickEditdebd(){StartCoroutine(EditDebd(If_facenumber.text,If_debd.text));}

    IEnumerator GetFacenumber(string facenumber){ 
        WWWForm form = new WWWForm(); form.AddField("_facenumber_", facenumber); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetFacenumber.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_facenumber.text = www.downloadHandler.text;
        //Debug.Log("получили из GetPlataIDs" + www.downloadHandler.text);
        }}
    }

    IEnumerator GetSurname(string facenumber){ 
        WWWForm form = new WWWForm(); form.AddField("_facenumber_", facenumber); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetSurname.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_surname.text = www.downloadHandler.text;
        //Debug.Log("получили из GetPlataIDs" + www.downloadHandler.text);
        }}
    }

    IEnumerator GetNachisl(string facenumber){ 
        WWWForm form = new WWWForm(); form.AddField("_facenumber_", facenumber); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetNachisl.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{If_nachisl.text = www.downloadHandler.text;
        //Debug.Log("получили из GetPlataIDs" + www.downloadHandler.text);
        }}
    }

    IEnumerator GetDebd(string facenumber){ 
        WWWForm form = new WWWForm(); form.AddField("_facenumber_", facenumber); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetDebd.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{If_debd.text = www.downloadHandler.text;
        //Debug.Log("получили из GetPlataIDs" + www.downloadHandler.text);
        }}
    }

    IEnumerator EditNachisl(string username, string new1) {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("new1", new1);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/EditNachisl.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{t_nachisl_ok.text = "OK";}
        }
    }

    IEnumerator EditDebd(string username, string new1) {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("new1", new1);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/EditDebd.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{t_debd_ok.text = "OK";}
        }
    }

    
}
