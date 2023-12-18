using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

namespace Scripts.CommandPattern
{
    public class PlayerMoveCommand :ICommand
    {
        
        private CommandPlayer commandPlayer;
        Vector3 targetPos;

        public PlayerMoveCommand(CommandPlayer inCommandPlayer,Vector3 inTargetPos)
        {
            commandPlayer = inCommandPlayer;
            targetPos = inTargetPos;
        }

        public void Execute()
        {
            Move();
        }
        
       

        void Move()
        {
            if (commandPlayer.GetMoveStatus()==false)
            {
                
                commandPlayer.SetMoveStatus(true);
                commandPlayer.transform.DOMove(targetPos, .75f).OnComplete(()=> { commandPlayer.SetMoveStatus(false); });
                
            }
        }
        
    }
}
