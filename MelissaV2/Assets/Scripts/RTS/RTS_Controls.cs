using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RTS_Controls : MonoBehaviour
{

    public GameObject SendActiveParticle;
    public GameObject SendPassiveParticle;

    Camera cam;
    Vector2 startPos = new Vector3();

    void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(SendActiveParticle, new Vector3(hit.point.x, 0.1f, hit.point.z), Quaternion.identity);
                foreach(GameObject robot in cam.GetComponent<RobotRepository>().ReqeustMinionList())
                {
                    robot.GetComponent<RobotBehaviour>().State = RobotState.Patrol;
                    robot.GetComponent<RobotBehaviour>().RTSPatrolGoal = hit.point;
                }
            }
        } else if(Input.GetKeyDown(KeyCode.R))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(SendPassiveParticle, new Vector3(hit.point.x, 0.1f, hit.point.z), Quaternion.identity);
                foreach (GameObject robot in cam.GetComponent<RobotRepository>().ReqeustMinionList())
                {
                    robot.GetComponent<RobotBehaviour>().State = RobotState.Control;
                    robot.GetComponent<RobotBehaviour>().RTSPatrolGoal = hit.point;
                }
            }
        }
    }
}
