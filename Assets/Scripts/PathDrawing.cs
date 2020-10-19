using UnityEngine;

public class PathDrawing : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer _line;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _line.material = GetComponentInParent<MeshRenderer>().material;
        _line.enabled = false;
    }

}
