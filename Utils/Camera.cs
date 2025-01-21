using Microsoft.Xna.Framework;

namespace TetrisTutorial.Utils
{
    public class Camera
    {
        private const float nearPlane = 0.01f;
        private const float farPlane = 100f;
        private int _screenWidth, _screenHeight;
        private float _fieldOfView;
        private Vector3 _position, _target;

        private Matrix _view, _projection;

        public Camera(Vector3 position, Vector3 target, int screenWidth, int screenHeight, float fieldOfView)
        {
            _position = position;
            _target = target;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _fieldOfView = fieldOfView;

            CalculateMatrices();
        }
        private void CalculateMatrices()
        {
            _view = Matrix.CreateLookAt(_position, _target, Vector3.Up);
            float aspect = _screenWidth / _screenHeight;
            _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOfView, aspect, nearPlane, farPlane);
        }

        public Matrix View { get => _view; }
        public Matrix Projection { get => _projection; }

        public void SetCameraPosition(Vector3 position)
        {
            _position = position;
            CalculateMatrices();
        }
        public void SetCameraTarget(Vector3 target)
        {
            _target = target;
            CalculateMatrices();
        }
    }
}
