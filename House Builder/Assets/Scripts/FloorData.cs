using UnityEngine;

[CreateAssetMenu(fileName = "FloorData", menuName = "Scriptable Objects/FloorData")]
public class FloorData : ScriptableObject
{
    [SerializeField] GameObject floorPrefab;
    [SerializeField] int floorNumber = 0;

    [Header("Default Position")]
    [SerializeField] public bool useDefaultPos;
    [SerializeField] Vector3 defaultPos;

    [Header("Spawn & Target Offsets")]
    [Tooltip("جابجایی موقعیت Spawn Point برای این طبقه (چپ/راست و جلو/عقب)")]
    [SerializeField] Vector3 spawnOffset = Vector3.zero;

    [Tooltip("جابجایی موقعیت Target Point برای این طبقه")]
    [SerializeField] Vector3 targetOffset = Vector3.zero;

    public GameObject FloorPrefab => floorPrefab;
    public int FloorNumber => floorNumber;
    public Vector3 DefaultPos => defaultPos;
    public Vector3 SpawnOffset => spawnOffset;
    public Vector3 TargetOffset => targetOffset;
}
