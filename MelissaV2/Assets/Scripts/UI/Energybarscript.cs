using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energybarscript : MonoBehaviour
{
    public Slider epSlider;

    public void SetMaxEnergy(int energy)
    {
        epSlider.maxValue = energy;
        epSlider.value = energy;
    }

    public void SetEnergy(int energy)
    {
        epSlider.value = energy;
    }
}
