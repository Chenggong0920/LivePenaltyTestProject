using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface ISoccerBall
{
    public void shoot(Vector3 targetPosition);
}

public class SoccerBall : MonoBehaviour, ISoccerBall
{
    [SerializeField]
    private float initialSpeed = 100f;
    Vector3 collisionPoint;

    private Rigidbody rigidbodyComponent;

    
    [Inject]
    private IUIManager uiManager;

    private void Awake() {
        rigidbodyComponent = GetComponent<Rigidbody>();
        rigidbodyComponent.isKinematic = true;
    }

    public void shoot(Vector3 targetPosition)
    {
        rigidbodyComponent.isKinematic = false;
        rigidbodyComponent.AddForce(Vector3.Normalize(targetPosition - transform.position) * initialSpeed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        collisionPoint = collision.contacts[0].point;

        uiManager.drawImpact(collision.contacts[0].point);
        rigidbodyComponent.isKinematic = true;
    }

    private void OnDrawGizmos() {
        if( collisionPoint != Vector3.zero )
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(collisionPoint, 0.2f);
        }

    }
}
