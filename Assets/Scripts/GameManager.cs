using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject startPanel;
    public GameObject playPanel;
    public GameObject endPanel;
    public bool isGameStarted = false;
    public int gas = 100;

    private TMP_Text gasText;
    private float _gasTimer = 0f;  // 가스 소모 타이머

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        ShowStartPanel();
    }

    void Update()
    {
        if (!isGameStarted) return;
        // 1초에 10gas 소모
        _gasTimer += Time.deltaTime;
        if (_gasTimer >= 1f)
        {
            gas -= 10;
            _gasTimer = 0f;
            UpdateGasDisplay();
        }
        
        if (gas <= 0)
        {
            gas = 0;
            GameOver();
        }
    }
    
    public void UpdateGasDisplay()
    {
        gasText.text = "Gas: " + gas.ToString();
    }

    public void StartGame()
    {
        ResetGame();
        
        startPanel.SetActive(false);
        endPanel.SetActive(false);
        playPanel.SetActive(true);

        isGameStarted = true;
        
        if (gasText == null)
        {
            gasText = GameObject.Find("GasText").GetComponent<TMP_Text>();
        }
        UpdateGasDisplay();
    }

    private void ResetGame()
    {
        gas = 100;
        _gasTimer = 0f;
    }
    
    private void GameOver()
    {
        isGameStarted = false;
        playPanel.SetActive(false);
        endPanel.SetActive(true);
    }

    private void ShowStartPanel()
    {
        startPanel.SetActive(true);
        playPanel.SetActive(false);
        endPanel.SetActive(false);
    }
}
