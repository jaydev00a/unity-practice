using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    
    private float _laneWidth = 3.5f; // 차선 간격
    private float _targetPosX = 0;
    private Camera _mainCamera; // 메인 카메라

    
    private void Start()
    {
        _targetPosX = transform.position.x;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameStarted) return;
        // 부드럽게 이동
        Vector3 targetPos = new Vector3(_targetPosX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }
    
    public void OnMove(InputValue value)
    {
        if (!GameManager.Instance.isGameStarted) return;

        Vector2 input = value.Get<Vector2>();

        if (input.x > 0)
        {
            _targetPosX = Mathf.Min(_targetPosX + _laneWidth, _laneWidth); // 오른쪽 최대 거리 제한
        }
        else if (input.x < 0)
        {
            _targetPosX = Mathf.Max(_targetPosX - _laneWidth, -_laneWidth); // 왼쪽 최대 제한
        }
    }

    public void OnClick(InputValue value)
    {
        if (!GameManager.Instance.isGameStarted) return;

        // 마우스 위치 좌표 화면 -> 월드 변환
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = _mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _mainCamera.nearClipPlane));

        if (worldPos.x < 0)
        {
            _targetPosX = Mathf.Max(_targetPosX - _laneWidth, -_laneWidth);
        }
        else
        {
            _targetPosX = Mathf.Min(_targetPosX + _laneWidth, _laneWidth);
        }
    }
}
