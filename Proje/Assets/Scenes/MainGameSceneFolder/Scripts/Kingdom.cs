using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Kingdom : MonoBehaviour 
{
    public static Kingdom[] kingdoms = new Kingdom[6];
    public static Kingdom myKingdom = new Kingdom();
    public static Kingdom[] Kingdoms
    {
        get { return kingdoms; }
    }

    private string kingdomName;
    private int owner;
    private int foodAmount, stoneAmount, goldAmount, woodAmount, ironAmount, warPower;
    private float soldierAmount;
    private Sprite flag; // Bayrak özelliði eklendi

    public string Name { get { return kingdomName; } set { kingdomName = value; } }
    public int Owner { get { return owner; } set { owner = value; } }
    public int FoodAmount { get { return foodAmount; } set { foodAmount = value; } }
    public int StoneAmount { get { return stoneAmount; } set { stoneAmount = value; } }
    public int GoldAmount { get { return goldAmount; } set { goldAmount = value; } }
    public int WoodAmount { get { return woodAmount; } set { woodAmount = value; } }
    public int IronAmount { get { return ironAmount; } set { ironAmount = value; } }
    public int WarPower { get { return warPower; } set { warPower = value; } }
    public float SoldierAmount { get { return soldierAmount; } set { soldierAmount = value; } }
    public Sprite Flag { get { return flag; } set { flag = value; } } // Bayrak özelliði için getter ve setter eklendi

    public Kingdom(string name, int owner, int foodAmount, int stoneAmount, int goldAmount, int woodAmount, int ironAmount, int warPower, int soldierAmount, Sprite flag) // Bayrak özelliði eklendi
    {
        this.kingdomName = name;
        this.owner = owner;
        this.foodAmount = foodAmount;
        this.stoneAmount = stoneAmount;
        this.goldAmount = goldAmount;
        this.woodAmount = woodAmount;
        this.ironAmount = ironAmount;
        this.warPower = warPower;
        this.soldierAmount = soldierAmount;
        this.flag = flag; // Bayrak özelliði için atama eklendi
    }
     public Kingdom() { }

    public static void CreateKingdoms()
    {
        // Bayrak özelliði için gerekli sprite'lar önce tanýmlanmalýdýr

        Sprite akhadzriaFlag = Resources.Load<Sprite>("akhadzriaFlag");
        Sprite alfgardFlag = Resources.Load<Sprite>("alfgardFlag");
        Sprite arianopolFlag = Resources.Load<Sprite>("arianopolFlag");
        Sprite dhamuronFlag = Resources.Load<Sprite>("dhamuronFlag");
        Sprite lexionFlag = Resources.Load<Sprite>("lexionFlag");
        Sprite zephyrionFlag = Resources.Load<Sprite>("zephyrionFlag");

        kingdoms[0] = new Kingdom("Arianopol", 0, 7500, 3500, 7500, 5500, 3500, 0, 0, arianopolFlag);
        kingdoms[1] = new Kingdom("Alfgard", 0, 75000, 35000, 75000, 55000, 35000, 0, 0, alfgardFlag);
        kingdoms[2] = new Kingdom("Akhadzria", 0, 7500, 3500, 7500, 5500, 3500, 0, 0, akhadzriaFlag);
        kingdoms[3] = new Kingdom("Dhamuron", 0, 7500, 3500, 7500, 5500, 3500, 0, 0, dhamuronFlag);
        kingdoms[4] = new Kingdom("Lexion", 0, 75000, 35000, 75000, 55000, 35000, 0, 0, lexionFlag);
        kingdoms[5] = new Kingdom("Zephyrion", 0, 7500, 3500, 7500, 5500, 3500, 0, 0, zephyrionFlag);



    }




    public void findOwner()
    {
        if(GetVariableFromHere.currentSpriteNum == 2)
        {
            kingdoms[2].owner = 1;
           // myKingdom = kingdoms[2];
        }
        else if(GetVariableFromHere.currentSpriteNum == 3)
        {
            kingdoms[1].owner = 1;
           // myKingdom = kingdoms[1];
        }
        else if(GetVariableFromHere.currentSpriteNum == 4)
        {
            kingdoms[0].owner = 1;
           // myKingdom = kingdoms[0];
        }
        else if(GetVariableFromHere.currentSpriteNum == 5)
        {
            kingdoms[3].owner = 1;
           // myKingdom = kingdoms[3];
        }
        else
        {
            kingdoms[4].owner = 1;
           // myKingdom = kingdoms[4];
        }
    }

    void Awake()
    {
        CreateKingdoms();
        findOwner();        
    }
}