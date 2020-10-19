using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Red,
    Green,
    Yellow
}
public struct Character
{
    public string _name;
    public float _speed;
}
public class Player : MonoBehaviour, IOnSelectable
{
    private DataManager _data;
    public Material _materialToBlink;
    public PlayerType _type;
    private Color _nativeMat;
    public float _speed;
    public string _name;
    public static Dictionary<string, float> character = new Dictionary<string, float>();

    // Start is called before the first frame update
    private void Awake()
    {
        _nativeMat = GetComponent<Renderer>().material.color;
        _data = FindObjectOfType<DataManager>().GetComponent<DataManager>();
        SetStats();


    }

    private Character stats;

    private void SetStats()
    {

        _data.Load();
        stats = _data.data;
        if (stats._name == null)
        {
            stats = new Character();

            switch (_type)
            {
                case PlayerType.Red:
                    stats = new Character
                    {
                        _speed = 1f,
                        _name = "Bob"

                    };
                    break;
                case PlayerType.Green:
                    stats = new Character
                    {
                        _speed = 1.15f,
                        _name = "James"
                    };
                    break;
                case PlayerType.Yellow:
                    stats = new Character
                    {
                        _speed = 1.3f,
                        _name = "Ostin"
                    };
                    break;
            }
        }

        _speed = stats._speed;
        _name = stats._name;

        character.Add(_name, _speed);

        _data.Save(stats);
    }
    public void Select(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        StartCoroutine(BlinkObject(renderer));

    }

    private IEnumerator BlinkObject(Renderer rend)
    {
        yield return new WaitForFixedUpdate();

        float elapsed = 0;

        float timeToDetect = 0.2f;

        while (elapsed < timeToDetect)
        {
            elapsed += Time.fixedDeltaTime;

            rend.material.color = Color.Lerp(rend.material.color, _materialToBlink.color, Time.fixedDeltaTime * 10f);

            yield return new WaitForFixedUpdate();

        }

        elapsed = 0;

        while (elapsed < timeToDetect)
        {
            elapsed += Time.fixedDeltaTime;

            rend.material.color = Color.Lerp(rend.material.color, _nativeMat, Time.fixedDeltaTime * 10f);

            yield return new WaitForFixedUpdate();

        }

        yield return new WaitForFixedUpdate();
    }
    private void OnApplicationQuit()
    {
        _data.Save(stats);

    }
}

public interface IOnSelectable
{
    void Select(GameObject obj);
}