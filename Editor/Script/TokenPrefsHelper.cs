using UnityEditor;

namespace JT
{
    public static class TokenPrefsHelper
    {
        private const string GithubToken = "jt.manager.github_token";

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