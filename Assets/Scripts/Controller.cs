using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Linq;

using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    private Vector3 firstTouchPosition;
    private bool draggingMode = false;
    private bool permission = true;
    private bool isControllerOn = false;
    private bool isFinishScreen = false;
    private Ray ray;
    private Camera mainCamera;

    void Start()
    {
        OnController();
    }

    public void FinishScreen()
    {
        
    }

    public void FinishScreenOff()
    {
        isFinishScreen = false;
    }

    public void OnController()
    {
        isControllerOn = true;
        mainCamera = Camera.main; 
    }

    public void OffController()
    {
        isControllerOn = false;
    }
    private void MouseDown()
    {
         if (!isFinishScreen)
        {
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = 90.0f;
            screenPoint = mainCamera.ScreenToWorldPoint(screenPoint);
            firstTouchPosition =  screenPoint;
            GameManager.instance.BoardManager.Cannon.OnTouch(1, 0);
            draggingMode = true;
        }
    }
       
    private void MouseMove()
    {
        if (draggingMode)
        {
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = 90.0f;
            screenPoint = mainCamera.ScreenToWorldPoint(screenPoint);
            Vector3 offset = screenPoint - firstTouchPosition;
            
            Vector2 screenPointXZ = new Vector2(screenPoint.x ,screenPoint.z);
            Vector2 firstTouchPositionXZ = new Vector2(firstTouchPosition.x ,firstTouchPosition.z);
            
            float dot = Vector2.Angle(screenPointXZ, firstTouchPositionXZ);
            Vector3 cross = Vector3.Cross(screenPointXZ, firstTouchPositionXZ);
            if (cross.z < 0)
                dot = - dot;
            GameManager.instance.BoardManager.Cannon.OnTouch(offset.y/2, dot);
        }
    }

    private void MouseUp()
    {
        if (draggingMode)
        {
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = 90.0f;
            screenPoint = mainCamera.ScreenToWorldPoint(screenPoint);
            Vector3 offset = screenPoint - firstTouchPosition;
            Vector2 screenPointXZ = new Vector2(screenPoint.x ,screenPoint.z);
            Vector2 firstTouchPositionXZ = new Vector2(firstTouchPosition.x ,firstTouchPosition.z);
            
            float dot = Vector2.Angle(screenPointXZ, firstTouchPositionXZ);
            Vector3 cross = Vector3.Cross(screenPointXZ, firstTouchPositionXZ);
            if (cross.z < 0)
                dot = - dot;
            GameManager.instance.BoardManager.Cannon.OnUp(offset.y/2, dot);
            GameManager.instance.Controller.OffController();
        }
        draggingMode = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (isControllerOn)
        {
            RunController();
        }
    }
    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    void RunController()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_STANDALONE_WIN

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0) && permission)
            {
                MouseDown();
            }

            if (Input.GetMouseButton(0) && permission )
            {
                MouseMove();

            }
            
            if (Input.GetMouseButtonUp(0) && permission)
            {
                MouseUp();
            }

        }
        #endif

        if (!EventSystem.current.IsPointerOverGameObject() && !IsPointerOverUIObject())
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (permission)
                        {
                            MouseDown();
                        }
                        break;

                    case TouchPhase.Moved:
                        if (permission)
                        {
                            MouseMove();
                        }

                        break;

                    case TouchPhase.Ended:
                        if (permission)
                        {
                            MouseUp();
                        }
                        break;
                }
            }
        }

    }








}















