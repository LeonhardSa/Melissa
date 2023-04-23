using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI messageText;
    public GameObject messagePanel;
    public PlayerStats playerStats;

    void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
    }
    void Update()
    {
        healthText.text = (playerStats.playerHealth.ToString(""));
        energyText.text = (playerStats.playerEnergy.ToString("F0"));
    }
    public IEnumerator DisplayMessage(string message)
    {
        messageText.text = (message);
        messagePanel.SetActive(true);
        yield return new WaitForSeconds(3);
        messagePanel.SetActive(false);
    }
}
