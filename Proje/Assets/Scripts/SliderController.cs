using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public UnityEngine.UI.Slider savasciSlider;
    public UnityEngine.UI.Slider okcuSlider;
    public UnityEngine.UI.Slider mizrakciSlider;
    public InputField savasciInputField;
    public InputField okcuInputField;
    public InputField mizrakciInputField;

    public TextMeshProUGUI AltinText;
    public TextMeshProUGUI KeresteText;
    public TextMeshProUGUI TasText;
    public TextMeshProUGUI YemekText;
    public TextMeshProUGUI DemirText;

    

    private void Start()
    {
        // Slider'larýn wholeNumbers özelliðini true olarak ayarlama
        savasciSlider.wholeNumbers = true;
        okcuSlider.wholeNumbers = true;
        mizrakciSlider.wholeNumbers = true;

        // Slider'larýn maksimum deðerlerini ayarlama
        savasciSlider.maxValue = 100; // Örneðin, savasciSlider'ýn maksimum deðeri 100
        okcuSlider.maxValue = 50;     // Örneðin, okcuSlider'ýn maksimum deðeri 50
        mizrakciSlider.maxValue = 75; // Örneðin, mizrakciSlider'ýn maksimum deðeri 75

        savasciInputField.text = "0";
        savasciSlider.value = 0;
        savasciSlider.onValueChanged.AddListener(delegate { OnSavasciSliderChanged(); });

        okcuInputField.text = "0";
        okcuSlider.value = 0;
        okcuSlider.onValueChanged.AddListener(delegate { OnOkcuSliderChanged(); });

        mizrakciInputField.text = "0";
        mizrakciSlider.value = 0;
        mizrakciSlider.onValueChanged.AddListener(delegate { OnMizrakciSliderChanged(); });

        savasciInputField.onEndEdit.AddListener(OnSavasciInputEndEdit);
        okcuInputField.onEndEdit.AddListener(OnOkcuInputEndEdit);
        mizrakciInputField.onEndEdit.AddListener(OnMizrakciInputEndEdit);
    }

    // Savaþçý slider'ý deðiþtiðinde hem maliyetleri hem de InputField'ý güncelle
    private void OnSavasciSliderChanged()
    {
        savasciInputField.text = savasciSlider.value.ToString();
        UpdateAllCosts();
    }

    // Okçu slider'ý deðiþtiðinde hem maliyetleri hem de InputField'ý güncelle
    private void OnOkcuSliderChanged()
    {
        okcuInputField.text = okcuSlider.value.ToString();
        UpdateAllCosts();
    }

    // Mýzrakçý slider'ý deðiþtiðinde hem maliyetleri hem de InputField'ý güncelle
    private void OnMizrakciSliderChanged()
    {
        mizrakciInputField.text = mizrakciSlider.value.ToString();
        UpdateAllCosts();
    }

    // Tüm slider'larýn mevcut deðerlerini kullanarak maliyetleri güncelleyen fonksiyon
    private void UpdateAllCosts()
    {
        // Her bir slider'ýn deðerini al
        float savasciCount = savasciSlider.value;
        float okcuCount = okcuSlider.value;
        float mizrakciCount = mizrakciSlider.value;

        // Tüm birimlerin maliyetlerini toplu olarak hesapla
        float totalAltin = (savasciCount * 5) + (okcuCount * 7) + (mizrakciCount * 7);
        float totalYemek = (savasciCount * 5) + (okcuCount * 6) + (mizrakciCount * 6);
        float totalDemir = (savasciCount * 5) + (okcuCount * 3) + (mizrakciCount * 3);
        float totalTas = (savasciCount * 5) + (okcuCount * 2) + (mizrakciCount * 2);
        float totalKereste = (savasciCount * 5) + (okcuCount * 10) + (mizrakciCount * 10);

        // Text alanlarýný güncelle
        AltinText.text = totalAltin.ToString();
        YemekText.text = totalYemek.ToString();
        DemirText.text = totalDemir.ToString();
        TasText.text = totalTas.ToString();
        KeresteText.text = totalKereste.ToString();
    }

    // InputField deðiþtiðinde slider'ý güncelle
    private void OnSavasciInputEndEdit(string value)
    {
        float floatValue;
        if (float.TryParse(value, out floatValue))
        {
            savasciSlider.value = Mathf.Clamp(floatValue, savasciSlider.minValue, savasciSlider.maxValue);
        }
    }

    private void OnOkcuInputEndEdit(string value)
    {
        float floatValue;
        if (float.TryParse(value, out floatValue))
        {
            okcuSlider.value = Mathf.Clamp(floatValue, okcuSlider.minValue, okcuSlider.maxValue);
        }
    }

    private void OnMizrakciInputEndEdit(string value)
    {
        float floatValue;
        if (float.TryParse(value, out floatValue))
        {
            mizrakciSlider.value = Mathf.Clamp(floatValue, mizrakciSlider.minValue, mizrakciSlider.maxValue);
        }
    }
}
