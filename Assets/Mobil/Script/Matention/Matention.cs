using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.UI; using System; using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Matention : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetAdd(1));StartCoroutine(GetUrl(1));
    }
    public void ClickM3(){SceneManager.LoadScene("M3");}

    public Text t_add;
    public void OpenAdd(){ Application.OpenURL(PlayerPrefs.GetString("url"));}
    IEnumerator GetAdd(int id_add){ 
        WWWForm form = new WWWForm(); form.AddField("_id_add_", id_add); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetAdd.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{t_add.text = www.downloadHandler.text;
        PlayerPrefs.SetString("add", www.downloadHandler.text);
        //Debug.Log("получили из GetPlataIDs" + www.downloadHandler.text);
        }}
    }

    IEnumerator GetUrl(int loginadd){ 
        WWWForm form = new WWWForm(); form.AddField("_loginadd_", loginadd); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetUrl.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else{//t_url.text = www.downloadHandler.text;
        PlayerPrefs.SetString("url", www.downloadHandler.text);
        //Debug.Log("url" + www.downloadHandler.text);
        }}
    }
    
}
