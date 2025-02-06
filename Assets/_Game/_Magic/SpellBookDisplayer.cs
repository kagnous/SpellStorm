using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookDisplayer : MonoBehaviour
{
    [SerializeField, Tooltip("Image de la forme affichée")]
    private Image _formImage;

    [SerializeField, Tooltip("Image de l'élement affiché")]
    private Image _elementImage;

    private void Awake()
    {
        _formImage = transform.Find("Form").GetComponent<Image>();
        _elementImage = transform.Find("Element").GetComponent<Image>();
    }

    public void DisplaySpellBook(MagicForm form, MagicElement element)
    {
        _formImage.sprite = form.Sprite;
        _elementImage.sprite = element.Sprite;
    }
}