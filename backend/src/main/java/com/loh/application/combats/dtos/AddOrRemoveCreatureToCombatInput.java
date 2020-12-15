package com.loh.application.combats.dtos;

import com.loh.domain.creatures.Creature;

import java.util.UUID;

public class AddOrRemoveCreatureToCombatInput<T extends Creature> {
    public UUID combatId;
    public T creature;
}
