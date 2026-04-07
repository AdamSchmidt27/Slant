using System.Collections;
using UnityEngine;

public class ParralaxLayer : MonoBehaviour
{
   public Transform cam;
    public float parallaxMultiplier = 0.5f;
    public float pixelsPerUnit = 100f;

    private Vector3 startPos;
    private Vector3 camStartPos;

    void Start()
    {
        if (cam == null) cam = Camera.main.transform;
        startPos = transform.position;
        camStartPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - camStartPos;

        Vector3 pos = startPos + new Vector3(delta.x * parallaxMultiplier, delta.y * parallaxMultiplier, 0f);

        float unit = 1f / pixelsPerUnit;
        pos.x = Mathf.Round(pos.x / unit) * unit;
        pos.y = Mathf.Round(pos.y / unit) * unit;

        transform.position = pos;
    }
}
