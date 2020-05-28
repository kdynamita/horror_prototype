using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    #region Variables

    [SerializeField] private bool overrideControl = false;

    //Movement Variables
    [Header("Movement")]
    [SerializeField] private float walkingSpeed = 5f;
    [SerializeField] private float runningSpeed = 8f;

    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    private float speed = 5f;

    //Camera Variables
    [Header("Camera")]
    [SerializeField] private bool invertX = true;
    [SerializeField] private bool invertY = false;
    [Range(0.5f, 10f)]
    [SerializeField] private float sensitivity = 1f;

    private float mouseInputX = 0f;
    private float mouseInputY = 0f;
    private Transform cam;

    [HideInInspector] public bool isRunning = false;
    public bool isMoving = false;

    #endregion

    private void Awake()
    {
        EntityManager.Instance.SetMainCamera(GetComponentInChildren<Camera>().transform);
        EntityManager.Instance.SetPlayer(transform);
        cam = EntityManager.Instance.GetMainCamera();
    }

    void Update()
    {
        if (!overrideControl)
        {
            MovePlayer();
            CameraControl();
            Wallpaper();
        }
    }

    private void OnGUI()
    {
        RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.forward, out hit, 5f);

        if (hit.transform != null)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wallpaper"))
            {
                GUI.Label(new Rect(Screen.width / 2 - 200 / 2, Screen.height / 2, 200, 50), "Peel Wallpaper");
            }
        }

    }

    private void MovePlayer()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            
            speed = runningSpeed;
            isRunning = true;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            speed = walkingSpeed;
            isRunning = false;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0f || verticalInput != 0f)
        {
            isMoving = true;
            transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime, transform);
        }
        else
        {
            isMoving = false;
        }
    }

    private void CameraControl()
    {
        mouseInputX = Input.GetAxis("Mouse X") * sensitivity;
        mouseInputY = Input.GetAxis("Mouse Y") * sensitivity;

        if (!invertX)
        {
            mouseInputX *= -1f;
        }

        if (!invertY)
        {
            mouseInputY *= -1f;
        }

        transform.Rotate(0f, mouseInputX, 0f);

        cam.localRotation = Quaternion.Euler(new Vector3(cam.localRotation.eulerAngles.x + mouseInputY, 0f, 0f));
        //cam.Rotate(mouseInputY, 0f, 0f);
    }

    private void Wallpaper()
    {
        RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.forward, out hit, 5f);

        if (hit.transform != null)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wallpaper") && Input.GetMouseButtonDown(0))
            {
                hit.transform.GetComponent<Wallpaper>().PlayAnimation();
            }
        }
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public void OverrideControl(bool b)
    {
        overrideControl = b;
    }
}
