#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

public class CustomBuildMenuItems : MonoBehaviour
{


    [MenuItem("Build/Development/Windows 86_64")]
    public static void TestWindows()
    {
        // CustomBuildProcess.BumpBundleVersion(CustomBuildProcess.BuildType.BUILD);
        CustomBuildProcess.CompileAndBuild(BuildTarget.StandaloneWindows64);
    }
    [MenuItem("Build/Development/Linux Universal")]
    public static void TestLinux()
    {
        throw new System.NotImplementedException();
        // uncoment to work
        // CustomBuildProcess.BumpBundleVersion(CustomBuildProcess.BuildType.BUILD);
        // CustomBuildProcess.CompileAndBuild(BuildTarget.StandaloneLinux);
    }
    [MenuItem("Build/Development/Mac ")]
    public static void TestMac()
    {
        throw new System.NotImplementedException();
        // uncoment to work
        // CustomBuildProcess.BumpBundleVersion(CustomBuildProcess.BuildType.BUILD);
        // CustomBuildProcess.CompileAndBuild(BuildTarget.StandaloneOSX);
    }

    [MenuItem("Build/Development/All platforms ")]
    public static void TestAllPlatforms()
    {
        throw new System.NotImplementedException();
        // uncoment to work
        // TestWindows();
        // TestMac();
        // TestLinux();
    }

    [MenuItem("Build/Version/Increment Major")]
    public static void BumpVersionMajor()
    {
        CustomBuildProcess.BumpBundleVersion(CustomBuildProcess.BuildType.MAJOR);
    }

    [MenuItem("Build/Version/Increment Minor")]
    public static void BumpVersionMinor()
    {
        CustomBuildProcess.BumpBundleVersion(CustomBuildProcess.BuildType.MINOR);
    }

    [MenuItem("Build/Version/Set to Alpha")]
    public static void BumpVersionAlpha()
    {
        CustomBuildProcess.BumpBundleVersion(CustomBuildProcess.BuildType.REVISION_ALPHA_0);
    }
    [MenuItem("Build/Version/Set to Beta")]
    public static void BumpVersionBeta()
    {
        CustomBuildProcess.BumpBundleVersion(CustomBuildProcess.BuildType.REVISION_BETA_1);
    }
    [MenuItem("Build/Version/Set to Release Candidate")]
    public static void BumpVersionRC()
    {
        CustomBuildProcess.BumpBundleVersion(CustomBuildProcess.BuildType.REVISION_RC_2);
    }
    [MenuItem("Build/Version/Set to Final")]
    public static void BumpVersionFinal()
    {
        CustomBuildProcess.BumpBundleVersion(CustomBuildProcess.BuildType.REVISION_FINAL_3);
    }

    [MenuItem("Build/Display Version")]
    public static void BumpVersion()
    {
        Debug.Log(Application.version);
    }


}
#endif