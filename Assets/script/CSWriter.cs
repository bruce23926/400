using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSWriter : MonoBehaviour
{
    void Awake() {
        Application.targetFrameRate = 60;
    }
    
    public float TimeStart = 0;
    bool startRun = false;
    public int timeCount = 60;
    float basex;
    float basey;
    float basez;
    Vector3 basePos;
    float dis;
    string filename = "";
    [System.Serializable]
    public class Player {
        public string name;
        public int aa;
        public int bb;
        public int cc;
        
    }
    public class Playerlist {
        public Player[] player;
    }

    public Playerlist myPlayerList = new Playerlist(); 

    void Start() {
        ///filename = Application.dataPath + "/test.csv";
        filename = "/Assets/test.csv";
    }

    void Update() {
        if (TimeStart <= 0) {
            WriteCSV();
            TimeStart = 10;
            
        }
        TimeStart -= Time.deltaTime;
        if ((startRun == false) && (timeCount >= 0)) {
            timeCount -= 1;
        } else if (startRun == false) {
            startRun = true;
            timeCount = 60;
            basex = gameObject.transform.position.x;
            basey = gameObject.transform.position.y;
            basez = gameObject.transform.position.z;
            basePos = new Vector3(basex, basey, basez);
        }
    }

    public void WriteCSV() {
        if (myPlayerList.player.Length > 0) {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("name, aa, bb, cc");
            tw.Close();

            tw = new StreamWriter(filename, true);

            if (startRun == true) {
                for (int i = 0; i < myPlayerList.player.Length; i++) {
                    
                    dis = Vector3.Distance(gameObject.transform.position, basePos);
                    tw.WriteLine(myPlayerList.player[i].name + ',' + gameObject.transform.position.x + ',' + gameObject.transform.position.y + ',' + gameObject.transform.position.z + ',' + dis);
                    ///tw.WriteLine(myPlayerList.player[i].name + ',' + myPlayerList.player[i].aa + ',' + myPlayerList.player[i].bb + ',' + myPlayerList.player[i].cc);
                }
            }
            tw.Close();
        }
    }
}
