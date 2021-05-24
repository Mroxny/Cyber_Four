using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryButtonHandler : MonoBehaviour
{
    public GameObject owner;
    public int id;

    public void SetIcon(Sprite image) {
        GameObject icon = transform.Find("WeaponIcon").gameObject;
        icon.GetComponent<Image>().sprite = image;
        float iconWidth = image.rect.width;
        float iconHeight = image.rect.height;
        icon.GetComponent<RectTransform>().sizeDelta = new Vector2(ExtensionMethods.Remap(iconWidth, 0, 20, 0, 47), ExtensionMethods.Remap(iconHeight, 0, 20, 0, 47));
    }
    public void SetDmg(string dmg) {
        transform.Find("Dmg").GetComponent<TextMeshProUGUI>().text = dmg;
    }
    public void SetAmmo(int ammo) {
        if (ammo == 0) {
            transform.Find("Ammo").gameObject.SetActive(false);
            transform.Find("AmmoText").gameObject.SetActive(false);
        }
        else {
            transform.Find("Ammo").GetComponent<TextMeshProUGUI>().text = ammo.ToString();
        }
    }
    public void SetName(string name) {
        transform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;
    }
    public void OnClick() {
        owner.GetComponent<Window_Inventory>().SetWeaponInSlot(id);
    }
}
