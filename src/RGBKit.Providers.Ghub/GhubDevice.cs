using System.Collections.Generic;
using LogitechGSDK.LogiLedInit();
using LogitechGSDK.LogiLedSaveCurrentLighting();
using LogitechGSDK.LogiLedSetLighting(red, blue, green);
using LogitechGSDK.LogiLedRestoreLighting();
using LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(04, 100, 0, 0);
using LogitechGSDK.LogiLedShutdown();
using RGBKit.Core;

namespace RGBKit.Providers.Ghub
{
    /// <summary>
    /// An Aura device
    /// </summary>
    class GhubDevice : IDevice
    {
        /// <summary>
        /// The device name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The lights the device has
        /// </summary>
        public IEnumerable<IDeviceLight> Lights { get => _lights; }

        /// <summary>
        /// The number of lights on the device
        /// </summary>
        public int NumberOfLights { get; }

        /// <summary>
        /// The device
        /// </summary>
        private IGhubSyncDevice _device;

        /// <summary>
        /// The lights the device has
        /// </summary>
        private List<GhubDeviceLight> _lights;

        /// <summary>
        /// Creates an Aura device
        /// </summary>
        /// <param name="device">The device</param>
        internal AuraDevice(IGhubSyncDevice device)
        {
            _device = device;
            Name = _device.Name;
            _lights = new List<GhubDeviceLight>();

            foreach (IGhubRgbLight light in _device.Lights)
            {
                _lights.Add(new GhubDeviceLight(light));
            }

            NumberOfLights = _device.Lights.Count;
        }

        /// <summary>
        /// Applies light changes to the device
        /// </summary>
        public void ApplyLights()
        {
            if (NumberOfLights > 0)
            {
                _device.Apply();
            }
        }
    }
}