using System;
using System.Collections;
using Fusion;
using UnityEngine;
using TMPro;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public float dodgeSpeed = 10f;
    public float dodgeDuration = 0.2f;

    private TextMeshProUGUI pingText;
    private Rigidbody rb;
    private bool isDodging = false;
    private Vector3 originalScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        originalScale = transform.localScale;
        pingText = GameObject.Find("PingText").GetComponent<TextMeshProUGUI>();
    }

    public override void FixedUpdateNetwork()
    {
        pingText.text = $"{Math.Round(Runner.GetPlayerRtt(default) * 1000)}ms";
        if (!isDodging)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        Vector3 move = new Vector3(moveX, 0, moveZ) * currentSpeed * Runner.DeltaTime;
        if (move != Vector3.zero) {
            rb.MovePosition(transform.position + move);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Dodge(move));
        }
    }

    private IEnumerator Dodge(Vector3 direction)
    {
        isDodging = true;
        Vector3 dodgeMove = direction.normalized * dodgeSpeed;
        float startTime = Time.time;

        // Halve the player's height
        transform.localScale = new Vector3(originalScale.x, originalScale.y / 2, originalScale.z);

        while (Time.time < startTime + dodgeDuration)
        {
            rb.MovePosition(transform.position + dodgeMove * Runner.DeltaTime);
            yield return null;
        }

        // Reset the player's height
        transform.localScale = originalScale;
        isDodging = false;
    }
}
