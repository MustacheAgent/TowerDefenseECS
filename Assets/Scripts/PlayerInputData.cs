using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Input")]
public class PlayerInputData : ScriptableObject
{
    public Vector3 keyInput;
    public Vector3 mousePosition;
    public bool leftMousePressed;
}
