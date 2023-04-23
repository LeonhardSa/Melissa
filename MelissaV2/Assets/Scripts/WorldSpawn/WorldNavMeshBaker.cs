using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldNavMeshBaker : MonoBehaviour
{
    public bool RunOnStart;
    //public bool Complete { get; private set; }
    public bool Complete { get; private set; }

    [SerializeField]
    NavMeshSurface[] Targets;

    void Start()
    {
        Complete = false;
        RefreshList();
        if (RunOnStart)
            StartCoroutine(World());
    }

    public void RefreshList()
    {
        Targets = FindObjectsOfType(typeof(NavMeshSurface)) as NavMeshSurface[];
    }

    public IEnumerator World()
    {
        Complete = false;
        foreach (NavMeshSurface surface in Targets)
        {
            surface.BuildNavMesh();
            yield return null;
        }
        Complete = true;

        FindObjectOfType<LoadingScreenStates>().EndLoading();
    }

    public void RefreshListAndGenerate()
    {
        RefreshList();
        StartCoroutine(World());
    }
}