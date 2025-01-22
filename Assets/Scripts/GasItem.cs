using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GasItem : MonoBehaviour
{
    public int gasValue = 10;
    private RoadManager roadManager;

    void Start()
    {
        roadManager = GameObject.Find("RoadManager").GetComponent<RoadManager>();
        
        if (roadManager == null)
        {
            Debug.LogError("No RoadManager found");
        }

    }
    
    void Update()
    {
        if (!GameManager.Instance.isGameStarted) return;

        // 도로 속도에 맞춰 이동
        transform.Translate(0, 0, -roadManager.moveSpeed * Time.deltaTime);
        if (transform.position.z < -15f)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager is null");
            }
            gameManager.gas += 30;
            gameManager.UpdateGasDisplay(); // UI 업데이트
            
            Debug.Log("Gas Collected");
            
            Destroy(gameObject);
        }
    }
}
