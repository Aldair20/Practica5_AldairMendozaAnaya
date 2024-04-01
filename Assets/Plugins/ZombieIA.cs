using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Agregar esta línea
using UnityEngine.AI;
using UnityEngine;

public class ZombieIA : MonoBehaviour
{
    public Transform user;
    public float attackRange = 60.0f; // Distancia mínima para atacar
    private NavMeshAgent enemyAgent; // Utiliza solo NavMeshAgent en lugar de UnityEngine.AI.NavMeshAgent
    public bool UserDetect;
    private Animator enemyAnimator;
    private bool isAttacking = false;
	private bool isDead = false;
	private float time = 0.0f; // Distancia mínima para atacar


    public void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisiona tiene el tag "MainCamera"
        if (other.CompareTag("Bala"))
        {
			isDead = true;
        }
		if (other.CompareTag("MainCamera"))
        {
			if(isDead == false)
			{
				SceneManager.LoadScene("GameOverScene");
			}
		}
    }

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>(); // Utiliza solo NavMeshAgent en lugar de UnityEngine.AI.NavMeshAgent
        enemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
		if (Vector3.Distance(transform.position, user.position) <= (4.5*attackRange) && isDead == false)
		{
			UserDetect = true;
		}
		if (Vector3.Distance(transform.position, user.position) > (4.5*attackRange) && isDead == false)
		{
			UserDetect = false;
            enemyAgent.ResetPath();
            enemyAnimator.SetInteger("RUN", 0);
            enemyAnimator.SetInteger("ATTACK", 0);
            isAttacking = false;
		}
        if (UserDetect && isDead == false)
        {
            enemyAgent.destination = user.position;
            enemyAnimator.SetInteger("RUN", 1);
            if (Vector3.Distance(transform.position, user.position) > attackRange && isDead == false)
            {
                enemyAnimator.SetInteger("ATTACK", 0);
                isAttacking = false;
                enemyAnimator.SetInteger("RUN", 1);
				
            }
            if (Vector3.Distance(transform.position, user.position) <= attackRange && enemyAnimator.GetInteger("RUN") == 1 && isDead == false)
            {
                if (!isAttacking && isDead == false)
                {
                    enemyAnimator.SetInteger("ATTACK", 1);
                    isAttacking = true;
                }
            }
            if (Vector3.Distance(transform.position, user.position) > attackRange && enemyAnimator.GetInteger("RUN") == 1 && isDead == false)
            {
                isAttacking = false;
            }
        }
        if (!UserDetect && isDead == false)
        {
            enemyAgent.ResetPath();
            enemyAnimator.SetInteger("RUN", 0);
            enemyAnimator.SetInteger("ATTACK", 0);
            isAttacking = false;
        }
		if (isDead)
        {
			enemyAnimator.SetInteger("DIE", 1);
			UserDetect = false;
            enemyAgent.ResetPath();
            enemyAnimator.SetInteger("RUN", 0);
            enemyAnimator.SetInteger("ATTACK", 0);
            isAttacking = false;
			time += 0.1f;
            // Esperar 5 segundos antes de revivir
			if(time >= 50.0f)
			{
				enemyAnimator.SetInteger("REVIVIR", 1);
				if(time >= 80.0f)
				{
					enemyAnimator.SetInteger("DIE", 0);
					enemyAnimator.SetInteger("REVIVIR", 0);
					time = 0.0f;
					isDead = false;
				}
			}
        }
    }
}
