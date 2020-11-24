using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Przepis : MonoBehaviour
{
    
    public class Initalize {
        private List<GameObject> bottomPlatform = new List<GameObject>();
        private List<GameObject> topPlatform = new List<GameObject>();
        private List<GameObject> leftPlatform = new List<GameObject>();
        private List<GameObject> rightPlatform = new List<GameObject>();
        private List<GameObject> crossPlatform = new List<GameObject>();
        private List<GameObject> hall = new List<GameObject>();

        public void LoadPlatforms(List<GameObject> bp = null, List<GameObject> tp = null, List<GameObject> lp = null, List<GameObject> rp = null, List<GameObject> cp = null, List<GameObject> h = null) {

            if (bp != null) {
                foreach (GameObject x in bp) {
                    bottomPlatform.Add(x);
                }
            }

            if (tp != null) {
                foreach (GameObject x in tp) {
                    topPlatform.Add(x);
                }
            }

            if (lp != null) {
                foreach (GameObject x in lp) {
                    leftPlatform.Add(x);
                }
            }

            if (rp != null) {
                foreach (GameObject x in rp) {
                    rightPlatform.Add(x);
                }
            }

            if (cp != null) {
                foreach (GameObject x in cp) {
                    crossPlatform.Add(x);
                }
            }

            if (h != null) {
                foreach (GameObject x in h) {
                    hall.Add(x);
                }
            }
           


        }
        public void BulidMap() {
            //Budowanko
         
            GameObject bp = bottomPlatform != null ? bottomPlatform[Random.Range(0, bottomPlatform.Count)] : null;
            GameObject tp = topPlatform != null ? topPlatform[Random.Range(0, topPlatform.Count)] : null;
            GameObject lp = leftPlatform != null ? leftPlatform[Random.Range(0, leftPlatform.Count)] : null;
            GameObject rp = rightPlatform != null ? rightPlatform[Random.Range(0, rightPlatform.Count)] : null;
            GameObject cp = crossPlatform != null ? crossPlatform[Random.Range(0, crossPlatform.Count)] : null;
            GameObject h = hall != null ? hall[Random.Range(0, hall.Count)] : null;
            //Instantiate(platform1,new Vector3(Random.Range(0,5), Random.Range(0,5), 0), Quaternion.identity);
        }
        public void SpawnEnemies(string[] XD) {
            for (int i = 0; i < XD.Length; i++ ){
                print(XD[i]);
            }
            
        }
    }

    public Initalize init = new Initalize();

void Start()
    {
       
    }

}
