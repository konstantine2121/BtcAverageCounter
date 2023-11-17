using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BtcAverageCounter
{
    public class BtcRecord
    {
        public BtcRecord(double amount, double price = 0)
        {
            Amount = amount;
            Price = price;
        }

        /// <summary>
        /// Количество
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Цена за штуку
        /// </summary>
        public double Price { get; set; }
    }

    public static class BtcRecordExtensions
    {
        /// <summary>
        /// Стоимость
        /// </summary>
        /// <returns></returns>
        public static double GetCost(this BtcRecord btcRecord)
        {
            return btcRecord.Amount * btcRecord.Price;
        }

        /// <summary>
        /// Стоимость всех транзакций
        /// </summary>
        /// <returns></returns>
        public static double GetFullCost(this IReadOnlyCollection<BtcRecord> btcRecords)
        {
            return btcRecords.Select(record => record.GetCost())
                .Sum();
        }

        /// <summary>
        /// Общее количество
        /// </summary>
        /// <returns></returns>
        public static double GetAllAmount(this IReadOnlyCollection<BtcRecord> btcRecords)
        {
            return btcRecords.Select(record => record.Amount)
                .Sum();
        }

        /// <summary>
        /// Найти среднюю стоимость одного битка
        /// </summary>
        /// <returns></returns>
        public static double GetAverageCost(this IReadOnlyCollection<BtcRecord> btcRecords)
        {
            if (btcRecords == null || btcRecords.Count == 0)
            { 
                return 0; 
            }

            var cost = GetFullCost(btcRecords);
            var amount = GetAllAmount(btcRecords);

            if (amount == 0)
            {
                return double.PositiveInfinity;
            }

            return cost / amount;
        }

    }
}
