using System.Collections.Generic;
using UnityEngine;

public class BarricadeController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _logs;

    public void SetLogActive(int level)
    {
        if (level < _logs.Count)
        {
            _logs[level].SetActive(true);
        }
    }
}
