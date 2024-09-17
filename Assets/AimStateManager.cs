using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; // Make sure to include this

public class AimStateManager : MonoBehaviour
{
    [SerializeField] float mouseSense = 1;
    float xAxis, yAxis;
    [SerializeField] Transform camFollowPos;
    [SerializeField] CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine virtual camera
    [SerializeField] float fightModeFOV = 50f; // FOV in fight mode
    [SerializeField] float normalModeFOV = 70f; // FOV in normal mode

    private const float xForFight = 0.6f;
    private const float yForFight = 0f;
    private const float xForWalk = 1.2f;
    private const float yForWalk = 0.5f;
    
    private MovementStateManager movementStateManager;
    void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("Cinemachine Virtual Camera is not assigned!");
        }

        // Get the instance
        movementStateManager = GetComponent<MovementStateManager>();
        // Set the turn to neutral
        movementStateManager.anim.SetFloat("Turn", 0.5f);
    }

    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        CalculateCharacterIdleTurn();
    }

    public void EnterFightMode()
    {
        // Set the FOV to fight mode (50)
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.FieldOfView = fightModeFOV;

            camFollowPos.transform.localPosition = new Vector3(xForFight, yForFight, 0f);
        }
    }

    public void ExitFightMode()
    {
        // Set the FOV to normal mode (70)
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.FieldOfView = normalModeFOV;


            camFollowPos.transform.localPosition = new Vector3(xForWalk, yForWalk, 0f);
        }
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    private void CalculateCharacterIdleTurn()
    {
        // Get the horizontal mouse movement (X-axis)
        float mouseDelta = Input.GetAxisRaw("Mouse X") * mouseSense;

        // The turn will be 0 for idle, we add the mouse delta to it
        float turn = mouseDelta;

        // Clamp the turn value between -1 (left turn) and 1 (right turn)
        turn = Mathf.Clamp(turn, -1f, 1f);

        // Update the animator's "Turn" parameter
        if (movementStateManager != null && movementStateManager.anim != null)
        {
            movementStateManager.anim.SetFloat("Turn", turn);
        }
    }

}
