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
        Init();
        await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

        // ロードを開始します。
        var asyncOp = LoadSceneAsyncWithInactivation(scene);

        // ロードが9割型完了するまで待つ。
        do
        {
            await UniTask.Yield(token);
            SliderUpdate(asyncOp);
        } while (asyncOp.progress < 0.9f);

        SliderEnd();
        await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);
        asyncOp.allowSceneActivation = true;
    }

    private void Init()
    {
        slider.value = 0.0f;
        canvas.SetActive(true);
    }

    private AsyncOperation LoadSceneAsyncWithInactivation(string scene)
    {
        var asyncOp = SceneManager.LoadSceneAsync(scene);
        asyncOp.allowSceneActivation = false; // OKするまでシーンをアクティブにしません。
        Debug.Log("Progress :" + asyncOp.progress);

        return asyncOp;
    }

    private void SliderUpdate(AsyncOperation asyncOp)
    {
        slider.value = asyncOp.progress;
        Debug.Log("Progress :" + asyncOp.progress);
    }

    private void SliderEnd()
    {
        slider.value = 1.0f;
    }
}
