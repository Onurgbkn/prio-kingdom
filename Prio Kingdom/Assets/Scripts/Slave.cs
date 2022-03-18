using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slave : MonoBehaviour
{
    ResourceHandler reshand;
    Animator animator;
    NavMeshAgent agent;

    public List<string> jobs;
    public string curJob;
    public string state;

    public GameObject axe;

    private void Start()
    {
        reshand = GameObject.Find("GameHandler").GetComponent<ResourceHandler>();
        reshand.slaves.Add(this);
        jobs.Add("wood");
        jobs.Add("copper");
        jobs.Add("iron");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        GetJob();
    }

    private void Update()
    {
        if (curJob == "idle")
        {
            animator.SetBool("moving", false);
            animator.SetBool("working", false);
        }else if(curJob == "copper") {
            DoMining("copper");
        }
        else if (curJob == "iron")
        {
            DoMining("iron");
        }
        else if (curJob == "wood")
        {
            CutTrees();
        }
    }

    public void GetJob()
    {
        foreach (string job in jobs)
        {
            if (reshand.available_jobs.Contains(job))
            {
                curJob = job;
                return;
            }
        }
        curJob = "idle";
    }

    private void DoMining(string type)
    {
        Transform nearest = GetNearestSource(type).transform;
        if (Vector3.Distance(agent.transform.position, nearest.position) < 5)
        {
            if (state == "moving")
            {
                state = "working";
                agent.ResetPath();
                animator.SetBool("moving", false);
                animator.SetBool("working", true);
                nearest.GetComponent<Resource>().workerCount += 1;
            }
            if (state == "working" && nearest.GetComponent<Resource>().cur == nearest.GetComponent<Resource>().max)
            {
                state = "carry";
                nearest.GetComponent<Resource>().cur = 0;
                nearest.GetComponent<Resource>().workerCount -= 1;
                animator.SetBool("moving", true);
                animator.SetBool("working", false);
                agent.destination = nearest.GetComponent<Resource>().nearest_storage.transform.position;
            }
            
        }
        else
        {
            if (state != "carry")
            {
                state = "moving";
                agent.destination = nearest.position;
                animator.SetBool("moving", true);
                animator.SetBool("working", false);
            }
            else
            {
                Transform nearstore = nearest.GetComponent<Resource>().nearest_storage.transform;
                agent.destination = nearstore.transform.position;
                if (Vector3.Distance(agent.transform.position, nearstore.position) < 5)
                {
                    nearstore.GetComponent<Storage>().AddResource(5);
                    state = "moving";
                }
            }
        }
    }

    private void CutTrees()
    {
        Transform nearest = GetNearestSource("wood").transform;
        if (Vector3.Distance(agent.transform.position, nearest.GetChild(3).transform.position) < 5)
        {
            if (state == "moving")
            {
                state = "working";
                agent.ResetPath();
                transform.LookAt(nearest.GetChild(3).transform.position);
                animator.SetBool("moving", false);
                animator.SetBool("cutting", true);
                nearest.GetComponent<Resource>().workerCount += 1;
            }
            if (state == "working" && nearest.GetComponent<Resource>().cur == nearest.GetComponent<Resource>().max)
            {
                state = "carry";
                agent.destination = nearest.GetComponent<Resource>().nearest_storage.transform.position;
                Destroy(nearest.gameObject);
                animator.SetBool("moving", true);
                animator.SetBool("cutting", false);
            }

        }
        else
        {
            if (state != "carry")
            {
                state = "moving";
                agent.destination = nearest.GetChild(3).transform.position;
                animator.SetBool("moving", true);
                animator.SetBool("cutting", false);
            }
            else
            {
                Transform nearstore = nearest.GetComponent<Resource>().nearest_storage.transform;
                agent.destination = nearstore.transform.position;
                if (Vector3.Distance(agent.transform.position, nearstore.position) < 4)
                {
                    nearstore.GetComponent<Storage>().AddResource(5);
                    state = "moving";
                }
            }
        }

        if (state == "working")
        {
            axe.SetActive(true);
        }
        else
        {
            axe.SetActive(false);
        }
    }

    private GameObject GetNearestSource(string type)
    {
        GameObject nearest = null;
        float nearDist = 9999;
        foreach (Resource source in reshand.GetSources(type))
        {
            float dist = Vector3.Distance(transform.position, source.transform.position);
            if (dist < nearDist)
            {
                nearest = source.gameObject;
                nearDist = dist;
            }
        }
        return nearest;
    }


    private GameObject GetNearestStorage(string type)
    {
        GameObject nearest = null;
        float nearDist = 9999;
        foreach (Storage storage in reshand.GetStorages(type))
        {
            float dist = Vector3.Distance(transform.position, storage.transform.position);
            if (dist < nearDist)
            {
                nearest = storage.gameObject;
                nearDist = dist;
            }
        }
        return nearest;
    }
}
