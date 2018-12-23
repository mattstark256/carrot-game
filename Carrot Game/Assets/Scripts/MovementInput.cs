using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RabbitCharacterController))]
public class MovementInput : MonoBehaviour
{
    private RabbitCharacterController characterController;


    private void Awake()
    {
        characterController = GetComponent<RabbitCharacterController>();
    }


    void Update()
    {
        Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (inputVector != Vector2.zero)
        {
            characterController.MoveTowardsPosition(transform.position + (Vector3)inputVector);
        }

    }
}
