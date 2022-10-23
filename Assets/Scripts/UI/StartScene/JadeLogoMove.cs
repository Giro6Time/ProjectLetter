using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JadeLogoMove : MonoBehaviour
{
    public float MoveSpeed = 1;
    public float MoveRange = 1;
    float StartYPos = 0;
    RectTransform thisRectTransform;
    // Start is called before the first frame update
    void Start()
    {
        thisRectTransform = GetComponent<RectTransform>();
        StartYPos = thisRectTransform.position.y;
    }

    private void FixedUpdate()
    {
        float rightTime = Time.time;
        Vector3 pos = thisRectTransform.position;
        pos.y = Mathf.Sin(rightTime * MoveSpeed) * MoveRange + StartYPos;
        thisRectTransform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
