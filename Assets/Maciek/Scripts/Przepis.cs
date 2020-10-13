using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Przepis : MonoBehaviour
{

    public class Initalize {
        private string test = "XD";
        public void BulidMap() {
            print(test);
        }
        public void SpawnEnemies(string[] XD) {
            for (int i = 0; i < 2; i++ ){
                print(XD[i]);
            }
            
        }
    }

    public Initalize init = new Initalize();

void Start()
    {
       
    }

}
