using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public GameObject ore;

    public Button icon;

    private void Start()
    {
        //ui
        Button iconObj = Instantiate(icon);
        iconObj.transform.SetParent(GameObject.Find("Slave Panel").transform.Find("Cont"));
        iconObj.transform.localScale = new Vector3(1, 1, 1);
        iconObj.onClick.AddListener(delegate { reshand.transform.Find("UI").GetComponent<UIHandler>().SlaveSelected(gameObject);});
        //ui

        ColorHandler();
        reshand = GameObject.Find("GameHandler").GetComponent<ResourceHandler>();
        reshand.slaves.Add(this);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        GetJob();
        Time2Work(curJob);
    }

    private void Update()
    {
        if (jobState == "movn2source" && !animator.GetBool("walkn"))
        {
            animator.SetBool("walkn", true);
            animator.SetBool("workn", false);
            animator.SetBool("caryn", false);
            ore.SetActive(false);
        }
        else if (jobState == "caryn" && !animator.GetBool("caryn"))
        {
            animator.SetBool("walkn", false);
            animator.SetBool("workn", false);
            animator.SetBool("caryn", true);
            ore.SetActive(true);
        }
        else if (jobState == "workn" && !animator.GetBool("workn"))
        {
            animator.SetBool("walkn", false);
            animator.SetBool("workn", true);
            animator.SetBool("caryn", false);
            ore.SetActive(false);

        }
        else if ((jobState == "begin2job" || jobState == "idle"))
        {
            animator.SetBool("walkn", false);
            animator.SetBool("workn", false);
            animator.SetBool("caryn", false);
            ore.SetActive(false);
        }
    }

    private void ColorHandler()
    {
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
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
            agent.ResetPath();
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
            targetObj = reshand.FindSource(type, transform.position);
            if (targetObj == null)
            {
                agent.ResetPath();
                jobState = "begin2job";
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
                        reshand.GetJob4Slave();
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
                ore.GetComponent<MeshRenderer>().material = targetObj.transform.Find("Ore").GetComponent<MeshRenderer>().material;
                
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
                GetJob();
            }
            else
            {
                if (near2target) // depends on slave and storage radius
                {
                    near2target = false;
                    agent.ResetPath();
                    targetObj.GetComponent<Storage>().AddResource(5);
                    jobState = "begin2job";
                    GetJob();
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
