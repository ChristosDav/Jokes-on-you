using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkOpener : MonoBehaviour
{
    [SerializeField] private string link;

    public void OpenLink()
    {
        Application.OpenURL(link);    
    }
}
