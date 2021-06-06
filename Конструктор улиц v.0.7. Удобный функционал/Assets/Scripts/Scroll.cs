using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//public delegate void SCROLLEventer(object sender, ScrollEventArgs e);
public class Scroll : MonoBehaviour
{
    //public SCROLLEventer scr;
    [SerializeField] public Slider _slider;
    [SerializeField] public TextMeshProUGUI _sliderValue;
    // Start is called before the first frame update
    void Start()
    {
        //_slider.onValueChanged.AddListener((v) => {
        //    _sliderValue.text = v.ToString("0.00");
        //});

    }
    public void OnScroll()
    {
        _sliderValue.text = _slider.value.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        //_slider.onValueChanged
    }


}
