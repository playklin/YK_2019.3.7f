using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.UI; using System; using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Mdopservic : MonoBehaviour
{
    public InputField if_id_servic;
    public GameObject g_servic_no;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClickM3(){SceneManager.LoadScene("M3");}
    public void ClickOtmenaOK(){g_servic_no.SetActive(false);}

    public void ClickOpenServic(){PlayerPrefs.SetString("id_servic", if_id_servic.text);
    StartCoroutine(CheckServic(PlayerPrefs.GetString("id_servic"),PlayerPrefs.GetString("facenumber")));}

    IEnumerator CheckServic(string idorder, string facenumber) {
        WWWForm form = new WWWForm();
        form.AddField("_id", idorder);
        form.AddField("_facenumber", facenumber);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/CheckServic.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}
        else{//Debug.Log("" + www.downloadHandler.text);
        //SceneManager.LoadScene("Web6");
        if(www.downloadHandler.text == "no"){g_servic_no.SetActive(true);}else{SceneManager.LoadScene("Mserviczapros");}
        }
        }
    }
}
