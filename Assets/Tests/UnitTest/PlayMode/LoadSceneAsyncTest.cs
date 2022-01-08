using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
using Cysharp.Threading.Tasks;

public class LoadSceneAsyncTest
{
    private CancellationToken cancelToken = default;

    private static (string src, string dst)[] scenes = new[]
    {
        ("Title", "GameMain"),
        ("GameMain", "Title"),
        ("GameMain", "GameOver"),
        ("GameOver", "Title")
    };

    [UnityTest]
    public IEnumerator LoadSceneAsyncForceTest([ValueSource(nameof(scenes))](string src, string dst) scenes) => UniTask.ToCoroutine(async () => {
        SceneManager.LoadScene(scenes.src);

        await UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: cancelToken);

        var loadSceneAsyncGameObject = GameObject.Find("LoadSceneAsync");
        Assert.NotNull(loadSceneAsyncGameObject);
        var loadSceneAsync = loadSceneAsyncGameObject.GetComponent<LoadSceneAsync>();
        Assert.NotNull(loadSceneAsync);

        await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancelToken);

        var scene = await loadSceneAsync.ExecTask(scenes.dst, cancelToken);

        await UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: cancelToken);

        Assert.That(scene.name, Is.EqualTo(scenes.dst));
    });
}
