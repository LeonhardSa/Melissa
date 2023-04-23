using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RobotState
{
    Patrol,
    Control
}

public enum RobotType
{
    Melee,
    Ranged
}

public enum RobotCluster
{
    Enemy,
    Minion,
    Adrift
}

public class RobotRepository : MonoBehaviour
{
    static private ComponentStorage componentStorage;
    static public void SetComponentStorage(ComponentStorage cs)
    {
        componentStorage = cs;
    }

    public List<GameObject> minionList = new List<GameObject>();
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> adriftList = new List<GameObject>();

    public List<Material> robotMaterialList = new List<Material>();

    /// <summary>
    /// Adds a robot to the list of his RobotCluster State. 
    /// </summary>
    /// <param name="robot">The enemy robot that gets add.</param>
    public void RegisterRobot(GameObject robot)
    {
        if(robot.GetComponent<RobotBehaviour>().Cluster == RobotCluster.Minion)
            minionList.Add(robot);
        else if(robot.GetComponent<RobotBehaviour>().Cluster == RobotCluster.Enemy)
            enemyList.Add(robot);
        else
            adriftList.Add(robot);
    }

    /// <summary>
    /// Removes a robot from the list that he belongs to.
    /// </summary>
    /// <param name="robot">The robot that gets removed.</param>
    public void RemoveRobot(GameObject robot)
    {
        if (robot.GetComponent<RobotBehaviour>().Cluster == RobotCluster.Minion)
        {
            foreach (GameObject rob in minionList)
            {
                if (rob == robot)
                {
                    minionList.Remove(rob);
                    break;
                }
            }
        }
        else if(robot.GetComponent<RobotBehaviour>().Cluster == RobotCluster.Enemy)
        {
            foreach (GameObject rob in enemyList)
            {
                if (rob == robot)
                {
                    enemyList.Remove(rob);
                    break;
                }
            }
        }
        else
        {
            foreach (GameObject rob in adriftList)
            {
                if (rob == robot)
                {
                    adriftList.Remove(rob);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Removes robot of his current "team" list. Replaces the Tag, Layer and RobotCluster State to "Minion" and adds the robot to the minion list.
    /// </summary>
    /// <param name="robot">The enemy robot that switches the cluster.</param>
    public void SwitchCluster(GameObject robot)
    {
        ChangeColorScheme(robot);
        RemoveRobot(robot);
        robot.tag = "Minion";
        robot.layer = LayerMask.NameToLayer("Minion");
        RobotBehaviour robotBehaviour = robot.GetComponent<RobotBehaviour>();
        robotBehaviour.Cluster = RobotCluster.Minion;
        robotBehaviour.GetComponent<RobotBehaviour>().Projectile = componentStorage.FriendlyProjectile;
        RegisterRobot(robot);
    }

    /// <summary>
    /// Requests a list of all minions robots in the scene.
    /// </summary>
    /// <returns>Returns a GameObject List of all minions in the scene.</returns>
    public List<GameObject> ReqeustMinionList()
    {
        return minionList;
    }

    /// <summary>
    /// Requests a list of all enemies robots in the scene.
    /// </summary>
    /// <returns>Returns a GameObject List of all enemies in the scene.</returns>
    public List<GameObject> RequestEnemyList()
    {
        return enemyList;
    }

    /// <summary>
    /// Requests a list of all adrift robots in the scene.
    /// </summary>
    /// <returns>Returns a GameObject List of all enemies in the scene.</returns>
    public List<GameObject> ReqeustAdriftList()
    {
        return adriftList;
    }

    /// <summary>
    /// For robots only! Goes through every child object and replaces all materials of the robot with the minion version.
    /// </summary>
    private void ChangeColorScheme(GameObject obj)
    {

        if (obj.GetComponent<Renderer>() != null)
        {
            if (obj.name == "Head" || obj.name.Contains("Panel"))
            {
                obj.GetComponent<Renderer>().material = robotMaterialList[0];
            }
            else if (obj.name == "Axel" || obj.name.Contains("Gun"))
            {
                obj.GetComponent<Renderer>().material = robotMaterialList[1];
            }
            else if (obj.name == "HeadLight")
            {
                obj.GetComponent<Renderer>().material = robotMaterialList[2];
            }
            else if (obj.name == "Thruster" || obj.name == "Saw")
            {
                obj.GetComponent<Renderer>().material = robotMaterialList[3];
            }
        }

        if (GetComponentsInChildren<Transform>().Length>0)
        {
            for (int c = 0; c < obj.transform.childCount; c++)
            {
                ChangeColorScheme(obj.transform.GetChild(c).gameObject);
            }
        }
    }
}
