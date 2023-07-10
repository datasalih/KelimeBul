using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word
{
    public char[] word = new char[5];
}

public class WordManager : MonoBehaviour
{

    public List<Word> words = new List<Word>();
    public string currentWord;
    public List<string> wordlistString = new List<string>();


    private void Awake()
    {
        currentWord = wordlistString[Random.Range(0, wordlistString.Count)];
        Converter();
    }


    void Converter()
    {
        foreach (string word in wordlistString)
        {
            Word kelime = new Word();
            kelime.word = word.ToCharArray();
            words.Add(kelime);
        }
    }

}
