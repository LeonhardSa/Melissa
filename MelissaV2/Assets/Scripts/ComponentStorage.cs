using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentStorage : MonoBehaviour
{
    public GameObject FriendlyProjectile;

    public void Start()
    {
        RobotRepository.SetComponentStorage(this);
    }
}
