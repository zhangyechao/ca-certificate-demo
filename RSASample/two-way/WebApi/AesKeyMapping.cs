using System.Text;

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

        public static string GenAesKey()
        {
            StringBuilder sb = new();
            for (int i = 0; i < 8; i++)
            {
                sb.Append(new Random().Next(1000, 9999));
            }

            return sb.ToString();
        }
    }
}
