using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

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

        public void BulidPanel(int maxX, int maxY,GameObject panelPart) {
            x = maxX;
            y = maxY;
            
            float posX = 0;
            float posY = 0;
            int countX = 1;
            int countY = 1;
            for (int i = 1; i <= (x*y); i++) {

                GameObject spawnedPart = Instantiate(panelPart, new Vector2(posX, posY), Quaternion.identity);
                spawnedPart.name = "PanelPart_" + countX + "-" + countY;

                if (x > countX) {
                    posX += 38.75f;
                    countX += 1;
                }
                else {
                    posX = 0;
                    posY -= 38.75f;
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
                    Instantiate(cX[Random.Range(0, cX.Count)], new Vector2(J.transform.position.x + 19.375f, J.transform.position.y), Quaternion.identity);
                }
                if (J.GetComponent<PlatformInfo>().platformKey.Contains("b")) {
                    Instantiate(cY[Random.Range(0, cY.Count)], new Vector2(J.transform.position.x, J.transform.position.y - 19.375f), Quaternion.identity);
                }
            }
        }
        public void SpawnEnemies(List<GameObject> e) {
            for (int j = 1; j <= spawnedPlatforms.Count-2; j++) {
                if (spawnedPlatforms[j].GetComponent<PlatformInfo>().withEnemies) {
                    int enemiesNum = Random.Range(3,7);
                    for(int i =0; i<=enemiesNum; i++) {
                        Instantiate(
                            e[Random.Range(0, e.Count)],
                            new Vector2(spawnedPlatforms[j].transform.position.x+Random.Range(-5,6), spawnedPlatforms[j].transform.position.y + Random.Range(-5, 6)),
                            Quaternion.identity);
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
                    spawnedBossPanel = Instantiate(bPanel, new Vector2(0, 43.75f), Quaternion.identity);
                    spawnedBossPanel.GetComponent<PlatformInfo>().platformKey = "b";
                    spawnedBossPanel.GetComponent<PlatformInfo>().OpenExits();
                    Instantiate(
                        cY[Random.Range(0, cY.Count)],
                        new Vector2( spawnedPlayerPanel.transform.position.x, spawnedPlayerPanel.transform.position.y+19.375f),
                        Quaternion.identity);

                    break;
                case 1:
                    spawnedPlayerPanel = Instantiate(pPanel, new Vector2(0, 0), Quaternion.identity);
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().platformKey = "r";
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().OpenExits();
                    spawnedBossPanel = Instantiate(bPanel, new Vector2(43.75f, 0), Quaternion.identity);
                    spawnedBossPanel.GetComponent<PlatformInfo>().platformKey = "l";
                    spawnedBossPanel.GetComponent<PlatformInfo>().OpenExits();
                    Instantiate(
                        cX[Random.Range(0, cX.Count)],
                        new Vector2(spawnedPlayerPanel.transform.position.x + 19.375f, spawnedPlayerPanel.transform.position.y ),
                        Quaternion.identity);
                    break;
                case 2:
                    spawnedPlayerPanel = Instantiate(pPanel, new Vector2(0, 0), Quaternion.identity);
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().platformKey = "b";
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().OpenExits();
                    spawnedBossPanel = Instantiate(bPanel, new Vector2(0, -43.75f), Quaternion.identity);
                    spawnedBossPanel.GetComponent<PlatformInfo>().platformKey = "t";
                    spawnedBossPanel.GetComponent<PlatformInfo>().OpenExits();
                    Instantiate(
                        cY[Random.Range(0, cY.Count)],
                        new Vector2(spawnedPlayerPanel.transform.position.x, spawnedPlayerPanel.transform.position.y - 19.375f),
                        Quaternion.identity);
                    break;
                case 3:
                    spawnedPlayerPanel = Instantiate(pPanel, new Vector2(0, 0), Quaternion.identity);
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().platformKey = "l";
                    spawnedPlayerPanel.GetComponent<PlatformInfo>().OpenExits();
                    spawnedBossPanel = Instantiate(bPanel, new Vector2(-43.75f, 0), Quaternion.identity);
                    spawnedBossPanel.GetComponent<PlatformInfo>().platformKey = "r";
                    spawnedBossPanel.GetComponent<PlatformInfo>().OpenExits();
                    Instantiate(
                        cX[Random.Range(0, cX.Count)],
                        new Vector2(spawnedPlayerPanel.transform.position.x - 19.375f, spawnedPlayerPanel.transform.position.y),
                        Quaternion.identity);
                    break;
            }
            playerSpawnPoint = spawnedPlayerPanel.transform.position;
            bossSpawnPoint = spawnedBossPanel.transform.position;

        }
        public void SpawnBoss(GameObject boss) {
            Instantiate(boss, bossSpawnPoint, Quaternion.identity);
        }
        public void BulidFinalPhase(List<GameObject> smallPlatform, GameObject bigPlatform, List<GameObject> corridors) {
            GameObject spawnedBigPlatform = null;
            List<GameObject> spawnedSmallPlatforms = new List<GameObject>();
            List<GameObject> cX = new List<GameObject>();
            List<GameObject> cY = new List<GameObject>();
            string pKey = "";
            foreach (GameObject J in corridors) {
                if (J.name.Contains("X")) {
                    cX.Add(J);
                }
                if (J.name.Contains("Y")) {
                    cY.Add(J);
                }
            }
            spawnedBigPlatform =  Instantiate(bigPlatform, new Vector2(0,0), Quaternion.identity);
            spawnedBigPlatform.name = "Big Panel";
            switch (Random.Range(0, 4)) {
                case 0:
                    spawnedSmallPlatforms.Add(Instantiate(smallPlatform[Random.Range(0, smallPlatform.Count)],new Vector2(0,25f),Quaternion.identity));
                    spawnedSmallPlatforms[0].GetComponent<PlatformInfo>().platformKey = "b";
                    spawnedSmallPlatforms[0].name = "Platform_1";
                    spawnedSmallPlatforms.Add(Instantiate(smallPlatform[Random.Range(0, smallPlatform.Count)], new Vector2(26.25f, 0), Quaternion.identity));
                    spawnedSmallPlatforms[1].GetComponent<PlatformInfo>().platformKey = "l";
                    spawnedSmallPlatforms[1].name = "Platform_2";
                    spawnedSmallPlatforms.Add(Instantiate(smallPlatform[Random.Range(0, smallPlatform.Count)], new Vector2(-26.25f, 0), Quaternion.identity));
                    spawnedSmallPlatforms[2].GetComponent<PlatformInfo>().platformKey = "r";
                    spawnedSmallPlatforms[2].name = "Platform_3";
                    pKey = "trl";
                    break;
                case 1:
                    spawnedSmallPlatforms.Add(Instantiate(smallPlatform[Random.Range(0, smallPlatform.Count)], new Vector2(0, 25f), Quaternion.identity));
                    spawnedSmallPlatforms[0].GetComponent<PlatformInfo>().platformKey = "b";
                    spawnedSmallPlatforms[0].name = "Platform_1";
                    spawnedSmallPlatforms.Add(Instantiate(smallPlatform[Random.Range(0, smallPlatform.Count)], new Vector2(26.25f, 0), Quaternion.identity));
                    spawnedSmallPlatforms[1].GetComponent<PlatformInfo>().platformKey = "l";
                    spawnedSmallPlatforms[1].name = "Platform_2";
                    spawnedSmallPlatforms.Add(Instantiate(smallPlatform[Random.Range(0, smallPlatform.Count)], new Vector2(0, -25f), Quaternion.identity));
                    spawnedSmallPlatforms[2].GetComponent<PlatformInfo>().platformKey = "t";
                    spawnedSmallPlatforms[2].name = "Platform_3";
                    pKey = "trb";
                    break;
                case 2:
                    spawnedSmallPlatforms.Add(Instantiate(smallPlatform[Random.Range(0, smallPlatform.Count)], new Vector2(26.25f, 0), Quaternion.identity));
                    spawnedSmallPlatforms[0].GetComponent<PlatformInfo>().platformKey = "l";
                    spawnedSmallPlatforms[0].name = "Platform_1";
                    spawnedSmallPlatforms.Add(Instantiate(smallPlatform[Random.Range(0, smallPlatform.Count)], new Vector2(0, -25f), Quaternion.identity));
                    spawnedSmallPlatforms[1].GetComponent<PlatformInfo>().platformKey = "t";
                    spawnedSmallPlatforms[1].name = "Platform_2";
                    spawnedSmallPlatforms.Add(Instantiate(smallPlatform[Random.Range(0, smallPlatform.Count)], new Vector2(-26.25f, 0), Quaternion.identity));
                    spawnedSmallPlatforms[2].GetComponent<PlatformInfo>().platformKey = "r";
                    spawnedSmallPlatforms[2].name = "Platform_3";
                    pKey = "rbl";
                    break;
                case 3:
                    spawnedSmallPlatforms.Add(Instantiate(smallPlatform[Random.Range(0, smallPlatform.Count)], new Vector2(0, -25f), Quaternion.identity));
                    spawnedSmallPlatforms[0].GetComponent<PlatformInfo>().platformKey = "t";
                    spawnedSmallPlatforms[0].name = "Platform_1";
                    spawnedSmallPlatforms.Add(Instantiate(smallPlatform[Random.Range(0, smallPlatform.Count)], new Vector2(-26.25f, 0), Quaternion.identity));
                    spawnedSmallPlatforms[1].GetComponent<PlatformInfo>().platformKey = "r";
                    spawnedSmallPlatforms[1].name = "Platform_2";
                    spawnedSmallPlatforms.Add(Instantiate(smallPlatform[Random.Range(0, smallPlatform.Count)], new Vector2(0, 25f), Quaternion.identity));
                    spawnedSmallPlatforms[2].GetComponent<PlatformInfo>().platformKey = "b";
                    spawnedSmallPlatforms[2].name = "Platform_3";
                    pKey = "blt";
                    break;
            }
            playerSpawnPoint = spawnedSmallPlatforms[Random.Range(0, spawnedSmallPlatforms.Count)].transform.position;

            spawnedBigPlatform.GetComponent<PlatformInfo>().platformKey=pKey;
            spawnedBigPlatform.GetComponent<PlatformInfo>().OpenExits();

            foreach (GameObject J in spawnedSmallPlatforms) {
                J.GetComponent<PlatformInfo>().OpenExits();
            }
            
        }


        public void StartFinalPhase(List<GameObject> enemies) {
            List<GameObject> platforms = new List<GameObject>();
            for (int i = 1; i <= 3; i++) {
                platforms.Add(GameObject.Find("Platform_"+i));
                Debug.Log(platforms[i - 1]);
            }
            int enemiesNum = Random.Range(20, 31);
            for (int i = 0; i <= enemiesNum; i++) {
                int platformId = Random.Range(0, platforms.Count);
                Instantiate(
                    enemies[Random.Range(0,enemies.Count)],
                    new Vector2(platforms[platformId].transform.position.x + Random.Range(-5,5)
                                ,platforms[platformId].transform.position.y + Random.Range(-5, 5)),
                    Quaternion.identity
                    );
            }
        }

        public void SpawnPlayer(GameObject player) {
            Instantiate(player, playerSpawnPoint,Quaternion.identity);
        }
        public void SpawnLights(GameObject  light) {
            Instantiate(light, new Vector2(0,0), Quaternion.identity);
        }
    }

    public Initalize init = new Initalize();
}