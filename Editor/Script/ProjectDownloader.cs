using System;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace G2DManager
{
    public class ProjectDownloader : EditorWindow
    {
        private EditorCoroutine _coroutine;
        
        private static readonly string[] PackagePaths =
        {
            "https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts",
            "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask",
        };

        [MenuItem("Tools/Project Downloader")]
        private static void ShowProjectSettingsTuner()
        {
            ShowProjectDownloaderWindow();
        }

        private static void ShowProjectDownloaderWindow()
        {
            ProjectDownloader window =
                (ProjectDownloader)GetWindow(typeof(ProjectDownloader));
            window.titleContent.text = "ProjectDownloader";
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            if (GUILayout.Button($"Download Project"))
            {
                SetupPackages();
            }
        }

        private void SetupPackages()
        {
            if (_coroutine != null)
            {
                EditorCoroutineUtility.StopCoroutine(_coroutine);
                _coroutine = null;
            }
            
            _coroutine = EditorCoroutineUtility.StartCoroutine(Cor(), this);
        }

        private IEnumerator Cor()
        {
            foreach (var package in PackagePaths)
            {
                var request = Client.Add(package);
                EditorUtility.DisplayProgressBar("Download", "Download Repository zip", 0);
                yield return new WaitUntil(() => request.IsCompleted);
                EditorUtility.ClearProgressBar();
            }
            
            DownloadProject();
        }
        
        private void DownloadProject()
        {
            var githubToken = "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";
            var url = "https://github.com/Game2Day/InitialPackage/zipball/master/";
            var path = Application.dataPath + "/G2D";

            using (var client = new System.Net.Http.HttpClient())
            {
                var credentials = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}:", githubToken);
                credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(credentials));
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
                var contents = client.GetByteArrayAsync(url).Result;
                EditorUtility.DisplayProgressBar("Download", "Download Repository zip", 0);
                ZipFile.UnZip(path, contents);
            }
            
            EditorUtility.ClearProgressBar();
        }
    }
}