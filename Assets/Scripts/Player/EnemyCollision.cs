using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private GameState gameStateScript;
    [SerializeField]
    private TMP_Text livesText;
    private SoundController soundController;
    private void Start()
    {
        animator = GetComponent<Animator>();
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("Attacked");
            gameStateScript.setLives(gameStateScript.getLives() - 1);
            livesText.text = gameStateScript.getLives() + "";
            soundController.PlayPlayerHit();
        }
    }

    private void ResetAttacked()
    {
        animator.ResetTrigger("Attacked");
    }
}
