using UnityEngine;

public class UpandDown : MonoBehaviour
{
    public float moveSpeed = 100.0f; // Speed of the movement
    public float moveDistance = 150.0f; // Distance the plane moves up and down
    private float startY; // Initial Y position

    void Start()
    {
        startY = transform.position.y; // Store the initial Y position
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave
        float newY = startY + Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        // Update the position of the plane
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
