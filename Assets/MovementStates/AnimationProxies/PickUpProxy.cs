using UnityEngine;

public class PickUpProxy : MonoBehaviour
{
    public GameObject akModel;

    private void Start()
    {
        if (akModel != null)
        {
            akModel.SetActive(false);
        }
    }

    // Called when the animation is at the state: "The player lifted up the object"
    public void PickUp()
    {
        Debug.Log("Pick up");

        if (akModel != null)
        {
            akModel.SetActive(true);
        }
    }
}
