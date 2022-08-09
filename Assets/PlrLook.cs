using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrLook : MonoBehaviour
{
    public Transform player;

    public float mouseSensitivity = 100f;

    private float x, y = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //input
        x += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        y += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        //clamp
        x = Mathf.Clamp(x, -90, 90);

        //rotation
        transform.localRotation = Quaternion.Euler(-x, 0, 0);
        player.transform.localRotation = Quaternion.Euler(0, y, 0);

        //cursor locking/unlocking
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
