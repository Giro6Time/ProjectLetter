using UnityEngine;

public class Enemy : MonoBehaviour, ISpeecher
{
    public SpeechBubble speechBubble;
    public string speecherName;

    public float Attack;
    public float Health;

    public SpeechBubble Bubble
    {
        get => speechBubble;
        set => speechBubble = value;
    }
    public string SpeecherName
    {
        get => speecherName;
        set => speecherName = value;
    }

    void Start()
    {
        speechBubble.Init(this);
    }

    public void PlayAttackAnimation()
    {

    }
    public void PlayDieAnimation()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
