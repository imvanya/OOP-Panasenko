using System;
using System.Collections.Generic;

namespace Lab18v18
{
    public class EventManagementService
    {
        private Dictionary<string, int> _capacities = new Dictionary<string, int>();
        private Dictionary<string, List<string>> _participants = new Dictionary<string, List<string>>();

        public void CreateEvent(string eventName, int maxCapacity)
        {
            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentException("Назва події порожня");
            }
            if (maxCapacity <= 0)
            {
                throw new ArgumentException("Місткість має бути більшою за нуль");
            }
            _capacities[eventName] = maxCapacity;
            _participants[eventName] = new List<string>();
        }

        public int GetParticipantsCount(string eventName)
        {
            if (!_participants.ContainsKey(eventName))
            {
                return 0;
            }
            return _participants[eventName].Count;
        }


        public void RegisterParticipant(string eventName, string participantName)
        {
            if (!_capacities.ContainsKey(eventName))
            {
                throw new ArgumentException("Події \"" + eventName + "\" не існує");
            }
            if (string.IsNullOrWhiteSpace(participantName))
            {
                throw new ArgumentException("Ім'я учасника порожнє");
            }

            List<string> list = _participants[eventName];
            if (list.Count >= _capacities[eventName])
            {
                throw new EventCapacityExceededException(eventName, _capacities[eventName]);
            }

            list.Add(participantName);
            Console.WriteLine("  " + participantName + " зареєстрований на \"" + eventName + "\" ("
                + list.Count + "/" + _capacities[eventName] + ")");
        }

        public Result<bool> CancelRegistration(string eventName, string participantName)
        {
            if (!_participants.ContainsKey(eventName))
            {
                return Result<bool>.Failure("Події \"" + eventName + "\" не існує");
            }

            List<string> list = _participants[eventName];
            if (!list.Remove(participantName))
            {
                return Result<bool>.Failure("Учасника \"" + participantName
                    + "\" не знайдено на події \"" + eventName + "\"");
            }
            return Result<bool>.Success(true);
        }
    }
}