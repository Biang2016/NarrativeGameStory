using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Letter : PoolObject
{
    [SerializeField] private Text Text;
    [SerializeField] private OnMouseClick OnMouseClick;
    [SerializeField] private Image Image;

    public void Initialize(LetterInfo li)
    {
        Text.text = li.LetterContent;
    }

    public override void PoolRecycle()
    {
        IsSelected = false;
        base.PoolRecycle();
    }

    void Start()
    {
        OnMouseClick.LeftClick.AddListener(delegate { IsSelected = true; });
        OnMouseClick.RightClick.AddListener(delegate
        {
            IsSelected = false;
            StoryManager.Instance.RemoveLetter(this);
            PoolRecycle();
        });
    }

    private bool isSelected;

    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            if (isSelected == value) return;
            isSelected = value;
            if (isSelected)
            {
                Image.color = Color.blue;
                StoryManager.Instance.ReleaseCurLetter();
                StoryManager.Instance.Cur_ChoseLetter = this;
            }
            else
            {
                Image.color = Color.black;
            }
        }
    }
}