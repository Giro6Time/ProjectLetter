using UnityEngine;

public class EmptySpeecher : MonoBehaviour,ISpeecher
{
    public SpeechBubble bubble;
    public SpeechBubble Bubble{
        get => bubble;
        set => bubble = value;
    }
    public string speecherName;
    public string SpeecherName{
        get => speecherName;
        set => speecherName = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        bubble.Init(this);
    }

    
}
