﻿using UnityEngine;

public class Player : MonoBehaviour
{
    public static int NUM_SPRITES = 4;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;
    public Sprite deathSprite;
    public bool isDead;

    private Vector3 direction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) || (Input.GetKeyDown(KeyCode.RightAlt)) || Input.GetMouseButtonDown(1))
        {
            direction = Vector3.down * strength;
        }

        // Apply gravity and update the position
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;

        /* if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase = TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        } */ /* I don't understand the error this gives */
    }

    private void AnimateSprite()
    {
        spriteIndex++;
        if (!isDead)
        {
            if (spriteIndex >= sprites.Length)
            {
                spriteIndex = 0;
            }

            if (spriteIndex < sprites.Length && spriteIndex >= 0)
            {
                spriteRenderer.sprite = sprites[spriteIndex];
            }
        }
        else
        {
            isDead = false;
            spriteRenderer.sprite = deathSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            isDead = true;
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.gameObject.CompareTag("Scoring"))
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }

}
