using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    private int health = 100;

    public int Health
    {
        get {return health;}
        set
        {
            
            health = Mathf.Clamp(value, 0, 100);

            GameManager.Instance.PostNotifiCation(EVENT_TP.HEALCHANGE, this, health);
            if (health == 0)
            {
                GameManager.Instance.PostNotifiCation(EVENT_TP.DEAD, this, health);
            }
        }
    }
    private int wealth = 50;

    public int Wealth
    {
        get { return wealth; }
        set
        {
            wealth = Mathf.Clamp(value, 0, 50);
            GameManager.Instance.PostNotifiCation(EVENT_TP.WEALTHCHANGE, this, wealth);
        }
    }

    private bool relifed = false;

    public bool Relifed
    {
        get { return relifed; }
        set
        {
            relifed = value;
            if (relifed)
            {
                GameManager.Instance.PostNotifiCation(EVENT_TP.RELIFE, this, Health);
            }
        }
    }

    public void Birth()
    {
        GameManager.Instance.PostNotifiCation(EVENT_TP.BIRTH, this, health);
    }

    // Use this for initialization
    public virtual void Start()
    {
        GameManager.Instance.AddListener(EVENT_TP.HEALCHANGE, OnStateChange);
        GameManager.Instance.AddListener(EVENT_TP.BIRTH, OnStateChange);
        GameManager.Instance.AddListener(EVENT_TP.DEAD, OnStateChange);
        GameManager.Instance.AddListener(EVENT_TP.WEALTHCHANGE, OnStateChange);
        GameManager.Instance.AddListener(EVENT_TP.RELIFE, OnStateChange);
        //GameManager.Instance.AddListener(EVENT_TP.HEALCHANGE, OnStateChange);
        Birth();
    }

    public virtual void Update()
    {
        //If you press space bar, the health is reduce
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Health > 0)
            {
                //Take some damage of space bar  press
                Health -= (int)Random.Range(0, 20);
            }
            else
            {
                Health = 100;
                Relifed = true;
            }
        }
    }

    public void OnStateChange(EVENT_TP evt_tp, Component sender, object param = null)
    {
        switch (evt_tp)
        {
            case EVENT_TP.BIRTH:
                OnBirth(sender, (int)param);
                break;
            case EVENT_TP.HEALCHANGE:
                OnHealthChange(sender, (int)param);
                break;
            case EVENT_TP.WEALTHCHANGE:
                OnWealthChange(sender, (int)param);

                break;
            case EVENT_TP.DEAD:
                OnDead(sender, (int)param);
                break;
            case EVENT_TP.RELIFE:
                OnRelife(sender, (int)param);
                break;
            default:
                break;
        }
    }

    private void OnRelife(Component sender, int p)
    {
        if (this.GetInstanceID() != sender.GetInstanceID()) return;
        Debug.Log("OnRelife: " + gameObject.name + "   life : " + p);
    }

    private void OnDead(Component sender, int p)
    {
        if (this.GetInstanceID() != sender.GetInstanceID()) return;
        Debug.Log("OnDead: " + gameObject.name + "   life : " + p);

    }

    private void OnWealthChange(Component sender, int p)
    {
        if (this.GetInstanceID() != sender.GetInstanceID()) return;
        Debug.Log("OnWealthChange: " + gameObject.name + "   life : " + p);

    }

    private void OnHealthChange(Component sender, int p)
    {
        if (this.GetInstanceID() != sender.GetInstanceID()) return;
        Debug.Log("OnHealthChange: " + gameObject.name + "   life : " + p);

    }

    private void OnBirth(Component sender, int p)
    {
        if (this.GetInstanceID() != sender.GetInstanceID()) return;
        Debug.Log("OnBirth: " + gameObject.name + "   life : " + p);

    }
}
