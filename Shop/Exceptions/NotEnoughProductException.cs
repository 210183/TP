using System;
using System.Runtime.Serialization;

namespace Shop
{
    [Serializable]
    internal class NotEnoughProductException : Exception
    {
        public NotEnoughProductException(string message) : base(message)
        {
        }
    }
}