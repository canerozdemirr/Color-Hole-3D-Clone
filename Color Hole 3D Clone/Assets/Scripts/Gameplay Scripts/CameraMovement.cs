using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance { get; set; }

    private void Start()
    {
        Instance = this;
    }

    public void Movement()
    {
        transform.DOMoveZ(11.5f, 2f);
    }
}
