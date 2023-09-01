using UnityEngine; 
using TMPro;
using System.Collections.Generic;
using System.IO;
using System;

public class FileLoader : MonoBehaviour
{
    [Header("File")]
    [SerializeField] private string fileName = "Colors";

    [Header("UI")]
    [SerializeField] private TMP_InputField wordToSearch;
    [SerializeField] private TMP_Text resultText; 

    private Dictionary<string, uint> _wordsCount = new Dictionary<string, uint>();
 
    void Start()
    {
        ReadTextFile(fileName);
    }

    private void ReadTextFile(string textFileName)
    {
        string filePath = $"{Application.streamingAssetsPath}/TestData/{textFileName}";

        try
        { 
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            // Loop through each line and split it into words
            foreach (string line in lines)
            {
                string[] words = line.Split(new char[] { ' ', '\t', ',', '.', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                // Check each word in the line
                foreach (string word in words)
                {
                    if (!_wordsCount.ContainsKey(word))
                    {
                        _wordsCount.Add(word, 1);
                        continue;
                    }
                    _wordsCount[word]++;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            resultText.text = $"{e}";
        }
    }

    public void OnButtonClickSearchForWord()
    {
        uint count = 0;
        string word = wordToSearch.text;

        if (_wordsCount.ContainsKey(word))
        {
            count = _wordsCount[word];
        }

        resultText.text = $"{word} - {count}";
    }
}
