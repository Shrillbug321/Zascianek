//This class holds is meant to hold all the data of a GameObject in the scene which has an ObjectIdentifier component. 
//The values from the OI component are mirrored here, along with misc. stuff like the activation state of the gameObect (bool active), and of course it's components.

using System.Collections.Generic;

[System.Serializable]
public class SceneObject
{

	public string prefabName;
	public string id;
	public string idParent;

	public bool active;
	public int layer;
	public string tag;

	public List<ObjectComponent> objectComponents = new List<ObjectComponent>();
}



