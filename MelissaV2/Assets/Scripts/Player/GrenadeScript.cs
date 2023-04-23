using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    void Awake()
    {
        Destroy(this.gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        RobotBehaviour rb = other.GetComponent<RobotBehaviour>();
        if (rb != null)
        {
            if (other.gameObject.tag.Contains("nemy"))
            {
                float rnd = UnityEngine.Random.Range(0, 100);
                RobotStats rs = other.GetComponent<RobotStats>();
                if (rnd <= rs.conRate)
                {
                    rb.rr.SwitchCluster(other.gameObject);
                    rs.conRate = 0;
                }

                if (rb.fightingState == false)
                {
                    rb.agent.SetDestination(GameObject.Find("Player").transform.position);
                    rb.scoutingDistance *= 2;
                }
            }
            else if (other.gameObject.tag.Contains("drift"))
            {
                rb.agent.enabled = true;
                rb.rr.SwitchCluster(other.gameObject);
                rb.enabled = true;
            }

            Destroy(this.gameObject, 1f);
        }
    }
}
