﻿namespace ShopOnline.Models.DataTransferObjects
{
    public class CartItemToAddDto
    {
        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
