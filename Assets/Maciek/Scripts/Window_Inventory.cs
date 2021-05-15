using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Inventory : MonoBehaviour
{
    public GameObject slot1;
    public GameObject slot2;
    public Animator animator;
    public GameObject button; 
    public GameObject sceneMenager;

    private Camera cam;
    private AudioManager am;
    private int currentSlot;
    private InventoryDataBase db;

    void OnEnable() {
        Initiate();
    }
    public void PlaySound(string soundName) {
        am.Play(soundName);
    }
    public void StopSound(string soundName) {
        am.StopPlaying(soundName);
    }
    public void Initiate() {
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        db = GameObject.Find("Inventory").GetComponent<InventoryDataBase>();
        cam = Camera.main;
        gameObject.transform.Find("Canvas").GetComponent<Canvas>().worldCamera = cam;
        GameObject.Find("Player(Clone)").GetComponent<Player>().DisableHUD();
        if (sceneMenager == null) {

            sceneMenager = GameObject.Find("/SceneMenager");
            print(sceneMenager);
        }
        if (GameObject.Find("TaskNotifier(Clone)") != null) {
            GameObject.Find("TaskNotifier(Clone)").GetComponent<TaskHandler>().HideNote();
        }
        Time.timeScale = 0;
        Player player = GameObject.Find("Player(Clone)").GetComponent<Player>();
        AddNewWeapon(player.gunSlot1);
        AddNewWeapon(player.gunSlot2);
        GetUnlockedWeapons();
        SetWeaponIcon(1, SaveSystem.LoadPlayer().cw[0]);
        SetWeaponIcon(2, SaveSystem.LoadPlayer().cw[1]);
        SetSlot(1);
    }
    private void AddNewWeapon(GameObject weapon) {
        List<int> unlocked = new List<int>();
        for (int i = 0; i < SaveSystem.LoadPlayer().uw.Length; i++) {
            unlocked.Add(SaveSystem.LoadPlayer().uw[i]);
            if (unlocked[i] == db.GetWeaponId(weapon)) {
                return;
            }
        }
        unlocked.Add(db.GetWeaponId(weapon));
        GameObject player = GameObject.Find("Player(Clone)");
        player.GetComponent<Player>().unlockedWeapons = unlocked.ToArray();
        SaveSystem.SavePlayer(player.GetComponent<Player>());
    }
    private void GetUnlockedWeapons() {
        int[] uw = SaveSystem.LoadPlayer().uw;
        foreach (int i in uw) {
            GameObject b = Instantiate(button) as GameObject;
            b.SetActive(true);
            b.GetComponent<InventoryButtonHandler>().SetName(db.GetWeapon(i).GetComponent<WeaponInteract>().weaponName);
            b.GetComponent<InventoryButtonHandler>().SetDmg(db.GetWeapon(i).GetComponent<WeaponInteract>().damage.ToString());
            b.GetComponent<InventoryButtonHandler>().SetIcon(db.GetWeaponIcon(i));
            b.GetComponent<InventoryButtonHandler>().id = i;
            b.transform.SetParent(button.transform.parent, false);
        }
    }
    public void SetWeaponInSlot(int weaponId) {
        GameObject player = GameObject.Find("Player(Clone)");
        if (currentSlot == 1) {
            Destroy(player.GetComponent<Player>().gunSlot1.gameObject);
            player.GetComponent<Player>().gunSlot1 = null;
        }
        else if (currentSlot == 2) {
            Destroy(player.GetComponent<Player>().gunSlot2.gameObject);
            player.GetComponent<Player>().gunSlot2 = null;
        }

        player.GetComponent<Player>().SetWeaponInSlot(Instantiate(db.GetWeapon(weaponId),new Vector3(100,100,0),Quaternion.identity),currentSlot);
        SetWeaponIcon(currentSlot, weaponId);
    }
    public void SetWeaponIcon(int slot, int weaponId) {
        if (slot == 1) {
            GameObject icon = slot1.transform.Find("WeaponIcon").gameObject;
            icon.GetComponent<Image>().sprite = db.GetWeaponIcon(weaponId);
            float iconWidth = db.GetWeaponIcon(weaponId).rect.width;
            float iconHeight = db.GetWeaponIcon(weaponId).rect.height;
            icon.GetComponent<RectTransform>().sizeDelta = new Vector2(ExtensionMethods.Remap(iconWidth, 0, 20, 0, 75), ExtensionMethods.Remap(iconHeight, 0, 20, 0, 75));
        }
        else if (slot == 2) {
            GameObject icon = slot2.transform.Find("WeaponIcon").gameObject;
            icon.GetComponent<Image>().sprite = db.GetWeaponIcon(weaponId);
            float iconWidth = db.GetWeaponIcon(weaponId).rect.width;
            float iconHeight = db.GetWeaponIcon(weaponId).rect.height;
            icon.GetComponent<RectTransform>().sizeDelta = new Vector2(ExtensionMethods.Remap(iconWidth, 0, 20, 0, 75), ExtensionMethods.Remap(iconHeight, 0, 20, 0, 75));
        }
    }
    public void SetSlot(int slot) {
        GameObject player = GameObject.Find("Player(Clone)");
        if (slot == 1) {
            slot1.transform.Find("Border").gameObject.SetActive(true);
            slot2.transform.Find("Border").gameObject.SetActive(false);
            player.GetComponent<Player>().ChangeCurrentGunSlot(1);
            currentSlot = 1;
        }
        else if (slot == 2) {
            slot2.transform.Find("Border").gameObject.SetActive(true);
            slot1.transform.Find("Border").gameObject.SetActive(false);
            player.GetComponent<Player>().ChangeCurrentGunSlot(2);
            currentSlot = 2;
        }
    }

    public void Close() {
        animator.SetTrigger("close");
        StartCoroutine(TurnOffList(.5f));
        StartCoroutine(DisableAfterTime(1.4f));
    }
    IEnumerator TurnOffList(float time) {
        yield return new WaitForSecondsRealtime(time);
        foreach (Transform child in button.transform.parent) {
            child.gameObject.SetActive(false);
        }
    }
        IEnumerator DisableAfterTime(float time) {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);
        Time.timeScale = 1;
        GameObject.Find("Player(Clone)").GetComponent<Player>().EnableHUD();
    }
}
