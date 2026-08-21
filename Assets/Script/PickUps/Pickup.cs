using UnityEngine;

public class Pickup : MonoBehaviour
{

    public float value = 1;

    void PickupObject(PlayerController player)
    {
        bool result = player.ReceivePickup(this);
        {
            if(result == true)
            {
                //Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PickupObject(other.GetComponent<PlayerController>());
        }
    }
}
