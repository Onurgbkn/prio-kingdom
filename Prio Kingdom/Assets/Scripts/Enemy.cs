using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health;
    public int power;
    public float attackRange;

    Animator animator;
    NavMeshAgent agent;
    ResourceHandler rh;
    RaidHandler raidHand;

    public Slave target;

    public string state;
    void Start()
    {
        state = "moving2target";
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rh = GameObject.Find("GameHandler").GetComponent<ResourceHandler>();
        raidHand = GameObject.Find("GameHandler").GetComponent<RaidHandler>();
        target = rh.slaves[Random.Range(0, rh.slaves.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 1)
        {
            if (state != "ded")
            {
                state = "ded";
                agent.ResetPath();
                animator.SetTrigger("ded");
                raidHand.enemies.Remove(this);
                DestroyBody();
            }
        }
        else
        {
            if (state != "beatBegin" && (target == null || target.GetComponent<Slave>().health <= 0))
            {
                if (raidHand.alives.Count == 0) // check if all workers dead
                {
                    agent.SetDestination(new Vector3(500, 0, 500));
                    state = "back2home";
                }
                else
                {
                    target = raidHand.alives[Random.Range(0, raidHand.alives.Count)];
                    agent.SetDestination(target.transform.position);
                    state = "moving2target";
                }
                animator.SetBool("walkn", true);
            }
            else
            {
                if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
                {
                    transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                    if (state == "moving2target")
                    {
                        target.targetEnemy = this;
                        state = "beatBegin";
                        animator.SetBool("walkn", false);
                        agent.ResetPath();
                        animator.SetTrigger("atk" + Random.Range(1, 5));
                    }
                }
                else
                {
                    if (state != "beatBegin")
                    {
                        agent.SetDestination(target.transform.position);
                        animator.SetBool("walkn", true);
                        state = "moving2target";
                    }
                }
            }
        }
    }

    public void HitLanded()
    {
        if (target != null)
        {
            target.GetComponent<Slave>().health -= power;
        }
    }

    public void BeatEnd()
    {
        state = "moving2target";
    }

    IEnumerator DestroyBody()
    {
        yield return new WaitForSeconds(10f);// Wait a bit
        Destroy(this.gameObject);
    }
}
