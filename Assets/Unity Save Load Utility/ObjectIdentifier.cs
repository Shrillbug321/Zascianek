//Add an ObjectIdentifier component to each Prefab that might possibly be serialized and deserialized.
//The unitName variable is not used by the serialization; it is just there so you can unitName your prefabs any way you want, 
//while the "in-game" unitName can be something different
//for example, an item that the play can inspect might have the prefab unitName "sword_01_a", 
//but the unitName (not the GameObject unitName; that is the prefab unitName! We are talking about the variable "unitName" here!) can be "Short Sword", 
//which is what the player will see when inspecting it in his inventory, a shop, et cetera.
//To clarify again: A GameObject's (and thus, prefab's) unitName should be the same as prefabName, while the variable "unitName" in this script can be anything you want (or nothing at all).

using System.Collections.Generic;
using UnityEngine;

//The data for the ObjectIdentifier is completely handled by the save function so it should never be saved directly.
//The save method contains abstractn additional checked to make sure this doesn't happen.
[SaveNoMembers]
public class ObjectIdentifier : MonoBehaviour
{

	public string prefabName;
	public string id;
	public string idParent;
	public bool dontSave = false;//If for whatever reason you don't want to save this GameObject even though it has an ObjectIdentifier component, set this to true.


	//componentSaveMode controls which components of this GameObject should be saved. 
	//All: All components will be saved
	//None: No components wil be saved
	//Mono: Only MonoBehaviors and Transforms
	//MonoListInclusive: Only MonoBehaviors, Transforms and those components whose Type names are included in the list
	//ListInclusive: Only those components whose Type names are included in the list (for example "Transform", "MyMonoScript").
	//ListExclusive: Only those components whose Type names are NOT included in the list (for example "Transform", "MyMonoScript").
	public ComponentSaveMode componentSaveMode = ComponentSaveMode.MonoListInclusive;
	public List<string> componentTypesToSave = new List<string>();

	public enum ComponentSaveMode
	{
		All,
		None,
		Mono,
		MonoListInclusive,
		ListInclusive,
		ListExclusive
	};

	public void SetID()
	{
		if (string.IsNullOrEmpty(id))
		{
			id = System.Guid.NewGuid().ToString();
		}

		CheckForParent();
	}

	private void CheckForParent()
	{

		if (transform.parent == null)
		{
			idParent = null;
		}
		else
		{
			ObjectIdentifier oi_parent = transform.parent.GetComponent<ObjectIdentifier>();
			if (oi_parent != null)
			{
				if (string.IsNullOrEmpty(oi_parent.id))
				{
					oi_parent.SetID();
				}
				idParent = oi_parent.id;
			}
		}
	}
}

