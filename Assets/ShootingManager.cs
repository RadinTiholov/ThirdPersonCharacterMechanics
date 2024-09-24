using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public GameObject muzzle;
    void Start()
    {
        if (muzzle != null)
        {
            muzzle.SetActive(false);
        }
    }

    public void EnableMuzzle() 
    {
        muzzle.SetActive(true);
    }

    public void DesableMuzzle()
    {
        muzzle.SetActive(false);
    }
}
