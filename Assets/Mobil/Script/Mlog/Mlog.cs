using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.UI; using System; using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Mlog : MonoBehaviour
{
    public InputField if_facenumber, if_surname, if_name;
    public Text t_street, t_house, t_flat, t_facenumber, t_info;
    public GameObject g_adress, g_loading, g_button;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetString("facenumber", "0");
        if(PlayerPrefs.GetString("facenumber") == "" ||PlayerPrefs.GetString("facenumber") == "0"){}else{SceneManager.LoadScene("M2");}
    }

    public void ClickEnter(){StartCoroutine(YKLogin(if_facenumber.text, if_surname.text, if_name.text));}
    public void ClickOtmena(){g_adress.SetActive(false);g_loading.SetActive(false);}
    public void ClickGO(){SceneManager.LoadScene("M1");}

    public void Toggle1(bool newValue){g_button.SetActive(newValue);}

    IEnumerator YKLogin(string facenumber, string surname, string name) {
        WWWForm form = new WWWForm();
        form.AddField("_facenumber", facenumber);
        form.AddField("_surname", surname);
        form.AddField("_name", name);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/YKLogin.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//t_info.text = "" + www.downloadHandler.text;//yield return new WaitForSeconds(0.5f);
        if(www.downloadHandler.text == "ok"){g_loading.SetActive(true);StartCoroutine(OpenPeople(if_facenumber.text));}else{t_info.text = "" + www.downloadHandler.text;}
        //SceneManager.LoadScene("M1");
        //Debug.Log("log " + www.downloadHandler.text);
        }
        }
    }

    IEnumerator OpenPeople(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetFacenumber.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        t_facenumber.text = "" + www.downloadHandler.text;
        StartCoroutine(GetStreet(if_facenumber.text));
        StartCoroutine(GetSurname(if_facenumber.text));
        StartCoroutine(GetName(if_facenumber.text));
        StartCoroutine(GetOtch(if_facenumber.text));
        StartCoroutine(GetPhone(if_facenumber.text));
        StartCoroutine(GetEmail(if_facenumber.text));
        PlayerPrefs.SetString("facenumber", www.downloadHandler.text);
        }}
    }

    IEnumerator GetStreet(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetStreet.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        t_street.text = "" + www.downloadHandler.text;
        StartCoroutine(GetHouse(if_facenumber.text));
        PlayerPrefs.SetString("street", www.downloadHandler.text);
        }}
    }

    IEnumerator GetHouse(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetHouse.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        t_house.text = "" + www.downloadHandler.text;
        StartCoroutine(GetFlat(if_facenumber.text));
        PlayerPrefs.SetString("house", www.downloadHandler.text);
        }}
    }

    IEnumerator GetFlat(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetFlat.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        t_flat.text = "" + www.downloadHandler.text;
        PlayerPrefs.SetString("flat", www.downloadHandler.text);
        t_facenumber.text = "(" + PlayerPrefs.GetString("facenumber") + ")";
        t_street.text = PlayerPrefs.GetString("street") + " ул.,";
        t_house.text = "д." + PlayerPrefs.GetString("house");
        t_flat.text = "кв." + PlayerPrefs.GetString("flat");

        g_adress.SetActive(true);
        }}
    }
    //--------------FIO---------------------
    IEnumerator GetSurname(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetSurname.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        //t_street.text = "" + www.downloadHandler.text;
        //StartCoroutine(GetHouse(if_facenumber.text));
        PlayerPrefs.SetString("surname", www.downloadHandler.text);
        }}
    }

    IEnumerator GetName(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetName.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        //t_street.text = "" + www.downloadHandler.text;
        //StartCoroutine(GetHouse(if_facenumber.text));
        PlayerPrefs.SetString("name", www.downloadHandler.text);
        }}
    }

    IEnumerator GetOtch(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetOtch.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        //t_street.text = "" + www.downloadHandler.text;
        //StartCoroutine(GetHouse(if_facenumber.text));
        PlayerPrefs.SetString("otch", www.downloadHandler.text);
        }}
    }
    //--------------Phone Email-------------------------
    IEnumerator GetPhone(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetPhone.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        //t_street.text = "" + www.downloadHandler.text;
        //StartCoroutine(GetHouse(if_facenumber.text));
        PlayerPrefs.SetString("phone", www.downloadHandler.text);
        }}
    }

    IEnumerator GetEmail(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetEmail.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        //t_street.text = "" + www.downloadHandler.text;
        //StartCoroutine(GetHouse(if_facenumber.text));
        PlayerPrefs.SetString("email", www.downloadHandler.text);
        }}
    }


}
