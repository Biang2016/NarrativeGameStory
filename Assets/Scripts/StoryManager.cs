using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoSingleton<StoryManager>
{
    void Awake()
    {
    }

    void Start()
    {
        PrefabManager.Instance.LoadPrefabs_Editor();
        readCSV();
    }

    void Update()
    {
    }

    [SerializeField] private Hand Hand;
    [SerializeField] private Slot[] Slots;
    public Letter Cur_ChoseLetter;

    public Dictionary<int, LetterInfo> LetterInfoDict = new Dictionary<int, LetterInfo>();

    void readCSV()
    {
        StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/TownStory.csv");
        string content = sr.ReadToEnd();
        string[] lineArray = content.Split(new string[] {"\r", "\""}, StringSplitOptions.RemoveEmptyEntries);
        foreach (string s in lineArray)
        {
            if (!string.IsNullOrEmpty(s.Trim(",\n".ToCharArray())))
            {
                LetterInfo li = new LetterInfo();
                li.LetterContent = s;
                LetterInfoDict.Add(LetterInfoDict.Count, li);
            }
        }

        TotalLetterCountText.text = LetterInfoDict.Count.ToString();
    }

    public Text TotalLetterCountText;

    public void ReleaseCurLetter()
    {
        foreach (Slot slot in Slots)
        {
            foreach (Letter letter in slot.Letters)
            {
                if (Cur_ChoseLetter == letter)
                {
                    letter.IsSelected = false;
                    Cur_ChoseLetter = null;
                }
            }
        }

        foreach (Letter letter in Hand.Letters)
        {
            if (Cur_ChoseLetter == letter)
            {
                letter.IsSelected = false;
                Cur_ChoseLetter = null;
            }
        }
    }

    public void RemoveLetter(Letter letter)
    {
        foreach (Slot slot in Slots)
        {
            if (slot.Letters.Contains(letter))
            {
                slot.Letters.Remove(letter);
            }
        }

        if (Cur_ChoseLetter == letter) Cur_ChoseLetter = null;
        if (Hand.Letters.Contains(letter)) Hand.Letters.Remove(letter);
    }
}