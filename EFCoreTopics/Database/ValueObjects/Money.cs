namespace EFCoreTopics.Database.ValueObjects
{
    public record Money
    {
        public decimal Value { get; set; }
        public MoneyType Unit { get; set; }

        public Money(decimal value, MoneyType unit)
        {
            Value = value;
            Unit = unit;
        }
    };

    public enum MoneyType
    {
        Dollar=0,
        Euro=1
    }
}
