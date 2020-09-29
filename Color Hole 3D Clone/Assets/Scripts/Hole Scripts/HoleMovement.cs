using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
    private Vector3 _mOffset;
    private float _zCoordinate, _yPositionAtStart;

    public Camera mainCamera;

    [Header("Hole Boundaries")] 
    public float xClamp;
    public float zClamp;

    public bool canTranslate;

    public static HoleMovement Instance { get; set; }

    private void Awake()
    {
        if (Camera.main) mainCamera = Camera.main;
        _yPositionAtStart = transform.position.y;
        Instance = this;
    }

    private void Update()
    {
        if (canTranslate || !LevelController.Instance.didLevelStarted || LevelController.Instance.isGameOver ||
            LevelController.Instance.didLevelEnded) return;
        HoleDrag();
    }
    
    private Vector3 GetMouseWorldPos()
    {
        var touchPoint = Input.mousePosition;
        touchPoint.z = _zCoordinate;
        return mainCamera.ScreenToWorldPoint(touchPoint);
    }

    //Hole Movement During The Stage
    private void HoleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var currentPosition = gameObject.transform.position;
            _zCoordinate = mainCamera.WorldToScreenPoint(currentPosition).z;
            _mOffset = currentPosition - GetMouseWorldPos();
        }

        if (Input.GetMouseButton(0))
        {
            //If we are in the first stage, use this transform formula
            if (!LevelController.Instance.didFirstStageEnded)
            {
                transform.position = new Vector3(Mathf.Clamp(GetMouseWorldPos().x + _mOffset.x, -xClamp, xClamp),
                    _yPositionAtStart,
                    Mathf.Clamp(GetMouseWorldPos().z + _mOffset.z, -zClamp, zClamp + 1f));
            }
            
            //If we are in the second stage, use this transform formula
            else
            {
                if (!canTranslate)
                    transform.position = new Vector3(Mathf.Clamp(GetMouseWorldPos().x + _mOffset.x, -xClamp, xClamp),
                        _yPositionAtStart,
                        Mathf.Clamp(GetMouseWorldPos().z + _mOffset.z, 14f, 19f));
            }
        }
    }

    public void HorizontalMovement()
    {
        transform.DOMoveX(0, 0.5f).OnComplete(VerticalMovement);
    }

    private void VerticalMovement()
    {
        CameraMovement.Instance.Movement();
        transform.DOMoveZ(14.5f, 2f).OnComplete(AllowMovement);
    }

    private void AllowMovement()
    {
        canTranslate = false;
    }
}