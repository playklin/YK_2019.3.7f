using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Toggletest : MonoBehaviour
{
    public Text t_1,t_2;
    ToggleGroup toggleGroupInstance;

    public Toggle currentSelection{
        get { return toggleGroupInstance.ActiveToggles ().FirstOrDefault ();}
    }

    public void ClickSend(){
        toggleGroupInstance = GetComponent<ToggleGroup>();
        //Debug.Log("First selected " + currentSelection.name);
        if(currentSelection.name == "Toggle1"){Debug.Log(t_1.text);}
        if(currentSelection.name == "Toggle2"){Debug.Log(t_2.text);}
    }
    
    void Start()
    {
        //SelectToggle(2);
    }

    public void SelectToggle(int id){
        var toggles = toggleGroupInstance.GetComponentsInChildren<Toggle>();
        toggles [id].isOn = true;}
}
