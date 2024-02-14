using UnityEngine;

public abstract class ABehaviorTree : MonoBehaviour
{
    private ANode _root;
    [SerializeField] public PlayerInfoManager playerInfo;
    [SerializeField] public WorldInfoManager worldInfo;
    [SerializeField] private AEnemyController enemyController;
    [SerializeField] private EnemyInfoManager enenmyInfo;

    public virtual void Start()
    {
        _root = SetupTree();
    }

    public virtual void Update()
    {
        if (_root != null)
        {
            _root.Evaluate();
        }
    }

    protected abstract ANode SetupTree();
}