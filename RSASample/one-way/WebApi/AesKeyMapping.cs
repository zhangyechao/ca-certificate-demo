namespace WebApi
{
    public class AesKeyMapping
    {
        private static Dictionary<string, string> _dict = new Dictionary<string, string>();

        public static void SetAes(string appId, string key)
        {
            _dict[appId] = key;
        }

        public static string? GetAesByAppId(string appId)
        {
            return _dict.TryGetValue(appId, out var key)
                ? key
                : null;
        }
    }
}
