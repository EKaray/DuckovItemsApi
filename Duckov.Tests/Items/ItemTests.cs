using Duckov.Api.Items.Helpers;

namespace Duckov.Tests.Items;

public class ItemTests
{
    [Fact]
    public void CalculateValuePerSlot_ReturnsExpectedValue()
    {
        // Arrange
        int value = 100;
        int maxQuantity = 5;
        double weight = 2;

        // Act
        double result = ValueCalculator.CalculateValuePerSlot(value, maxQuantity, weight);

        // Assert
        Assert.Equal(440, result);
    }

    [Fact]
    public void CalculateValuePerSlot_WeightTooHigh_LimitedByWeight()
    {
        // Arrange
        int value = 50;
        int maxQuantity = 5;
        double weight = 50; // weight higher than maxWeight

        // Act
        double result = ValueCalculator.CalculateValuePerSlot(value, maxQuantity, weight);

        // unitsByWeight = floor(45 / 50) = 0
        // effectiveUnits = min(50, 0) = 0
        // effectiveSlotsUsed = ceil(0 / 5) = 0
        // valuePerSlot = 0 / 0 -> we should handle division by zero

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateValuePerSlot_MaxQuantityOne()
    {
        // Arrange
        int value = 10;
        int maxQuantity = 1;
        double weight = 2;

        // Act
        double result = ValueCalculator.CalculateValuePerSlot(value, maxQuantity, weight);

        // unitsBySlots = 10*1 = 10
        // unitsByWeight = floor(45/2) = 22
        // effectiveUnits = min(10,22) = 10
        // effectiveSlotsUsed = ceil(10/1) = 10
        // valuePerSlot = 10*10/10 = 10

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public void CalculateValuePerSlot_EdgeCase_WeightExactlyDividesMaxWeight()
    {
        // Arrange
        int value = 10;
        int maxQuantity = 5;
        double weight = 9; // 45 / 9 = 5 unitsByWeight

        // Act
        double result = ValueCalculator.CalculateValuePerSlot(value, maxQuantity, weight);

        // unitsBySlots = 10*5 = 50
        // unitsByWeight = floor(45/9) = 5
        // effectiveUnits = min(50,5) = 5
        // effectiveSlotsUsed = ceil(5/5) = 1
        // valuePerSlot = 5*10/1 = 50

        // Assert
        Assert.Equal(50, result);
    }
}