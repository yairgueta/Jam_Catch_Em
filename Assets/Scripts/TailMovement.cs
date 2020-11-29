using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailMovement : MonoBehaviour
{
    public Vector2 range;
    private float time;
    private Animator animator;
    private int animationID;
    void Start()
    {
        animator = GetComponent<Animator>();
        animationID = Animator.StringToHash("TailTrigger");
        time = Random.Range(range.x, range.y);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = Random.Range(range.x, range.y);
            animator.SetTrigger(animationID);
        }
    }
}
