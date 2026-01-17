namespace Duckov.Api.Items.Helpers;

public static class ValueCalculator
{
    public static double CalculateValuePerSlot(int value, int maxQuantity, double weight, int maxSlots = 10, int maxWeight = 45)
    {
        int unitsBySlots = maxSlots * maxQuantity;
        int unitsByWeight = (int)Math.Floor(maxWeight / weight);
        int effectiveUnits = Math.Min(unitsBySlots, unitsByWeight);
        int effectiveSlotsUsed = (int)Math.Ceiling((double)effectiveUnits / maxQuantity);

        if (effectiveSlotsUsed == 0)
        {
            return 0;
        }

        double valuePerSlot = effectiveUnits * value / effectiveSlotsUsed;
        return valuePerSlot;
    }
}