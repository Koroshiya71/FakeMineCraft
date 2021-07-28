using System;
using UnityEngine;
using System.Collections;

// stores the currently held block, and switches it with 1-9 keys

namespace Uniblocks
{
    public class HoldBlockManager : MonoBehaviour
    {
        public static ushort HeldBlock;

        private void Update()
        {
            if (GameSceneManager.Instance.IsInGame)
            {
                
                HeldBlock = (ushort)BagData.Instance.IdList[BagData.Instance.Selectindex];
                if (HeldBlock==65535)
                {
                    HeldBlock = 0;
                }
            }
        }
    }
    
}