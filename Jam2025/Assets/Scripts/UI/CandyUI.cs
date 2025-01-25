using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIEffects;

public class CandyUI : MonoBehaviour
{
    [SerializeField] private Image _imageCandy;
    [SerializeField] private GameObject _blockObject;
    [SerializeField, Range(0, 10)] private int _outlineShadow = 10;

    public ParticleSystem UIParticleSystem;

    public void SetUpCandy(CandySO candy)
    {
        _imageCandy.sprite = candy.Image;
        _imageCandy.GetComponent<UIEffect>().shadowColor = candy.Color;
        _imageCandy.GetComponent<UIEffect>().shadowIteration = _outlineShadow;
    }

    public void Explode(Action onEndAction)
    {
        StartCoroutine(ExplodeCoroutine(onEndAction));
    }
    private IEnumerator ExplodeCoroutine(Action onEndAction)
    {
        transform.localScale = Vector3.one * 1.1f;
        UIParticleSystem.transform.parent.gameObject.SetActive(true);
        UIParticleSystem.Emit(5);

        while (_imageCandy.color.a > 0)
        {
            _imageCandy.color = new Color(_imageCandy.color.r, _imageCandy.color.g, _imageCandy.color.b, _imageCandy.color.a - (5f * Time.deltaTime));

            yield return new WaitForEndOfFrame();
        }

        while (UIParticleSystem.particleCount > 0)
        {
            yield return new WaitForEndOfFrame();
        }

        onEndAction?.Invoke();
    }

    public void Block(bool block)
    {
        _blockObject.SetActive(block);
    }
}
