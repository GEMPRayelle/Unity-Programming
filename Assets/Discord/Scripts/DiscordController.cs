using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Discord;
using UnityEditor.SceneManagement;
using System;
using System.Threading.Tasks;

[InitializeOnLoad]
public class DiscordController
{
    private static Discord.Discord discord;
    private static string projectName {get{return Application.productName;}}
    private static string version {get{return Application.version;}}
    private static RuntimePlatform platform {get{return Application.platform;}}
    private static string activeSceneName {get{return EditorSceneManager.GetActiveScene().name;}}
    private static long lastTimestamp;

    private const string applicationId = "826331554775171112";

    static DiscordController(){
        DelayInit();
    }

    private static async void DelayInit(int delay = 1000){
        await Task.Delay(delay);
        SetupDiscord();
    }

    private static void SetupDiscord(){
        discord = new Discord.Discord(long.Parse(applicationId), (ulong)CreateFlags.Default);
        lastTimestamp = GetTimestamp();
        UpdateActivity();

        EditorApplication.update += EdiotrUpdate;
        EditorSceneManager.sceneOpened += SceneOpened;
    }
    private static void EdiotrUpdate(){
        discord.RunCallbacks();
    }
    private static void SceneOpened(UnityEngine.SceneManagement.Scene scene, OpenSceneMode sceneMode){
        UpdateActivity();
    }

    public static void UpdateActivity(){
        ActivityManager activityManager = discord.GetActivityManager();
        Activity activity = new Activity(){
            Details = "Editing" + projectName,
            State = activeSceneName + " | " + platform,
            Timestamps = { Start = lastTimestamp },
            Assets = 
            {
                LargeImage = "unity_3d_image",
                LargeText = version,
                SmallImage = "unity_image",
                SmallText = version
            }
        };
        activityManager.UpdateActivity(activity, result => {
            Debug.Log("Discord result: " + result);
        });
    }

    private static long GetTimestamp(){
        long unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        return unixTimestamp;
    }
}
