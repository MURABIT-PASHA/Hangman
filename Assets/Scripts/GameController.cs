using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class GameController : MonoBehaviour
{
    public Text timeField;
    public GameObject[] hangMan;
    public GameObject winText;
    public GameObject loseText;
    public GameObject replayButton;
    public Text wordText;
    private float time;
    private string[] words = File.ReadAllLines(@"Assets/Texts/Words.txt");
    private string chosenWord;
    private string hiddenWord;
    private int fails = 0;
    private bool gameEnd = false;
    void Start()
    {
        chosenWord = words[Random.Range(0, words.Length)];

        for (int i = 0; i < chosenWord.Length; i++)
        {
            char letter = chosenWord[i];

            if (char.IsWhiteSpace(letter))
            {
                hiddenWord += " ";
            }
            else
            {
                hiddenWord += "_";
            }
        }
        wordText.text = hiddenWord;
    }

    void Update()
    {
        if (!gameEnd) {
        time += Time.deltaTime;
        timeField.text = time.ToString();
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            string pressedLetter = e.keyCode.ToString();

            if (chosenWord.Contains(pressedLetter))
            {
                int i = chosenWord.IndexOf(pressedLetter);
                while(i != -1)
                {
                    hiddenWord = hiddenWord.Substring(0, i) + pressedLetter + hiddenWord.Substring(i + 1);

                    chosenWord = chosenWord.Substring(0, i) + "_" + chosenWord.Substring(i + 1);
                    
                    i = chosenWord.IndexOf(pressedLetter);
                }

                wordText.text = hiddenWord;
            }
            else
            {
                hangMan[fails].SetActive(true);
                fails++;
            }

            if (fails == hangMan.Length)
            {
                loseText.SetActive(true);
                replayButton.SetActive(true);
                gameEnd = true;

            }
            if (!hiddenWord.Contains("_"))
            {
                winText.SetActive(true);
                replayButton.SetActive(true);
                gameEnd = true;
            }

        }
    }

}