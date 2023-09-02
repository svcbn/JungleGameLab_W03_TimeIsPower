using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tryText;
    public void EnableEndingUI()=> gameObject.SetActive(true);
    public void DisableEndingUI() => gameObject.SetActive(false);

    public void SetTryText()
    {
        Debug.Log(DeathCounterManager.instance.count);
        tryText.text = DeathCounterManager.instance.count.ToString();
    }
}
