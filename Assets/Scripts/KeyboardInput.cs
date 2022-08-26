using System;
using UnityEngine;
using UnityEngine.Animations;


public class KeyboardInput : MonoBehaviour
{
        [SerializeField] private PhysicsMovement movement;

        private void Update()
        {
                float xMove = Input.GetAxis("Vertical");
                float zMove = Input.GetAxis("Horizontal");

                movement.Move(new Vector3(xMove, 0, -zMove));
        }
}
