package com.loh.combat;

import com.loh.creatures.Creature;

import java.util.UUID;

public class AddOrRemoveCreatureToCombatInput<T extends Creature> {
    public UUID combatId;
    public T creature;
}
