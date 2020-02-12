using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BttleCameraController : MonoBehaviour
{
    private bool isMove = false;
    private Vector3 startPos;
    private Vector3 endPos;

    private float scroll;

    private void Update()
    {
        CameraMove();
        CameraZoom();
    }

    private void CameraMove()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isMove = true;

            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("始発マウス座標" + startPos);
            Debug.Log("始発カメラ座標" + transform.position);
        }

        if (Input.GetMouseButton(1) && isMove)
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position -= endPos - startPos;

            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }


        if (Input.GetMouseButtonUp(1))
        {
            isMove = false;
        }
    }

    private void CameraZoom()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");

        Camera camera = this.GetComponent<Camera>();

        if(scroll > 0)
        {
            camera.orthographicSize -= 2;
        }
        if(scroll < 0)
        {
            camera.orthographicSize += 2;
        }
    }
}
