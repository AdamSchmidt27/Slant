using UnityEngine;

public class pickupHover : MonoBehaviour
{
     public float hoverHeight = 0.2f;  
    public float hoverSpeed = 2f;      

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
