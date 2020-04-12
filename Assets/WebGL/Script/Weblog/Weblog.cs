using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;using UnityEngine.Assertions.Must;
using System;
using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Weblog : MonoBehaviour
{
    public InputField if_log, if_pass;
    public Text t_info, t_server_datetime;

    public static string pass = "";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetServerDate());
        StartCoroutine(GetPIC("1"));
    }

    public void ClicEnter(){
        if(if_log.text == "max" && if_pass.text == "123"){pass = "123";SceneManager.LoadScene("Web");}else{t_info.text = "Что то пошло не так";}
        if(if_log.text == "order" && if_pass.text == "456"){SceneManager.LoadScene("Web5");}else{t_info.text = "Что то пошло не так";}
        if(if_log.text == "all" && if_pass.text == "789"){pass = "789";SceneManager.LoadScene("Web");}else{t_info.text = "Что то пошло не так";}
    }
    public void ClickTEST(){SceneManager.LoadScene("WebTEST");}//создать опрос

    public IEnumerator GetServerDate()
    {   UnityWebRequest www = UnityWebRequest.Get("http://p905504y.beget.tech/yk/GetServerDate.php");
        yield return www.SendWebRequest(); if (www.isNetworkError || www.isHttpError) {Debug.Log(www.error);} else
        {//Debug.Log(www.downloadHandler.text);
        string _timeData = www.downloadHandler.text;
        string[] words = _timeData.Split(' ');    
        //timerTestLabel.text = www.text;
        //Debug.Log ("The date is : " + words[0]);
        //Debug.Log ("The time is : " + words[1]);
        PlayerPrefs.SetString("date", words[0]);
        //_data.text = words[0];
        //setting current time
        //t_date.text = words[0];
        //string _currentTime = words[1];
        t_server_datetime.text = _timeData;
        //StartCoroutine(CreateMessage(Web5.Web5idorder,"0",if_message.text,"Бухгалтер",_timeData));
        }
    }

    public RawImage ttyy;
    //public GameObject butImage;
    //public Renderer ttyy;
    private String imageString = "";
    private String url = "http://p905504y.beget.tech/";
   
// выводим картинку из БД
    IEnumerator GetPIC(string picphp){ WWWForm form = new WWWForm();
        form.AddField("pic", picphp); // correct
        using (UnityWebRequest www = UnityWebRequest.Post(url+"yk/Test.php",form))
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

    // не настроено

    IEnumerator GetLP(string log, string pass) { WWWForm form = new WWWForm();
        form.AddField("log", log);
        form.AddField("pass", pass);
        UnityWebRequest www = UnityWebRequest.Post("https://playklin.000webhostapp.com/yk/GetLP.php", form);
        {yield return www.SendWebRequest();if (www.isNetworkError || www.isHttpError){Debug.Log(www.error);}else{
        //Debug.Log(www.downloadHandler.text);
        if(if_log.text == "max" && if_pass.text == "123"){pass = "123";SceneManager.LoadScene("Web");}else{t_info.text = "Что то пошло не так";}

        //t_news_ok.text = "OK";
        }}
    }

}



/**


<?php
$servername = "localhost"; $username = "p905504y_bd";
$password = "1416Zxcv"; $dbname = "p905504y_bd";

//$login = $_POST["login"];
//$loginPass = $_POST["loginPass"];

$conn = new mysqli($servername, $username, $password, $dbname);// Create connection
$conn->set_charset("utf8");
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error); // Check connection
} 
//echo "Connected successfully<br><br>";

$sql = "SELECT img FROM test WHERE id = 1";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    while($row = $result->fetch_assoc()) {
    echo "".base64_encode($row["img"]);
    }
} else { echo "нет данных"; }
$conn->close();
?>


*/