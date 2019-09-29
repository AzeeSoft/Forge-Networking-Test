using System;
using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class HoverCraftController : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float boostFactor = 1;
    public float rotSpeed;
    public float boostRotFactor = 1;
    public float worldYToMaintain = 5;

    private HoverCraftModel _hoverCraftModel;
    private CharacterController _characterController;
    private Animator _animator;
    public HoverCraftNetworkObject networkObject => _hoverCraftModel.networkObject;

    private Vector3 originalPosition;
    private float curSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        _hoverCraftModel = GetComponent<HoverCraftModel>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        originalPosition = transform.position;
    }
    
    void FixedUpdate()
    {
        if (!networkObject.IsOwner)
        {
            SyncWithOwner();
            return;
        }

        Move();
    }

    void Move()
    {
        float rot = Input.GetAxis("Horizontal");
        float forward = Input.GetAxis("Vertical");

        if (forward < 0)
        {
            rot *= -1;
        }

        float tarSpeed = forward * speed;
        float tarRotSpeed = rotSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            tarSpeed *= boostFactor;
            tarRotSpeed *= boostRotFactor;
        }
        curSpeed = Mathf.Lerp(curSpeed, tarSpeed, acceleration * Time.fixedDeltaTime);

        transform.Rotate(transform.up, rot * tarRotSpeed * Time.fixedDeltaTime);
        _characterController.Move(transform.forward * curSpeed * Time.fixedDeltaTime);

        var correctedPos = transform.position;
        correctedPos.y = worldYToMaintain;
        transform.position = correctedPos;

        _animator.SetFloat("hor", rot);
        _animator.SetFloat("speed", Math.Abs(tarSpeed));

        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
    }

    void SyncWithOwner()
    {
        transform.position = networkObject.position;
        transform.rotation = networkObject.rotation;
    }
}
