using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float arenaResistanceForce = 3f;
    public Transform northBorder;
    public Transform southBorder;
    public Transform westBorder;
    public Transform eastBorder;
    

    void Awake()
    {
        Instance = this;
    }

    public Vector3 GetArenaForce(Vector3 pos)
    {
        float x = pos.x, z = pos.z;
        var forcesVector = new Vector3(
            -Mathf.Clamp01(x - eastBorder.position.x) + Mathf.Clamp01(westBorder.position.x - x), 0,
            - Mathf.Clamp01(z - northBorder.position.z) + Mathf.Clamp01(southBorder.position.z - z));
        return forcesVector.normalized * arenaResistanceForce;
    }
}
