using System.Collections.Generic;
using Unity.InferenceEngine.Tokenization.Normalizers;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] float floorMoveSpeed = 5f;
    [SerializeField] List<GameObject> spwanPoints;
    [SerializeField] List<string> layerNames;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int number;
        number = Random.Range(0, spwanPoints.Count);
        this.transform.position = spwanPoints[number].transform.position;
    }

    void Update()
    {
        
    }
}
