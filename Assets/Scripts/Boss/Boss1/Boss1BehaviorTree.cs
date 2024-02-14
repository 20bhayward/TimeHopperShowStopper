using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1BehaviorTree : ABehaviorTree
{



    protected override ANode SetupTree()
    {

        AEnemyController BossController = new Boss1Controller();
        EnemyInfoManager BossInfoManager = new EnemyInfoManager();

        //DONT USE DEFAULT CONSTRUCTORS FOR THINGS OTHER THAN STATE NODES
        List<ANode> rootList = new List<ANode>();
        ASequence AttackPattern1 = new StandardSequence();
        ASequence AttackPattern2 = new StandardSequence();
        ASequence AttackPattern3 = new StandardSequence();


        AConditionNode AttackPattern1Range = new Boss1PlayerinRange();
        AConditionNode AttackPattern1LineOfSight = new Boss1PlayerinRange();

        //Build entire list within the below call using the list constructor
        ANode BossRoot = new SelectRandomIndefinite();
        BossRoot.SetControllerAndInfo(BossController, BossInfoManager, playerInfo, worldInfo);
        return BossRoot;
    }
}
