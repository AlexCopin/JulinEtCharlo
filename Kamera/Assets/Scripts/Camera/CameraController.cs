using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kamera
{
    internal class CameraController : MonoBehaviour
    {
        private static CameraController _instance;
        public static CameraController Instance
            => _instance
            = _instance != null
            ? _instance
            : new GameObject(nameof(CameraController)).AddComponent<CameraController>();

        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public CameraConfiguration CameraConfiguration { get; private set; }

        private readonly List<AView> _activeViews = new List<AView>();

        private Coroutine _currentTransitionCoroutine = null;

        public void AddView(in AView view) => _activeViews.Add(view);
        public void RemoveView(in AView view) => _activeViews.Remove(view);

        private void ApplyConfig(in CameraConfiguration cameraConfiguration)
        {
            if (_currentTransitionCoroutine != null) StopCoroutine(_currentTransitionCoroutine);
           _currentTransitionCoroutine = StartCoroutine(EaseOutCamTransition(CameraConfiguration, cameraConfiguration));
        }

        private CameraConfiguration ComputeAverage()
        {
            var yawSum = Vector2.zero;
            var picthSum = Vector2.zero;
            var rollSum = Vector2.zero;
            var posSum = Vector3.zero;
            var fovSum = 0f;
            var weightSum = 0f;

            foreach (var view in _activeViews)
            {
                var cfg = view.Configuration;

                yawSum += new Vector2(Mathf.Cos(cfg.Yaw * Mathf.Deg2Rad), Mathf.Sin(cfg.Yaw * Mathf.Deg2Rad)) * view.Weight;
                picthSum += new Vector2(Mathf.Cos(cfg.Pitch * Mathf.Deg2Rad), Mathf.Sin(cfg.Pitch * Mathf.Deg2Rad)) * view.Weight;
                rollSum += new Vector2(Mathf.Cos(cfg.Roll * Mathf.Deg2Rad), Mathf.Sin(cfg.Roll * Mathf.Deg2Rad)) * view.Weight;
                posSum += cfg.Position * view.Weight;
                fovSum += cfg.Fov * view.Weight;
                weightSum += view.Weight;
            }

            var yaw = Vector2.SignedAngle(Vector2.right, yawSum);
            var pitch = Vector2.SignedAngle(Vector2.right, picthSum);
            var roll = Vector2.SignedAngle(Vector2.right, rollSum);
            var pos = posSum / weightSum;
            var fov = fovSum / weightSum;

            return new CameraConfiguration { Yaw = yaw, Pitch = pitch, Roll = roll, Pivot = pos, Fov = fov };
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                return;
            }

            Destroy(this);
        }

        private IEnumerator EaseOutCamTransition(CameraConfiguration start, CameraConfiguration end, float time = 1.0f, float speed = 1.0f)
        {

            float t = 0f;
            while(t < time)
            {
                float fT = t / time;
                fT = fT * fT * (3f - 2f * fT);

                Camera.transform.position = Vector3.Lerp(start.Position, end.Position, fT);
                Camera.transform.rotation = Quaternion.Lerp(start.Rotation, end.Rotation, fT);
                Camera.fieldOfView = Mathf.Lerp(start.Fov, end.Fov, fT);
                t += Time.deltaTime;
                yield return null;
            }

            CameraConfiguration = end;
            Camera.transform.position = CameraConfiguration.Position;
            Camera.transform.rotation = CameraConfiguration.Rotation;
            Camera.fieldOfView = CameraConfiguration.Fov;
            yield return null;
        }

        public void SetTransform(Vector3 position, Quaternion rotation) => Camera.transform.SetPositionAndRotation(position, rotation);
        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 70, 50, 30), "Click - Lerp Blend views"))
            {
                ApplyConfig(ComputeAverage());
            }
        }
    }
}