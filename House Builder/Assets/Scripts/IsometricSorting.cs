using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IsometricSorting : MonoBehaviour
{
    [SerializeField] private int sortingMultiplier = 100;

    private SpriteRenderer spriteRenderer;
    private Camera cam;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        float depth = Vector3.Dot(transform.position, cam.transform.forward);

        spriteRenderer.sortingOrder =
    Mathf.RoundToInt(-(transform.position.x + transform.position.z) * 100);
    }
}