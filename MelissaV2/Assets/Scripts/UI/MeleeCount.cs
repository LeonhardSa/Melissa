using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MeleeCount : MonoBehaviour
{
    public static MeleeCount meleeSingelton;

    public int meleeUp;

    public TextMeshProUGUI meleeRobos;

    void Start()
    {
        meleeSingelton = this;
        GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        meleeRobos.text = meleeUp.ToString();
    }
}
