using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDataBase : MonoBehaviour
{
    public List<GameObject> weaponPool;

    void Start()
    {
        
    }
    public int GetWeaponId(GameObject weapon) {
        for (int i=0; i<weaponPool.Count; i++) {
            if (weapon.name.IndexOf(" ") >0) {
                if (weapon.name.Substring(0, weapon.name.IndexOf(" ")) == weaponPool[i].name) {
                    return i;
                }
            }
            else if (weapon.name.IndexOf("(") > 0) {
                if (weapon.name.Substring(0, weapon.name.IndexOf("(")) == weaponPool[i].name) {
                    return i;
                }
            }
            else if (weapon.name == weaponPool[i].name) {
                return i;
            }
        }
        return -1;
    }
    public GameObject GetWeapon(int id) {
        if (id>=0 && id<weaponPool.Count) {
              return weaponPool[id];
        }
        return null;
    }

    public Sprite GetWeaponIcon(int id) {
        if (id >= 0 && id < weaponPool.Count) {
            return weaponPool[id].transform.Find("Mesh").GetComponent<SpriteRenderer>().sprite;
        }
        return null;
    }
    }
