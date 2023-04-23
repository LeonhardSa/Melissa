using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class RobotStats : MonoBehaviour
{
    public float maxHealth, health = 50f;
    public float hullDamage = 0f;
    public float damage = 0f;
    public float conRate = 10f;

    public GameObject[] lootPrefab;
    CharacterAudio botSfx;

    void Awake()
    {
        botSfx = GetComponent<CharacterAudio>();
        Camera.main.GetComponent<RobotRepository>().RegisterRobot(gameObject);
    }

    /// <summary>
    /// Inflicts damage on the robot as well as half the amountvgets add to hullDamage. And if the robot is fighting the half amount will be add to his conversion rate.
    /// </summary>
    public void TakeDamage(float amount)
    {

        if (health > 0)
        {
            health -= amount;
            botSfx.HurtSFX();
            hullDamage += amount / 2;
            
            if (health <= 0f) {
                botSfx.DeathSFX();
                health = 0f;
                Die();
            }
            else if(GetComponent<RobotBehaviour>().fightingState == false)
            {
                GetComponent<RobotBehaviour>().agent.SetDestination(GameObject.Find("Player").transform.position);
                GetComponent<RobotBehaviour>().scoutingDistance *= 2;
            }
            else
            {
                if (gameObject.tag == "Enemy")
                {
                   RaiseConversion(amount/2);
                }
                
            }
        }
    }

    /// <summary>
    /// Heals damage of the robot!
    /// </summary>
    /// <param name="amount">The size of the healing amount</param>
    public void HealDamage(float amount)
    {
        health += amount;
        botSfx.HealSFX();
        if (health > maxHealth - hullDamage)
        {
            health = maxHealth - hullDamage;
        }
    }

    /// <summary>
    /// Heals damage of the robot!
    /// </summary>
    /// <param name="amount">The size of the healing amount</param>
    /// <param name="ignoreHullDamage">false: It can not heal over the amount [health-hullDamage]; true: heal can go over the amount [health-hullDamage]. But it won't reset the hullDamage.</param>
    public void HealDamage(float amount, bool ignoreHullDamage)
    {
        if (ignoreHullDamage == false)
        {
            HealDamage(amount);
        } else
        {
            health += amount;
        }
    }

    /// <summary>
    /// Raises the conversion rate [max. 100]
    /// </summary>
    public void RaiseConversion(float amount)
    {
        conRate += amount;
        if (conRate > 100) conRate = 100;
    }

    public void DropLoot()
    {
            int randomNumber = Mathf.RoundToInt(UnityEngine.Random.Range(0f, lootPrefab.Length - 1));
            Instantiate(lootPrefab[randomNumber], transform.position, Quaternion.identity);
    }


    /// <summary>
    /// Destroys the robot and removes the object from the list for the RTS Movement and more.
    /// </summary>
    public void Die()
    {
        if (gameObject != null)
        {
            Camera.main.GetComponent<RobotRepository>().RemoveRobot(this.gameObject);

            if (gameObject.tag == "Enemy")
            {
                DropLoot();
            }
            
            Destroy(this.gameObject);
        }
    }
}
