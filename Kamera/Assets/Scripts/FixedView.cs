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
            return base.GetConfiguration();
        }
    }
}

