using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Web11 : MonoBehaviour
{
    public InputField if_facenumber, if_surname, if_name,if_otch, if_street, if_house, if_flat, if_phone, if_email;
    public GameObject CreatePeople;
    public Text t_create_ok;
    // Start is called before the first frame update
    void Start()
    {
        if_facenumber.text = SpisokAllPeoplWs.yk_facenumber;
        StartCoroutine(GetHouse(SpisokAllPeoplWs.yk_facenumber));
        StartCoroutine(GetFlat(SpisokAllPeoplWs.yk_facenumber));
        StartCoroutine(GetStreet(SpisokAllPeoplWs.yk_facenumber));
        StartCoroutine(GetSurname(SpisokAllPeoplWs.yk_facenumber));
        StartCoroutine(GetName(SpisokAllPeoplWs.yk_facenumber));
        StartCoroutine(GetOtch(SpisokAllPeoplWs.yk_facenumber));
        StartCoroutine(GetPhone(SpisokAllPeoplWs.yk_facenumber));
        StartCoroutine(GetEmail(SpisokAllPeoplWs.yk_facenumber));

        //Debug.Log(SpisokAllPeoplWs.yk_facenumber);
        //Debug.Log(SpisokAllPeoplWs.yk_id);
    }

    //public void ClickOpenCreateP(){CreatePeople.SetActive(true);}
    public void ClickExit(){SceneManager.LoadScene("Web1");}
    //public void ClickLoad(){SceneManager.LoadScene("Web1");}
    public void ClickEditP(){StartCoroutine(EditPeople(SpisokAllPeoplWs.yk_id,if_facenumber.text, if_surname.text, if_name.text,if_otch.text, if_street.text, if_house.text, if_flat.text, if_phone.text, if_email.text));}
    
    IEnumerator EditPeople(string id,string facenumber, string surname, string name,string otch, string street, string house, string flat, string phone, string email) {
        WWWForm form = new WWWForm();
        form.AddField("_facenumber", facenumber);form.AddField("_id", id);
        form.AddField("_surname", surname);form.AddField("_name", name);form.AddField("_otch", otch);form.AddField("_email", email);
        form.AddField("_street", street);form.AddField("_house", house);form.AddField("_flat", flat);form.AddField("_phone", phone);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/EditPeople.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{t_create_ok.text = "Данные сохранены";
        //SceneManager.LoadScene("Web1");
        //Debug.Log("" + www.downloadHandler.text);
        }
        }
    }

    IEnumerator Check1(){
        if(if_surname.text == "" || if_name.text == ""||if_otch.text == ""||if_facenumber.text == ""||if_street.text == ""||
        if_house.text == "" || if_flat.text == ""||if_phone.text == ""||if_email.text == ""){t_create_ok.text = "Не все поля заполнены";}else{
            //StartCoroutine(CreatePeople1(if_facenumber.text, if_surname.text, if_name.text,if_otch.text, if_street.text, if_house.text, if_flat.text, if_phone.text, if_email.text));
        }
        yield return new WaitForSeconds(.0f);
    }



    //-----------------------------Получаем все данные--------------------------------------------------

    IEnumerator GetStreet(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetStreet.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        if_street.text = "" + www.downloadHandler.text;
        //StartCoroutine(GetHouse(if_facenumber.text));
        //PlayerPrefs.SetString("street", www.downloadHandler.text);
        }}
    }

    IEnumerator GetHouse(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetHouse.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        if_house.text = "" + www.downloadHandler.text;
        //StartCoroutine(GetFlat(if_facenumber.text));
        //PlayerPrefs.SetString("house", www.downloadHandler.text);
        }}
    }

    IEnumerator GetFlat(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetFlat.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        if_flat.text = "" + www.downloadHandler.text;
        }}
    }

        //--------------FIO---------------------
    IEnumerator GetSurname(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetSurname.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        if_surname.text = "" + www.downloadHandler.text;
        //StartCoroutine(GetHouse(if_facenumber.text));
        //PlayerPrefs.SetString("surname", www.downloadHandler.text);
        }}
    }

    IEnumerator GetName(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetName.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        if_name.text = "" + www.downloadHandler.text;
        //StartCoroutine(GetHouse(if_facenumber.text));
        //PlayerPrefs.SetString("name", www.downloadHandler.text);
        }}
    }

    IEnumerator GetOtch(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetOtch.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        if_otch.text = "" + www.downloadHandler.text;
        //StartCoroutine(GetHouse(if_facenumber.text));
        //PlayerPrefs.SetString("otch", www.downloadHandler.text);
        }}
    }
    //--------------Phone Email-------------------------
    IEnumerator GetPhone(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetPhone.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        if_phone.text = "" + www.downloadHandler.text;
        //StartCoroutine(GetHouse(if_facenumber.text));
        //PlayerPrefs.SetString("phone", www.downloadHandler.text);
        }}
    }

    IEnumerator GetEmail(string id_facen){ WWWForm form = new WWWForm(); form.AddField("_facenumber_", id_facen);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetEmail.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
        //Debug.Log("title " + www.downloadHandler.text);
        if_email.text = "" + www.downloadHandler.text;
        //StartCoroutine(GetHouse(if_facenumber.text));
        //PlayerPrefs.SetString("email", www.downloadHandler.text);
        }}
    }

}
