using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CashCount : MonoBehaviour
{
    public static CashCount cashSingelton;

    public int cashUp;

    public TextMeshProUGUI cash;

    void Start()
    {
        cashSingelton = this;
        GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        cash.text = cashUp.ToString();
    }
}
