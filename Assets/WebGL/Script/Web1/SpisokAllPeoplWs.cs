using System.Collections;using System.Collections.Generic;
using UnityEngine;using UnityEngine.UI;
using System;using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class SpisokAllPeoplWs : MonoBehaviour {

    public RectTransform prefarb;
    public RectTransform content;
    public static string yk_id = "";
    public static string yk_facenumber = "";

    void Start()
    {
        StartCoroutine(GetJson(PlayerPrefs.GetString("facenumber"), results => OnReceivedModels(results))); 
    }

    public void ClickExit(){SceneManager.LoadScene("Web");}

   #region HASH KOD ----

    public IEnumerator GetJson(string face,System.Action<TestItemModel[]> callback){
        WWWForm form = new WWWForm();form.AddField("id", face);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetALLpeopleYK.php",form)){
        yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
            TestItemModel[] mList = JsonHelper.getJsonArray<TestItemModel>(www.downloadHandler.text);
            //Debug.Log("WWW Success: " + www.downloadHandler.text);
            callback(mList);
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
        public string id;
        public string street;
        public string house;
        public string flat;
        public string phone;
        public string email;
        public string facenumber;
        public string kod;
        public string surname;
        public string name;
        public string otch;
        public string nachisl;
        public string debd;
        public string mesdebd;
        public string xbc;
        public string datexbc;
        public string gbc;
        public string dategbc;
        public string lastxbc;
        public string lastgbc;
        //public string buttonText;
    }

    public class TestItemView // view Привязываем данные из таблицы к префабу
    {
        public Text id;
        public Text street;
        public Text house;
        public Text flat;
        public Text phone;
        public Text email;
        public Text facenumber;
        public Text kod;
        public Text surname;
        public Text name;
        public Text otch;
        public Text nachisl;
        public Text debd;
        public Text mesdebd;
        public Text xbc;
        public Text datexbc;
        public Text gbc;
        public Text dategbc;
        public Text lastxbc;
        public Text lastgbc;
        public Button clickButton;
        //public Text titleText;
        //public Button clickButton;

        public TestItemView (Transform rootView)
        {
            id = rootView.Find("id").GetComponent<Text>();
            street = rootView.Find("street").GetComponent<Text>();
            house = rootView.Find("house").GetComponent<Text>();
            flat = rootView.Find("flat").GetComponent<Text>();
            phone = rootView.Find("phone").GetComponent<Text>();
            email = rootView.Find("email").GetComponent<Text>();
            facenumber = rootView.Find("facenumber").GetComponent<Text>();
            kod = rootView.Find("kod").GetComponent<Text>();
            surname = rootView.Find("surname").GetComponent<Text>();
            name = rootView.Find("name").GetComponent<Text>();
            otch = rootView.Find("otch").GetComponent<Text>();
            nachisl = rootView.Find("nachisl").GetComponent<Text>();
            debd = rootView.Find("debd").GetComponent<Text>();
            mesdebd = rootView.Find("mesdebd").GetComponent<Text>();
            xbc = rootView.Find("xbc").GetComponent<Text>();
            datexbc = rootView.Find("datexbc").GetComponent<Text>();
            gbc = rootView.Find("gbc").GetComponent<Text>();
            dategbc = rootView.Find("dategbc").GetComponent<Text>();
            lastxbc = rootView.Find("lastxbc").GetComponent<Text>();
            lastgbc = rootView.Find("lastgbc").GetComponent<Text>();
            //titleText = rootView.Find("TitleText").GetComponent<Text>();
            clickButton = rootView.Find("ClickButton").GetComponent<Button>();
        }
    }

    void InitializeItemView (GameObject viewGameObject, TestItemModel model)
    {
        TestItemView view = new TestItemView(viewGameObject.transform);
        view.id.text = model.id;
        view.street.text = model.street;
        view.house.text = model.house;
        view.flat.text = model.flat;
        view.phone.text = model.phone;
        view.email.text = model.email;
        view.facenumber.text = model.facenumber;
        view.kod.text = model.kod;
        view.surname.text = model.surname;
        view.name.text = model.name;
        view.otch.text = model.otch;
        view.nachisl.text = model.nachisl;
        view.debd.text = model.debd;
        view.mesdebd.text = model.mesdebd;
        view.xbc.text = model.xbc;
        view.datexbc.text = model.datexbc;
        view.gbc.text = model.gbc;
        view.dategbc.text = model.dategbc;
        view.lastxbc.text = model.lastxbc;
        view.lastgbc.text = model.lastgbc;
        //view.clickButton.GetComponentInChildren<Text>().text = model.buttonText;
        view.clickButton.onClick.AddListener(
            ()=> 
            {
                //Debug.Log(view.titleText.text + " is clicked!");
                //StartCoroutine(DeletPromo(view.id.text));
                //PlayerPrefs.SetString("id_servic", view.id.text);
                yk_facenumber = view.facenumber.text;
                yk_id = view.id.text;
                //yk_id = model.id;
                SceneManager.LoadScene("Web1.1");

            }
        );
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