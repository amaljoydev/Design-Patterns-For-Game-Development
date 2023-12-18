using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.CommandPattern
{
    public class CommandPlayer : MonoBehaviour
    {
        bool isMoving = false;
        public Stack<ICommand> moveCommands = new Stack<ICommand>();

        void Start()
        {
            Initialise();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M) && isMoving == false)
            {
                Vector3 targetPos = new Vector3(Random.Range(-5,5), transform.position.y, Random.Range(-5,5));
                PlayerMoveCommand playerMoveCommand = new PlayerMoveCommand(this,targetPos);
                playerMoveCommand.Execute();
                moveCommands.Push(playerMoveCommand);
            }
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Rewind();
            }
        }

        void Initialise()
        {
            PlayerMoveCommand playerMoveCommand = new PlayerMoveCommand(this, transform.position);
            playerMoveCommand.Execute();
            moveCommands.Push(playerMoveCommand);
        }

        public void SetMoveStatus(bool instatus)
        {
            isMoving = instatus;
        }

        public bool GetMoveStatus()
        {
            return isMoving;
        }

        public void Rewind()
        {
            StartCoroutine(RewindFunc());
        }

        IEnumerator RewindFunc()
        {
            while(moveCommands.Count>0)
            {
                PlayerMoveCommand playerMoveCommand =(PlayerMoveCommand) moveCommands.Pop();
                playerMoveCommand.Execute();
                yield return new WaitUntil(()=> GetMoveStatus()==false);
            }
            Initialise();
        }
    }
}
