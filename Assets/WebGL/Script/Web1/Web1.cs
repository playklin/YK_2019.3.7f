using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Web1 : MonoBehaviour
{
    public InputField if_facenumber, if_surname, if_name,if_otch, if_street, if_house, if_flat, if_phone, if_email;
    public GameObject CreatePeople;
    public Text t_create_ok;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClickOpenCreateP(){CreatePeople.SetActive(true);}
    public void ClickCloseCreateP(){CreatePeople.SetActive(false);}
    public void ClickLoad(){SceneManager.LoadScene("Web1");}
    public void ClickCreateP(){StartCoroutine(Check1());}
    
    IEnumerator CreatePeople1(string facenumber, string surname, string name,string otch, string street, string house, string flat, string phone, string email) {
        WWWForm form = new WWWForm();
        form.AddField("_facenumber", facenumber);
        form.AddField("_surname", surname);form.AddField("_name", name);form.AddField("_otch", otch);form.AddField("_email", email);
        form.AddField("_street", street);form.AddField("_house", house);form.AddField("_flat", flat);form.AddField("_phone", phone);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CreatePeople.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{t_create_ok.text = "Житель создан";
        SceneManager.LoadScene("Web1");
        //Debug.Log("yk " + www.downloadHandler.text);
        }
        }
    }

    IEnumerator Check1(){
        if(if_surname.text == "" || if_name.text == ""||if_otch.text == ""||if_facenumber.text == ""||if_street.text == ""||
        if_house.text == "" || if_flat.text == ""||if_phone.text == ""||if_email.text == ""){t_create_ok.text = "Не все поля заполнены";}else{
            StartCoroutine(CreatePeople1(if_facenumber.text, if_surname.text, if_name.text,if_otch.text, if_street.text, if_house.text, if_flat.text, if_phone.text, if_email.text));
        }
        yield return new WaitForSeconds(.0f);
    }

}
