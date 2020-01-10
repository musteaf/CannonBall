using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    private BallMovement ballMovement;
    private bool launched = true;
    private TargetPlatform targetPlatform;
    private ProjectilePathDrawer projectilePathDrawer;

    public void OnTouch(float angleY, float angleZ)
    {
        Vector3 initialVelocity = ballMovement.CalculateInitialVelocity(angleY, angleZ, targetPlatform.Position);
        projectilePathDrawer.DrawPath(ballMovement, initialVelocity);
    }

    public void OnUp(float angleY, float angleZ)
    {
        Vector3 initialVelocity = ballMovement.CalculateInitialVelocity(angleY, angleZ, targetPlatform.Position);
        projectilePathDrawer.RemovePath();
        ballMovement.Launch(initialVelocity);
        launched = true;
    }

    public bool Refresh()
    {
        if (launched)
        {
            if (ballMovement && ballMovement.gameObject)
                Destroy(ballMovement.gameObject);
            GameObject currentBall = Instantiate(ballPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            ballMovement = currentBall.GetComponent<BallMovement>();
            launched = false;
            return true;
        }
        return false;
    }

    public void setTargetPlatform(TargetPlatform targetPlatform)
    {
        this.targetPlatform = targetPlatform;
    }
    
    public void setProjectilePathDrawer(ProjectilePathDrawer projectilePathDrawer)
    {
        this.projectilePathDrawer = projectilePathDrawer;
    }
}
