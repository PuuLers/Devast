using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public float cameraSpeed = 2.0f;
    public float mouseOffsetAmount = 1.0f;
    public float maxDistanceFromPlayer = 5.0f; // Максимальное расстояние от игрока

    private Vector3 offset;
    private Vector3 targetPosition;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            targetPosition = player.position + offset;

            Vector3 direction = mousePos - player.position;
            targetPosition += direction * mouseOffsetAmount;

            float distanceToPlayer = Vector3.Distance(targetPosition, player.position);
            if (distanceToPlayer > maxDistanceFromPlayer)
            {
                targetPosition = player.position + (targetPosition - player.position).normalized * maxDistanceFromPlayer;
            }

            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        }
    }
}