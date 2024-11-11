using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            animator.SetTrigger("isSkeleton");
        } else if (random == 1)
        {
            animator.SetTrigger("isDC");
        }
        // gob is default animation
    }
}
