using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Spisokall : MonoBehaviour
{
    //public Text t_game_p, t_name_p, t_cup_p;
    //public GameObject I_krug, s1, s2, s3, s4, s5;
    //public static int star; // звезды
    // для количества людей
    //public Text t_count_all_p, t_count_for_priz;
    public static int _all_p;
    public string userId = "0"; // номер по которому сортируем список
    // для правил турнира
    //public GameObject Rules;

    Action <string> _createItemsCallback;

    void Start()
    {
        _createItemsCallback = (jsonArrayString) => {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };
        //string userId = PlayerPrefs.GetString("nych"); // был id поменяли на nych // забираем ""добавить GetID.php 
        //string userId = "0"; 
        StartCoroutine (GetPlataIDs(userId ,_createItemsCallback));
        //StartCoroutine (GetPlataIDs(userId ,7 ,_createItemsCallback));
        //StartCoroutine(GetPEOPL(0));
    }


    IEnumerator CreateItemsRoutine(string jsonArrayString){
        // разбираем json массив на строчную таблицу
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for(int i = 0; i < jsonArray.Count ; i++){
            //Debug.Log("new+++");
            //создаём локальную переменную
            bool isDone = false; // мы загружены?
            string itemId = jsonArray[i].AsObject["id"]; //
            JSONObject itemInfoJason = new JSONObject();
            // создаём callback что бы получить информацию из Web.sc
            Action<string> getItemInfoCallback = (itemInfo) => {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJason = tempArray[0].AsObject;
            };
            // ожидаем пока Web.sc отправить callback и получит наши параметры
            StartCoroutine(GetPlata(itemId, getItemInfoCallback));
            // ожидаем пока callback полученный из Web.sc (инфа об окончание загрузки)
            yield return new WaitUntil(() => isDone == true);
            // создаём объект (item prefab)
            GameObject item = Instantiate (Resources.Load("Prefabs/item") as GameObject);
            item.transform.SetParent(this.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;
            // заполняем информацией
            item.transform.Find("street").GetComponent<Text>().text = itemInfoJason["street"];
            item.transform.Find("house").GetComponent<Text>().text = itemInfoJason["house"];
            item.transform.Find("flat").GetComponent<Text>().text = itemInfoJason["flat"];
            item.transform.Find("phone").GetComponent<Text>().text = itemInfoJason["phone"];
            item.transform.Find("email").GetComponent<Text>().text = itemInfoJason["email"];
            item.transform.Find("facenumber").GetComponent<Text>().text = itemInfoJason["facenumber"];
            item.transform.Find("kod").GetComponent<Text>().text = itemInfoJason["kod"];
            item.transform.Find("surname").GetComponent<Text>().text = itemInfoJason["surname"];
            item.transform.Find("name").GetComponent<Text>().text = itemInfoJason["name"];
            item.transform.Find("otch").GetComponent<Text>().text = itemInfoJason["otch"];
            item.transform.Find("nachisl").GetComponent<Text>().text = itemInfoJason["nachisl"];
            item.transform.Find("debd").GetComponent<Text>().text = itemInfoJason["debd"];
            item.transform.Find("mesdebd").GetComponent<Text>().text = itemInfoJason["mesdebd"];
            item.transform.Find("xbc").GetComponent<Text>().text = itemInfoJason["XBC"];
            item.transform.Find("datexbc").GetComponent<Text>().text = itemInfoJason["dateXBC"];
            item.transform.Find("gbc").GetComponent<Text>().text = itemInfoJason["GBC"];
            item.transform.Find("dategbc").GetComponent<Text>().text = itemInfoJason["dateGBC"];
            item.transform.Find("lastxbc").GetComponent<Text>().text = itemInfoJason["lastXBC"];
            item.transform.Find("lastgbc").GetComponent<Text>().text = itemInfoJason["lastGBC"];
            //StartCoroutine(EffOpen(item));
            //Debug.Log("+++");
            // повторяем операцию создания объекта если еще есть
        }
    }
    //--------------------------------------
     // для получения всех ID из таблицы (1,2,3, ...)
    public IEnumerator GetPlataIDs(string userID,System.Action<string> callbackU){//I_krug.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        //form.AddField("limit", Limit);
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetIDsALLyk.php",form)){
        yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
            //Debug.Log("получили из GetPlataIDs" + www.downloadHandler.text);
            string jsonArray = www.downloadHandler.text;
            // call callback function to pass results
            callbackU(jsonArray);
            }//I_krug.SetActive(false);
        }
        //I_krug.SetActive(false);
    }
    // для получения инфы по каждому ID из GetUSERsIDs
    public IEnumerator GetPlata(string itemID, System.Action<string> callbackU){
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetInfoIDyk.php", form)){
        yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }else{
            //Debug.Log("получили из GetPlata" + www.downloadHandler.text);
            string jsonArray = www.downloadHandler.text;
            // call callback function to pass results
            callbackU(jsonArray);
            }
        }
    }


    //-----------------------------------------------------------------------------------------
    IEnumerator EffOpen(GameObject objectName){ objectName.SetActive(true);//yield return new WaitForSeconds(0);
       for (float q = 0.5f; q < 1f; q += 0.05f) // было 0.1f
       {   //objectName.transform.position = new Vector3();
           objectName.transform.localScale = new Vector3(q, q, q); yield return new WaitForSeconds(.01f);}}

    IEnumerator EffMove(GameObject objectName){ //yield return new WaitForSeconds(0);
       for (float q = -2f; q < 2.4f; q += 0.05f)
       { objectName.transform.position = new Vector3(q, -3, 1); yield return new WaitForSeconds(.01f);}
        for (float q = 1.4f; q > 1f; q -= 0.05f)
       { objectName.transform.localScale = new Vector3(q, q, q); yield return new WaitForSeconds(.01f);}
    }

    public void ClickTopGame(){SceneManager.LoadScene("Topgame");}
    public void ClickHome(){SceneManager.LoadScene("Web");}
    //public void ClickOpenRules(){Rules.SetActive(true);}
    //public void ClickCloseRules(){Rules.SetActive(false);}

    void FixedUpdate()
    {
        //float speed8 = 10f;
        //Quaternion rotationX = Quaternion.AngleAxis(speed8, new Vector3(0,0,-1));// x,y,z
        //I_krug.transform.rotation *= rotationX;
    }
}
