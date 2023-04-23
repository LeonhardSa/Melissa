using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenStates : MonoBehaviour
{

    public GameObject Player;
    public GameObject HUD;
    public GameObject LoadingScreen;

    public void StartLoading()
    {
        Player.GetComponent<PlayerController>().enabled = false;
        HUD.SetActive(false);
        LoadingScreen.SetActive(true);
        InvokeRepeating("Loading", 0f, 0.5f);

        FindObjectOfType<WorldNavMeshBaker>().RefreshListAndGenerate();
    }

    public void EndLoading()
    {
        SpawnInRobotsAndMore();
        CancelInvoke();
        Player.GetComponent<PlayerController>().enabled = true;
        HUD.SetActive(true);
        LoadingScreen.SetActive(false);
    }

    private void SpawnInRobotsAndMore()
    {
        SpawnObject[] spawners = FindObjectsOfType(typeof(SpawnObject)) as SpawnObject[];
        foreach (SpawnObject sr in spawners)
            sr.Spawn();
        DoorScript[] doorscripts = FindObjectsOfType(typeof(DoorScript)) as DoorScript[];
        foreach (DoorScript ds in doorscripts)
            ds.enabled = true;
    }

    private void Loading()
    {
        if (!LoadingScreen.GetComponentInChildren<TextMeshProUGUI>().text.Contains("....."))
            LoadingScreen.GetComponentInChildren<TextMeshProUGUI>().text += ".";
        else
            LoadingScreen.GetComponentInChildren<TextMeshProUGUI>().text = "LOADING.";
    }
}