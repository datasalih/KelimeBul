using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public WordManager wordManager;
    public InputField wordInputField;
    public Text enteredWordText;

    public char[] wordInput = new char[5];
    public char[] word = new char[5];
    public int correctCounter = 0;
    public int currentRow = 0;
    private int currentLevel = 1;

    public List<Row> rows = new List<Row>();

    public void Start()
    {
        word = wordManager.currentWord.ToCharArray();
       // levelManager.LoadCurrentLevelScene();
    }

  


    public void deleteWord()
    {
        wordInputField.text = "";
    }



    public void SubmitWord()
    {
        string enteredWord = wordInputField.text.ToUpperInvariant();

        if (enteredWord.Length < 5)
        {
            Debug.Log("Enter at least 5 letters");
            return;
        }

        enteredWord = enteredWord.Replace("i", "Ý")
                                 .Replace("I", "Ý")
                                 .Replace("ý", "I")
                                 .Replace("ð", "Ð")
                                 .Replace("ö", "Ö")
                                 .Replace("ü", "Ü")
                                 .Replace("ç", "Ç");

        wordInput = enteredWord.ToCharArray();
        enteredWordText.text = enteredWord;

        for (int i = 0; i < 5; i++)
        {
            if (wordInput[i] == '\0')
            {
                Debug.Log("Complete the word");
                break;
            }
            else if (word[i] == wordInput[i])
            {
                rows[currentRow].images[i].sprite = null;

                rows[currentRow].texts[i].text = wordInput[i].ToString();
                rows[currentRow].images[i].color = Color.green;

                correctCounter++;
            }
            else
            {
                for (int j = 0; j < 5; j++)
                {
                    if (wordInput[i] == word[j])
                    {
                        rows[currentRow].images[i].sprite = null;

                        rows[currentRow].texts[i].text = wordInput[i].ToString();
                        rows[currentRow].images[i].color = Color.yellow;
                        break;
                    }
                    else
                    {
                        rows[currentRow].images[i].sprite = null;

                        rows[currentRow].texts[i].text = wordInput[i].ToString();
                        rows[currentRow].images[i].color = Color.gray;
                    }
                }
             
            }
        }

        if (correctCounter >= 5)
        {
            string currentWord = new string(word);
            PlayerPrefs.SetString("CurrentWord", currentWord);
            SceneManager.LoadScene("Win");
        }
        else
        {
            correctCounter = 0;
            currentRow++;
            if (currentRow >= rows.Count)
            {
                string currentWord = new string(word);
                PlayerPrefs.SetString("CurrentWord", currentWord);
                SceneManager.LoadScene("Lose");
            }
        }
    }

    public void DisplayRewardedLetter(int index)
    {
        // Make sure the index is within bounds
        if (index >= 0 && index < 5)
        {
            // Update the UI elements of the current row
            rows[currentRow].images[index].sprite = null;
            rows[currentRow].texts[index].text = word[index].ToString();
            rows[currentRow].images[index].color = Color.green;
        }
        else
        {
            Debug.LogError("Invalid index provided for displaying rewarded letter.");
        }
    }


}

