using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Transform Content;
    public List<Letter> Letters = new List<Letter>();
    public Button AddLetterToSlotButton;

    void Start()
    {
        AddLetterToSlotButton.onClick.AddListener(delegate
        {
            if (StoryManager.Instance.Cur_ChoseLetter)
            {
                StoryManager.Instance.Cur_ChoseLetter.transform.SetParent(Content);
                Letters.Add(StoryManager.Instance.Cur_ChoseLetter);
                StoryManager.Instance.ReleaseCurLetter();
            }
        });
    }
}