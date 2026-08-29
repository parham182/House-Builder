
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Floor : MonoBehaviour
{
    [SerializeField] float floorMoveSpeed = 5f;
    [SerializeField] List<string> layerNames;
    SpriteRenderer spriteRenderer;

    Transform target;
    Vector3 currentPos;

    // const string layerNameDown = "DownFloor";
    // const string layerNameUp = "UpFloor";

    bool movingToTarget;

    [SerializeField] float arriveThreshold = 0.01f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        movingToTarget = true;
        FloorManager.instance.canSpwan = false;
        target = FloorManager.instance.myStartPoint;
    }

    void Update()
    {
        if (target == null) return;

        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100);

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            floorMoveSpeed * Time.deltaTime
        );

        currentPos = transform.position;

        if (Vector3.Distance(transform.position, target.position) <= arriveThreshold)
        {
            movingToTarget = !movingToTarget;
            target = movingToTarget ? FloorManager.instance.myTargetPoint : FloorManager.instance.myStartPoint;
        }
        if (FloorManager.instance.currentFloor != this)
            return;

        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            if (Vector3.Distance(currentPos, FloorManager.instance.defaultPos.position) <= arriveThreshold)
            {
                transform.position = FloorManager.instance.defaultPos.position;
            }

            floorMoveSpeed = 0;
            FloorManager.instance.canSpwan = true;
            // spriteRenderer.sortingLayerName = layerNameDown;
            FloorManager.instance.floorCounter++;
        }
    }
}
