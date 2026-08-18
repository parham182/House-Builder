using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Floor : MonoBehaviour
{
    [SerializeField] float floorMoveSpeed = 5f;
    [SerializeField] List<GameObject> spawnPoints;
    [SerializeField] List<GameObject> targets;
    [SerializeField] List<string> layerNames;
    [SerializeField] GameObject floorPrefab;
    SpriteRenderer spriteRenderer;

    Transform myStartPoint;   // اسپاون پوینت اختصاصی این ابجکت
    Transform myTargetPoint;  // تارگت اختصاصی این ابجکت (هم‌ایندکس با اسپاون پوینت)
    Transform target;         // مقصد فعلی حرکت (یکی از دو تا بالا)

    bool movingToTarget; // true یعنی داره میره سمت myTargetPoint، false یعنی داره برمی‌گرده سمت myStartPoint

    [SerializeField] float arriveThreshold = 0.01f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spawnPoints == null || targets == null || spawnPoints.Count == 0 || spawnPoints.Count != targets.Count)
        {
            Debug.LogError("Floor: spawnPoints و targets باید پر باشن و تعدادشون برابر باشه.");
            enabled = false;
            return;
        }

        int index = Random.Range(0, spawnPoints.Count);

        myStartPoint = spawnPoints[index].transform;
        myTargetPoint = targets[index].transform;

        Instantiate(floorPrefab, myStartPoint.transform.position, Quaternion.identity);

        movingToTarget = true;
        target = myTargetPoint;
    }

    void Update()
    {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            floorMoveSpeed * Time.deltaTime
        );

        // اگه به مقصد فعلی رسید، جهت رو برعکس کن (بین استارت‌پوینت و تارگت خودش لوپ بزن)
        if (Vector3.Distance(transform.position, target.position) <= arriveThreshold)
        {
            movingToTarget = !movingToTarget;
            target = movingToTarget ? myTargetPoint : myStartPoint;
        }
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            floorMoveSpeed = 0;
        }
    }
}