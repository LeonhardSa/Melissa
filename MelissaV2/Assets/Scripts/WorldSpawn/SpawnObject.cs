using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject Obj;

    public void Spawn()
    {
        Obj = Instantiate(Obj, transform.position, transform.rotation);
        Obj.transform.parent = gameObject.transform.parent.transform;
        Destroy(this.gameObject);
    }
}