using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.UI; using System; using SimpleJSON;
using UnityEngine.Networking;using UnityEngine.SceneManagement;

public class Mnews : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void ClickM2(){SceneManager.LoadScene("M2");}
   public void ClickM3(){SceneManager.LoadScene("M3");}
   public void ClickM1(){SceneManager.LoadScene("M1");}

}
