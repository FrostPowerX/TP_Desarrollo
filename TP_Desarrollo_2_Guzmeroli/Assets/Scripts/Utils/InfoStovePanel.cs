using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoStovePanel : MonoBehaviour
{
    [SerializeField] Stove stove;

    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI timespend;

    [SerializeField] Image statusImg;
    [SerializeField] Image iconImg;

    [SerializeField] Color empty;
    [SerializeField] Color coocking;
    [SerializeField] Color done;

    private void Start()
    {
        statusImg.color = empty;

        stove.OnIconGenerated += SetIcon;
        stove.OnCoockingStart += SetStatusCoocking;
        stove.OnCoockingDone += SetStatusDone;
        stove.OnCoockingEmpty += SetStatusEmpty;
    }

    private void Update()
    {
        timespend.text = ((int)stove.TimeSpend).ToString();
    }

    [ContextMenu("Set Icon")]
    void SetIcon()
    {
        iconImg.sprite = stove.IconResult;
    }

    void SetStatusEmpty() => statusImg.color = empty;
    void SetStatusDone() => statusImg.color = done;
    void SetStatusCoocking() => statusImg.color = coocking;
}
