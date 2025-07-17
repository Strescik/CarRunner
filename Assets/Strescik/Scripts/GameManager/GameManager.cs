using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI newScoreText;
    [SerializeField] TextMeshProUGUI inGameGoldText;
    [SerializeField] TextMeshProUGUI totalGoldText;

    [SerializeField] GameObject fnishPanel;
    [SerializeField] GameObject gamePanel;

    [SerializeField] internal MovementPlayer player;

    private int point;
    private int gold;
    public float lvSpeed { get; private set; } = 2;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(instance);
        instance = this;

    }


    internal void AddGold()
    {
        gold++;
        inGameGoldText.text = gold.ToString();
    }

    internal void SetPoint(float playerPositionZ)
    {
        point = (gold * 10) + (int)(playerPositionZ * 5);
        scoreText.text = point.ToString();
    }
    internal void UpLvSpeed() => lvSpeed += .1f;

    public void ResetProperty()
    {
        point = 0;
        gold = 0;
        lvSpeed = 2;

        scoreText.text = "0";
        inGameGoldText.text = "0";

        player.transform.position = Vector3.zero;
    }


    internal void GameOver()
    {
        player.SetIsMoving(false);
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", point);
        }
        else if (point > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", point);
        }
        var totalGold = PlayerPrefs.GetInt("Gold") + gold;

        PlayerPrefs.SetInt("Gold", totalGold);
        PlayerPrefs.Save();

        totalGoldText.text = PlayerPrefs.GetInt("Gold").ToString();
        newScoreText.text = $"New Score\n{point.ToString()}";
        bestScoreText.text = $"Best Score\n {PlayerPrefs.GetInt("BestScore")}";



        fnishPanel.SetActive(true);
        gamePanel.SetActive(false);
    }
}
