using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    public float maxMana = 100;         // Maksimum mana miktarý
    public float startingMana = 100;    // Baþlangýçta verilen mana miktarý
    public float manaRegenRate = 5;     // Mana yenilenme hýzý

    public Slider manaBar2d;            // 2D UI için mana çubuðu
    public Slider manaBar3d;            // 3D UI için mana çubuðu
    public Text manaText2d;             // 2D UI için mana metni

    private float currentMana;          // Þu anki mana miktarý

    // Start is called before the first frame update
    void Start()
    {
        currentMana = startingMana;     // Baþlangýçta mana miktarýný ayarla
        UpdateManaUI();                 // UI'yý güncelle
    }

    // Update is called once per frame
    void Update()
    {
        RegenerateMana();               // Mana yenilenmesini saðla
    }

    // Mana yenilenmesini saðlayan fonksiyon
    private void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaRegenRate * Time.deltaTime;  // Zamanla mana miktarýný arttýr
            currentMana = Mathf.Clamp(currentMana, 0f, maxMana);  // Mana miktarýný sýnýrla (0 ile maxMana arasýnda)
            UpdateManaUI();                                 // UI'yý güncelle
        }
    }

    // UI'yý güncellemek için kullanýlan fonksiyon
    public void UpdateManaUI()
    {
        if (manaBar2d != null)
        {
            manaBar2d.value = currentMana / maxMana;    // 2D mana çubuðunu güncelle (oran olarak)
        }
        if (manaBar3d != null)
        {
            manaBar3d.value = currentMana / maxMana;    // 3D mana çubuðunu güncelle (oran olarak)
        }
        if (manaText2d != null)
        {
            manaText2d.text = Mathf.RoundToInt(currentMana).ToString() + "/" + maxMana;  // 2D mana metnini güncelle
        }
    }

    // Belirli bir yeteneði kullanma maliyetini karþýlayýp karþýlayamayacaðýný kontrol eden fonksiyon
    public bool CanAffordAbility(float abilityCost)
    {
        return currentMana >= abilityCost;   // Eðer þu anki mana yetenek maliyetini karþýlýyorsa true döndür
    }

    // Belirli bir yeteneði kullanma fonksiyonu
    public void UseAbility(float abilityCost)
    {
        currentMana -= abilityCost;             // Yetenek maliyetini düþ
        currentMana = Mathf.Clamp(currentMana, 0f, maxMana);  // Mana miktarýný sýnýrla (0 ile maxMana arasýnda)
        UpdateManaUI();                         // UI'yý güncelle
    }
}
