using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{

    
    AudioClip deathClip;
    public AudioClip[] death;

    AudioClip footstepClip;
    public AudioClip[] footstep;

    AudioClip hurtClip;
    public AudioClip[] hurt;

    AudioClip healClip;
    public AudioClip[] heal;

    AudioClip attackClip;
    public AudioClip[] attack;

    AudioClip convertClip;
    public AudioClip[] convert;

    AudioClip energyClip;
    public AudioClip[] energy;

    public AudioClip warningClip;
    public AudioClip failClip;
    AudioSource audioSrc;


    public void Awake()
    {
        audioSrc = GetComponentInParent<AudioSource>();
    }

    public void AttackSFX()
    {
        attackClip = attack[Random.Range(0, attack.Length)];
        audioSrc.clip = attackClip;
        audioSrc.PlayOneShot(attackClip);
    }
    public void WarningSFX()
    {
        audioSrc.clip = warningClip;
        audioSrc.PlayOneShot(warningClip);
    }
    public void FailSFX()
    {
        audioSrc.clip = failClip;
        audioSrc.PlayOneShot(failClip);
    }
    public void ConvertSFX()
    {
        convertClip = convert[Random.Range(0, convert.Length)];
        audioSrc.clip = convertClip;
        audioSrc.PlayOneShot(convertClip);
    }
    public void FootstepSFX()
    {
        footstepClip = footstep[Random.Range(0, footstep.Length)];
        audioSrc.clip = footstepClip;
        audioSrc.PlayOneShot(footstepClip);
    }
    public void HurtSFX()
    {
        hurtClip = hurt[Random.Range(0, hurt.Length)];
        audioSrc.clip = hurtClip;
        audioSrc.PlayOneShot(hurtClip);
    }
    public void HealSFX()
    {
        healClip = heal[Random.Range(0, heal.Length)];
        audioSrc.clip = healClip;
        audioSrc.PlayOneShot(healClip);
    }
    public void EnergySFX()
    {
        energyClip = energy[Random.Range(0, energy.Length)];
        audioSrc.clip = energyClip;
        audioSrc.PlayOneShot(energyClip);
    }
    public void DeathSFX()
    {
        deathClip = death[Random.Range(0, death.Length)];
        audioSrc.clip = deathClip;
        audioSrc.PlayOneShot(deathClip);
    }
}
