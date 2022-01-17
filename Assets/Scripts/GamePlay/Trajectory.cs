using UnityEngine;

public interface ITrajectory
{
    void ShowTrajectory(Vector3 originPoint, Vector3 speed);
    void Activate(bool isOn);
}

public class Trajectory : MonoBehaviour, ITrajectory
{
    [SerializeField] private float _lengthCoeficient;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
    }

    public void ShowTrajectory(Vector3 originPoint, Vector3 speed)
    {
        Vector3[] linePoints = new Vector3[10];
        _lineRenderer.positionCount = linePoints.Length;

        for (int i = 0; i < linePoints.Length; i++)
        {
            float time = i * 0.1f;
            linePoints[i] = originPoint - speed * _lengthCoeficient * time;
        }

        _lineRenderer.SetPositions(linePoints);
    }

    public void Activate(bool isOn)
    {
        gameObject.SetActive(isOn);
    }
}
