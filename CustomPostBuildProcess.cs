#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

using System;
using System.Diagnostics;
using System.IO;

public class CustomPostBuildProcess
{
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        UnityEngine.Debug.Log("Starting compress process");
        Process zipProcess = new Process();
        zipProcess.StartInfo.FileName = "C:\\Program Files\\7-Zip\\7z.exe";

        string directoryName = Path.GetDirectoryName(pathToBuiltProject); // C:/pasta/builds/
        string archivePath = directoryName + "/../" + target.ToString() + ".zip";
        string pathToArchive = directoryName;

        zipProcess.StartInfo.Arguments = string.Format(
            "a {0} {1}",
            archivePath,
            pathToArchive
        );

        zipProcess.Start();
        UnityEngine.Debug.LogFormat("Finished compress process. Location: {0}", archivePath);
    }
}

#endif