using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneEntry : MonoBehaviour
{
    [SerializeField] private GameObject canvas = default;
    [SerializeField] private GameObject screen = default;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private Ease easeType = Ease.InOutCubic;

    // Start is called before the first frame update
    void Start()
    {
        // まずはキャンバスを使えるようにする。
        canvas.SetActive(true);

        // 黒画面からフェードアウトします。
        var image = screen.GetComponent<Image>();
        image.DOFade(endValue: 0.0f, duration: fadeDuration)
            .SetEase(easeType)
            .OnComplete(() => canvas.SetActive(false));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
