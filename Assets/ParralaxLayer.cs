using System.Collections;
using UnityEngine;

public class ParralaxLayer : MonoBehaviour
{
  [Range(0f, 1f)] public float parallax = 0.15f;

    Transform cam;
    Vector3 lastCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
        StartCoroutine(ParallaxRoutine());
    }

    IEnumerator ParallaxRoutine()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame(); // camera has finished following

            Vector3 delta = cam.position - lastCamPos;
            transform.position += new Vector3(delta.x * parallax, delta.y * parallax, 0f);

            lastCamPos = cam.position;
        }
    }
}
