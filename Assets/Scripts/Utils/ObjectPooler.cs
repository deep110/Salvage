using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler {

	public int PoolSize {get { return pooledObjects.Count; }}
	public bool WillGrow {get; set;}

	private GameObject pooledObject;
	private List<GameObject> pooledObjects;

	public ObjectPooler(GameObject pooledObject, int amount) : this(pooledObject, amount, true){}

	public ObjectPooler(GameObject pooledObject, int amount, bool willGrow){
		WillGrow = willGrow;
		init(pooledObject, amount);
	}

	private void init(GameObject pooledObject, int amount) {

		if(pooledObjects != null || pooledObject == null || amount < 1){
			return;
		}

		this.pooledObject = pooledObject;

		pooledObjects = new List<GameObject>(amount);

		for(int i=0; i<amount;i++){
			GameObject obj = Object.Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject GetPooledObject(){

		if(pooledObjects==null) return null;

		for(int i=0; i< pooledObjects.Count; i++){

			if(!pooledObjects[i].activeInHierarchy){
				return pooledObjects[i];
			}
		}

		if(WillGrow){
			GameObject obj = Object.Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add(obj);
			return obj;
		}

		return null;
	}

	public GameObject Spawn(Vector3 position, Quaternion rotation){
		GameObject obj = GetPooledObject();
		obj.transform.position = position;
		obj.transform.rotation = rotation;
		obj.SetActive(true);

		return obj;
	}

	public GameObject Spawn(Vector3 position){
		return Spawn (position, Quaternion.identity);
	}

	public int CountPooled() {
		int count = 0;
		for(int i=0; i< pooledObjects.Count; i++){
			if(pooledObjects[i].activeInHierarchy){
				count++;
			}
		}

		return count;
	}

	public override string ToString() {
		return string.Format("PoolSize: {0}", PoolSize);
	}
	
}
