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

public class SceneTransitionTest
{
    private GameObject loadSceneAsyncGameObject = default;
    private LoadSceneAsync loadSceneAsync = default;
    private CancellationToken cancelToken = default;

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("Title");

        loadSceneAsyncGameObject = new GameObject();
        loadSceneAsync = loadSceneAsyncGameObject.AddComponent<LoadSceneAsync>();
    }

    [UnityTest]
    public IEnumerator LoadScenAsyncTest() => UniTask.ToCoroutine(async () => {
        //await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancelToken);

        await loadSceneAsync.ExecTask("GameMain", cancelToken);

        //await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancelToken);

        Assert.That(SceneManager.GetActiveScene().name, Is.EqualTo("GameMain"));
    });

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(loadSceneAsyncGameObject);
    }
}
