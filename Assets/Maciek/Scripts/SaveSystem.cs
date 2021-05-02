using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SavePlayer(Player player) {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.xd";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

        PlayerData data = new PlayerData(player);
        //Debug.Log("saved");

        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static PlayerData LoadPlayer() {
        string path = Application.persistentDataPath + "/player.xd";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            if (stream.Length <= 174) {
                return null;
            }
            else {
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                return data;
            }
        }
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    public static bool IsValid() {
        string path = Application.persistentDataPath + "/player.xd";
        if (File.Exists(path)) {
            return true;
        }
        else{
            return false;
        }
    }
    public static void ResetValues() {

        string path = Application.persistentDataPath + "/player.xd";
        File.Delete(path);
    }
}
