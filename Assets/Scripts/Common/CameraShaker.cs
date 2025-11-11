using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private Camera _camera;
    public static CameraShaker s_instance;
    private bool _isShaking = false;
    private Vector3 _startPosition;
    private Vector3 _shakeVector;

    [Header("진동 설정값")]
    private float _currentDuration = 0;
    [SerializeField] private float _shakeDuration = 0.5f;
    [SerializeField] private float _vibrateMultiplier = 1.0f;

    private void Start()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _camera = Camera.main;
        _startPosition = _camera.transform.position;
    }

    private void Update()
    {
        if (_isShaking)
        {
            _currentDuration += Time.deltaTime;
            if (_currentDuration >= _shakeDuration)
            {
                StopShake();
                return;
            }
            _shakeVector = new Vector3(_vibrateMultiplier * Random.Range(0, 1f), _camera.transform.position.y, _camera.transform.position.z);
            _camera.transform.position = _shakeVector;
        }
    }

    public void StartShake()
    {
        if (_isShaking)
        {
            StopShake();
        }

        _isShaking = true;
    }

    public void StopShake()
    {
        _isShaking = false;
        _camera.transform.position = _startPosition;
        _currentDuration = 0;
    }
}
