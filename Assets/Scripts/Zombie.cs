using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    Animator animator = null;
    public GameObject playerObject = null;
    public float checkDistance = 5f;
    public float attackDistance = 1.0f;
    public float HP = 100;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Idle();
    }

    // 대기, 추격(이동), 공격, 피격, 죽음
    void Idle()
    {
        animator.SetBool("isMove", false);

        Vector3 playerVector = playerObject.transform.position;
        Vector3 zombieVector = transform.position;
        float playerZombieDistance = Vector3.Distance(playerVector, zombieVector);
        
        // distance between zombie and player is smaller than certain threashold.
        if(playerZombieDistance < attackDistance)
        {
            Attack();
        }
        else if (playerZombieDistance < checkDistance)
        {
            // 추격한다
            Move();
        }
        else // 다시 걸리가 멀어졌다면 거리가 멀었다면 set move to false
        {
            animator.SetBool("isMove", false);
        }
    }

    void Move()
    {
        animator.SetBool("isMove", true);
        GetComponent<NavMeshAgent>().destination = playerObject.transform.position;
    }

    private void Attack()
    {
        animator.SetTrigger("isAttack");        
    }


    private void OnCollisionEnter(Collision collision)
    {
        // HP가 깍인다. hp = hp - 상대방의 공격력
        // 애니메이션 출력 (피격)
        if(collision.gameObject.name == "JusticeSword")
        {
            Hit();
        }
        
        // 만약 HP가 0 이하라면 죽어야한다.
    }

    private void Hit()
    {
        HP = HP - 10;
        if (HP <= 0)
        {
            Death();
        }
        else
        {
            animator.SetTrigger("isHit");
        }
    }

    private void Death()
    {
        animator.SetBool("isDeath", true);
    }


}
