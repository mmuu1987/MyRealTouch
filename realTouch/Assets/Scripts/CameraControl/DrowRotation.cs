using UnityEngine;
using System.Collections;

public class DrowRotation : MonoBehaviour
{

    public float x;
    public float y;
    public float z;
    public float rotationSpeed = 2.0F;


    // Use this for initialization
    void Start()
    {
        x = 180;
        


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            x -= Input.GetAxis("Mouse X") * rotationSpeed * 0.02f;
            y += Input.GetAxis("Mouse Y") * rotationSpeed * 0.02f;
        }

        var rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.time * 0.005f);



    }
   static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
