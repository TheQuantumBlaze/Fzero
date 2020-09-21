using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Sprite still;
    public Sprite below200;
    public Sprite above200;
    public Sprite stillTilted;
    public Sprite below200Tilted;
    public Sprite above200Tilted;
    public Sprite exploding;

    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!PlayerController.Singleton.IsAlive)
            spriteRenderer.sprite = exploding;
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                spriteRenderer.flipX = true;

                if (PlayerController.Singleton.acceleration < 0.01f)
                {
                    spriteRenderer.sprite = stillTilted;
                }
                else if (PlayerController.Singleton.acceleration < 0.5f)
                {
                    spriteRenderer.sprite = below200Tilted;
                }
                else
                {
                    spriteRenderer.sprite = above200Tilted;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                spriteRenderer.flipX = false;

                if (PlayerController.Singleton.acceleration < 0.01f)
                {
                    spriteRenderer.sprite = stillTilted;
                }
                else if (PlayerController.Singleton.acceleration < 0.5f)
                {
                    spriteRenderer.sprite = below200Tilted;
                }
                else
                {
                    spriteRenderer.sprite = above200Tilted;
                }
            }
            else
            {
                if (PlayerController.Singleton.acceleration < 0.01f)
                {
                    spriteRenderer.sprite = still;
                }
                else if (PlayerController.Singleton.acceleration < 0.5f)
                {
                    spriteRenderer.sprite = below200;
                }
                else
                {
                    spriteRenderer.sprite = above200;
                }
            }
        }
    }
}
