using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float pushForce = 3.0f;

    private int _health;
    private ControllerColliderHit _contact;

    public void Hurt(int damage)
    {
        Managers.Player.ChangeHealth(-damage);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}
