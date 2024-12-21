    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchController : MonoBehaviour
{


    public Image[] lockItems = new Image[18];

    public void OpenResearchUnit(int buildLevel)
    {
        if (buildLevel == 1)
        {
            // Ýlk seviyenin kilidini aç
            lockItems[0].enabled = false;         
        }
    }
    public void OpenTwoAndThreeLevels()
    {

        if (ResearchButtonEvents.isResearched[0] == true)
        {

            lockItems[1].enabled = false;
            lockItems[2].enabled = false;
        }
    }

    public void OpenFourLevel()
    {
        if (ResearchButtonEvents.isResearched[1] == true)
        {
            lockItems[3].enabled = false;
            
        }
    }

    public void OpenFiveLevel()
    {

        if (ResearchButtonEvents.isResearched[2] == true)
        {
            lockItems[4].enabled = false;
        }
    }

    public void controlBuildLevelTwoResearches()
    {
        if (ResearchButtonEvents.isResearched[3] == true && Lab.buildLevel >= 2)
        {
            lockItems[5].enabled = false;
           // researchItems[5].color = new Color(255f, 255f, 255f, 255f);
        }
        if (ResearchButtonEvents.isResearched[4] == true && Lab.buildLevel >= 2)
        {
            lockItems[7].enabled = false;
            //researchItems[7].color = new Color(255f, 255f, 255f, 255f);
        }
        if(ResearchButtonEvents.isResearched[3] == true && ResearchButtonEvents.isResearched[4] == true && Lab.buildLevel >= 2)
        {
            lockItems[6].enabled = false;
            //researchItems[6].color = new Color(255f, 255f, 255f, 255f);
        }
        
    }

    public void control9And10Levels()
    {
        if(ResearchButtonEvents.isResearched[5] == true)
        {
            Debug.Log("Level6 Araþtýrýldý");
        }
        if (ResearchButtonEvents.isResearched[6] == true)
        {
            Debug.Log("Level7 Araþtýrýldý");
        }
        if (ResearchButtonEvents.isResearched[5] == true && ResearchButtonEvents.isResearched[6] == true)
        {
            lockItems[8].enabled = false;
            Debug.Log("Seviye9 Açýldý");
        }
        if (ResearchButtonEvents.isResearched[6] == true && ResearchButtonEvents.isResearched[7] == true)
        {
            lockItems[9].enabled = false;
        }
    }

    public void control11And12And13Levels()
    {

        if (ResearchButtonEvents.isResearched[8] == true && Lab.buildLevel >= 2)
        {
            lockItems[10].enabled = false;
        }
        if (ResearchButtonEvents.isResearched[9] == true && Lab.buildLevel >= 2)
        {
            lockItems[12].enabled = false;
        }
        if(ResearchButtonEvents.isResearched[8] == true && ResearchButtonEvents.isResearched[9] == true
           && Lab.buildLevel >= 2)
        {
            lockItems[11].enabled = false;
        }
    }


    public void controlBuildLevelThreeResearches()
    {
        if (ResearchButtonEvents.isResearched[10] == true && ResearchButtonEvents.isResearched[11]
            && Lab.buildLevel >= 3)
        {
            lockItems[13].enabled = false;
        }
        if (ResearchButtonEvents.isResearched[11] == true && ResearchButtonEvents.isResearched[12] == true
            && Lab.buildLevel >= 3)
        {
            lockItems[14].enabled = false;
        }
    }

    public void control16And17Levels()
    {
        if (ResearchButtonEvents.isResearched[13] == true && Lab.buildLevel >= 3)
        {
            lockItems[15].enabled = false;
        }
        if (ResearchButtonEvents.isResearched[14] == true && Lab.buildLevel >= 3)
        {
            lockItems[16].enabled = false;
        }
    }

    public void level18Control()
    {
        if (ResearchButtonEvents.isResearched[15] && ResearchButtonEvents.isResearched[16] && Lab.buildLevel >= 3)
        {
            lockItems[17].enabled = false;
        }
    }

}
