using UnityEngine;

namespace Source.Camera
{
    public class CameraViewMatcher : MonoBehaviour
    {
        [SerializeField] private Vector2 _defaultResolution;
        [SerializeField, Range(0f, 1f)] private float _widthOrHeight;

        private UnityEngine.Camera _camera;
        private float _initialSize;
        private float _initialFov;
        private float _targetAspect;
        private float _verticalFov;

        private void Start()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _initialSize = _camera.orthographicSize;
            _initialFov = _camera.fieldOfView;
            _targetAspect = _defaultResolution.x / _defaultResolution.y;
            _verticalFov = CalculateVerticalFov(_initialFov, 1 / _targetAspect);
        
            SetUpCameraView();
        }

        private void SetUpCameraView()
        {
            if (_camera.orthographic)
            {
                float constantWidthSize = _initialSize * (_targetAspect / _camera.aspect);
                _camera.orthographicSize = Mathf.Lerp(constantWidthSize, _initialSize, _widthOrHeight);
            }
            else
            {
                float constantWidthFov = CalculateVerticalFov(_verticalFov, _camera.aspect);
                _camera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, _widthOrHeight);
            }
        }

        private float CalculateVerticalFov(float horizontalFovInDeg, float aspectRatio)
        {
            float horizontalFovInRads = horizontalFovInDeg * Mathf.Deg2Rad;
            float verticalFovInRads = 2 * Mathf.Atan(Mathf.Tan(horizontalFovInRads / 2) / aspectRatio);
            return verticalFovInRads * Mathf.Rad2Deg;
        }
    }
}