using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestPlayer : MonoBehaviour
{
    public float SpeedMultiplier;
    NavMeshAgent nma;
    bool locked;

    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            if (!locked)
            {
                locked = true;
                Vector3 mousePosition = Input.mousePosition;
                // mousePosition.z = mousePosition.y;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mousePosition.y = 1;
                print("Moving to " + mousePosition);
                nma.SetDestination(mousePosition);
            }
        }
        else
        {
            locked = false;
        }

        /*
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, -1) * Time.deltaTime * SpeedMultiplier;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * SpeedMultiplier;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, 1) * Time.deltaTime * SpeedMultiplier;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * SpeedMultiplier;
        }
        */
    }
}