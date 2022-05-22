using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
	public abstract class Order // generic order (to keep in queue)
	{
		public virtual void PassToSoldier(Soldier targetSoldier) // "translate" the order to the soldier
		{// depending on implementation, for example call soldier's method to execute/plan this task
			Debug.LogWarning($"Generic order passing not overriden\nSoldier {targetSoldier.name} received generic order");
		}
    }

	public class MovementOrder : Order // example how to add new types of orders
	{
		public MovementOrder(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		public readonly int x;
		public readonly int y;
		public override void PassToSoldier(Soldier targetSoldier)
		{// here we would set soldier's target position for example
			targetSoldier.HandleMovementOrder(new Vector2Int(x, y));
			Debug.Log($"Soldier {targetSoldier.name} received movement order towards coordinates {x},{y}");
		}
	}

	[SerializeField] private List<Soldier> soldiers = new List<Soldier>(); // soldiers belonging to the squad
    private Queue<Order> orders = new Queue<Order>(); // orders given to the squad

	public void TempAddSoldierToSquad(Soldier soldier)
	{
		soldiers.Add(soldier);
	}

	private void Awake()
	{
		TickSystem.OnTick += HandleTick;
	}

	private void HandleTick(TickSystem.OnTickEventArgs eventArgs)
	{// pass a single order to all soldiers
		if (orders.Count < 1)
			return; // for now nothing to do here

		Order currentOrder = orders.Dequeue();
		Debug.Log($"Passing order {currentOrder.ToString()} on tick #{eventArgs.tickNumber}");
		foreach (Soldier soldier in soldiers)
		{
			currentOrder.PassToSoldier(soldier);
		}
	}

	[ContextMenu("DEBUG ADD PSEUDO MOVEMENT ORDER")]
	public void DebugAddMovementOrder()
	{
		int targetX = 4;
		int targetY = 2;
		orders.Enqueue(new MovementOrder(targetX, targetY));
	}
}
