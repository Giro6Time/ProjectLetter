using UnityEngine;

public class Jack : MonoBehaviour,ISpeecher
{
    public SpeechBubble bubble;
    public SpeechBubble Bubble
    {
        get => bubble;
        set => bubble = value;
    }
    public string speecherName;
    public string SpeecherName
    {
        get => speecherName;
        set => speecherName = value;
    }
    void Start()
    {
        bubble.Init(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
