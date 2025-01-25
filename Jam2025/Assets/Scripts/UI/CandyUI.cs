using Coffee.UIEffects;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CandyUI : MonoBehaviour
{
    [SerializeField] private GameObject _blockObject;
    [SerializeField, Range(0, 10)] private int _outlineShadow = 10;

    private Image _image;

    public void SetUpCandy(CandySO candy)
    {
        _image = GetComponent<Image>();
        _image.sprite = candy.Image;
        _image.GetComponent<UIEffect>().shadowColor = candy.Color;
        _image.GetComponent<UIEffect>().shadowIteration = _outlineShadow;
    }

    public void Explode(Action onEndAction)
    {
        StartCoroutine(ExplodeCoroutine(onEndAction));
    }
    private IEnumerator ExplodeCoroutine(Action onEndAction)
    {
        transform.localScale = Vector3.one * 1.1f;

        while (_image.color.a > 0)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a - (5f * Time.deltaTime));

            yield return new WaitForEndOfFrame();
        }


        onEndAction?.Invoke();
    }

    public void Block(bool block)
    {
        _blockObject.SetActive(block);
    }
}
