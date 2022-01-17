using UnityEngine;
public interface ImainTrajectory
{
    void SetLastPoint(Vector3 point);
}

public class MainTrajectory : Trajectory, ImainTrajectory
{
    private Vector3 _lastPoint;
    public void SetLastPoint(Vector3 point)
    {
        _lastPoint = point;
    }

    public override void ShowTrajectory(Vector3 originPoint, Vector3 speed)
    {
        Activate(true);
        Vector3 newDirection = _lastPoint - originPoint;
        float newDirectionLenght = newDirection.magnitude;
        float Speedlenght = speed.magnitude;

        if (Speedlenght > newDirectionLenght)
        {
            // speed = -newDirection;
            // base.ShowTrajectory(originPoint, -newDirection);

            Debug.DrawRay(originPoint, -newDirection, Color.red);
            Vector3[] linePoints = new Vector3[2] { originPoint, _lastPoint };
            _lineRenderer.positionCount = linePoints.Length;

            for (int i = 0; i < linePoints.Length; i++)
            {
                _lineRenderer.SetPosition(i, linePoints[i]);
            }

            //_lineRenderer.SetPositions(linePoints);
        }
        else
        {
            base.ShowTrajectory(originPoint, speed);
        }
    }

}
