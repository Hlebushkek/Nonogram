using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class InLevelCameraControl : MonoBehaviour
{
    [SerializeField] private Transform background;
    private float backLength, backHigh;
    private CinemachineVirtualCamera _vc;
    private Transform _followObj;
    private Vector3 startPos;
    private void Awake()
    {
        _vc = this.GetComponent<CinemachineVirtualCamera>();
        _followObj = _vc.Follow;

        SpriteRenderer backR = background.GetComponent<SpriteRenderer>();
        backLength = backR.bounds.size.x;
        backHigh = backR.bounds.size.y;
        Debug.Log(backLength + " " + backHigh);
    }
    private void Update()
    {
        CheckZoom();
        CheckMove();
    }
    private void CheckZoom()
    {
        float deltaY = Input.mouseScrollDelta.y;
        //Debug.Log(Input.mouseScrollDelta.y);
        if (Mathf.Abs(deltaY) >= 0.01f)
        {
            if (_vc.m_Lens.OrthographicSize + deltaY * 0.1f <= 2)
            {
                _vc.m_Lens.OrthographicSize = 2;
            }
            else if (_vc.m_Lens.OrthographicSize + deltaY * 0.1f >= 9)
            {
                _vc.m_Lens.OrthographicSize = 9;
            }
            else
            {
                _vc.m_Lens.OrthographicSize += deltaY * 0.1f;
            }
        }
    }
    private void CheckMove()
    {
        if (Input.GetMouseButtonDown(2))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(2))
        {
            _followObj.position += startPos - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_followObj.position.y > background.transform.position.y + backHigh / 2.0f)
            {
                _followObj.position = new Vector3(_followObj.position.x, background.transform.position.y + backHigh / 2.0f, _followObj.position.z);
            }
            else if (_followObj.position.y < background.transform.position.y - backHigh / 2.0f)
            {
                _followObj.position = new Vector3(_followObj.position.x, background.transform.position.y - backHigh / 2.0f, _followObj.position.z);
            }
            
            if (_followObj.position.x > background.transform.position.x + backLength / 2.0f)
            {
                _followObj.position = new Vector3(background.transform.position.x + backLength / 2.0f, _followObj.position.y, _followObj.position.z);
            }
            else if (_followObj.position.x < background.transform.position.x - backLength / 2.0f)
            {
                _followObj.position = new Vector3(background.transform.position.x - backLength / 2.0f, _followObj.position.y, _followObj.position.z);
            }
        }
    }
}
