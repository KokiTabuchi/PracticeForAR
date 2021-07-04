using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCamera_Looking : MonoBehaviour
{
    public Transform verRot;
    public Transform horRot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        verRot.transform.Rotate(0, -X_Rotation * 30f, 0);
        horRot.transform.Rotate(-Y_Rotation * 15f, 0, 0);
    }
}
