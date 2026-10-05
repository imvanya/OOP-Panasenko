using System;

namespace Lab4Variant18
{
    public class CurrencyAmount
    {
        private decimal _value;
        private string _code = string.Empty;

        public decimal Value
        {
            get => _value;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Сума не може бути від'ємною!");
                }
                _value = value;
            }
        }

        public string Code
        {
            get => _code;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Код валюти не може бути порожнім!");
                }
                _code = value.Trim().ToUpper(); 
            }
        }

        public static CurrencyAmount ZeroUSD => new CurrencyAmount(0, "USD");

        public CurrencyAmount(decimal value, string code)
        {
            Value = value; 
            Code = code;
        }

        #region Перевантаження операторів

        public static CurrencyAmount operator +(CurrencyAmount left, CurrencyAmount right)
        {
            if (left.Code != right.Code)
            {
                throw new InvalidOperationException($"Неможливо додати різні валюти: {left.Code} та {right.Code}!");
            }

            return new CurrencyAmount(left.Value + right.Value, left.Code);
        }

        public static bool operator >(CurrencyAmount left, CurrencyAmount right)
        {
            CheckSameCurrency(left, right);
            return left.Value > right.Value;
        }

        public static bool operator <(CurrencyAmount left, CurrencyAmount right)
        {
            CheckSameCurrency(left, right);
            return left.Value < right.Value;
        }

        public static bool operator >=(CurrencyAmount left, CurrencyAmount right)
        {
            CheckSameCurrency(left, right);
            return left.Value >= right.Value;
        }

        public static bool operator <=(CurrencyAmount left, CurrencyAmount right)
        {
            CheckSameCurrency(left, right);
            return left.Value <= right.Value;
        }

        public static bool operator ==(CurrencyAmount? left, CurrencyAmount? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;

            return left.Equals(right);
        }

        public static bool operator !=(CurrencyAmount? left, CurrencyAmount? right)
        {
            return !(left == right);
        }

        #endregion

        #region Перевизначені базові методи Object

        public override string ToString()
        {
            return $"{Value:F2} {Code}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is CurrencyAmount other)
            {
                return Value == other.Value && Code == other.Code;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Code);
        }

        #endregion

        private static void CheckSameCurrency(CurrencyAmount left, CurrencyAmount right)
        {
            if (left.Code != right.Code)
            {
                throw new InvalidOperationException($"Неможливо порівняти різні валюти ({left.Code} і {right.Code})!");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Лабораторна робота №4. Варіант 18");
            Console.WriteLine("Клас CurrencyAmount\n");

            CurrencyAmount wallet1 = new CurrencyAmount(150.50m, "USD");
            CurrencyAmount wallet2 = new CurrencyAmount(49.50m, "USD");
            CurrencyAmount walletEur = new CurrencyAmount(100.00m, "EUR");
            CurrencyAmount zeroUsd = CurrencyAmount.ZeroUSD;

            Console.WriteLine($"Гаманець 1: {wallet1}");
            Console.WriteLine($"Гаманець 2: {wallet2}");
            Console.WriteLine($"Гаманець у Євро: {walletEur}");
            Console.WriteLine($"Статичний ZeroUSD: {zeroUsd}");
            Console.WriteLine(new string('-', 40));

            CurrencyAmount totalUsd = wallet1 + wallet2;
            Console.WriteLine($"Результат додавання {wallet1} + {wallet2} = {totalUsd}");
            Console.WriteLine(new string('-', 40));

            Console.WriteLine($"Чи {wallet1} > {wallet2}? -> {wallet1 > wallet2}");
            Console.WriteLine($"Чи {wallet1} == {wallet2}? -> {wallet1 == wallet2}");

            CurrencyAmount copyWallet1 = new CurrencyAmount(150.50m, "usd"); 
            Console.WriteLine($"Чи {wallet1} == {copyWallet1} (створено з \"usd\")? -> {wallet1 == copyWallet1}");
            Console.WriteLine(new string('-', 40));

            Console.WriteLine("Спроба встановити від'ємне значення:");
            try
            {
                CurrencyAmount invalid = new CurrencyAmount(-50m, "USD");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"ПОМИЛКА ВАЛІДАЦІЇ: {ex.Message}");
            }

            Console.WriteLine("\nСпроба додати різні валюти (USD + EUR):");
            try
            {
                CurrencyAmount failSum = wallet1 + walletEur;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"ПОМИЛКА ОПЕРАЦІЇ: {ex.Message}");
            }

            Console.WriteLine("\nРоботу програми завершено успішно.");
        }
    }
}