package com.rolerolls.application.combats.dtos;

import java.util.UUID;

public class AttackInput {
    public UUID attackerId;
    public UUID targetId;
    public AttackType attackType;
}
