using System.IO;
using UnityEngine;
using UnityEngine.InputSystem; // Required for the new Input System

[RequireComponent(typeof(CharacterController))]
public class SimplePlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 12f;
    private CharacterController controller;

    public static SimplePlayerMovement Instance { get; private set; }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ensure a keyboard is connected
        if (Keyboard.current == null) return;

        float moveX = 0f;
        float moveZ = 0f;

        // Check WASD or Arrow keys using the new Input System
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveX = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveX = 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveZ = -1f;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveZ = 1f;

        // Combine inputs into a direction vector and normalize so diagonal movement isn't faster
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        // Move the player
        controller.Move(move * speed * Time.deltaTime);
    }
    private void Awake() {
        Instance = this;
        /*
        string json = JsonUtility.ToJson(new SaveObject {
            position = transform.position,
        });
        Debug.Log(Application.dataPath + "\n" + Application.persistentDataPath);  
        File.WriteAllText(Application.dataPath + "/Saves/saveFile.sav" ,json);
        */
        /* string json = File.ReadAllText(Application.dataPath + "/Saves/saveFile.sav");
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(json);
        Debug.Log(saveObject.position);
        */
        
    }

    [System.Serializable]
    public class SaveObject
    {
        public Vector3 position;
    }

    public SaveObject Save()
    {
        return new SaveObject
        {
            position = transform.position
        };
    }

    public void Load(SaveObject saveObject)
    {
        CharacterController controller =
            GetComponent<CharacterController>();

        controller.enabled = false;
        transform.position = saveObject.position;
        controller.enabled = true;
    }

}