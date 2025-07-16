﻿namespace Application.DTO
{
    // Passed as input to create requests
    public class ProductDTO
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string? CategoryName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
