using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI logText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateLogText()
    {
        logText.text = PlayerManager.Instance.numberOfItems.ToString();
    }
}
