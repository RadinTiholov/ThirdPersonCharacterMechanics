using UnityEngine;

public class CombatStateManager : MonoBehaviour
{
    public static CombatStateManager Instance { get; private set; }

    public bool weaponInHittingPosition = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional if you want it to persist between scenes
        }
    }
}
