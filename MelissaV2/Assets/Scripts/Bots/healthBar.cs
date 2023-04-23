using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Image healthBarImg;
    public Image healBarImg;
    public Image convBarImg;
    float startHealth;
    RobotStats stats;
    void Awake()
    {
        stats = GetComponentInParent<RobotStats>();
        startHealth = stats.health;
    }
    void Update()
    {
        Camera camera = Camera.main;
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        healthBarImg.fillAmount = stats.health / startHealth;
        healBarImg.fillAmount = (stats.maxHealth - stats.hullDamage) / startHealth;
        convBarImg.fillAmount = (stats.conRate / 100);
        
    }
}
