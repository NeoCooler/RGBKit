using RGBKit.Providers.Ghub;

namespace RGBKit.Core
{
    /// <summary>
    /// The Aura RGB Kit extension methods
    /// </summary>
    public static class GhubDeviceProviderExtensions
    {
        /// <summary>
        /// Uses the Aura device provider
        /// </summary>
        /// <param name="rgbKit">The RGB Kit instance to add the provider to</param>
        public static void UseGhub(this IRGBKitService rgbKit)
        {
            rgbKit.AddProvider(new GhubDeviceProvider());
        }
    }
}
