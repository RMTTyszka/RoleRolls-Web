package com.loh.creatures;


import javax.persistence.Embeddable;
import java.lang.reflect.Field;
import java.util.Arrays;
import java.util.List;

@Embeddable
public class Attributes {
    public static String Strength = "strength";
    public static String Agility = "agility";
    public static String Vitality = "vitality";
    public static String Wisdom = "wisdom";
    public static String Intuition = "intuition";
    public static String Charisma = "charisma";
    public Integer strength;
    public Integer agility;
    public Integer vitality;
    public Integer wisdom;
    public Integer intuition;
    public Integer charisma;

    public Attributes GetSumOfAttributes(Attributes attributes){
        Attributes totalAttribute = new Attributes();
        totalAttribute.agility = agility + attributes.agility;
        totalAttribute.strength = strength + attributes.strength;
        totalAttribute.vitality = vitality + attributes.vitality;
        totalAttribute.wisdom = wisdom + attributes.wisdom;
        totalAttribute.intuition = intuition + attributes.intuition;
        totalAttribute.charisma = charisma + attributes.charisma;

        return totalAttribute;
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

    public Attributes(Integer strength, Integer agility, Integer vitality, Integer wisdom, Integer intuition, Integer charisma) throws Exception {
        this.strength = strength;
        this.agility = agility;
        this.vitality = vitality;
        this.wisdom = wisdom;
        this.intuition = intuition;
        this.charisma = charisma;
        this.validate();
    }
    public void levelUp(String attribute) {
        try {
            Field field = this.getClass().getDeclaredField(attribute);
            field.setAccessible(true);
            field.setInt(this, field.getInt(this) + 1);
        } catch (NoSuchFieldException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }
    }

    public Integer getAttributePoints(String attr) {
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
    public static List<String> getList() {
        return Arrays.asList(Strength, Agility, Vitality, Wisdom, Intuition, Charisma);
    }

    public void validate() throws Exception {
        if (!isTotalValid()) {
            throw new Exception("Base Total Attributes cannot be above 70");
        }
        if (!isValuesValid()) {
            throw new Exception("Base Attribute higher than 14");
        }
    }

    private boolean isTotalValid() {
        return strength + agility + vitality + wisdom + intuition + charisma <= 70;
    }
    private boolean isValuesValid() {
        return strength <= 14 &&
                agility <= 14 &&
                vitality <= 14 &&
                wisdom <= 14 &&
                intuition <= 14 &&
                charisma <= 14;
    }


}
