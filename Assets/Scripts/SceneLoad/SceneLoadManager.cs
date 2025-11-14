using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    private AssetReference currentScene;
    public AssetReference gameScene;

    protected override void Awake()
    {
        base.Awake();
        //LoadGameScene();
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    private async Task LoadSceneTask()
    {
        //异步加载场景
        var s = currentScene.LoadSceneAsync(LoadSceneMode.Additive);
        //等待加载
        await s.Task;

        if (s.Status == AsyncOperationStatus.Succeeded)
        {
            //加载完激活场景
            SceneManager.SetActiveScene(s.Result.Scene);
        }
    }
    
    public async void LoadGameScene()
    {
        currentScene = gameScene;
        await LoadSceneTask();
    }
}