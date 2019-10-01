using UnityEngine;
using System.Collections.Generic;

public static class TransformExtension
{
    
     //Breadth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName) {
        var result = aParent.Find(aName);
        if(result != null)
            return result;
        foreach(Transform child in aParent) {
            result = child.FindDeepChild(aName);
            if(result != null)
                return result;
        }
        return null;
    }

    public static List<Transform> FindDeepChildrenByTag(this Transform aParent, string tag) {
        List<Transform> toReturn = new List<Transform>();
        List<GameObject> children = GetChildRecursive(aParent.gameObject);
        foreach(GameObject go in children) {
            if(go.CompareTag(tag)) {
                toReturn.Add(go.transform);
            }                
        }

        return toReturn;
    }

    private static List<GameObject> GetChildRecursive(GameObject obj) {

        List<GameObject> listOfChildren = new List<GameObject>();

        if(null == obj) {
            return null;
        }

        foreach(Transform child in obj.transform) {
            if(child == null) {
                continue;
            }
            //child.gameobject contains the current child you can do whatever you want like add it to an array
            listOfChildren.Add(child.gameObject);
            GetChildRecursive(child.gameObject);
        }

        return listOfChildren;
    }

	public static Vector3 CenterOfVectors(List<Transform> vectors)
	{
		Vector3 sum = Vector3.zero;
		if(vectors == null || vectors.Count == 0)
		{
			return sum;
		}

		foreach(Transform vec in vectors)
		{
			sum += vec.position;
		}
		return sum / vectors.Count;
	}
}
