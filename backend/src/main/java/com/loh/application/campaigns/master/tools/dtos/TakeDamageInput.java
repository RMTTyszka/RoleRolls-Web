package com.loh.application.campaigns.master.tools.dtos;

import java.util.List;
import java.util.UUID;

public class TakeDamageInput extends MasterToolInput {
    public UUID creatureId;
    public List<Integer>  physicalDamages;
    public List<Integer>  arcaneDamages;
    public List<Integer>  fireDamages;
    public List<Integer>  iceDamages;
    public List<Integer>  lightningDamages;
    public List<Integer>  sonicDamages;
    public List<Integer>  poisonDamages;
    public List<Integer>  holyDamages;
    public List<Integer>  necroticDamages;
}
