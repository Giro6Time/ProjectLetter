using System.Collections.Generic;
using UnityEngine;

public static class ActionInRoom//�������鶼ֻ����һ��string�����������������RoomNoToEvent�����Ӧ�¼�����һ��������ַ���
{
    static int reactNum = 0;//��ʾ��ǰ���ŵڼ����¼�
    static readonly int combatFollowNum = 4;
    static readonly int combatBetrayNum = 3;
    static readonly int combatOnewayNum = 1;
    static readonly int eventFollowNum = 1;
    static readonly int eventBetrayNum = 1;
    static readonly int eventOnewayNum = 1;
    static readonly int recoverFollowNum = 1;
    static readonly int recoverBetrayNum = 1;
    static readonly int recoverOnewayNum = 1;
    static readonly int ChooseFollow = 3;
    static readonly int ChooseBetray = 3;
    static string roomType;
    public static void Dialogue(string contentType)
    {
        //DialogueManager.Instance.ClearEndListener();
        DialogueManager.Instance.SetLine(contentType);
        DialogueManager.Instance.PlayDialogue();
    }
    public static void React(string content)//���ݷ�Ӧֱ�ӿ�����Ӧ�ĶԻ�
    {
        reactNum++;
        roomType = RoomNoToRoomType(MainScript.S.roomNo);
        EventManager.AddEventListener("DialogueEnd", UpdateTrust);
        switch (MainScript.S.whetherBetray)
        {
            case BetrayType.betray:
                if (roomType == "CombatRoom")
                    DialogueManager.Instance.SetLine("Combat_Betray_" + (reactNum % combatBetrayNum + 1));
                else if (roomType == "EventRoom")
                    DialogueManager.Instance.SetLine("Event_Betray_" + (reactNum % eventBetrayNum + 1));
                else if (roomType == "RecoverRoom")
                    DialogueManager.Instance.SetLine("Recover_Betray_" + (reactNum % recoverBetrayNum + 1));
                break;
            case BetrayType.oneway:
                if (roomType == "CombatRoom")
                    DialogueManager.Instance.SetLine("Combat_Oneway_" + (reactNum % combatOnewayNum + 1));
                else if (roomType == "EventRoom")
                    DialogueManager.Instance.SetLine("Event_Oneway_" + (reactNum % eventOnewayNum + 1));
                else if (roomType == "RecoverRoom")
                    DialogueManager.Instance.SetLine("Recover_Oneway_" + (reactNum % recoverOnewayNum + 1));
                break;
            case BetrayType.follow:
                if (roomType == "CombatRoom")
                    DialogueManager.Instance.SetLine("Combat_Follow_" + (reactNum % combatFollowNum + 1));
                else if (roomType == "EventRoom")
                    DialogueManager.Instance.SetLine("Event_Follow_" + (reactNum % eventFollowNum + 1));
                else if (roomType == "RecoverRoom")
                    DialogueManager.Instance.SetLine("Recover_Follow_" + (reactNum % recoverFollowNum + 1));
                break;
            default:
                return;
        }
        reactNum++;//��ֹ��ȡ�����Ի�
        DialogueManager.Instance.PlayDialogue();

    }
    public static void Combat(string enemy)
    {
        string[] tmp = enemy.Split("|");
        CombatSystem.Instance.SetEnemyByName(tmp[0]);
        CombatSystem.Instance.enemy.Attack = float.Parse(tmp[1]);
        CombatSystem.Instance.enemy.Health = float.Parse(tmp[2]);
        CombatSystem.Instance.StartCombat();
    }

    public static void Move(string xy)
    {
        string[] tmp = xy.Split("|");
        Protagonist.Instance.moveTriggerer.ClearArriveListener();
        Protagonist.Instance.moveTriggerer.AddArriveListener(MainScript.S.PlayNext);

        Protagonist.Instance.moveTriggerer.MoveTo(new Vector3(float.Parse(tmp[0]), float.Parse(tmp[1]), 0));
    }
    public static void Animation(string inputStr)
    {
        EventManager.EventTrigger("AnimationEnd");
        return;
        //��ǰ��event����еĶ�����δ��ӣ����ֱ�������·��Ĵ���϶��ᱨ��
        //ȷ��event����е����ж�������ӽ���֮�󣬼���ɾ���������д���
        string[] tmp = inputStr.Split("|");
        Animator anim = GameObject.Find(tmp[0]).GetComponent<Animator>();
        anim.SetTrigger(tmp[1]);

    }
    public static void Recover(string hp)
    {
        Protagonist.Instance.Health += int.Parse(hp);
        EventManager.EventTrigger("RecoverEnd");
    }
    public static void SelectDoor()//����б�Ҫ������Բ���һ���Ի��¼�
    {
        bool isTrust = Protagonist.Instance.isTrust;
        if (MapGeneration.Instance.GetAvailableDoors(MainScript.S.roomNo).Count == 1)
        {
            MainScript.S.whetherBetray = BetrayType.oneway;
        }
        else if (isTrust)
        {
            MainScript.S.whetherBetray = BetrayType.follow;
        }
        else
        {
            MainScript.S.whetherBetray = BetrayType.betray;
        }
        EventManager.AddEventListener("DialogueEnd", SelectDoorEnd);
        if (!isTrust)//��������Σ��������һ����
        {
            DialogueManager.Instance.SetLine("Choose_Betray_" + (reactNum % ChooseBetray + 1));
            List<int> aval = MapGeneration.Instance.GetAvailableDoors(MainScript.S.roomNo);
            int doorInd = UnityEngine.Random.Range(0, aval.Count);
            MapGeneration.Instance.SceneDoorsObjects[aval[doorInd]].GetComponent<Door>().InitDoorClickEvent();
        }
        else
        {
            DialogueManager.Instance.SetLine("Choose_Follow_" + (reactNum % ChooseFollow + 1));
        }
        DialogueManager.Instance.PlayDialogue();
    }
    static void SelectDoorEnd()
    {
        EventManager.EventTrigger("DoorClickEventEnd");
    }
    public static void Choose(string inputStr)
    {
        SpecialEventManager.Instance.StartSpecialEvent(SpecialEventType.Choose, inputStr);
    }
    public static void SetDoorClickable(bool clickable)
    {
        for (int i = 0; i < 5; i++)
        {
            MapGeneration.Instance.SceneDoorsObjects[i].GetComponent<Door>().SetCollider(clickable);
        }
    }
    static void UpdateTrust()
    {
        switch (MainScript.S.whetherBetray)
        {
            case BetrayType.betray:
                if (roomType == "Combat") Protagonist.Instance.Trust += 2;
                else if (roomType == "Recover") Protagonist.Instance.Trust -= 3;
                else if (roomType == "Event") Protagonist.Instance.Trust -= 1;
                break;
            case BetrayType.oneway:
                break;
            case BetrayType.follow:
                if (roomType == "Combat") Protagonist.Instance.Trust -= 2;
                else if (roomType == "Recover") Protagonist.Instance.Trust += 3;
                else if (roomType == "Event") Protagonist.Instance.Trust += 1;
                break;
            default:
                return;
        }
        EventManager.EventTrigger("ReactEnd");
    }
    public static string RoomNoToRoomType(int RoomNo)
    {
        csvController.GetInstance().loadFile(Application.dataPath + "/Scripts/Maps", "Layout.csv");
        return csvController.GetInstance().getString(RoomNo, 1);
    }

}
