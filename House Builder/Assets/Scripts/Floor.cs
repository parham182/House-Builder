using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Floor : MonoBehaviour
{
    [SerializeField] float floorMoveSpeed = 5f;
    [SerializeField] List<string> layerNames;
    [SerializeField] float arriveThreshold = 0.01f;

    [Header("Snap Settings")]
    [SerializeField] float snapTolerance = 0.45f; // فاصله مجاز برای اسنپ شدن (قابل تنظیم)

    SpriteRenderer spriteRenderer;
    Transform target;
    Vector3 currentPos;
    private FloorData floorData;
    bool movingToTarget;

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

        // پشتیبانی از تاچ و موس (برای تست در ادیتور)
        bool pressed = false;
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            pressed = true;
        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            pressed = true;

        if (pressed)
        {
            TrySnapAndPlace();
        }
    }

    void TrySnapAndPlace()
    {
        Vector3 perfectPos = FloorManager.instance.defaultPos.position;
        bool shouldSnap = false;

        if (floorData != null && floorData.SnapOnY)
        {
            // اسنپ کامل روی هر سه محور
            float dist = Vector3.Distance(currentPos, perfectPos);
            if (dist <= snapTolerance)
            {
                transform.position = perfectPos;
                shouldSnap = true;
            }
        }
        else
        {
            // فقط اسنپ افقی (X و Z) - ارتفاع فعلی حفظ می‌شود
            float horizontalDist = Vector2.Distance(
                new Vector2(currentPos.x, currentPos.z),
                new Vector2(perfectPos.x, perfectPos.z)
            );

            if (horizontalDist <= snapTolerance)
            {
                Vector3 snapped = perfectPos;
                snapped.y = currentPos.y; // ارتفاع فعلی رو نگه دار
                transform.position = snapped;
                shouldSnap = true;
            }
        }

        // در هر صورت حرکت رو متوقف کن و طبقه بعدی رو اجازه بده
        floorMoveSpeed = 0;
        FloorManager.instance.canSpwan = true;
        FloorManager.instance.floorCounter++;
    }

    public void SetFloorData(FloorData data)
    {
        floorData = data;
    }
}
