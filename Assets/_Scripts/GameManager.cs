using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum EVENT_TP
{
    GAME_INIT,
    GAME_END,
    BIRTH,
    HEALCHANGE,
    WEALTHCHANGE,
    DEAD,
    RELIFE
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get { return GameManager.instance; }
        set { GameManager.instance = value; }
    }

    public delegate void OnEvent(EVENT_TP Evt, Component sender, object param = null);
    private Dictionary<EVENT_TP, List<OnEvent>> EvtDic = new Dictionary<EVENT_TP, List<OnEvent>>();

    void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    public void AddListener(EVENT_TP evt_tp, OnEvent onevt)
    {
        List<OnEvent> evtList = null;
        if (EvtDic.TryGetValue(evt_tp, out evtList))
        {
            evtList.Add(onevt);
            return;
        }

        evtList = new List<OnEvent>();
        evtList.Add(onevt);
        EvtDic.Add(evt_tp, evtList);
    }

    public void PostNotifiCation(EVENT_TP evt_tp, Component sender, object param=null)
    {
        List<OnEvent> evtList = null;
        if (!EvtDic.TryGetValue(evt_tp,out evtList))
        {
            return;
        }

        for (int i = 0; i < evtList.Count; i++)
        {
            if (!evtList[i].Equals(null))
            {
                evtList[i](evt_tp, sender, param);
            }
        }
    }

    //Remove all redundant entries from the Dictionary
    public void RemoveEvt(EVENT_TP evt_tp)
    {
        EvtDic.Remove(evt_tp);
    }

    public void RemoveRedundancies()
    {
        Dictionary<EVENT_TP, List<OnEvent>> temEvtDic = new Dictionary<EVENT_TP, List<OnEvent>>();
        foreach (KeyValuePair<EVENT_TP,List<OnEvent>> item in EvtDic)
        {
            for (int i = item.Value.Count-1; i >=0; i--)
            {
                if (item.Value[i].Equals(null))
                {
                    item.Value.RemoveAt(i);
                }
            }

            if (item.Value.Count>0)
            {
                temEvtDic.Add(item.Key, item.Value);
            }
        }
        EvtDic = temEvtDic;
    }

    void OnLevelWasLoaded()
    {
        RemoveRedundancies();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
