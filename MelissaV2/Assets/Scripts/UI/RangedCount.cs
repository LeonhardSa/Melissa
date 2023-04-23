using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangedCount : MonoBehaviour
{
    public static RangedCount rangedSingelton;

    public int rangedUp;

    public TextMeshProUGUI rangeRobos;

    void Start()
    {
        rangedSingelton = this;
        GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        rangeRobos.text = rangedUp.ToString();
    }
}
