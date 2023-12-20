﻿namespace Entities.Exceptions
{
    public abstract partial class BadRequestException
    {
        public class PriceOutOfRangeBadRequestException :BadRequestException
        {
            public PriceOutOfRangeBadRequestException() : base("Maximum price should than 1000 and greater then 10")
            {
                
            }
        }


    }
}
