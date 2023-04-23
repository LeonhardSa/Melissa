using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private List<GameObject> robots = new List<GameObject>();
    private GameObject RoomParent;
    bool doorClosed = true;

    private void Start()
    {
        InvokeRepeating("CheckRoom", 0.25f, 0.25f);
    }

    public void CheckRoom()
    {
        RoomParent = GetParent();
        doorClosed = false;
        robots = FindObjectOfType<RobotRepository>().RequestEnemyList();
        foreach (GameObject r in robots)
        {
            if(r.transform.parent == RoomParent.transform)
            {
                doorClosed = true;
                break;
            }
        }
        robots = FindObjectOfType<RobotRepository>().ReqeustAdriftList();
        foreach (GameObject r in robots)
        {
            if (r.transform.parent == RoomParent.transform)
            {
                doorClosed = true;
                break;
            }
        }

        if (!doorClosed)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position+Vector3.up, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity))
            {
                if (hit.transform.name.Contains("oor")) Destroy(hit.transform.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    GameObject GetParent()
    {
        Transform parent = transform.parent;
        while (!parent.gameObject.name.Contains("Room"))
        {
            parent = parent.parent;
        }
        return parent.gameObject;
    }
}