using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunObject : ScriptableObject
{
    public string gunName;
    public GameObject prefab;
}
