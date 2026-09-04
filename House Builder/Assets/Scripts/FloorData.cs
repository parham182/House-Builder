using UnityEngine;

[CreateAssetMenu(fileName = "FloorData", menuName = "Scriptable Objects/FloorData")]
public class FloorData : ScriptableObject
{
    [SerializeField] GameObject floorPrefab;
    [SerializeField] int floorNumber = 0;
    [SerializeField] Vector3 defaultPos;
    [SerializeField] public bool useDefaultPos;
    
    public GameObject FloorPrefab => floorPrefab;
    public int FloorNumber => floorNumber;
    public Vector3 DefaultPos => defaultPos;
}
