using UnityEngine;
using System.Collections;
using Uniblocks;

public class VoxelRock : DefaultVoxelEvents {

    public override void OnMouseDown(int mouseButton, VoxelInfo voxelInfo)
    {

        if (GameSceneManager.Instance.HasShowMenu || GameSceneManager.Instance.IsOpenBag)
        {
            return;
        }

        if (mouseButton == 0)
        {
            var lastPos = GameSceneManager.Instance.lastClickPos;
            Vector3 currentPos = GameObject.Find("selected block graphics").transform.position;
            var voxel = voxelInfo.GetVoxelType();
            if (GameTool.HasKey("LastPos.X"))
            {

                if (lastPos != currentPos)
                {

                    voxel.InitVoxelType(E_BlockType.Rock);
                    voxel.hasInit = true;
                }
            }

            if (!voxel.hasInit)
            {
                voxel.InitVoxelType(E_BlockType.Rock);
                voxel.hasInit = true;
            }

            voxel.HitBlock();

            if (voxel.Durability <= 0)
            {
                Voxel.DestroyBlock(voxelInfo);
                voxel.InitVoxelType(E_BlockType.Rock);
            }
            GameTool.SetFloat("LastPos.X", currentPos.x);
            GameTool.SetFloat("LastPos.Y", currentPos.y);
            GameTool.SetFloat("LastPos.Z", currentPos.z);

            // destroy a block with LMB
        }
        else if (mouseButton == 1)
        {
            // place a block with RMB

            if (voxelInfo.GetVoxel() == 8)
            {
                // if we're looking at a tall grass block, replace it with the held block
                Voxel.PlaceBlock(voxelInfo, HoldBlockManager.HeldBlock);
            }
            else
            {
                // else put the block next to the one we're looking at
                VoxelInfo newInfo =
                    new VoxelInfo(voxelInfo.adjacentIndex, voxelInfo.chunk); // use adjacentIndex to place the block
                Voxel.PlaceBlock(newInfo, HoldBlockManager.HeldBlock);
            }
        }

    }
}
