using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Operation : MonoBehaviour
{
    public GameObject cube;
    float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x_camera_rotation = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float y_camera_rotation = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        cube.transform.Rotate(
             x_camera_rotation,  y_camera_rotation, 0.0f
        );
    }
}
