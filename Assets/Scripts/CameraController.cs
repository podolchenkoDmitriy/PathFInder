using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float zPos = 7f;
    private Vector3 topPos;
    private void Start()
    {
        topPos = new Vector3(0, 20, 5);
    }
    public void MoveCamera()
    {
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (Vector3.Distance(transform.position, topPos) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, topPos, Time.fixedDeltaTime * 5f);
            yield return new WaitForFixedUpdate();

        }
        while (transform.eulerAngles.x < 85f)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(90, 0, 0), Time.fixedDeltaTime * 2f);
            yield return new WaitForFixedUpdate();

        }
        topPos = new Vector3(0, topPos.y * 0.5f, topPos.z - zPos);
    }
    public void MoveToNextLevel()
    {
        StartCoroutine(NextLevelRoutine());
    }

    private IEnumerator NextLevelRoutine()
    {
        while (Vector3.Distance(transform.position, topPos) > 3f)
        {

            transform.position = Vector3.MoveTowards(transform.position, topPos, Time.fixedDeltaTime * 5f);
            yield return new WaitForFixedUpdate();

        }
        while (transform.eulerAngles.x > 46f)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(45, 0, 0), Time.fixedDeltaTime * 2f);
            yield return new WaitForFixedUpdate();

        }
        topPos = new Vector3(0, 20, topPos.z + zPos * 2f);

    }
}
