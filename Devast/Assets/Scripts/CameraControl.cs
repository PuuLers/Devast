using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public float cameraSpeed;
    public float mouseOffsetAmount;
    public float cameraFollowZoneWidth;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.position;
    }
  void FixedUpdate()
{
    if (player != null)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = player.position + offset;        
        float deltaX = mousePos.x - player.position.x; // Разница по оси x
        float deltaY = mousePos.y - player.position.y; // Разница по оси y       
        if (Mathf.Abs(deltaX) < cameraFollowZoneWidth / 2 && Mathf.Abs(deltaY) < cameraFollowZoneWidth / 2)
        {
            targetPosition = player.position + offset;
        }
        else
        {
            Vector3 direction = new Vector3(deltaX, deltaY, 0f); // Учитываем y-компоненту в направлении
            targetPosition += direction * mouseOffsetAmount;

            float distanceToPlayer = Vector3.Distance(targetPosition, player.position);

            if (distanceToPlayer < cameraFollowZoneWidth / 2)
            {
                targetPosition = player.position + (targetPosition - player.position).normalized * cameraFollowZoneWidth / 2;
            }
            else if (distanceToPlayer > cameraFollowZoneWidth)
            {
                targetPosition = player.position + (targetPosition - player.position).normalized * cameraFollowZoneWidth;
            }
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}

    
}
