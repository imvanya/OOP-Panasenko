using System;

namespace Lab18v18
{
    public class EventCapacityExceededException : Exception
    {
        public string EventName { get; private set; }
        public int MaxCapacity { get; private set; }

        public EventCapacityExceededException()
        {
        }

        public EventCapacityExceededException(string message)
            : base(message)
        {
        }

        public EventCapacityExceededException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public EventCapacityExceededException(string eventName, int maxCapacity)
            : base("Подія \"" + eventName + "\" заповнена, максимум місць: " + maxCapacity)
        {
            EventName = eventName;
            MaxCapacity = maxCapacity;
        }
    }
}