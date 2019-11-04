using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public Transform Container;
    public List<Letter> Letters = new List<Letter>();
    public Button HandAddSelectedLetterButton;
    public Button HandAddNewLetterButton;

    void Start()
    {
        HandAddSelectedLetterButton.onClick.AddListener(delegate
        {
            if (StoryManager.Instance.Cur_ChoseLetter)
            {
                StoryManager.Instance.Cur_ChoseLetter.transform.SetParent(Container);
                Letters.Add(StoryManager.Instance.Cur_ChoseLetter);
                StoryManager.Instance.ReleaseCurLetter();
            }
        });
        HandAddNewLetterButton.onClick.AddListener(delegate
        {
            List<int> keys = StoryManager.Instance.LetterInfoDict.Keys.ToList();
            int letterIndex = keys[Random.Range(0, keys.Count)];
            LetterInfo li = StoryManager.Instance.LetterInfoDict[letterIndex];
            StoryManager.Instance.LetterInfoDict.Remove(letterIndex);
            StoryManager.Instance.TotalLetterCountText.text = StoryManager.Instance.LetterInfoDict.Count.ToString();
            Letter letter = GameObjectPoolManager.Instance.PoolDict[GameObjectPoolManager.PrefabNames.Letter].AllocateGameObject<Letter>(Container);
            letter.Initialize(li);
            Letters.Add(letter);
        });
    }
}