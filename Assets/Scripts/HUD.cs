using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI lapStatusText;

    public GameObject finishPanel;
    public TextMeshProUGUI finishPositionText;

    void Awake()
    {
        finishPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        lapStatusText.text = CheckpointSystem.Instance.lapsCompleted + "/" + LevelManager.Instance.totalLaps;
    }

    public void EndGame()
    {
        finishPositionText.text = "You finished in position " + LevelManager.Instance.playerHoverCraftModel.networkObject.racePosition;
        finishPanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}