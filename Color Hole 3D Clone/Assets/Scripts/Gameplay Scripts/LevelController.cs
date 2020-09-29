using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [Header("UI Elements")] 
    public GameObject gameUi;
    public GameObject winUi; 
    public GameObject failUi;
    
    [Header("Buttons")] 
    public Button winButton; 
    public Button failButton;
    
    [Header("Cubes")]
    public List<GameObject> firstStageCubes;
    public List<GameObject> secondStageCubes;

    public static LevelController Instance { get; set; }
    public bool didFirstStageEnded, didLevelEnded, canTranslate, didLevelStarted, isGameOver;

    public Slider firstStageBar, secondStageBar;
    
    private void Awake()
    {
        firstStageCubes = GameObject.FindGameObjectsWithTag(Tags.FirstStageCatchableCubeTag).ToList();
        secondStageCubes = GameObject.FindGameObjectsWithTag(Tags.SecondStageCatchableCubeTag).ToList();
        Instance = this;
        gameUi = GameObject.FindGameObjectWithTag(Tags.GameUiTag);
        winUi = gameUi.transform.GetChild(1).gameObject;
        failUi = gameUi.transform.GetChild(2).gameObject;
        winButton = winUi.GetComponent<Button>();
        failButton = failUi.GetComponent<Button>();
        winButton.onClick.AddListener(ReloadTheScene);
        failButton.onClick.AddListener(ReloadTheScene);
        firstStageBar = gameUi.transform.GetChild(3).GetComponent<Slider>();
        firstStageBar.maxValue = firstStageCubes.Count;
        secondStageBar = gameUi.transform.GetChild(4).GetComponent<Slider>();
        secondStageBar.maxValue = secondStageCubes.Count;
    }

    private void Update()
    {
        //checking for the end of the first stage
        if (!didFirstStageEnded && firstStageCubes.Count == 0)
        {
            HoleMovement.Instance.canTranslate = true;
            HoleMovement.Instance.HorizontalMovement();
            didFirstStageEnded = true;
        }
        
        //checking for the end of the second stage
        if (!didLevelEnded && secondStageCubes.Count == 0)
        {
            winUi.SetActive(true);
            secondStageBar.gameObject.SetActive(false);
            firstStageBar.gameObject.SetActive(false);
            didLevelEnded = true;
        }
        
        //delivering a game over when the condition is met
        if (isGameOver && !failUi.activeInHierarchy)
        {
            failUi.SetActive(true);
            firstStageBar.gameObject.SetActive(false);
            secondStageBar.gameObject.SetActive(false);
        }
    }

    private void ReloadTheScene()
    {
        SceneManager.LoadScene(0);
    }
}
