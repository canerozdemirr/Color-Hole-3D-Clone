using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStarter : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        LevelController.Instance.didLevelStarted = true;
        gameObject.SetActive(false);
        LevelController.Instance.firstStageBar.gameObject.SetActive(true);
        LevelController.Instance.secondStageBar.gameObject.SetActive(true);
    }
}
