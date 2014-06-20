﻿using System;

namespace NProg.Distributed.OrderService.Domain
{
    [Serializable]
    public sealed class Order
    {
        public Guid OrderId { get; set; }

        public int Count { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal UnitPrice { get; set; }

        public string UserName { get; set; }

        public override string ToString()
        {
            return string.Format("OrderId: {0}, Count: {1}, OrderDate: {2}, UnitPrice: {3}, UserName: {4}", OrderId,
                Count, OrderDate, UnitPrice, UserName);
        }

        protected bool Equals(Order other)
        {
            return OrderId.Equals(other.OrderId) && Count == other.Count && OrderDate.Equals(other.OrderDate) &&
                   UnitPrice == other.UnitPrice && string.Equals(UserName, other.UserName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Order) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = OrderId.GetHashCode();
                hashCode = (hashCode*397) ^ Count;
                hashCode = (hashCode*397) ^ OrderDate.GetHashCode();
                hashCode = (hashCode*397) ^ UnitPrice.GetHashCode();
                hashCode = (hashCode*397) ^ (UserName != null ? UserName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}