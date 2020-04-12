using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.UI; using System; using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class M3 : MonoBehaviour
{
    public Text t_add;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetPIC("1"));
        StartCoroutine(GetAdd(1));StartCoroutine(GetUrl(1));
    }

    public void Openurl(){ Application.OpenURL(PlayerPrefs.GetString("url"));}

    public void ClickM1(){SceneManager.LoadScene("M1");}
    public void ClickM2(){SceneManager.LoadScene("M2");}
    public void ClickMlog(){PlayerPrefs.SetString("facenumber", "0");SceneManager.LoadScene("Mlog");}

    public void ClickMnews(){SceneManager.LoadScene("Mnews");}
    public void ClickMorder(){SceneManager.LoadScene("Morder");}
    public void ClickMquiz(){SceneManager.LoadScene("Mquiz");}
    public void ClickMcounter(){SceneManager.LoadScene("Mcounter");}
    public void ClickMdopservic(){SceneManager.LoadScene("Mdopservic");}
    public void ClickMvacance(){SceneManager.LoadScene("Mvacance");}
    public void ClickMatention(){SceneManager.LoadScene("Matention");}
    public void ClickMadministracia(){SceneManager.LoadScene("Madministracia");}
    public void ClickMpromo(){SceneManager.LoadScene("Mpromo");}

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

    public RawImage ttyy;
    public GameObject butImage;
    //public Renderer ttyy;
    private String imageString = "";
   
// выводим картинку из БД
    IEnumerator GetPIC(string picphp){ WWWForm form = new WWWForm();
        form.AddField("pic", picphp); // correct
        using (UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetIMG.php",form))
        {yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
         else{ //Debug.Log(www.downloadHandler.text);
             imageString = (www.downloadHandler.text);
             if(imageString.Length > 100){
                 byte[] Bytes = System.Convert.FromBase64String (imageString);
                 Texture2D texture = new Texture2D(1,1);
                 texture.LoadImage (Bytes);
                 //GUI.DrawTexture(new Rect(200,20,440,440), texture, ScaleMode.ScaleToFit, true, 1f);
                 //ttyy.material.mainTexture = texture;
                 ttyy.texture = texture;
                 //butImage.SetActive(true);
             }else{
                 //butImage.SetActive(false);
             }
            }
        } // correct
    }
}
