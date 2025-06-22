using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject panel;

    [SerializeField] private float rotateSpeed;

    private Vector3 previousPosition;
    private float zoom;
    private bool isRotating;
    private bool isInfoVisible;


    // Start is called before the first frame update
    void Start()
    {
        zoom = 10.0f;
        isRotating = false;
        AutoRotate();
        isInfoVisible = false;
    }


    public void AutoRotate()
    {
        //camera.transform.position = target.transform.position;
        //camera.transform.Translate(new Vector3(0, 0, -zoom));

        if (isRotating)
            AutoRotateStop();
        else
            AutoRotateStart();
    }
    public void ShowInfo()
    {
        if (isInfoVisible)
        {
            isInfoVisible = false;
            panel.SetActive(false);
        }
        else
        {
            isInfoVisible = true;
            panel.SetActive(true);
        }
    }

    private void AutoRotateStart()
    { isRotating = true; }

    private void AutoRotateStop()
    { isRotating = false; }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 direction = previousPosition - camera.ScreenToViewportPoint(Input.mousePosition);

            camera.transform.position = target.transform.position;
            camera.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            camera.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            camera.transform.Translate(new Vector3(0, 0, -zoom));

            //camera.transform.position = Vector3.MoveTowards(camera.transform.position, target.transform.position, zoom);
            //camera.transform.RotateAround(new Vector3(), new Vector3(1, 0, 0), direction.y * 180);
            //camera.transform.RotateAround(new Vector3(), new Vector3(0, 1, 0), -direction.x * 180);

            previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }

        
        // mouse wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            zoom = zoom - 0.1f;
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, target.transform.position, 0.1f);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            zoom = zoom + 0.1f;
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, previousPosition, -0.1f);
        }


        if(isRotating)
            camera.transform.RotateAround(target.transform.position, new Vector3(0, 1, 0), rotateSpeed);
    }
}
