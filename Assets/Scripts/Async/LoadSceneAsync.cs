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
    [SerializeField] private GameObject canvas = default;
    [SerializeField] private Slider slider = default;

    readonly private double delay = 1.0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Exec(string scene)
    {
        ExecTask(scene, this.GetCancellationTokenOnDestroy())
            .Forget();
    }

    private async UniTask ExecTask(string scene, CancellationToken token)
    {
        slider.value = 0.0f;
        canvas.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);

        // ロードを開始します。
        var asyncOp = LoadSceneAsyncWithInactivation(scene);

        // ロードが完了するまで待ちます。
        await Loading(asyncOp, token);

        await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
        asyncOp.allowSceneActivation = true;
    }

    private AsyncOperation LoadSceneAsyncWithInactivation(string scene)
    {
        var asyncOp = SceneManager.LoadSceneAsync(scene);
        asyncOp.allowSceneActivation = false; // 許可するまでシーンをアクティブにしません。
        Debug.Log("Progress :" + asyncOp.progress);

        return asyncOp;
    }

    private async UniTask Loading(AsyncOperation asyncOp, CancellationToken token)
    {
        do
        {
            await UniTask.Yield(token);

            slider.value = asyncOp.progress;
            Debug.Log("Progress :" + asyncOp.progress);
        } while (asyncOp.progress < 0.9f);

        slider.value = 1.0f;
    }    
}
