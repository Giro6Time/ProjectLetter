using UnityEngine;

public class Jack : SpecialEventObject,ISpeecher
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
    protected override void OnMouseDown()
    {
        SetCollider(false);
        Debug.Log("Jack is clicked");
        EventManager.EventTrigger("CharactorClicked");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
