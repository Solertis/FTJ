using UnityEngine;
using System.Collections;

public class GrabbableScript : MonoBehaviour {
	public int id_;
	public int held_by_player_;
	
	void Start () {
		BoardScript.Instance().RegisterGrabbableObject(gameObject);
		if(Network.isServer){
			held_by_player_ = -1;
		}
	}
	
	void OnDestroy() {
		if(BoardScript.Instance()){
			BoardScript.Instance().UnRegisterGrabbableObject(gameObject);
		}
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		if(stream.isWriting){
			int id = id_;
			stream.Serialize(ref id);
		} else {
			int id = -1;
			stream.Serialize(ref id);
			id_ = id;
		}
	}
}