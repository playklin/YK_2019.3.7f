using System.Collections;using System.Collections.Generic;
using UnityEngine;using UnityEngine.UI;
using System;using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class SpisokWeb5closeWs : MonoBehaviour {

    public RectTransform prefarb;
    public RectTransform content;

    public GameObject ContentOpen, ContentWork, ContentClose;

    void Start()
    {
        if(Web5.status == "Закрытые заявки"){ContentWork.SetActive(false);ContentOpen.SetActive(false);
        StartCoroutine(GetJson(PlayerPrefs.GetString("id_adm"), results => OnReceivedModels(results)));
        }else{}
        //PlayerPrefs.GetString("facenumber")
    }

   #region 

    public IEnumerator GetJson(string face,System.Action<TestItemModel[]> callback){
        WWWForm form = new WWWForm();form.AddField("id", face);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/Order/GetALLCLOSEykorder.php",form)){
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

    void InitializeItemView (GameObject viewGameObject, TestItemModel model)
    {
        TestItemView view = new TestItemView(viewGameObject.transform);
        view.id.text = model.id;
        view.facenumber.text = model.facenumber;
        view.street.text = model.street;
        view.house.text = model.house;
        //view.surname.text = model.surname;
        view.name.text = model.name;
        view.text.text = model.text;
        view.date.text = model.date;
        view.title.text = model.title;
        //view.status.text = model.status;
        //view.clickButton.GetComponentInChildren<Text>().text = model.id;
        //view.clickButton.GetComponentInChildren<Text>().text = model.facenumber;
        view.clickButton.onClick.AddListener(()=> 
            {
                //Debug.Log(view.id.text + " is clicked!");
                PlayerPrefs.SetString("id_order", view.id.text);
                Web5chat.yk_facenumber = view.facenumber.text;
                Web5.Web5idorder = view.id.text;
                SceneManager.LoadScene("Web5chat");
            }
        );
    }

    public class TestItemView // view Привязываем данные из таблицы к префабу
    {
        public Text id;
        public Text facenumber;
        public Text street;
        public Text house;
        //public Text surname;
        public Text name;
        public Text text;
        public Text title;
        public Text date;
        public Button clickButton;
        //public Text status;

        public TestItemView (Transform rootView)
        {
            id = rootView.Find("Id").GetComponent<Text>();
            facenumber = rootView.Find("Facenumber").GetComponent<Text>();
            street = rootView.Find("Street").GetComponent<Text>();
            house = rootView.Find("House").GetComponent<Text>();
            //surname = rootView.Find("Surname").GetComponent<Text>();
            name = rootView.Find("Name").GetComponent<Text>();
            //status = rootView.Find("Status").GetComponent<Text>();
            text = rootView.Find("Text").GetComponent<Text>();
            date = rootView.Find("Date").GetComponent<Text>();
            title = rootView.Find("Title").GetComponent<Text>();
            //titleText = rootView.Find("TitleText").GetComponent<Text>();
            clickButton = rootView.Find("ClickButton").GetComponent<Button>();
        }
    }

    [System.Serializable]
    public class TestItemModel // model колонки забираем из таблицы базы данных 
    {
        public string id;
        public string facenumber;
        public string street;
        public string house;
        //public string surname;
        public string name;
        public string title;
        public string text;
        public string date;
        //public string status;
        //public string title;
        //public string buttonText;
    }

  #region JSONHELPER -----

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