
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureRoomTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player"))
            return;

        triggered = true;

        // Overwrite the save file with the starting position.
        SimplePlayerMovement.SaveObject saveObject = new SimplePlayerMovement.SaveObject
            {
            position = new Vector3(0f, 2.5f, -3)
        };

        string path = Path.Combine(Application.dataPath,"Saves","saveFile.sav");

        Directory.CreateDirectory(Path.GetDirectoryName(path));

        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(path, json);

        Debug.Log("Save overwritten with starting position: " + saveObject.position);

        //Advance to the next level.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.currentLevel++;

            Debug.Log("Advancing to Level: " + GameManager.Instance.currentLevel);
        }

        //Reload the scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
