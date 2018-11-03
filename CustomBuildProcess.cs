#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

// Output the build size or a failure depending on BuildPlayer.
public class CustomBuildProcess : MonoBehaviour
{

    public enum BuildType
    {
        MAJOR,
        MINOR,
        REVISION_ALPHA_0,
        REVISION_BETA_1,
        REVISION_RC_2,
        REVISION_FINAL_3,
        BUILD,
    }

    public static string[] testScenes = new[] {
            "Assets/Scenes/Demo 1.unity",
            "Assets/Scenes/Demo 2.unity",
            "Assets/Scenes/Demo 3.unity",
            "Assets/Scenes/Demo 4.unity",
        };



    public static void CompileAndBuild(BuildTarget platform, bool production = false)
    {
        switch (platform)
        {
            case BuildTarget.StandaloneWindows64:
                Build(BuildTarget.StandaloneWindows64, "UmaBuscaEspacial.exe", production);
                break;
            case BuildTarget.StandaloneWindows:
                Build(BuildTarget.StandaloneWindows, "UmaBuscaEspacial.exe", production);
                break;
            case BuildTarget.StandaloneOSX:
                Build(BuildTarget.StandaloneOSX, "UmaBuscaEspacial", production);
                break;
            case BuildTarget.StandaloneLinux:
                Build(BuildTarget.StandaloneLinux, "UmaBuscaEspacial", production);
                break;
        }
    }

    public static bool Build(BuildTarget platform, string pathname, bool production = false)
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = testScenes;
        buildPlayerOptions.target = platform;

        if (production)
        {
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP);
            buildPlayerOptions.locationPathName = "./builds/prod/" + platform.ToString() + "/" + pathname;
            buildPlayerOptions.options = BuildOptions.CompressWithLz4HC; // use HC for final release in compress

        }
        else
        {
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.Mono2x);
            buildPlayerOptions.locationPathName = "./builds/dev/" + platform.ToString() + "/" + pathname;
            buildPlayerOptions.options = BuildOptions.CompressWithLz4 | BuildOptions.Development; // use HC for final release in compress
        }

        // PlayerSettings.bundleVersion
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        Debug.ClearDeveloperConsole();

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.LogFormat(
                "Build succeeded {0}: {1} mb and took {2} seconds with {3} error(s). Version: {4}. Location: {5}.",
                summary.platform.ToString(),
                (summary.totalSize / 1024 / 1024).ToString(),
                summary.totalTime.Seconds,
                summary.totalErrors,
                Application.version,
                summary.outputPath
            );

            return true;
        }
        else
        {
            Debug.LogFormat(
               "<color=#f00>Build {0}</color> {1}: {2} mb and took {3} seconds with {4} error(s).",
               summary.result.ToString(),
               summary.platform.ToString(),
               (summary.totalSize / 1024 / 1024).ToString(),
               summary.totalTime.Seconds,
               summary.totalErrors
           );
            return false;
        }
    }

    public static void BumpBundleVersion(BuildType versionReleasing = BuildType.BUILD)
    {
        string[] versionChar = Application.version.Split('.');
        if (versionChar.Length < 4)
        {
            Debug.LogError("Formato da versão alterado incorretamente. Setando para versão 1.0.0.8");
            versionChar = new string[] { "1", "0", "0", "1" };
        }
        if (versionChar.Length > 4)
        {
            Debug.LogError("Formato da versão alterado incorretamente. Utilizando apenas primeiros 4 números");
        }

        int[] appVersion = new int[4] {
            int.Parse(versionChar[0]),
            int.Parse(versionChar[1]),
            int.Parse(versionChar[2]),
            int.Parse(versionChar[3]),
        };

        // versao de build sempre incremental
        // mesmo que haja erros
        appVersion[3] += 1;

        // se a versão principal mudar 
        // resetamos as outras e mantemos a build
        // seguindo o padrão 
        // 0 for alpha (status); 1 for beta (status); 2 for release candidate; 3 for (final) release
        if (versionReleasing == BuildType.MAJOR)
        {
            appVersion[0] += 1;  // major
            appVersion[1] = 0;   // reset minor
            appVersion[2] = 0;   // reset revision
        }
        else if (versionReleasing == BuildType.MINOR)
        {
            appVersion[1] += 1;  // minor
            appVersion[2] = 0;   // reset revision
        }
        else if (versionReleasing == BuildType.REVISION_ALPHA_0)
        {
            appVersion[2] = 1;   // revision
        }
        else if (versionReleasing == BuildType.REVISION_BETA_1)
        {
            appVersion[2] = 1;   // revision
        }
        else if (versionReleasing == BuildType.REVISION_RC_2)
        {
            appVersion[2] = 2;   // revision
        }
        else if (versionReleasing == BuildType.REVISION_FINAL_3)
        {
            appVersion[2] = 3;   // revision
        }

        // Atualiza os valores
        PlayerSettings.bundleVersion = string.Format(
            "{0}.{1}.{2}.{3}",
            appVersion[0],
            appVersion[1],
            appVersion[2],
            appVersion[3]
        );

        // Update build number
        PlayerSettings.macOS.buildNumber = appVersion[3].ToString();
        // Printa os valores atuais
        Debug.LogFormat("Version updated to {0} from BuildType {1}", Application.version, versionReleasing.ToString());
    }
}
#endif