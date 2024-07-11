﻿using Microsoft.EntityFrameworkCore;
using MoneStore.Data;
using MoneStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Work.Repository
{
    public class CategoryRepositoryImpl : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int categoryId)
        {
            Category category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IList<Category>> GetCategoryList()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        public async Task Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
