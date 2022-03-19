using System.Collections;
using System.Collections.Generic;
using BattleOfHeroes.Showcase.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Image _imgBar;

    [SerializeField]
    private TMP_Text _txtHealth;

    [SerializeField]
    private TMP_Text _txtDamage;

    [SerializeField]
    private Vector2 _offset;
    private Transform _target;

    public void SetTarget(Transform target)
    { 
        _target = target;
        Vector2 tempScreenPos = Camera.main.WorldToScreenPoint(_target.transform.position);
        this.GetComponent<RectTransform>().position = tempScreenPos + _offset;
    }

    public void SetHealth(float health)
    {
        _imgBar.fillAmount = health;
    }

    public void SetText(string title)
    {
        _txtHealth.text = title;
    }

    public void SetDamage(float dmg)
    {
        _txtDamage.gameObject.SetActive(true);
        _txtDamage.text = $"-{dmg}";
        //_txtDamage.transform.DOShakePosition(1.0f, strength: new Vector3(0, 0.1f, 0), vibrato: 5, randomness: 1, snapping: false, fadeOut: true);
       // _txtDamage.transform.DOMoveY( _txtDamage.transform.position.y + 2,1).OnComplete(()=> _txtDamage.gameObject.SetActive(false));          
    }
}
