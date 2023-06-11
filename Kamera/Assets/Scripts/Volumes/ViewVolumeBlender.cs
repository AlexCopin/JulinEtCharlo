using System.Collections.Generic;
using UnityEngine;

namespace Kamera
{
    internal class ViewVolumeBlender : MonoBehaviour
    {
        private static ViewVolumeBlender _instance;
        public static ViewVolumeBlender Instance
            => _instance
            = _instance != null
            ? _instance
            : new GameObject(nameof(ViewVolumeBlender)).AddComponent<ViewVolumeBlender>();

        private readonly List<AViewVolume> ActiveViewVolumes = new List<AViewVolume>();
        private readonly Dictionary<AView, List<AViewVolume>> VolumesPerView = new Dictionary<AView, List<AViewVolume>>();

        public void AddVolume(AViewVolume viewVolume)
        {
            ActiveViewVolumes.Add(viewVolume);

            var view = viewVolume.View;
            if (!VolumesPerView.ContainsKey(view))
            {
                VolumesPerView.Add(view, new List<AViewVolume>());
                view.SetActive(true);
            }
            else
                VolumesPerView[view].Add(viewVolume);
        }

        public void RemoveVolume(AViewVolume viewVolume)
        {
            ActiveViewVolumes.Remove(viewVolume);

            var view = viewVolume.View;
            VolumesPerView[view].Remove(VolumesPerView[view].Find(x => x == viewVolume));
            if (VolumesPerView[view].Count <= 0)
            {
                VolumesPerView.Remove(view);
                view.SetActive(false);
            }
        }
    }
}