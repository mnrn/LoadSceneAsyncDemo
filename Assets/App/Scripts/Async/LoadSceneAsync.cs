using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class LoadSceneAsync : MonoBehaviour
{
#nullable enable
    [SerializeField] private GameObject? canvas = default;
    [SerializeField] private Slider? slider = default;
#nullable restore

    readonly private double delay = 1.0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 非同期によるシーンのロードを行います。
    /// </summary>
    /// <param name="scene">ロードするシーン名</param>
    public void Exec(string scene)
    {
        ExecTask(scene)
            .Forget();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scene">ロードするシーン名</param>
    /// <returns></returns>
    public UniTask ExecTask(string scene)
    {
        return ExecTask(scene, this.GetCancellationTokenOnDestroy());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scene">ロードするシーン名</param>
    /// <param name="token">非同期処理キャンセル用トークン</param>
    /// <returns></returns>
    public async UniTask<Scene> ExecTask(string scene, CancellationToken token)
    {
        UpdateSlider(slider, 0.0f);
        canvas?.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);

        // ロードを開始します。
        var asyncOp = LoadSceneAsyncWithInactivation(scene);

        // ロードが完了するまで待ちます。
        await Loading(asyncOp, token);

        await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
        asyncOp.allowSceneActivation = true;
        while (!asyncOp.isDone)
        {
            await UniTask.Yield(token);
        }
        return SceneManager.GetActiveScene();
    }

    /// <summary>
    /// 非同期によるシーンのロードを行い、対象シーンを非アクティブとします。
    /// </summary>
    /// <param name="scene">ロードするシーン名</param>
    /// <returns></returns>
    private AsyncOperation LoadSceneAsyncWithInactivation(string scene)
    {
        var asyncOp = SceneManager.LoadSceneAsync(scene);
        asyncOp.allowSceneActivation = false; // 許可するまでシーンをアクティブにしません。
        Debug.Log("Progress :" + asyncOp.progress);

        return asyncOp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="asyncOp"></param>
    /// <param name="token">非同期処理キャンセル用トークン</param>
    /// <returns></returns>
    private async UniTask Loading(AsyncOperation asyncOp, CancellationToken token)
    {
        do
        {
            await UniTask.Yield(token);

            //slider.value = asyncOp.progress;
            UpdateSlider(slider, asyncOp.progress);
            Debug.Log("Progress :" + asyncOp.progress);
        } while (asyncOp.progress < 0.9f);

        UpdateSlider(slider, 1.0f);
    }

#nullable enable
    private void UpdateSlider(Slider? slider, float v)
    {
        if (slider is null)
        {
            return;
        }
        slider!.value = v;
    }
#nullable restore
}