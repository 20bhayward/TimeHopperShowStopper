using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1BehaviorTree : ABehaviorTree
{



    protected override ANode SetupTree()
    {
        //DONT USE DEFAULT CONSTRUCTORS FOR THINGS OTHER THAN STATE NODES


        AConditionNode BoneMissilesRange = new PlayerInRange(500);
        AConditionNode NegativeWaveRange = new PlayerInRange(100);
        AConditionNode ChainSlashRange = new PlayerInRange(10);


        AStateNode BoneMissiles = new BoneMissiles();
        AStateNode NegativeWave = new NegativeWave();
        AStateNode ChainSlash = new ChainSlash();





        //Aggro Patterns (Inserted into the BossRootList)
        ASequence AggroPattern1 = new StandardSequence(new List<ANode> {BoneMissilesRange, BoneMissiles});
        ASequence AggroPattern2 = new StandardSequence(new List<ANode> {NegativeWaveRange, NegativeWave});
        ASequence AggroPattern3 = new StandardSequence(new List<ANode> {ChainSlashRange, ChainSlash});
        //For later, replace State with random Selector within

        ANode Aggro = new SelectRandomIndefinite(new List<ANode> { AggroPattern1, AggroPattern2, AggroPattern3 });
        ANode Move = new SelectRandomIndefinite(new List<ANode> {new MoveToPlayer(), new MoveBackwards()});




        //Build entire list within the below call using the list constructor
        ANode BossRoot = new SelectRandomIndefinite(new List<ANode> {Aggro, Move});
        return BossRoot;
    }
}
