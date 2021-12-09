using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamera : MonoBehaviour
{
    private float rotX = 0;
    private Quaternion rotOriginal;
    public float velRot = 10;

    // Start is called before the first frame update
    void Start()
    {
        rotOriginal = transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rotX += Input.GetAxisRaw("Mouse Y") * velRot * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -60, 60);

        Quaternion rotLado = Quaternion.AngleAxis(rotX, Vector3.left);

        transform.localRotation = rotOriginal * rotLado;

    }
}
