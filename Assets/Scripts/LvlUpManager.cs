using UnityEngine;

public class LvlUpManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int _lvlupCost = 10;
    public void LvlUp(Player obj)
    {
        if (Coins._currentCoins >= _lvlupCost)
        {
            UIManager.instance.UpdateCoins(-_lvlupCost);

            obj.transform.localScale *= 1.01f;

            Player.character[obj._name] *= 1.5f;
        }
    }

}
