using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorManager : MonoBehaviour
{
    [SerializeField] List<FloorData> floors;
    [SerializeField] List<GameObject> spawnPoints;
    [SerializeField] List<GameObject> targets;
    [SerializeField] public Transform defaultPos;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] float hightValue = .5f;
    [SerializeField] float cameraHightValue = 5f;
    [SerializeField] float cameraSpeed = 5f;
    [SerializeField] public bool canSpwan;
    public Transform myStartPoint;
    public Transform myTargetPoint;
    public Floor currentFloor;
    public int floorCounter = 0;
    public int floorNumber = 0;
    float targetY = 0;
    float sum = 0;
    int lastCounter = 0;
    public static FloorManager instance;
    Vector3 defaultDefaultPos;
    List<Vector3> defaultSpawnPointPos;
    List<Vector3> defaultTargetPointPos;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        canSpwan = true;
        floorNumber = 0;
        targetY = mainCamera.transform.position.y;
        defaultDefaultPos = defaultPos.transform.position;

        // Fix: Initialize lists before using them
        defaultSpawnPointPos = new List<Vector3>();
        defaultTargetPointPos = new List<Vector3>();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            defaultSpawnPointPos.Add(spawnPoints[i].transform.position);
            defaultTargetPointPos.Add(targets[i].transform.position);
        }
    }

    void Update()
    {
        if (canSpwan)
        {
            int index = Random.Range(0, spawnPoints.Count);

            myStartPoint = spawnPoints[index].transform;
            myTargetPoint = targets[index].transform;

            FloorData selectedFloorData = floors[floorNumber];
            if (selectedFloorData.useDefaultPos)
            {
                // Fix: Properly update both spawn and target positions
                Vector3 spawnPos = spawnPoints[index].transform.position;
                spawnPos.y = selectedFloorData.DefaultPos.y;
                spawnPoints[index].transform.position = spawnPos;

                Vector3 targetPos = targets[index].transform.position;
                targetPos.y = selectedFloorData.DefaultPos.y;
                targets[index].transform.position = targetPos;

                defaultPos.transform.position = selectedFloorData.DefaultPos;
            }
            else
            {
                Vector3 _pos = defaultPos.position;
                _pos.x = defaultDefaultPos.x;
                _pos.z = defaultDefaultPos.z;
                defaultPos.position = _pos;

                Vector3 _pos1 = spawnPoints[index].transform.position;
                _pos1.x = defaultSpawnPointPos[index].x;
                _pos1.z = defaultSpawnPointPos[index].z;
                spawnPoints[index].transform.position = _pos1;

                // Also restore target point x/z for consistency
                Vector3 _pos2 = targets[index].transform.position;
                _pos2.x = defaultTargetPointPos[index].x;
                _pos2.z = defaultTargetPointPos[index].z;
                targets[index].transform.position = _pos2;
            }

            print(defaultPos.transform.position);

            GameObject newFloor = Instantiate(
                selectedFloorData.FloorPrefab,
                myStartPoint.position,
                Quaternion.Euler(0, 45, 0)
            );
            currentFloor = newFloor.GetComponent<Floor>();

            currentFloor.SetFloorData(selectedFloorData);

            canSpwan = false;
            floorNumber++;
        }

        if (floorNumber >= floors.Count)
        {
            print("You Win");
            Invoke("reloadScene", 10f);
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

            targetY += hightValue;
        }

        Vector3 pos = mainCamera.transform.position;

        pos.y = Mathf.Lerp(
            pos.y,
            targetY,
            cameraSpeed * Time.deltaTime
        );

        mainCamera.transform.position = pos;
    }

    void reloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
