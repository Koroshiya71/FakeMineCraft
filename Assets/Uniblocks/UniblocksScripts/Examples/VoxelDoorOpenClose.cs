using UnityEngine;
using System.Collections;
namespace Uniblocks {
	

public class VoxelDoorOpenClose : DefaultVoxelEvents {


	public override void OnMouseDown (int mouseButton, VoxelInfo voxelInfo) {

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

                    voxel.InitVoxelType(E_BlockType.Door);
                    voxel.hasInit = true;
                }
            }

            if (!voxel.hasInit)
            {
                voxel.InitVoxelType(E_BlockType.Door);
                voxel.hasInit = true;
            }

            voxel.HitBlock();

            if (voxel.Durability <= 0)
            {
                Voxel.DestroyBlock(voxelInfo);
                voxel.InitVoxelType(E_BlockType.Door);
            }
            GameTool.SetFloat("LastPos.X", currentPos.x);
            GameTool.SetFloat("LastPos.Y", currentPos.y);
            GameTool.SetFloat("LastPos.Z", currentPos.z);

            // destroy a block with LMB
        }
        else if (mouseButton == 1) { // open/close with right click
		
			if (voxelInfo.GetVoxel() == 70) { // if open door
				Voxel.ChangeBlock (voxelInfo, 7); // set to closed
			}
			
			else if (voxelInfo.GetVoxel() == 7) { // if closed door
				Voxel.ChangeBlock (voxelInfo, 70); // set to open
			}	

		}
	}

}

}