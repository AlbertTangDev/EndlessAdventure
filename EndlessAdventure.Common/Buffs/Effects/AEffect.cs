﻿using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Buffs.Effects {
	public abstract class AEffect : ABuff {

		public int StackCount { get; }

		public AEffect(string pName, string pDescription, double pValue, int pDuration) : base(pName, pDescription, pValue, pDuration) {
			StackCount = 1;
		}

		public override void Equip(Character character) {
			if (DurationTotal == 0) {
				ApplyEffect(character);
				character.ActiveEffects.Remove(this);
			}
		}

		public abstract void ApplyEffect(Character character);
	}
}