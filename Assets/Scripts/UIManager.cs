using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image oxygenbar;
    public void UpdateOxygenBar()
    {
        Status playerstat = transform.parent.GetComponent<Status>();

        Debug.Log("호출되냐?");
        oxygenbar.fillAmount = playerstat.oxygen / playerstat.max_oxygen;
    }
    public void SetPlayerUIDirection()
    {
        transform.localScale = transform.parent.transform.localScale;
    }
}
