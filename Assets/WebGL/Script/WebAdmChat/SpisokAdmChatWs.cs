using System.Collections;using System.Collections.Generic;
using UnityEngine;using UnityEngine.UI;
using System;using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class SpisokAdmChatWs : MonoBehaviour {

    public RectTransform prefarb;
    public RectTransform content;
    public GameObject g_loading;

    void Start()
    {
        g_loading.SetActive(true);
        StartCoroutine(GetJson(SpisokAdmWs.id_yk_adm, results => OnReceivedModels(results))); 
    }

   #region HASH KOD ----

    public IEnumerator GetJson(string face,System.Action<TestItemModel[]> callback){
        WWWForm form = new WWWForm();form.AddField("id", face);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetALLykAdmChat.php",form)){
        yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
            TestItemModel[] mList = JsonHelper.getJsonArray<TestItemModel>(www.downloadHandler.text);
            //Debug.Log("WWW Success: " + www.downloadHandler.text);
            callback(mList);
            g_loading.SetActive(false);
            }
        }
    }

   #endregion

    void OnReceivedModels (TestItemModel[] models)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var model in models)
        {
            var instance = GameObject.Instantiate(prefarb.gameObject) as GameObject;
            instance.transform.SetParent(content, false);
            InitializeItemView(instance, model);
        }
    }

    [System.Serializable]
    public class TestItemModel // model колонки забираем из таблицы базы данных 
    {
        //public string id;
        //public string idadm;
        //public string facenumber;
        //public string street;
        //public string house;
        public string surname;
        public string name;
        //public string title;
        public string text;
        public string date;
        //public string buttonText;
    }

    public class TestItemView // view Привязываем данные из таблицы к префабу
    {
        //public Text id;
        //public Text idadm;
        //public Text facenumber;
        //public Text street;
        //public Text house;
        public Text surname;
        public Text name;
        //public Text title;
        public Text text;
        public Text date;
        //public Button clickButton;
        //public Text titleText;
        //public Button clickButton;

        public TestItemView (Transform rootView)
        {
            //id = rootView.Find("Id").GetComponent<Text>();
            //idadm = rootView.Find("Idadm").GetComponent<Text>();
            //facenumber = rootView.Find("Facenumber").GetComponent<Text>();
            //street = rootView.Find("Street").GetComponent<Text>();
            //house = rootView.Find("House").GetComponent<Text>();
            surname = rootView.Find("Surname").GetComponent<Text>();
            name = rootView.Find("Name").GetComponent<Text>();
            //title = rootView.Find("Title").GetComponent<Text>();
            text = rootView.Find("Text").GetComponent<Text>();
            date = rootView.Find("Date").GetComponent<Text>();
            //titleText = rootView.Find("TitleText").GetComponent<Text>();
            //clickButton = rootView.Find("ClickButton").GetComponent<Button>();
        }
    }

    void InitializeItemView (GameObject viewGameObject, TestItemModel model)
    {
        TestItemView view = new TestItemView(viewGameObject.transform);
        //view.id.text = model.id;
        //view.idadm.text = model.idadm;
        //view.facenumber.text = model.facenumber;
        //view.street.text = model.street;
        //view.house.text = model.house;
        view.surname.text = model.surname;
        view.name.text = model.name;
        //view.title.text = model.title;
        view.text.text = model.text;
        view.date.text = model.date;
        //view.clickButton.GetComponentInChildren<Text>().text = model.buttonText;
        //view.clickButton.onClick.AddListener(
            //()=> 
            //{
                //Debug.Log(view.titleText.text + " is clicked!");
                //StartCoroutine(DeletPromo(view.id.text));
                //PlayerPrefs.SetString("id_servic", view.id.text);
                //SceneManager.LoadScene("WebPromo");

            //}
        //);
    }

    IEnumerator DeletPromo(string id) {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        //form.AddField("_text", text);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/DeletPromo.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log("" + www.downloadHandler.text);
        //t_news_ok.text = "OK";
        SceneManager.LoadScene("WebPromo");}
        }
    }


  #region JsonHelper

    public class JsonHelper
    {
        public static T[] getJsonArray<T>(string json)
        {
            string newJson = "{ \"array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        public static string arrayToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.array = array;
            return JsonUtility.ToJson(wrapper);
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] array;
        }
    }
  #endregion

}