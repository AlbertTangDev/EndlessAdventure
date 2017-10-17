﻿using System;
using System.Collections.Generic;
using System.Linq;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common {
	public class World {

		public string Name { get; private set; }
		private Dictionary<int, CombatantData> _enemyData;
		private List<int> _weights;
		private int _totalWeight;

		public World(string name, List<CombatantData> enemyData) {
			if (name == null || enemyData == null) throw new ArgumentException();

			Name = name;
			_enemyData = new Dictionary<int, CombatantData>();
			_weights = new List<int>();

			foreach (CombatantData data in enemyData) {
				_totalWeight += data.Weight;
				_enemyData.Add(_totalWeight, data);
				_weights.Add(_totalWeight);
			}
			_weights.Sort();
		}

		public Combatant SpawnEnemy() {
			Random random = new Random();
			int result = random.Next(_totalWeight);

			// get the key that is the random value rounded up
			int key = _weights.FirstOrDefault( x => x >= result);
			_enemyData.TryGetValue(key, out CombatantData data);

			return CombatantFactory.CreateCombatant(data.Name,
																							attack: data.Attack,
																							defense: data.Defense,
																							health: data.Health,
																							energy: data.Energy,
																							expReward: data.ExpReward);
		}
	}
}