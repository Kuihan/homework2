using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, GameController, IUserAction
{
    public List<GameObject> onBoat = new List<GameObject>();
    public List<GameObject> Devils_Left = new List<GameObject>();
    public List<GameObject> Devils_Right = new List<GameObject>();
    public List<GameObject> Priests_Left = new List<GameObject>();
    public List<GameObject> Priests_Right = new List<GameObject>();
    public GameObject Boat;
    public int Capacity = 2;
    public int side = 0;
    public bool op = true;
    public int result_code = 1;
    void Awake()
    {
        GameDirector gameDirector = GameDirector.getInstance();
        gameDirector.setFPS(60);
        gameDirector.currentGameController = this;
        gameDirector.currentGameController.LoadResources();
    }
    public void LoadResources()
    {
        GameObject game = Instantiate<GameObject>(
            Resources.Load<GameObject>("prefabs/Game"), Vector3.zero, Quaternion.identity);
        game.name = "Game";
        Transform[] parent = game.GetComponentsInChildren<Transform>();
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag.IndexOf("Priest") >= 0)
            {
                Priests_Right.Add(child.gameObject);
            }
            else if (child.gameObject.tag.IndexOf("Devil") >= 0)
            {
                Devils_Right.Add(child.gameObject);
            }
            else if (child.gameObject.tag == "Boat")
            {
                Boat = child.gameObject;
            }
        }
    }
    public void Priest_Right_On()
    {
        if (!op || result_code != 1)
        {
            return;
        }
        if (Capacity > 0 && Priests_Right.Count > 0 && side == 0)
        {
            op = false;
            Hashtable arg = new Hashtable();
            arg.Add("time", 0.5f);
            arg.Add("position", Boat);
            if (onBoat.Count == 1 && onBoat[0].tag.IndexOf("Priest") >= 0)
            {
                arg.Add("path", iTweenPath.GetPath(Priests_Right[0].tag + "Right2"));
            }
            else
            {
                arg.Add("path", iTweenPath.GetPath(Priests_Right[0].tag + "Right1"));
            }
            arg.Add("oncomplete", "oncomplete");
            arg.Add("oncompletetarget", this.gameObject);
            onBoat.Add(Priests_Right[0]);
            iTween.MoveTo(Priests_Right[0], arg);
            Priests_Right.RemoveAt(0);
            Capacity--;

        }
    }
    public void Priest_Left_On()
    {
        if (!op || result_code != 1)
        {
            return;
        }
        if (Capacity > 0 && Priests_Left.Count > 0 && side == 1)
        {
            op = false;
            Hashtable arg = new Hashtable();
            arg.Add("time", 0.5f);
            arg.Add("position", Boat);
            if (onBoat.Count == 1 && onBoat[0].tag.IndexOf("Priest") >= 0)
            {
                arg.Add("path", iTweenPath.GetPath(Priests_Left[0].tag + "Left1"));
            }
            else
            {
                arg.Add("path", iTweenPath.GetPath(Priests_Left[0].tag + "Left2"));
            }
            arg.Add("oncomplete", "oncomplete");
            arg.Add("oncompletetarget", this.gameObject);
            onBoat.Add(Priests_Left[0]);
            iTween.MoveTo(Priests_Left[0], arg);
            Priests_Left.RemoveAt(0);
            Capacity--;
        }
    }
    public void Devil_Right_On()
    {
        if (!op || result_code != 1)
        {
            return;
        }
        if (Capacity > 0 && Devils_Right.Count > 0 && side == 0)
        {
            op = false;
            Hashtable arg = new Hashtable();
            arg.Add("time", 0.5f);
            arg.Add("position", Boat);
            if (onBoat.Count == 1 && onBoat[0].tag.IndexOf("Devil") >= 0)
            {
                arg.Add("path", iTweenPath.GetPath(Devils_Right[0].tag + "Right2"));
            }
            else
            {
                arg.Add("path", iTweenPath.GetPath(Devils_Right[0].tag + "Right1"));
            }
            arg.Add("oncomplete", "oncomplete");
            arg.Add("oncompletetarget", this.gameObject);
            onBoat.Add(Devils_Right[0]);
            iTween.MoveTo(Devils_Right[0], arg);
            Devils_Right.RemoveAt(0);
            Capacity--;
        }
    }
    public void Devil_Left_On()
    {
        if (!op || result_code != 1)
        {
            return;
        }
        if (Capacity > 0 && Devils_Left.Count > 0 && side == 1)
        {
            op = false;
            Hashtable arg = new Hashtable();
            arg.Add("time", 0.5f);
            arg.Add("position", Boat);
            if (onBoat.Count == 1 && onBoat[0].tag.IndexOf("Devil") >= 0)
            {
                arg.Add("path", iTweenPath.GetPath(Devils_Left[0].tag + "Left1"));
            }
            else
            {
                arg.Add("path", iTweenPath.GetPath(Devils_Left[0].tag + "Left2"));
            }
            arg.Add("oncomplete", "oncomplete");
            arg.Add("oncompletetarget", this.gameObject);
            onBoat.Add(Devils_Left[0]);
            iTween.MoveTo(Devils_Left[0], arg);
            Devils_Left.RemoveAt(0);
            Capacity--;
        }
    }
    private void oncomplete()
    {
        op = true;
    }
    public void Boat_Left_Off()
    {
        if (!op || result_code != 1)
        {
            return;
        }
        if (side != 1)
        {
            return;
        }
        if (onBoat.Count > 0)
        {
            op = false;
        }
        for (int i = 0; i < onBoat.Count; i++)
        {
            Hashtable arg = new Hashtable();
            arg.Add("time", 0.5f);
            arg.Add("position", Boat);
            arg.Add("oncomplete", "oncomplete");
            arg.Add("oncompletetarget", this.gameObject);
            if (onBoat[i].tag.IndexOf("Priest") >= 0)
            {
                Priests_Left.Add(onBoat[i]);
                arg.Add("path", iTweenPath.GetPathReversed(onBoat[i].tag + "Left1"));
                iTween.MoveTo(onBoat[i], arg);
                Capacity++;
            }
            else if (onBoat[i].tag.IndexOf("Devil") >= 0)
            {
                arg.Add("path", iTweenPath.GetPathReversed(onBoat[i].tag + "Left2"));
                iTween.MoveTo(onBoat[i], arg);
                Devils_Left.Add(onBoat[i]);
                Capacity++;
            }
        }
        onBoat.Clear();
    }
    public void Boat_Right_Off()
    {
        if (!op || result_code != 1)
        {
            return;
        }
        if (side != 0)
        {
            return;
        }
        if (onBoat.Count > 0)
        {
            op = false;
        }
        for (int i = 0; i < onBoat.Count; i++)
        {
            Hashtable arg = new Hashtable();
            arg.Add("time", 0.5f);
            arg.Add("position", Boat);
            arg.Add("oncomplete", "oncomplete");
            arg.Add("oncompletetarget", this.gameObject);
            if (onBoat[i].tag.IndexOf("Priest") >= 0)
            {
                arg.Add("path", iTweenPath.GetPathReversed(onBoat[i].tag + "Right1"));
                iTween.MoveTo(onBoat[i], arg);
                Priests_Right.Add(onBoat[i]);
                Capacity++;
            }
            else if (onBoat[i].tag.IndexOf("Devil") >= 0)
            {
                arg.Add("path", iTweenPath.GetPathReversed(onBoat[i].tag + "Right2"));
                iTween.MoveTo(onBoat[i], arg);
                Devils_Right.Add(onBoat[i]);
                Capacity++;
            }
        }
        onBoat.Clear();
    }
    public void Boat_Go()
    {
        if (!op || result_code != 1)
        {
            return;
        }
        if (side == 0 && onBoat.Count > 0)
        {
            op = false;
            Hashtable arg = new Hashtable();
            arg.Add("time", 0.5f);
            arg.Add("position", Boat.transform.position + new Vector3(-3.8f, 0, 0));
            arg.Add("oncomplete", "Result");
            arg.Add("oncompletetarget", this.gameObject);
            iTween.MoveTo(Boat, arg);
            for (int i = 0; i < onBoat.Count; i++)
            {
                Hashtable Arg = new Hashtable();
                Arg.Add("time", 0.5f);
                Arg.Add("position", onBoat[i].transform.position + new Vector3(-3.8f, 0, 0));
                iTween.MoveTo(onBoat[i], Arg);
            }
            side = 1;
        }
        else if (side == 1 && onBoat.Count > 0)
        {
            op = false;
            Hashtable arg = new Hashtable();
            arg.Add("time", 0.5f);
            arg.Add("position", Boat.transform.position + new Vector3(3.8f, 0, 0));
            arg.Add("oncomplete", "Result");
            arg.Add("oncompletetarget", this.gameObject);
            iTween.MoveTo(Boat, arg);
            for (int i = 0; i < onBoat.Count; i++)
            {
                Hashtable Arg = new Hashtable();
                Arg.Add("time", 0.5f);
                Arg.Add("position", onBoat[i].transform.position + new Vector3(3.8f, 0, 0));
                iTween.MoveTo(onBoat[i], Arg);
            }
            side = 0;
        }
    }
    private void Result()
    {
        op = true;
        int Devils_On_Boat = 0, Priests_On_Boat = 0;
        for (int i = 0; i < onBoat.Count; i++)
        {
            if (onBoat[i].tag.IndexOf("Priest") >= 0)
            {
                Priests_On_Boat++;
            }
            else
            {
                Devils_On_Boat++;
            }
        }
        if (side == 1 && ((Priests_Left.Count + Priests_On_Boat < Devils_Left.Count + Devils_On_Boat && Priests_Left.Count + Priests_On_Boat > 0)
            || (Priests_Right.Count < Devils_Right.Count && Priests_Right.Count > 0)))
        {
            result_code = 0;
            return;
        }
        else if (side == 0 && ((Priests_Right.Count + Priests_On_Boat < Devils_Right.Count + Devils_On_Boat && Priests_Right.Count + Priests_On_Boat > 0)
          || (Priests_Left.Count < Devils_Left.Count) && Priests_Left.Count > 0))
        {
            result_code = 0;
            return;
        }
        result_code = 1;
    }
    public int Check()
    {
        if (result_code == 0)
        {
            return 0;
        }
        if (Priests_Left.Count + Devils_Left.Count == 6)
        {
            result_code = 2;
            return 2;
        }
        return 1;
    }
}