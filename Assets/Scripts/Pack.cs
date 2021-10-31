using System;
using UnityEngine;

public class Pack : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    public event Action PackCleared = delegate {  };
    private bool _isEventAlreadySent;
    
    private void Update()
    {
        var isAllDead = true;
        foreach (var enemy in enemies)
        {
            if (!enemy.isDead)
            {
                isAllDead = false;
                break;
            }
        }

        if (isAllDead && !_isEventAlreadySent)
        {
            _isEventAlreadySent = true;
            PackCleared();
        }
    }
}
