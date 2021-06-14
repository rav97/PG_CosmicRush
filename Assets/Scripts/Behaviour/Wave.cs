using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float WaveDuration;
    private Animation anima;
    private float activateTime, exitTime;
    private bool isActive = false, isOver = false;
    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animation>();
        // pick one of entering animaion and then play it
        int l = Random.Range(0, 2);
        switch (l)
        {
            case 0: { anima.Play("WaveEnter"); activateTime = Time.time + anima["WaveEnter"].length; }; break;
            case 1: { anima.Play("WaveEnter2"); activateTime = Time.time + anima["WaveEnter2"].length; }; break;
            case 2: { anima.Play("WaveEnter3"); activateTime = Time.time + anima["WaveEnter3"].length; }; break;
        }
        exitTime = Time.time + WaveDuration;
    }
    // Update is called once per frame
    void Update()
    {
        //when wave entered the scene it activates scripts of containing enemies
        if(Time.time > activateTime && !isActive)
        {
            ActivateEnemyScripts();
            exitTime = Time.time + WaveDuration;
            isActive = true;
        }
        // if all enemies were destroyed it signals it to LevelManager and then destroys wave object
        if(transform.childCount <= 0 && !isOver)
        {
            LevelManager.WaveExist = false;
            isOver = true;
            Destroy(gameObject);
        }
        // if wave is out of time, deactivates scripts of containing enemies and play exit animation
        if(Time.time > exitTime && !isOver)
        {
            DeactivateEnemyScripts();
            anima.Play("WaveExit");
            Destroy(gameObject, anima["WaveExit"].length);
            LevelManager.WaveExist = false;
            isOver = true;
        }
    }
    private void ActivateEnemyScripts()
    {
        foreach(Transform child in transform)
        {
            //Debug.Log(child.gameObject);
            if(child.gameObject.GetComponent<EnemyShooting>() != null)
                child.gameObject.GetComponent<EnemyShooting>().enabled = true;
            if(child.gameObject.GetComponent<FollowPath>() != null)
                child.gameObject.GetComponent<FollowPath>().enabled = true;
            if(child.gameObject.GetComponent<Tracking>() != null)
                child.gameObject.GetComponent<Tracking>().enabled = true;
        }
    }
    private void DeactivateEnemyScripts()
    {
        foreach (Transform child in transform)
        {
            //Debug.Log(child.gameObject);
            if (child.gameObject.GetComponent<EnemyShooting>() != null)
                child.gameObject.GetComponent<EnemyShooting>().enabled = false;
            if (child.gameObject.GetComponent<FollowPath>() != null)
                child.gameObject.GetComponent<FollowPath>().enabled = false;
            if (child.gameObject.GetComponent<Tracking>() != null)
                child.gameObject.GetComponent<Tracking>().enabled = false;
        }
    }
}
