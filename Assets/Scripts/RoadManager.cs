using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RoadManager : MonoBehaviour
{
    public GameObject roadPrefab;
    public int roadCount = 2;
    public float roadLength = 30f;
    
    private List<GameObject> _roads = new List<GameObject>();
    public float moveSpeed = 25f;
    private float[] _lanes = { -2.8f, 0f, 2.8f };

    void Start()
    {
        // 초기 도로 생성
        for (int i = 0; i < roadCount; i++)
        {
            GameObject road = Instantiate(roadPrefab, new Vector3(0, 0, i * roadLength), Quaternion.identity);
            _roads.Add(road);
            
        }
    }

    void Update()
    {
        if (!GameManager.Instance.isGameStarted) return;

        // 도로 이동
        for (int i = 0; i < _roads.Count; i++)
        {
            _roads[i].transform.Translate(0, 0, -moveSpeed * Time.deltaTime);

            // 화면 밖으로 도로가 벗어나면 맨 앞으로 이동
            if (_roads[i].transform.position.z < -roadLength)
            {
                float newZ = _roads[i].transform.position.z + roadCount * roadLength;
                _roads[i].transform.position = new Vector3(0, 0, newZ);
            }
        }
    }
}
