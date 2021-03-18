using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// The Aura device provider
    /// </summary>
    class GhubDeviceProvider : IDeviceProvider
    {
        /// <summary>
        /// The provider name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The devices the provider has
        /// </summary>
        public IEnumerable<IDevice> Devices { get => _devices; }

        /// <summary>
        /// The devices the provider has
        /// </summary>
        private List<GhubDevice> _devices;

        /// <summary>
        /// The provider sdk
        /// </summary>
        private IGhubSdk _sdk;

        /// <summary>
        /// If the provider is in exclusive mode
        /// </summary>
        private bool inExcluseMode;

        /// <summary>
        /// Creates an Aura device provider
        /// </summary>
        public GhubDeviceProvider()
        {
            Name = "GHUB";
            _devices = new List<GhubDevice>();
            _sdk = new GhubSdk();

            inExcluseMode = false;
        }

        /// <summary>
        /// Initializes the provider
        /// </summary>
        public void Initialize()
        {
            PerformHealthCheck();

            Thread.Sleep(30000);

            foreach (IGhubSyncDevice device in _sdk.Enumerate(0))
            {
                _devices.Add(new GhubDevice(device));
            }
        }

        /// <summary>
        /// Performs a health check on the provider
        /// </summary>
        public void PerformHealthCheck()
        {
            var lightingServiceRunning = Process.GetProcessesByName("LightingService").Length != 0;

            while (!lightingServiceRunning)
            {
                Thread.Sleep(1000);
                lightingServiceRunning = Process.GetProcessesByName("LightingService").Length != 0;
            }
        }

        /// <summary>
        /// Requests exclusive control over the provider
        /// </summary>
        public void RequestControl()
        {
            if (!inExcluseMode)
            {
                _sdk.SwitchMode();
            }
        }

        /// <summary>
        /// Releases exclusive control over the provider
        /// </summary>
        public void ReleaseControl()
        {
            if (inExcluseMode)
            {
                _sdk.SwitchMode();
            }
        }
    }
}
