using UnityEngine;
using UnityEngine.InputSystem;
using PurrNet;
using PurrNet.Prediction;

public class PlayerMovement : PredictedIdentity<PlayerMovement.Input, PlayerMovement.State>
{
    [SerializeField] private PredictedRigidbody rb;
    [SerializeField] private Transform visual;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedSmoothing = 0f;
    [SerializeField] private float rotationSmoothing = 0f;


    protected override void Simulate(Input input, ref State state, float delta)
    {
        if(input.direction != Vector3.zero){
            visual.forward = Vector3.Slerp(visual.forward, input.direction, rotationSmoothing * Time.deltaTime);
        }

        Vector3 targetSpeed = input.direction * speed + new Vector3(0f, rb.linearVelocity.y, 0f);

        Vector3 targetVelocity = speedSmoothing > 0
            ? Vector3.Lerp(rb.linearVelocity, targetSpeed, speedSmoothing * Time.fixedDeltaTime)
            : targetSpeed;
        rb.AddForce(targetVelocity - rb.linearVelocity, ForceMode.VelocityChange);
    }

    protected override void GetFinalInput(ref Input input)
    {
        Vector3 tempDirection = Vector3.zero;
        if (Keyboard.current != null)
        {
            tempDirection.x = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0);
            tempDirection.z = (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0);
        }
        tempDirection = Quaternion.Euler(0f, PlayerCamera.Instance.yRotation, 0f) * tempDirection;
        if (tempDirection.sqrMagnitude > 1f) tempDirection = tempDirection.normalized;
        input.direction = tempDirection;
    }

    public struct State : IPredictedData<State>
    {
        public void Dispose() {}
    }

    public struct Input : IPredictedData
    {
        public Vector3 direction;
        public void Dispose() {}
    }
}
