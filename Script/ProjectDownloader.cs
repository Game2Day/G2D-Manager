using System;
using UnityEditor;
using UnityEngine;

namespace G2DManager
{
    public class ProjectDownloader
    {
        [MenuItem("Tools/Download Project")]
        private static void DownloadProject()
        {
            var githubToken = "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";
            var url = "https://github.com/Game2Day/InitialPackage/zipball/master/";
            var path = Application.dataPath + "/G2D2";

            using (var client = new System.Net.Http.HttpClient())
            {
                var credentials = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}:", githubToken);
                credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(credentials));
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
                var contents = client.GetByteArrayAsync(url).Result;
                ZipFile.UnZip(path, contents);
            }
        }
    }
}