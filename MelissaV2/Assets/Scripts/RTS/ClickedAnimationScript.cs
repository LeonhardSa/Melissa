using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedAnimationScript : MonoBehaviour
{
    private float duration = 2f;
    private float time = 0f;
    private Vector3 startScale;
    void Start()
    {
        startScale = transform.localScale;
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time <= duration/2)
        {
            transform.localScale = startScale + new Vector3(time, 0f, time);
        }
        else if (time > duration)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.localScale = startScale + new Vector3(duration-time, 0f, duration-time);
        }
    }
}
