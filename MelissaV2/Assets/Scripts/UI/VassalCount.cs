using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VassalCount : MonoBehaviour
{
    public static VassalCount vassalSingelton;

    public int vassalUp;

    public TextMeshProUGUI vassalNumber;

    void Start()
    {
        vassalSingelton = this;
        GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        vassalNumber.text = vassalUp.ToString();
    }
}
