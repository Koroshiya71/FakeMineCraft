using System;
using UnityEngine;
using System.Collections;

namespace Uniblocks
{
    public class VoxelGrass : DefaultVoxelEvents
    {
        
        
        private void Awake()
        {
            
            
        }
        

        public override void OnMouseDown(int mouseButton, VoxelInfo voxelInfo)
        {
            if (mouseButton == 0)
            {
                var lastPos = GameSceneManager.Instance.lastClickPos;
                Vector3 currentPos = GameObject.Find("selected block graphics").transform.position;
                var voxel = voxelInfo.GetVoxelType();
                if (GameTool.HasKey("LastPos.X") )
                {

                    if (lastPos != currentPos)
                    {

                        voxel.InitVoxelType(E_BlockType.Grass);
                        voxel.hasInit = true;
                    }
                }
               
                if (!voxel.hasInit)
                {
                    voxel.InitVoxelType(E_BlockType.Grass);
                    voxel.hasInit = true;
                }
                voxel.HitBlock(10);
                if (voxel.Durability<=0)
                {
                    Voxel.DestroyBlock(voxelInfo);
                    voxel.InitVoxelType(E_BlockType.Grass);

                }

                GameTool.SetFloat("LastPos.X",currentPos.x);
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

        public override void OnBlockPlace(VoxelInfo voxelInfo)
        {
            // switch to dirt if the block above isn't 0
            Index adjacentIndex = voxelInfo.chunk.GetAdjacentIndex(voxelInfo.index, Direction.up);
            if (voxelInfo.chunk.GetVoxel(adjacentIndex) != 0)
            {
                voxelInfo.chunk.SetVoxel(voxelInfo.index, 1, true);
            }

            // if the block below is grass, change it to dirt
            Index indexBelow = new Index(voxelInfo.index.x, voxelInfo.index.y - 1, voxelInfo.index.z);

            if (voxelInfo.GetVoxelType().VTransparency == Transparency.solid
                && voxelInfo.chunk.GetVoxel(indexBelow) == 2)
            {
                voxelInfo.chunk.SetVoxel(indexBelow, 1, true);
            }
        }
    }
}