
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] List<GameObject> floors;
    [SerializeField] List<GameObject> spawnPoints;
    [SerializeField] List<GameObject> targets;
    [SerializeField] public Transform defaultPos;
    [SerializeField] float hightValue = .5f;
    [SerializeField] public bool canSpwan;
    public Transform myStartPoint;
    public Transform myTargetPoint;
    public Floor currentFloor;
    public int floorCounter = 0;
    public int floorNumber = 0;
    int lastCounter = 0;
    public static FloorManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        canSpwan = true;
        floorNumber = 0;
    }

    void Update()
    {
        int floorState;
        if (floorNumber >= 1)
        {
            floorState = 1;
        }
        else
        {
            floorState = 0;
        }
        if (canSpwan)
        {
            int index = Random.Range(0, spawnPoints.Count);

            myStartPoint = spawnPoints[index].transform;
            myTargetPoint = targets[index].transform;

            GameObject newFloor = Instantiate(
                floors[floorState],
                myStartPoint.position,
                Quaternion.Euler(0, 45, 0)
            );

            currentFloor = newFloor.GetComponent<Floor>();

            canSpwan = false;
            floorNumber++;
        }

        if (floorCounter > lastCounter)
        {
            lastCounter = floorCounter;

            foreach (GameObject obj in spawnPoints)
            {
                obj.transform.position += new Vector3(0f, hightValue, 0f);
            }
            foreach (GameObject obj in targets)
            {
                obj.transform.position += new Vector3(0f, hightValue, 0f);
            }
            defaultPos.transform.position += new Vector3(0f, hightValue, 0f);
        }

    }
}



