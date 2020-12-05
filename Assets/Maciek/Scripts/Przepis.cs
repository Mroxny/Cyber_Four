using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Przepis : MonoBehaviour
{

    public class Initalize{

        private int x;
        private int y;
        private List<GameObject> selectedParts = new List<GameObject>();
        private List<GameObject> spawnedPlatforms = new List<GameObject>();
        private List<GameObject> hall = new List<GameObject>();
        private Vector2 playerSpawnPoint;
        private Vector2 bossSpawnPoint;

        public void BulidPanel(int maxX, int maxY) {
            x = maxX;
            y = maxY;
            GameObject panelPart = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Maciek/Prefabs/PanelPart.prefab", typeof(GameObject));
            GameObject corridor = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Maciek/Prefabs/Corridor.prefab", typeof(GameObject));
            float posX = 0;
            float posY = 5;
            int countX = 1;
            int countY = 1;
            for (int i = 1; i <= (x*y); i++) {

                GameObject spawnedPart = Instantiate(panelPart, new Vector2(posX, posY), Quaternion.identity);
                spawnedPart.name = "PanelPart_" + countX + "-" + countY;

                if (x > countX) {
                    posX += 22.8f;
                    countX += 1;
                }
                else {
                    posX = 0;
                    posY -= 26.8f;
                    countX = 1;
                    countY += 1;
                }

            }
        }

        public void FindPlatfromPlaces() {
            selectedParts.Add(GameObject.Find("PanelPart_"+Random.Range(1,x+1)+"-1"));

            bool onBottom = false;
            int i = 0;

            while (onBottom == false) {
                for (int z = 0; z < selectedParts.Count; z++) {
                    print(selectedParts[z]);
                }
                int coX = int.Parse(selectedParts[i].name.Substring((selectedParts[i].name.IndexOf("_") + 1), 1));
                int coY = int.Parse(selectedParts[i].name.Substring((selectedParts[i].name.IndexOf("-") + 1), 1));

                if (coY==y ) {
                    onBottom = true;
                    switch (Random.Range(0, 3)) {
                        case 0:
                            if (coX != x) {
                                selectedParts.Add(GameObject.Find("PanelPart_" + (coX + 1) + "-" + coY));
                                print("Moved Right");
                            }
                            else {
                                selectedParts.Add(GameObject.Find("PanelPart_" + (coX - 1) + "-" + coY));
                                print("Moved Left");
                            }
                            break;
                        case 1:
                            if (coX != 1) {
                                selectedParts.Add(GameObject.Find("PanelPart_" + (coX - 1) + "-" + coY));
                                print("Moved Left");
                            }
                            else {
                                selectedParts.Add(GameObject.Find("PanelPart_" + (coX + 1) + "-" + coY));
                                print("Moved Right");
                            }
                            break;
                        case 2:
                            print("Not Moved");
                            break;
                    }
                }
                else {
                    if (coX==1) {
                        if (i > 0 && int.Parse(selectedParts[i-1].name.Substring(selectedParts[i-1].name.IndexOf("-") + 1, 1))==coY) {
                            selectedParts.Add(GameObject.Find("PanelPart_" + coX + "-" + (coY + 1)));
                            print("Moved Down");
                        }
                        else {
                            switch (Random.Range(0,2)) {
                                case 0:
                                    selectedParts.Add(GameObject.Find("PanelPart_" + (coX + 1) + "-" + coY));
                                    print("Moved Right");
                                    break;
                                case 1:
                                    selectedParts.Add(GameObject.Find("PanelPart_" + coX + "-" + (coY + 1)));
                                    print("Moved Down");
                                    break;
                            }
                        }
                    }
                    else if (coX == x) {
                        if (i > 0 && int.Parse(selectedParts[i - 1].name.Substring(selectedParts[i - 1].name.IndexOf("-") + 1, 1)) == coY) {
                            selectedParts.Add(GameObject.Find("PanelPart_" + coX + "-" + (coY + 1)));
                            print("Moved Down");
                        }
                        else {
                            switch (Random.Range(0, 2)) {
                                case 0:
                                    selectedParts.Add(GameObject.Find("PanelPart_" + (coX - 1) + "-" + coY));
                                    print("Moved Left");
                                    break;
                                case 1:
                                    selectedParts.Add(GameObject.Find("PanelPart_" + coX + "-" + (coY + 1)));
                                    print("Moved Down");
                                    break;
                            }
                        }
                    }
                    else {
                        if (i > 0 && int.Parse(selectedParts[i - 1].name.Substring(selectedParts[i - 1].name.IndexOf("_") + 1, 1)) == (coX - 1)) {
                            switch (Random.Range(0, 2)) {
                                case 0:
                                    selectedParts.Add(GameObject.Find("PanelPart_" + (coX + 1) + "-" + coY));
                                    print("Moved Roght");
                                    break;
                                case 1:
                                    selectedParts.Add(GameObject.Find("PanelPart_" + coX + "-" + (coY + 1)));
                                    print("Moved Down");
                                    break;
                            }
                        }
                        else if (i > 0 && int.Parse(selectedParts[i - 1].name.Substring(selectedParts[i - 1].name.IndexOf("_") + 1, 1)) == (coX + 1)) {
                            switch (Random.Range(0, 2)) {
                                case 0:
                                    selectedParts.Add(GameObject.Find("PanelPart_" + (coX - 1) + "-" + coY));
                                    print("Moved Left");
                                    break;
                                case 1:
                                    selectedParts.Add(GameObject.Find("PanelPart_" + coX + "-" + (coY + 1)));
                                    print("Moved Down");
                                    break;
                            }
                        }
                        else {
                            switch (Random.Range(0, 3)) {
                                case 0:
                                    selectedParts.Add(GameObject.Find("PanelPart_" + (coX + 1) + "-" + coY));
                                    print("Moved Right");
                                    break;
                                case 1:
                                    selectedParts.Add(GameObject.Find("PanelPart_" + (coX - 1) + "-" + coY));
                                    print("Moved Left");
                                    break;
                                case 2:
                                    selectedParts.Add(GameObject.Find("PanelPart_" + coX + "-" + (coY+1)));
                                    print("Moved Down");
                                    break;
                            }
                        }
                    }
                    i++;
                }
            }
           

        }

        public void PlacePlatforms(List<GameObject> platforms) {

            for (int i = 0; i < selectedParts.Count; i++) {
                if (selectedParts[i] == null) {
                    selectedParts = null;
                    i = 0;
                    FindPlatfromPlaces();
                }
            }

            string pkey = "";
            int cox = int.Parse(selectedParts[0].name.Substring((selectedParts[0].name.IndexOf("_") + 1), 1));
            int coy = int.Parse(selectedParts[0].name.Substring((selectedParts[0].name.IndexOf("-") + 1), 1));
            foreach (GameObject k in selectedParts) {
                if (k.name.Contains("_" + cox + "-" + (coy - 1))) {
                    pkey += "t";
                }
                if (k.name.Contains("_" + (cox + 1) + "-" + coy)) {
                    pkey += "r";
                }
                if (k.name.Contains("_" + cox + "-" + (coy + 1))) {
                    pkey += "b";
                }
                if (k.name.Contains("_" + (cox - 1) + "-" + coy)) {
                    pkey += "l";
                }
            }
            spawnedPlatforms.Add(Instantiate(platforms[0], new Vector2(selectedParts[0].transform.position.x, selectedParts[0].transform.position.y - 6.4f), Quaternion.identity));
            spawnedPlatforms[0].GetComponent<PlatformInfo>().platformKey = pkey;
            spawnedPlatforms[0].GetComponent<PlatformInfo>().OpenExits();
            playerSpawnPoint = spawnedPlatforms[0].transform.position;

            for (int i = 1; i < selectedParts.Count; i++) {
                string pKey="";
                int coX = int.Parse(selectedParts[i].name.Substring((selectedParts[i].name.IndexOf("_") + 1), 1));
                int coY = int.Parse(selectedParts[i].name.Substring((selectedParts[i].name.IndexOf("-") + 1), 1));
                foreach (GameObject k in selectedParts) {
                    if (k.name.Contains("_" + coX + "-" + (coY - 1))) {
                        pKey += "t";
                    }
                    if (k.name.Contains("_" + (coX + 1) + "-" + coY)) {
                        pKey += "r";
                    }
                    if (k.name.Contains("_" + coX + "-" + (coY + 1))) {
                        pKey += "b";
                    }
                    if (k.name.Contains("_" + (coX - 1) + "-" + coY)) {
                        pKey += "l";
                    }
                }
                spawnedPlatforms.Add(Instantiate(platforms[Random.Range(1,platforms.Count)],new Vector2(selectedParts[i].transform.position.x, selectedParts[i].transform.position.y-6.4f),Quaternion.identity));
                spawnedPlatforms[i].GetComponent<PlatformInfo>().platformKey = pKey;
                spawnedPlatforms[i].GetComponent<PlatformInfo>().OpenExits();
            }
        }

        public void PlaceCorridors(List<GameObject> corridors) {
            List<GameObject> cX = new List<GameObject>();
            List<GameObject> cY = new List<GameObject>();
            foreach (GameObject J in corridors) {
                if (J.name.Contains("X")) {
                    cX.Add(J);
                }
                if (J.name.Contains("Y")) {
                    cY.Add(J);
                }
            }
            foreach (GameObject J in spawnedPlatforms) {
                if (J.GetComponent<PlatformInfo>().platformKey.Contains("r")) {
                    Instantiate(cX[Random.Range(0, cX.Count)], J.transform.GetChild(1).transform.GetChild(0).transform.position, Quaternion.identity);
                }
                if (J.GetComponent<PlatformInfo>().platformKey.Contains("b")) {
                    Instantiate(cY[Random.Range(0, cY.Count)], J.transform.GetChild(1).transform.GetChild(1).transform.position, Quaternion.identity);
                }
            }
        }
        public void SpawnEnemies(List<GameObject> e) {
            foreach (GameObject J in spawnedPlatforms) {
                if (J.GetComponent<PlatformInfo>().withEnemies) {
                    foreach (Transform K in J.transform.GetChild(2).transform) {
                        Instantiate(e[Random.Range(0, e.Count)],K);
                    }
                }
            }

        }
        public void SpawnBossLair(GameObject pPanel, GameObject bPanel,List<GameObject> corridors) {
            GameObject spawnedPlayerPanel=null;
            GameObject spawnedBossPanel=null;
            List<GameObject> cX = new List<GameObject>();
            List<GameObject> cY = new List<GameObject>();
            foreach (GameObject J in corridors) {
                if (J.name.Contains("X")) {
                    cX.Add(J);
                }
                if (J.name.Contains("Y")) {
                    cY.Add(J);
                }
            }
            switch (Random.Range(0,4)) {
                case 0:
                    spawnedPlayerPanel= Instantiate(pPanel, new Vector2(0,0), Quaternion.identity);
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().platformKey = "t";
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().OpenExits();
                    spawnedBossPanel = Instantiate(bPanel, new Vector2(0, 20), Quaternion.identity);
                    spawnedBossPanel.GetComponent<PlatformInfo>().platformKey = "b";
                    spawnedBossPanel.GetComponent<PlatformInfo>().OpenExits();
                    Instantiate(cY[Random.Range(0, cY.Count)],new Vector2( spawnedPlayerPanel.transform.GetChild(1).transform.GetChild(1).transform.position.x, spawnedPlayerPanel.transform.GetChild(1).transform.GetChild(1).transform.position.y+10), Quaternion.identity);

                    break;
                case 1:
                    spawnedPlayerPanel = Instantiate(pPanel, new Vector2(0, 0), Quaternion.identity);
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().platformKey = "r";
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().OpenExits();
                    spawnedBossPanel = Instantiate(bPanel, new Vector2(20, 0), Quaternion.identity);
                    spawnedBossPanel.GetComponent<PlatformInfo>().platformKey = "l";
                    spawnedBossPanel.GetComponent<PlatformInfo>().OpenExits();
                    Instantiate(cX[Random.Range(0, cX.Count)], spawnedPlayerPanel.transform.GetChild(1).transform.GetChild(0).transform.position, Quaternion.identity);
                    break;
                case 2:
                    spawnedPlayerPanel = Instantiate(pPanel, new Vector2(0, 0), Quaternion.identity);
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().platformKey = "b";
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().OpenExits();
                    spawnedBossPanel = Instantiate(bPanel, new Vector2(0, -20), Quaternion.identity);
                    spawnedBossPanel.GetComponent<PlatformInfo>().platformKey = "t";
                    spawnedBossPanel.GetComponent<PlatformInfo>().OpenExits();
                    Instantiate(cY[Random.Range(0, cY.Count)], spawnedPlayerPanel.transform.GetChild(1).transform.GetChild(1).transform.position, Quaternion.identity);
                    break;
                case 3:
                    spawnedPlayerPanel = Instantiate(pPanel, new Vector2(0, 0), Quaternion.identity);
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().platformKey = "l";
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().OpenExits();
                    spawnedBossPanel = Instantiate(bPanel, new Vector2(-20, 0), Quaternion.identity);
                    spawnedBossPanel.GetComponent<PlatformInfo>().platformKey = "r";
                    spawnedBossPanel.GetComponent<PlatformInfo>().OpenExits();
                    Instantiate(cX[Random.Range(0, cX.Count)], new Vector2(spawnedPlayerPanel.transform.GetChild(1).transform.GetChild(0).transform.position.x-10, spawnedPlayerPanel.transform.GetChild(1).transform.GetChild(0).transform.position.y), Quaternion.identity);
                    break;
            }
            playerSpawnPoint = spawnedPlayerPanel.transform.position;
            bossSpawnPoint = spawnedBossPanel.transform.position;

        }
        public void SpawnBoss(GameObject boss) {
            Instantiate(boss, bossSpawnPoint, Quaternion.identity);
        }
        public void SpawnPlayer(GameObject player) {
            Instantiate(player, playerSpawnPoint,Quaternion.identity);
        }
    }

    public Initalize init = new Initalize();
}