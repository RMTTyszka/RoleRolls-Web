package com.loh.creatures;


import javax.persistence.Embeddable;
import javax.persistence.Transient;
import java.lang.reflect.Field;

@Embeddable
public class Attributes {
    @Transient
    public static String Strength = "strength";
    public Integer strength;
    public Integer agility;
    public Integer vitality;
    public Integer wisdom;
    public Integer intuition;
    public Integer charisma;

    public Attributes GetSumOfAttributes(Attributes attributes){
        Attributes totalAttribuute = new Attributes();
        totalAttribuute.agility = agility + attributes.agility;
        totalAttribuute.strength = strength + attributes.strength;
        totalAttribuute.vitality = vitality + attributes.vitality;
        totalAttribuute.wisdom = wisdom + attributes.wisdom;
        totalAttribuute.intuition = intuition + attributes.intuition;
        totalAttribuute.charisma = charisma + attributes.charisma;

        return totalAttribuute;
    }

    public Attributes(){
        this(0);
    }
    public Attributes(Integer value){
        strength = value;
        agility = value;
        vitality = value;
        wisdom = value;
        intuition = value;
        charisma = value;
    }

    public Integer getAttributeLevel(String attr) {
        try {
            Field field = this.getClass().getDeclaredField(attr);
            field.setAccessible(true);
            return (Integer) field.get(this);
        } catch (NoSuchFieldException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }

        return 0;
    }


}