using UnityEngine;

public class Collectible : MonoBehaviour
{

    public PlayerType _boxType;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Player>())
        {
            if (collision.collider.GetComponent<Player>()._type == _boxType)
            {
                OnDestroying();
            }
        }
    }

    private void OnDestroying()
    {
        gameObject.SetActive(false);
        UIManager.instance.UpdateCoins(Coins._coinModifier);

    }
}
