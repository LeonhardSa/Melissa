using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGen : MonoBehaviour
{

    public List<GameObject> Room;
    public List<GameObject> RoomEnd;
    public List<GameObject> RoomI;
    public List<GameObject> RoomL;
    public List<GameObject> RoomT;
    public List<GameObject> RoomX;
    public List<GameObject> Corridor;
    private GameObject roomElement;

    void Start()
    {
        string[,] dungeon = new string[7, 5];
        int[,] dungeonCheck = new int[7, 5];

        int curX = 1;
        int curY = 2;

        int roomCount = 1;

        for (int l = 0; l < dungeon.GetLength(0); l++)
        {
            for (int w = 0; w < dungeon.GetLength(1); w++)
            {
                if (l == 0 || l == 6)
                {
                    dungeonCheck[l, w] = -1;
                }
                else
                {
                    dungeonCheck[l, w] = 0;
                }
                dungeon[l, w] = "";

                if (l == curX - 1 && w == curY)
                {
                    dungeonCheck[l, w] = 1;
                    dungeon[l, w] = "e";
                }
                else if (l == curX && w == curY)
                {
                    dungeonCheck[curX, curY] = 0;
                    dungeon[l, w] = "w";
                }
                else if (l == 6 && w == curY)
                {
                    dungeonCheck[l, w] = 27;
                    dungeon[l, w] = "w";
                }
                else if (l == 5 && w == curY)
                {
                    dungeonCheck[l, w] = 0;
                    dungeon[l, w] = "e";
                }
            }
        }

        int direction;
        bool goBack = false;
        int biggestClosestNum = 0;
        int biggestClosestSaveSmallerNum = biggestClosestNum;
        bool check = false;
        while (check == false)
        {
            biggestClosestNum = 0;
            biggestClosestSaveSmallerNum = biggestClosestNum;
            check = true;
            goBack = false;

            if (curY == 0)
            {
                if (dungeonCheck[curX, curY + 1] != 0 && dungeonCheck[curX - 1, curY] != 0 && dungeonCheck[curX + 1, curY] != 0) goBack = true;
            }
            else if (curY == 4)
            {
                if (dungeonCheck[curX, curY - 1] != 0 && dungeonCheck[curX - 1, curY] != 0 && dungeonCheck[curX + 1, curY] != 0) goBack = true;
            }
            else
            {
                if (dungeonCheck[curX, curY - 1] != 0 && dungeonCheck[curX, curY + 1] != 0 && dungeonCheck[curX - 1, curY] != 0 && dungeonCheck[curX + 1, curY] != 0) goBack = true;
            }

            if (goBack == true)
            {
                biggestClosestNum = dungeonCheck[curX + 1, curY];
                if (biggestClosestNum > dungeonCheck[curX, curY]) biggestClosestNum = biggestClosestSaveSmallerNum;
                biggestClosestSaveSmallerNum = biggestClosestNum;
                biggestClosestNum = Mathf.Max(biggestClosestNum, dungeonCheck[curX - 1, curY]);
                if (biggestClosestNum > dungeonCheck[curX, curY]) biggestClosestNum = biggestClosestSaveSmallerNum;
                if (curY == 0)
                {
                    biggestClosestSaveSmallerNum = biggestClosestNum;
                    biggestClosestNum = Mathf.Max(biggestClosestNum, dungeonCheck[curX, curY + 1]);
                    if (biggestClosestNum > dungeonCheck[curX, curY]) biggestClosestNum = biggestClosestSaveSmallerNum;
                    if (dungeonCheck[curX, curY + 1] == biggestClosestNum)
                    {
                        curY++;
                    }
                }
                else if (curY == 4)
                {
                    biggestClosestSaveSmallerNum = biggestClosestNum;
                    biggestClosestNum = Mathf.Max(biggestClosestNum, dungeonCheck[curX, curY - 1]);
                    if (biggestClosestNum > dungeonCheck[curX, curY]) biggestClosestNum = biggestClosestSaveSmallerNum;
                    if (dungeonCheck[curX, curY - 1] == biggestClosestNum)
                    {
                        curY--;
                    }
                }
                else
                {
                    biggestClosestSaveSmallerNum = biggestClosestNum;
                    biggestClosestNum = Mathf.Max(biggestClosestNum, dungeonCheck[curX, curY + 1]);
                    if (biggestClosestNum > dungeonCheck[curX, curY]) biggestClosestNum = biggestClosestSaveSmallerNum;
                    biggestClosestSaveSmallerNum = biggestClosestNum;
                    biggestClosestNum = Mathf.Max(biggestClosestNum, dungeonCheck[curX, curY - 1]);
                    if (biggestClosestNum > dungeonCheck[curX, curY]) biggestClosestNum = biggestClosestSaveSmallerNum;
                    if (dungeonCheck[curX, curY - 1] == biggestClosestNum)
                    {
                        curY--;
                    }
                    else if (dungeonCheck[curX, curY + 1] == biggestClosestNum)
                    {
                        curY++;
                    }
                }

                if (dungeonCheck[curX - 1, curY] == biggestClosestNum)
                {
                    curX--;
                }
                else if (dungeonCheck[curX + 1, curY] == biggestClosestNum)
                {
                    curX++;
                }

                goBack = false;
                check = false;
                continue;
            }

            direction = Random.Range(0, 4);

            switch (direction)
            {
                case 0:
                    if (curY > 0)
                    {
                        if (dungeonCheck[curX, curY - 1] == 0)
                        {
                            roomCount++;
                            if (dungeonCheck[curX, curY] == 0) dungeonCheck[curX, curY] = roomCount;
                            dungeon[curX, curY] += "n";
                            curY--;
                            dungeonCheck[curX, curY] = roomCount + 1;
                            dungeon[curX, curY] += "s";
                        }
                    }
                    break;
                case 1:
                    if (dungeonCheck[curX + 1, curY] == 0)
                    {
                        roomCount++;
                        if (dungeonCheck[curX, curY] == 0) dungeonCheck[curX, curY] = roomCount;
                        dungeon[curX, curY] += "e";
                        curX++;
                        dungeonCheck[curX, curY] = roomCount + 1;
                        dungeon[curX, curY] += "w";
                    }
                    break;
                case 2:
                    if (curY < 4)
                    {
                        if (dungeonCheck[curX, curY + 1] == 0)
                        {
                            roomCount++;
                            if (dungeonCheck[curX, curY] == 0) dungeonCheck[curX, curY] = roomCount;
                            dungeon[curX, curY] += "s";
                            curY++;
                            dungeonCheck[curX, curY] = roomCount + 1;
                            dungeon[curX, curY] += "n";
                        }
                    }
                    break;
                case 3:
                    if (dungeonCheck[curX - 1, curY] == 0)
                    {
                        roomCount++;
                        if (dungeonCheck[curX, curY] == 0) dungeonCheck[curX, curY] = roomCount;
                        dungeon[curX, curY] += "w";
                        curX--;
                        dungeonCheck[curX, curY] = roomCount + 1;
                        dungeon[curX, curY] += "e";
                    }
                    break;
            }

            foreach (int r in dungeonCheck)
            {
                if (r == 0)
                {
                    check = false;
                    break;
                }
            }

        }

        int rndZ = 0;
        while (rndZ == 0)
        {
            int rndX = Random.Range(1, dungeon.GetLength(0) - 1);
            int rndY = Random.Range(0, dungeon.GetLength(1) - 0);

            if (!dungeon[rndX, rndY].Contains("n") && rndY > 0)
            {
                dungeon[rndX, rndY] += "n";
                dungeon[rndX, rndY - 1] += "s";
            }
            if (!dungeon[rndX, rndY].Contains("s") && rndY < dungeon.GetLength(1) - 1)
            {
                dungeon[rndX, rndY] += "s";
                dungeon[rndX, rndY + 1] += "n";
            }
            if (!dungeon[rndX, rndY].Contains("e") && rndX < dungeon.GetLength(0) - 2)
            {
                dungeon[rndX, rndY] += "e";
                dungeon[rndX + 1, rndY] += "w";
            }
            if (!dungeon[rndX, rndY].Contains("w") && rndX > 1)
            {
                dungeon[rndX, rndY] += "w";
                dungeon[rndX - 1, rndY] += "e";
            }

            rndZ = Random.Range(0, 2);
        }



        for (int w = 0; w < dungeon.GetLength(1); w++)
        {
            for (int l = 0; l < dungeon.GetLength(0); l++)
            {
                if (dungeon[l, w] == "") continue;

                int rndRotation = Random.Range(0, 5);
                int rndRoom = Random.Range(0, 3);

                switch (dungeon[l, w].Length)
                {
                    case 1:

                        if (dungeonCheck[l, w] == 1)
                        {
                            roomElement = Instantiate(RoomEnd[RoomEnd.Count - 2], new Vector3(l * 60f, 0f, -w * 60f), Quaternion.identity);
                        }
                        else if (dungeonCheck[l, w] == 27)
                        {
                            roomElement = Instantiate(RoomEnd[RoomEnd.Count - 1], new Vector3(l * 60f, 0f, -w * 60f), Quaternion.identity);
                        }
                        else roomElement = Instantiate(RoomEnd[rndRoom], new Vector3(l * 60f, 0f, -w * 60f), Quaternion.identity);
                        roomElement.name = "RoomEnd";
                        break;
                    case 2:
                        if (dungeon[l, w].Contains("s") && dungeon[l, w].Contains("n") || dungeon[l, w].Contains("w") && dungeon[l, w].Contains("e"))
                        {
                            roomElement = Instantiate(RoomI[rndRoom], new Vector3(l * 60f, 0f, -w * 60f), Quaternion.identity);
                            roomElement.name = "RoomIShape";
                        }
                        else
                        {
                            roomElement = Instantiate(RoomL[rndRoom], new Vector3(l * 60f, 0f, -w * 60f), Quaternion.identity);
                            roomElement.name = "RoomLShape";
                        }
                        break;
                    case 3:
                        roomElement = Instantiate(RoomT[rndRoom], new Vector3(l * 60f, 0f, -w * 60f), Quaternion.identity);
                        roomElement.name = "RoomTShape";
                        break;
                    case 4:
                        roomElement = Instantiate(RoomX[rndRoom], new Vector3(l * 60f, 0f, -w * 60f), Quaternion.identity);
                        roomElement.name = "RoomXShape";
                        break;
                }

                roomElement.name += " " + dungeon[l, w];

                if (dungeon[l, w].Contains("n"))
                {
                    if (dungeon[l, w].Contains("w"))
                    {
                        if (dungeon[l, w].Contains("s"))
                        {
                            if (dungeon[l, w].Contains("e"))
                            {
                                roomElement.transform.Rotate(0, rndRotation * 90, 0);
                            }
                            else
                            {
                                roomElement.transform.Rotate(0, 90, 0);
                            }
                        }
                        else
                        {
                            roomElement.transform.Rotate(0, 180, 0);
                        }
                    }
                    else if (dungeon[l, w].Contains("e"))
                    {
                        roomElement.transform.Rotate(0, 270, 0);
                    }
                    else if (dungeon[l, w].Contains("s"))
                    {
                        roomElement.transform.Rotate(0, 180 + rndRotation * 180, 0);
                    }
                    else
                    {
                        roomElement.transform.Rotate(0, 180, 0);
                    }
                }
                else if (dungeon[l, w].Contains("w"))
                {
                    if (dungeon[l, w].Contains("e") && dungeon[l, w].Contains("s"))
                    {
                        roomElement.transform.Rotate(0, 0, 0);
                    }
                    else if (dungeon[l, w].Contains("e"))
                    {
                        roomElement.transform.Rotate(0, 90 + rndRotation * 180, 0);
                    }
                    else
                    {
                        roomElement.transform.Rotate(0, 90, 0);
                    }
                }
                else if (dungeon[l, w] == "e")
                {
                    roomElement.transform.Rotate(0, 270, 0);
                }
                else
                {
                    roomElement.transform.Rotate(0, 0, 0);
                }

                rndRotation = Random.Range(0, 5);
                rndRoom = Random.Range(0, 3);

                if (dungeon[l, w].Contains("e"))
                {
                    roomElement = Instantiate(Corridor[rndRoom], new Vector3(l * 60f + 30f, 0f, -w * 60f), Quaternion.identity);
                    roomElement.transform.Rotate(0, 90 + 180 * rndRotation, 0);
                    roomElement.name = "Corridor";
                }
                if (dungeon[l, w].Contains("s"))
                {
                    roomElement = Instantiate(Corridor[rndRoom], new Vector3(l * 60f, 0f, -w * 60f - 30f), Quaternion.identity);
                    roomElement.transform.Rotate(0, 180 * rndRotation, 0);
                    roomElement.name = "Corridor";
                }
            }
        }

        FindObjectOfType<LoadingScreenStates>().StartLoading();
    }
}
