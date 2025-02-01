using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public Vector3 offset; // Offset from the player position
    public Vector3 tiltAngle = new Vector3(45, 0, 0); // Tilt angle for the camera

    private void LateUpdate()
    {
        // Set the camera position to the player's position plus the offset
        transform.position = player.position + offset;

        // Set the camera rotation to the tilt angle
        transform.rotation = Quaternion.Euler(tiltAngle);

        // Optionally, you can set the camera to look at the player
        transform.LookAt(player);
    }
}