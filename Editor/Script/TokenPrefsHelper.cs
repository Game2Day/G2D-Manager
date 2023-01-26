using UnityEditor;

namespace G2DManager
{
    public static class TokenPrefsHelper
    {
        private const string GithubToken = "g2d.manager.github_token";

        public static string Load()
        {
            return EditorPrefs.GetString(GithubToken, string.Empty);
        }

        public static void Save(string token)
        {
            EditorPrefs.SetString(GithubToken, token);
        }
    }
}