using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathCreator : MonoBehaviour
{
    // Start is called before the first frame update
    private Player _player;
    private PathDrawing _drawer;
    private Ray _ray;
    private RaycastHit hit;
    private Dictionary<NavMeshAgent, float> players = new Dictionary<NavMeshAgent, float>();
    private Ray Hit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, ray.direction * 100, Color.blue);
            if (hit.collider.GetComponent<Player>() && _player == null)
            {
                _player = hit.collider.GetComponent<Player>();

                _player.Select(_player.gameObject);

                _drawer = _player.GetComponentInChildren<PathDrawing>();

                StartCoroutine(DrawPath());
            }
        }
        _ray = ray;

        return _ray;
    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Hit();
        }
    }

    private IEnumerator DrawPath()
    {
        LineRenderer line = _drawer.GetComponent<LineRenderer>();

        line.enabled = true;

        line.SetPosition(0, _player.transform.position);

        NavMeshAgent agent = _player.GetComponent<NavMeshAgent>();

        while (_player != null)
        {
            Hit();

            agent.destination = hit.point;

            agent.speed = 0;

            int n = 1;

            while (n < agent.path.corners.Length)
            {
                if (agent.path.corners.Length > 2)
                {

                    line.positionCount = agent.path.corners.Length;

                    line.SetPositions(agent.path.corners);

                }

                n++;

                yield return null;

                if (Input.GetKeyUp(KeyCode.Mouse0))
                {

                    if (!players.ContainsKey(agent))
                    {

                        players.Add(agent, Player.character[_player._name]);
                    }
                    _player = null;

                    CheckIfAllPlayersHavePath();
                }

            }
            yield return null;


        }
    }

    private void CheckIfAllPlayersHavePath()
    {
        if (players.Count == (System.Enum.GetValues(typeof(PlayerType)).Length))
        {
            StartCoroutine(StartMoving());

            UIManager.instance._buttons.SetActive(false);
        }
    }

    private IEnumerator StartMoving()
    {
        Camera.main.GetComponent<CameraController>().MoveCamera();

        float remainDist = 0;

        foreach (KeyValuePair<NavMeshAgent, float> item in players)
        {
            item.Key.speed = item.Value;
            yield return new WaitForFixedUpdate();

            remainDist += item.Key.remainingDistance;

        }


        foreach (KeyValuePair<NavMeshAgent, float> item in players)
        {
            while (item.Key.remainingDistance > 0.5f)
            {
                yield return new WaitForFixedUpdate();

            }
        }

        players.Clear();
        Camera.main.GetComponent<CameraController>().MoveToNextLevel();
        UIManager.instance.SetOnText();
        UIManager.instance._buttons.SetActive(true);


    }

}
