using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

    public Text currenttext;


    void Start()
    {
        string currentWord = PlayerPrefs.GetString("CurrentWord");
        currenttext.text = "Kelime: " + currentWord;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
