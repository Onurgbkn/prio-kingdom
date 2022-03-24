using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[SelectionBase]
public class Slave : MonoBehaviour
{
    ResourceHandler reshand;
    Animator animator;
    NavMeshAgent agent;

    public List<string> jobs;
    public string curJob;
    public string jobState;
    public bool near2target;

    public GameObject targetObj;

    public GameObject axe;

    private void Start()
    {
        reshand = GameObject.Find("GameHandler").GetComponent<ResourceHandler>();
        reshand.slaves.Add(this);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        GetJob();
        Time2Work(curJob);
    }

    private void Update()
    {

    }

    public void GetJob()
    {
        if (jobState != "caryn")
        {
            foreach (string job in jobs)
            {
                if (reshand.available_jobs.Contains(job))
                {
                    if (curJob != job)
                    {
                        if (jobState == "workn")
                        {
                            targetObj.GetComponent<Resource>().workerCount -= 1;
                        }
                        jobState = "begin2job";
                    }
                    curJob = job;
                    Time2Work(curJob);
                    return;
                }
            }
            curJob = "idle";
            jobState = "idle";
        }
        else
        {
            Time2Work(curJob);
        }
    }

    public void Time2Work(string type)
    {
        if (jobState == "begin2job")
        {
            targetObj = reshand.FindSource(type, transform.position);
            if (targetObj == null)
            {
                jobState = "begin2job";
            }
            else
            {
                jobState = "movn2source";
                GetJob();
            }
        }
        else if (jobState == "movn2source")
        {
            if (targetObj == null)
            {
                GetJob();
            }
            else
            {
                agent.destination = targetObj.transform.position;
                if (targetObj.GetComponent<Resource>().maxWorker != targetObj.GetComponent<Resource>().workerCount)
                {
                    if (near2target)
                    {
                        near2target = false;
                        agent.ResetPath();
                        jobState = "workn";
                        targetObj.GetComponent<Resource>().workerCount += 1;
                    }
                }
                else
                {
                    jobState = "begin2job";
                }
            }
        }
        else if (jobState == "workn")
        {
            if (targetObj.GetComponent<Resource>().cur == targetObj.GetComponent<Resource>().max)
            {
                jobState = "caryn";
                targetObj.GetComponent<Resource>().cur = 0;
                targetObj.GetComponent<Resource>().workerCount -= 1;
                if (type == "wood") Destroy(targetObj);
                reshand.GetJob4Slave();
            }
        }
        else if (jobState == "caryn")
        {
            targetObj = reshand.FindStorage(curJob, transform.position);
            if (targetObj == null)
            {
                jobState = "begin2job";
            }
            else
            {
                if (near2target) // depends on slave and storage radius
                {
                    near2target = false;
                    agent.ResetPath();
                    targetObj.GetComponent<Storage>().AddResource(5);
                    if (curJob == type)
                    {
                        targetObj = reshand.FindSource(type, transform.position);
                        if (targetObj == null)
                        {
                            jobState = "begin2job";
                            GetJob();
                            return;
                        }
                        else
                        {
                            jobState = "movn2source";
                            agent.destination = targetObj.transform.position;
                        }
                    }
                }
                else
                {
                    agent.destination = targetObj.transform.position;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == targetObj && !near2target)
        {
            if (jobState == "movn2source" || jobState == "caryn")
            {
                near2target = true;
                Time2Work(curJob);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetObj && !near2target)
        {
                near2target = false;
        }
    }
}
