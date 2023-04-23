using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUi : MonoBehaviour
{
    public GameObject meleeRobots;

    public GameObject rangedRobots;

    public GameObject overviewUI;

    public void RangedUI()
    {
        meleeRobots.SetActive(false);
        overviewUI.SetActive(false);
        rangedRobots.SetActive(true);
    }

    public void MeleeUI()
    {
        overviewUI.SetActive(false);
        rangedRobots.SetActive(false);
        meleeRobots.SetActive(true);
    }

    public void Overview() 
    {
        rangedRobots.SetActive(false);
        meleeRobots.SetActive(false);
        overviewUI.SetActive(true);
    }

     
}
