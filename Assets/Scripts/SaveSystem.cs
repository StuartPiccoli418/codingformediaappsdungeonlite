using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveSystem : MonoBehaviour
{
   
private void Update()
{
    if (Keyboard.current == null)
        return;

    if (Keyboard.current.tKey.wasPressedThisFrame)
    {
        SaveGame();
    }

    if (Keyboard.current.yKey.wasPressedThisFrame)
    {
      LoadGame();
    }
}

private bool LoadGame()
{
    string path = Application.dataPath + "/Saves/saveFile.sav";

    if (!File.Exists(path))
    {
        Debug.LogError("Save file not found: " + path);
        return false;
    }

    string json = File.ReadAllText(path);

    SimplePlayerMovement.SaveObject saveObject =
        JsonUtility.FromJson<SimplePlayerMovement.SaveObject>(json);

    if (saveObject == null)
    {
        Debug.LogError("Failed to load save data.");
        return false;
    }

    if (SimplePlayerMovement.Instance == null)
    {
        Debug.LogError("Player instance was not found!");
        return false;
    }


        DungeonGenerator dungeon = Object.FindAnyObjectByType<DungeonGenerator>();

        if (dungeon == null)
        {
            Debug.LogError("DungeonGenerator was not found!");
            return false;
        }

        // Spawn all rooms leading up to the saved position.
        dungeon.SpawnRoomsUpToPosition(saveObject.position);

        // Then restore the player's position.
        SimplePlayerMovement.Instance.Load(saveObject);

        Debug.Log("Player position loaded: " + saveObject.position);

        return true;

    }

    private void SaveGame()
    {
        SimplePlayerMovement.SaveObject saveObject = SimplePlayerMovement.Instance.Save();
        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/Saves/saveFile.sav", json);
    }
}

