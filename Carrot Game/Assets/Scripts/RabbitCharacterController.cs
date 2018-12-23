using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RabbitCharacterController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1;

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void MoveTowardsPosition(Vector3 position)
    {
        Vector3 vectorToPosition = position - transform.position;
        float maxMoveDistance = moveSpeed * Time.deltaTime;
        Vector3 destinationPosition =
            (vectorToPosition.magnitude > maxMoveDistance) ?
            transform.position + vectorToPosition.normalized * maxMoveDistance :
            position;
        rb.MovePosition(destinationPosition);
    }
}
