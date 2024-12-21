using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HastaneSliderController : MonoBehaviour
{
    public int yaraliSavasciSayisi;
    public int yaraliOkcuSayisi;
    public int yaraliMizrakciSayisi;

    public Slider savasciSlider;
    public Slider okcuSlider;
    public Slider mizrakciSlider;

    public InputField savasciInputField;
    public InputField okcuInputField;
    public InputField mizrakciInputField;

    public TextMeshProUGUI AltinText;
    public TextMeshProUGUI KeresteText;
    public TextMeshProUGUI TasText;
    public TextMeshProUGUI YemekText;
    public TextMeshProUGUI DemirText;



    private float toplamAltin = 0;
    private float toplamKereste = 0;
    private float toplamTas = 0;
    private float toplamYemek = 0;
    private float toplamDemir = 0;

    void Start()
    {   //Furkanýn sahneden alýna bilgiler burada eþitlenecek.
        yaraliSavasciSayisi = 100;
        yaraliOkcuSayisi = 150;
        yaraliMizrakciSayisi = 250;

        savasciSlider.wholeNumbers = true;
        okcuSlider.wholeNumbers = true;
        mizrakciSlider.wholeNumbers = true;

        savasciSlider.maxValue = yaraliSavasciSayisi;
        okcuSlider.maxValue = yaraliOkcuSayisi;
        mizrakciSlider.maxValue = yaraliMizrakciSayisi;

        savasciInputField.text = "0";
        savasciSlider.value = 0;
        savasciSlider.onValueChanged.AddListener(OnSavasciSliderValueChanged);
        savasciInputField.onEndEdit.AddListener(OnSavasciInputFieldEndEdit);

        okcuInputField.text = "0";
        okcuSlider.value = 0;
        okcuSlider.onValueChanged.AddListener(OnOkcuSliderValueChanged);
        okcuInputField.onEndEdit.AddListener(OnOkcuInputFieldEndEdit);

        mizrakciInputField.text = "0";
        mizrakciSlider.value = 0;
        mizrakciSlider.onValueChanged.AddListener(OnMizrakciSliderValueChanged);
        mizrakciInputField.onEndEdit.AddListener(OnMizrakciInputFieldEndEdit);
    }

    void OnSavasciSliderValueChanged(float value)
    {
        savasciInputField.text = value.ToString();
        CalculateTotalCosts();
    }

    void OnOkcuSliderValueChanged(float value)
    {
        okcuInputField.text = value.ToString();
        CalculateTotalCosts();
    }

    void OnMizrakciSliderValueChanged(float value)
    {
        mizrakciInputField.text = value.ToString();
        CalculateTotalCosts();
    }

    void OnSavasciInputFieldEndEdit(string value)
    {
        float floatValue;
        if (float.TryParse(value, out floatValue))
        {
            savasciSlider.value = Mathf.Clamp(floatValue, savasciSlider.minValue, savasciSlider.maxValue);
            CalculateTotalCosts();
        }
    }

    void OnOkcuInputFieldEndEdit(string value)
    {
        float floatValue;
        if (float.TryParse(value, out floatValue))
        {
            okcuSlider.value = Mathf.Clamp(floatValue, okcuSlider.minValue, okcuSlider.maxValue);
            CalculateTotalCosts();
        }
    }

    void OnMizrakciInputFieldEndEdit(string value)
    {
        float floatValue;
        if (float.TryParse(value, out floatValue))
        {
            mizrakciSlider.value = Mathf.Clamp(floatValue, mizrakciSlider.minValue, mizrakciSlider.maxValue);
            CalculateTotalCosts();
        }
    }

    void CalculateTotalCosts()
    {
        float savasciCount = savasciSlider.value;
        float okcuCount = okcuSlider.value;
        float mizrakciCount = mizrakciSlider.value;

        toplamAltin = (savasciCount * 5) + (okcuCount * 7) + (mizrakciCount * 7);
        toplamYemek = (savasciCount * 5) + (okcuCount * 6) + (mizrakciCount * 6);
        toplamDemir = (savasciCount * 5) + (okcuCount * 3) + (mizrakciCount * 3);
        toplamTas = (savasciCount * 5) + (okcuCount * 2) + (mizrakciCount * 2);
        toplamKereste = (savasciCount * 5) + (okcuCount * 10) + (mizrakciCount * 10);

        UpdateCostTexts();
    }

    void UpdateCostTexts()
    {
        AltinText.text = toplamAltin.ToString();
        YemekText.text = toplamYemek.ToString();
        DemirText.text = toplamDemir.ToString();
        TasText.text = toplamTas.ToString();
        KeresteText.text = toplamKereste.ToString();
    }
}
