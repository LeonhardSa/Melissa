using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFocus : MonoBehaviour
{
    public GameObject player;
    public float smoothRate = 0.1f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, smoothRate);
    }
}
