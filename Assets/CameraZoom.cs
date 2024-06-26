using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _speed = 25f;
    [SerializeField] private float _smoothing = 5f;
    [SerializeField] private Vector2 _range = new Vector2(30f, 70f);
    [SerializeField] private Transform _cameraHolder;

    private Vector3 _cameraDirection => transform.InverseTransformDirection(_cameraHolder.forward);

    private Vector3 _targetPosition;
    private float _input;

    private void Awake()
    {
        _targetPosition = _cameraHolder.localPosition;
    }

    private void HandleInput()
    {
        _input = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log("Mouse ScrollWheel Input: " + _input); // Debugging input
    }

    private void Zoom()
    {
        Vector3 nextTargetPosition = _targetPosition + _cameraDirection * (_input * _speed);
        if (IsInBounds(nextTargetPosition)) _targetPosition = nextTargetPosition;
        _cameraHolder.localPosition = Vector3.Lerp(_cameraHolder.localPosition, _targetPosition, Time.deltaTime * _smoothing);
    }

    private bool IsInBounds(Vector3 position)
    {
        return position.magnitude > _range.x && position.magnitude < _range.y;
    }

    private void Update()
    {
        HandleInput();
        Zoom();
    }
}