using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class FieldOfVieweEdit : Editor
{
    private void OnSceneGUI()
    {
        Enemy fov = (Enemy)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.FovRadius);

        Vector3 viewAngleLeft = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.Angle / 2);
        Vector3 viewAngleRigth = DirectionFromAngle(fov.transform.eulerAngles.y, fov.Angle / 2);

        Handles.color = Color.yellow;
        
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleLeft * fov.FovRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleRigth * fov.FovRadius);

        if(fov.CanSeePlayer)
        {
            Handles.color = Color.cyan;
            Handles.DrawLine(fov.transform.position, fov.Player.transform.position);
        }
        
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees*Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees*Mathf.Deg2Rad));
    }
}
