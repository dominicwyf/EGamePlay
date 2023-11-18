using EGamePlay.Combat;
using EGamePlay;
using ET;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MiniExampleInit : MonoBehaviour
{
    public bool EntityLog;
    public SkillConfigObject SkillConfigObject;
    public ReferenceCollector ConfigsCollector;


    private async void Awake()
    {
        SynchronizationContext.SetSynchronizationContext(ThreadSynchronizationContext.Instance);
        Entity.EnableLog = EntityLog;
        MasterEntity.Create();
        Entity.Create<TimerManager>();
        Entity.Create<CombatContext>();
        MasterEntity.Instance.AddComponent<ConfigManageComponent>(ConfigsCollector);

        await TimerManager.Instance.WaitAsync(2000);
        //��������ս��ʵ��
        var monster = CombatContext.Instance.AddChild<CombatEntity>();
        //����Ӣ��ս��ʵ��
        var hero = CombatContext.Instance.AddChild<CombatEntity>();
        //��Ӣ�۹��ϼ���
        var heroSkillAbility = hero.AttachSkill(SkillConfigObject);
        Debug.Log($"1 monster.CurrentHealth={monster.CurrentHealth.Value}");
        //ʹ��Ӣ�ۼ��ܹ�������
        hero.GetComponent<SpellComponent>().SpellWithTarget(heroSkillAbility, monster);
        await TimerManager.Instance.WaitAsync(2000);
        Debug.Log($"2 monster.CurrentHealth={monster.CurrentHealth.Value}");
        //--ʾ������--
    }

    private void Update()
    {
        MasterEntity.Instance?.Update();
        TimerManager.Instance?.Update();
    }

    private void OnApplicationQuit()
    {
        MasterEntity.Destroy();
    }
}