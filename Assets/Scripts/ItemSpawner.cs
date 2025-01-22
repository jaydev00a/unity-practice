using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float spawnZPos = 20f;
    
    private float _spawnInterval;
    private float _timer = 0f;
    private float[] _lanes = { -3.5f, 0f, 3.5f };
    
    
    void Update()
    {
        if (!GameManager.Instance.isGameStarted) return;

        _timer += Time.deltaTime;
    
        _spawnInterval = Random.Range(1f, 2f);
        
        if (_timer >= _spawnInterval)
        {
            _timer = 0f;
            
            // 랜덤 차선 선택
            float laneX = _lanes[Random.Range(0, _lanes.Length)];
            
            // 아이템 생성
            Vector3 spawnPos = new Vector3(laneX, 0f, spawnZPos);
            GameObject gasItem = Instantiate(itemPrefab, spawnPos, Quaternion.identity);
            
            gasItem.AddComponent<GasItem>();
        }
    }
}
