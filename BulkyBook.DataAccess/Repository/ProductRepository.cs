﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(x=> x.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                objFromDb.Author= obj.Author;
                objFromDb.CategoryId= obj.CategoryId;
                objFromDb.CoverTypeId= obj.CoverTypeId; 
                objFromDb.Price50= obj.Price50;
                objFromDb.Price100= obj.Price100;
                objFromDb.ListPrice= obj.ListPrice;
                if(obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
