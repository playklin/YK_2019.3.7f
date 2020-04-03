using System.Collections;using System.Collections.Generic;
using UnityEngine;using UnityEngine.UI;
using System;using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;
//using System;

public class ScrollViewAdapterConnected : MonoBehaviour {

    public RectTransform prefarb;
    public Text countText;
    public RectTransform content;

    void Start()
    {
        StartCoroutine(GetPlataIDs("0", results => OnReceivedModels(results))); 
    }

    public void UpdateItems ()
    {
        int modelsCount = 0;
        int.TryParse(countText.text, out modelsCount);
        //StartCoroutine(GetItems(modelsCount, results => OnReceivedModels(results))); 
    }

    IEnumerator GetItems (int count, System.Action<TestItemModel[]> callback)
    {
        yield return new WaitForSeconds(1f);
        var results = new TestItemModel[count];
        for (int i = 0; i < count; i++)
        {
            results[i] = new TestItemModel();
            results[i].title = "Item " + i;
            results[i].buttonText = "Button " + i;
        }

        callback(results);
    }

   #region HASH KOD ----

    public IEnumerator GetPlataIDs(string userID,System.Action<TestItemModel[]> callback){
        WWWForm form = new WWWForm();form.AddField("userID", userID);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetIDsALLtest.php",form)){
        yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
            TestItemModel[] mList = JsonHelper.getJsonArray<TestItemModel>(www.downloadHandler.text);
            Debug.Log("WWW Success: " + www.downloadHandler.text);
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
        view.titleText.text = model.title;
        view.clickButton.GetComponentInChildren<Text>().text = model.buttonText;
        view.clickButton.onClick.AddListener(
            ()=> 
            {
                Debug.Log(view.titleText.text + " is clicked!");
                PlayerPrefs.SetString("id_order", view.titleText.text);
                SceneManager.LoadScene("Morderchat");

            }
        );
    }

    public class TestItemView
    {
        public Text titleText;
        public Button clickButton;

        public TestItemView (Transform rootView)
        {
            titleText = rootView.Find("TitleText").GetComponent<Text>();
            clickButton = rootView.Find("ClickButton").GetComponent<Button>();
        }
    }
    [System.Serializable]
    public class TestItemModel
    {
        public string title;
        public string buttonText;
    }

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
}