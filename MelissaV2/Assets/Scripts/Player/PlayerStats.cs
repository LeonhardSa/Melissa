using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerStats : MonoBehaviour
{
    public float playerHealth = 100f;
    public float playerEnergy = 100f;
    public float damageTimeout = 1f;
    public float rechargeRate = 1f;
    public bool isAlive = true;
    private float maxEnergy = 100f;

    PlayerAnimator anim;
    PlayerHUD hud;
    CharacterAudio playerSfx;
    private bool canTakeDamage = true;
    private void Awake()
    {

        isAlive = true;
        hud = GetComponentInChildren<PlayerHUD>();
        anim = GetComponentInChildren<PlayerAnimator>();
        playerSfx = GetComponentInChildren<CharacterAudio>();
        StartCoroutine(hud.DisplayMessage("SYSTEM: ONLINE"));
    }
    void Update()
    {
        if (playerEnergy < maxEnergy)
        {
            playerEnergy += Time.deltaTime/rechargeRate;
        }
    }
    public void TakeDamage(float amount)
    {
        
        if (canTakeDamage && playerHealth > 0)
        {
            playerHealth -= amount;
            anim.Hit();
            StartCoroutine(damageTimer());
            if (playerHealth <= 0f)
            {
                playerHealth = 0f;
                Die();
            }
            if (playerHealth <= 25f)
            {
                playerSfx.WarningSFX();
               StartCoroutine(hud.DisplayMessage("WARNING: CONDITION CRITICAL"));
                
            }
        }
    }
    public void GiveHealth(float amount)
    {
        if (playerHealth < 100 && playerHealth > 0)
        {
            playerSfx.HealSFX();
            playerHealth += amount;
            
            if (playerHealth > 100)
            {
                playerHealth = 100;
            }
        }
    }
    public void GiveEnergy(float amount)
    {
        if (playerEnergy < 100)
        {
            playerSfx.EnergySFX();
            playerEnergy += amount;

            if (playerEnergy > 100)
            {
                playerEnergy = 100;
            }
        }
    }
    public void UseEnergy(float amount)
    {
        if (playerEnergy >= amount)
        {
            playerEnergy -= amount;
        }
    }
    private IEnumerator damageTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        canTakeDamage = true;
    }
    void Die()
    {
        anim.Die();
        playerSfx.FailSFX();
        StartCoroutine(hud.DisplayMessage("SYSTEM: OFFLINE"));
        Invoke("LoadMenu", 1f);
        isAlive = false;
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
