using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float paddingLeft = .5f;
    [SerializeField] float paddingRight = .5f;
    [SerializeField] float paddingTop = .5f;
    [SerializeField] float paddingBottom = .5f;
    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    Shooter shooter;

    void Awake() {
        shooter = GetComponent<Shooter>();
    }

    void Start() {
        InitBounds();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPosition = new Vector2();
        
        newPosition.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPosition.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        
        transform.position = newPosition;
    }

    void OnMove(InputValue value) {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue inputValue)
    {
        if (shooter != null)
        {
            shooter.isFiring = inputValue.isPressed;
        }
    }
}
