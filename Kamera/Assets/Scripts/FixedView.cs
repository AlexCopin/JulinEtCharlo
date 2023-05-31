using System.Net.Http.Headers;

namespace Kamera
{
    internal class FixedView : AView
    {
        public float Yaw;
        public float Pitch;
        public float Roll;
        public float Fov;

        public override CameraConfiguration GetConfiguration()
        {
            CameraConfiguration config = new CameraConfiguration();
            config.Yaw = Yaw;
            config.Pitch = Pitch;
            config.Roll = Roll;
            config.Fov = Fov;
            config.Pivot = transform.position;
            config.Distance = 0;
            return config;
        }
    }
}

