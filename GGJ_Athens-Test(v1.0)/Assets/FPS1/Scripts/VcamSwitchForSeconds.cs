using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(ICinemachineCamera))]
public class VcamSwitchForSeconds : MonoBehaviour
{
    // Any cinemachine camera (they all implement ICinemachineCamera interface)
    // Could have been CinemachineVirtualCamera type for simple virtual cameras
    ICinemachineCamera vcam;

    [SerializeField] float delaySeconds = 5f; 

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<ICinemachineCamera>();
    }

    // Start TakeControlCo coroutines
    public void TakeControl()
    {
        // Stop All Coroutines running on this behaviour to avoid parallel coroutines co-existance
        StopAllCoroutines();

        // Start / Call TakeControlCo . Can't simple write TakeControlCo()
        StartCoroutine(TakeControlCo());
    }

    // Give virtual camera high priority for given seconds
    IEnumerator TakeControlCo() //Co for Coroutine
    {
        // Take high priority and take control of main camera
        // If we switch from ICinemachineCamera type to any virtual camera that implements...
        // ...ICinemachineCamera then we write vcam.m_Priority = 10;
        vcam.Priority = 10;

        // Wait for seconds
        yield return new WaitForSeconds(delaySeconds);

        // Take low priority and lose control of main camera
        vcam.Priority = -10;
    }
}
