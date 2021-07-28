using UnityEngine;
using System.Collections;
using GameCore;
namespace Uniblocks {

public class Voxel : MonoBehaviour {
	
	public string VName;
	public Mesh VMesh;
	public bool VCustomMesh;
	public bool VCustomSides;
	public Vector2[] VTexture; // index of the texture. Array index specifies face (VTexture[0] is the up-facing texture, for example)
	public Transparency VTransparency;
	public ColliderType VColliderType;
	public int VSubmeshIndex;
	public MeshRotation VRotation;
    public bool hasInit;
    private E_BlockType blockType;

    public E_BlockType BlockType
    {
        get { return blockType; }
    }

	//耐久度
    private float durability;
    public float Durability
    {
        get { return durability; }
    }
	//打击方块
    public void HitBlock()
    {
        float damage = GameDefine.toolDamageDic[GameSceneManager.Instance.ActiveToolType];
        durability -= damage;
        GameSceneManager.Instance.PlayAttackAnim();
    }
	//初始化方块类型，并根据类型初始化耐久度
    public void InitVoxelType(E_BlockType type)
    {
        hasInit = true;

        this.blockType = type;
        this.durability = GameDefine.blockDurabilityDic[this.blockType];
    }
    
	public static void DestroyBlock ( VoxelInfo voxelInfo ) {
	
		// multiplayer - send change to server
		if (Engine.EnableMultiplayer) {
			Engine.UniblocksNetwork.GetComponent<UniblocksClient>().SendPlaceBlock ( voxelInfo, 0 );
		}
		
		// single player - apply change locally
		else {
			GameObject voxelObject = Instantiate ( Engine.GetVoxelGameObject (voxelInfo.GetVoxel()) ) as GameObject;
			if (voxelObject.GetComponent<VoxelEvents>() != null) {
				voxelObject.GetComponent<VoxelEvents>().OnBlockDestroy(voxelInfo);
			}
            GameObject dropGo = Instantiate(ResourcesManager.Instance.LoadAsset("GamePrefab/" + "DropPrefab"),
                GameSceneManager.Instance.currentPos, Quaternion.identity) as GameObject;
            var dropCube = dropGo.GetComponent<DropCube>();
            dropCube.InitMtl(voxelInfo.GetVoxel());
            voxelInfo.chunk.SetVoxel (voxelInfo.index, 0, true);
			Destroy (voxelObject);;

            
        }
        PlayerData.Instance.EditorEnt(PlayerData.Instance.Ent - 0.3f);

}

		public static void PlaceBlock ( VoxelInfo voxelInfo, ushort data) {
		// multiplayer - send change to server
		if (Engine.EnableMultiplayer) {
			Engine.UniblocksNetwork.GetComponent<UniblocksClient>().SendPlaceBlock ( voxelInfo, data );
		}
		
		// single player - apply change locally
		else {
			voxelInfo.chunk.SetVoxel (voxelInfo.index, data, true);
		
			GameObject voxelObject = Instantiate ( Engine.GetVoxelGameObject (data) ) as GameObject;
			if (voxelObject.GetComponent<VoxelEvents>() != null) {
				voxelObject.GetComponent<VoxelEvents>().OnBlockPlace(voxelInfo);
			}
			Destroy (voxelObject);
		}
        AllCompose.Instance.Compos(BagData.Instance.Selectindex, BagData.Instance.IdList[BagData.Instance.Selectindex],
            BagData.Instance.BagCuts[BagData.Instance.Selectindex], 1, true);
		AllCompose.Instance.UpdateBagCutAndToolbarCut();
        if (data!=0)
        {
            PlayerData.Instance.EditorEnt(PlayerData.Instance.Ent-0.1f);

}
		}
	
	public static void ChangeBlock ( VoxelInfo voxelInfo, ushort data ) {
	
		// multiplayer - send change to server
		if (Engine.EnableMultiplayer) {
			Engine.UniblocksNetwork.GetComponent<UniblocksClient>().SendChangeBlock ( voxelInfo, data );
		}
		
		// single player - apply change locally
		else {
			voxelInfo.chunk.SetVoxel (voxelInfo.index, data, true);
		
			GameObject voxelObject = Instantiate ( Engine.GetVoxelGameObject (data) ) as GameObject;
			if (voxelObject.GetComponent<VoxelEvents>() != null) {
				voxelObject.GetComponent<VoxelEvents>().OnBlockChange(voxelInfo);
			}
			Destroy (voxelObject);
		}
	}
	
	// multiplayer
	
	public static void DestroyBlockMultiplayer ( VoxelInfo voxelInfo, NetworkPlayer sender ) { // received from server, don't use directly
		
		GameObject voxelObject = Instantiate ( Engine.GetVoxelGameObject (voxelInfo.GetVoxel()) ) as GameObject;
		VoxelEvents events = voxelObject.GetComponent<VoxelEvents>();
		if (events != null) {
			events.OnBlockDestroy(voxelInfo);
			events.OnBlockDestroyMultiplayer(voxelInfo, sender);
		}
		voxelInfo.chunk.SetVoxel (voxelInfo.index, 0, true);
		Destroy(voxelObject);
	}
	
	public static void PlaceBlockMultiplayer ( VoxelInfo voxelInfo, ushort data, NetworkPlayer sender ) { // received from server, don't use directly
		
		voxelInfo.chunk.SetVoxel (voxelInfo.index, data, true);
		
		GameObject voxelObject = Instantiate ( Engine.GetVoxelGameObject (data) ) as GameObject;
		VoxelEvents events = voxelObject.GetComponent<VoxelEvents>();
		if (events != null) {
			events.OnBlockPlace(voxelInfo);
			events.OnBlockPlaceMultiplayer(voxelInfo, sender);
		}
		Destroy (voxelObject);
	}
	
	public static void ChangeBlockMultiplayer ( VoxelInfo voxelInfo, ushort data, NetworkPlayer sender ) { // received from server, don't use directly
		
		voxelInfo.chunk.SetVoxel (voxelInfo.index, data, true);
		
		GameObject voxelObject = Instantiate ( Engine.GetVoxelGameObject (data) ) as GameObject;
		VoxelEvents events = voxelObject.GetComponent<VoxelEvents>();
		if (events != null) {
			events.OnBlockChange(voxelInfo);
			events.OnBlockChangeMultiplayer(voxelInfo, sender);
		}
		Destroy (voxelObject);
	}


	// block editor functions
	public ushort GetID () {
		return ushort.Parse(this.gameObject.name.Split('_')[1]);
		
	}
	
	public void SetID ( ushort id ) {
		this.gameObject.name = "block_" + id.ToString();
	}

}

}