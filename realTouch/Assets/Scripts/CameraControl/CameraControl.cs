using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour {

    public Transform target;

    private float MouseWheelSensitivity = 15.0f;
    private int MouseZoomMin = 10;
    private int MouseZoomMax = 50;
    private float normalDistance = 3;

    private Vector3 normalized;

    private float xSpeed = 250.0f;
    private float ySpeed = 120.0f;

    private int yMinLimit = -20;
    private int yMaxLimit = 80;

    private float x = 0.0f;
    private float y = 0.0f;

    private Text ui;
   
    private Vector3 CameraTarget;
    void Start()
    {
        ui.transform.Find("");

        //CameraTarget = target.position;
        //if (target != null) 
        //transform.LookAt(target);

        //var angles = transform.eulerAngles;
        //x = angles.y;
        //y = angles.x;
    }

    void Update()
    {


        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x/Screen.width > 0.15f && Input.mousePosition.x/Screen.width < 0.85f)
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            normalized = (transform.position - CameraTarget).normalized;

            if (normalDistance >= MouseZoomMin && normalDistance <= MouseZoomMax)
            {
                normalDistance -= Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity;
            }
            if (normalDistance < MouseZoomMin)
            {
                normalDistance = MouseZoomMin;
            }
            if (normalDistance > MouseZoomMax)
            {
                normalDistance = MouseZoomMax;
            }
            transform.position = normalized * normalDistance;

        }

        var rotation = Quaternion.Euler(y, x, 0);

        var position = rotation * new Vector3(0.0f, 0.0f, -normalDistance) + CameraTarget;

        transform.position = position;

       // transform.position = Vector3.Slerp(this.transform.position, position, 0.09f);

        transform.LookAt(CameraTarget);

        Debug.Log(transform.Find("Label_Level").name);

    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    void OnEnable()
    {

        normalDistance = Vector3.Distance(transform.position, target.position);

        

        CameraTarget = target.position;
     
        transform.LookAt(target);

        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

       
    }
 
}
