using UnityEngine;

[CreateAssetMenu(fileName = "FloorData", menuName = "Scriptable Objects/FloorData")]
public class FloorData : ScriptableObject
{
    [SerializeField] GameObject floorPrefab;
    [SerializeField] int floorNumber = 0;

    [Header("Position Settings")]
    [SerializeField] public bool useDefaultPos;
    [SerializeField] Vector3 defaultPos;

    [Tooltip("اگر فعال باشد، علاوه بر X و Z روی محور Y هم اسنپ می‌شود")]
    [SerializeField] bool snapOnY = false;

    [Header("Spawn Offset")]
    [Tooltip("جابجایی چپ و راست (و جلو عقب) اسپان پوینت برای این طبقه")]
    [SerializeField] Vector3 spawnOffset = Vector3.zero;

    public GameObject FloorPrefab => floorPrefab;
    public int FloorNumber => floorNumber;
    public Vector3 DefaultPos => defaultPos;
    public bool SnapOnY => snapOnY;
    public Vector3 SpawnOffset => spawnOffset;
}
