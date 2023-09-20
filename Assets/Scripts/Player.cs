using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.UIElements;



public class Player : MonoBehaviour
{
 
    public float moveSpeed = 1;

    Animator animator = null;
    public GameObject playerObject = null;
    public float HP = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");



        if (ver != 0 || hor != 0)
        {

            GetComponent<Animator>().SetBool("isMove", true);
             
            
            Vector3 moveVector = new Vector3();

            moveVector.z = ver * moveSpeed * Time.deltaTime;
            moveVector.x = hor * moveSpeed * Time.deltaTime;
            
            transform.position = transform.position + moveVector;

            Vector3 checkVector = new Vector3();
            if(moveVector != checkVector)
            {
                transform.forward = moveVector;
            }

        //    transform.position += moveVector;
        }
        else
        {
            GetComponent<Animator>().SetBool("isMove", false);
        }


        bool att = Input.GetButtonDown("Fire1");
        if (att == true)
        {
            GetComponent<Animator>().SetTrigger("isAttack");
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            // HP가 깍인다. hp = hp - 상대방의 공격력
            HP = HP - 10;
            // 애니메이션 출력 (피격)
            if (HP <= 0)
            {
                Death();
            }
            else
            {
                animator.SetTrigger("isHit");
            }
            // 만약 HP가 0 이하라면 죽어야한다.
        }


        private void Death()
        {
            animator.SetBool("isDeath", true);
        }




    }
}
