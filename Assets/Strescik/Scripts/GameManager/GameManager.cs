using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] TextMeshProUGUI skorText;

    private int point;
    private int gold;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(instance);
        instance = this;

    }


    internal void AddGold() => gold++;
    internal void SetPoint(float playerPositionZ)
    {
        point = (gold * 10) + (int)(playerPositionZ * 5);
        skorText.text = point.ToString();
    }
    internal void ResetProperty()
    {
        point = 0;
        gold = 0;
    }
}
