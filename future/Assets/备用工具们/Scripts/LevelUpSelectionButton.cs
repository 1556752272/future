using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{   //�����������ͽ�����ť��˳�����������ͽ������ı�
    public Image CharacterImage;
    public TMP_Text upgradeDescText,nameLevelText,nameLevelLittleText,LevelText;
    public Image weaponIcon;
    public GameObject panel;
    private Weapon assignWeapon;

    public void UpdataButtonDisplay(Weapon theWeapon)
    {
        if(theWeapon.gameObject.activeSelf == true)
        {
            CharacterImage.sprite = theWeapon.stats[theWeapon.weaponLevel + 1].ChIcon;
            upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel + 1].upgradeText;
        weaponIcon.sprite = theWeapon.icon;
        nameLevelText.text = theWeapon.stats[theWeapon.weaponLevel + 1].nameLevelText;
           nameLevelLittleText.text = theWeapon.stats[theWeapon.weaponLevel + 1].nameLevelLittleText;
            LevelText.text = "LV" + (theWeapon.weaponLevel + 1);
        }
        else
        {
            upgradeDescText.text = "Unlock " + theWeapon.name;
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name;
        }
        assignWeapon = theWeapon;
    }
    public void SelectUpgrade()
    {
        if(assignWeapon != null)
        {
            if(assignWeapon.gameObject.activeSelf == true)
            {
                Debug.Log(assignWeapon);
                assignWeapon.LevelUp();
            }else
            {
               // PlayerController.instance.AddWeapon(assignWeapon);
            }
            

            UIController.instance.levelUpPanel.SetActive(false);

            //���������������ã�����ò����ٶ�
            if (TestParameter.instance)
            {
                Time.timeScale = TestParameter.instance.timeScale;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

}
