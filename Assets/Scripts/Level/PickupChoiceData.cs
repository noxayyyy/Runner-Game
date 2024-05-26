using System;

[Serializable]
public class PickupChoiceData {
    public enum BodyPart {
        HEAD,
        TORSO,
        LEGS
    }
    public BodyPart part;
    public int[] partIndexes;
    public bool[] rightOrWrong;
}
