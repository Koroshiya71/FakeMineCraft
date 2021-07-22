using System;
using UnityEngine;
using System.Collections;
using Uniblocks;

public class VoxelDirt : DefaultVoxelEvents
{
    public override void OnMouseDown(int mouseButton, VoxelInfo voxelInfo)
    {
        if (mouseButton == 0)
        {
            var lastPos = GameSceneManager.Instance.lastClickPos;

            Vector3 currentPos = GameObject.Find("selected block graphics").transform.position;
            var voxel = voxelInfo.GetVoxelType();
            if (GameTool.HasKey("LastPos.X"))
            {
                lastPos = new Vector3(GameTool.GetFloat("LastPos.X"), GameTool.GetFloat("LastPos.Y"),
                    GameTool.GetFloat("LastPos.Z"));
                if (lastPos != currentPos)
                {

                    voxel.InitVoxelType(E_BlockType.Dirt);
                    voxel.hasInit = true;
                }
            }

            if (!voxel.hasInit)
            {
                voxel.InitVoxelType(E_BlockType.Dirt);
                voxel.hasInit = true;
            }
            voxel.HitBlock(10);
            if (voxel.Durability <= 0)
            {
                Voxel.DestroyBlock(voxelInfo);
                voxel.InitVoxelType(E_BlockType.Dirt);

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
                Voxel.PlaceBlock(voxelInfo, ExampleInventory.HeldBlock);
            }
            else
            {
                // else put the block next to the one we're looking at
                VoxelInfo newInfo =
                    new VoxelInfo(voxelInfo.adjacentIndex, voxelInfo.chunk); // use adjacentIndex to place the block
                Voxel.PlaceBlock(newInfo, ExampleInventory.HeldBlock);
            }
        }

    }
    private  void Awake()
    {
    }

}
