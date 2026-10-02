using Microsoft.Maui.Storage;
using PeakMind.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PeakMind.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection db;
        private static DatabaseService _instance;
        public static DatabaseService Instance => _instance ??= new DatabaseService();

        private DatabaseService() { }

        public async Task InitAsync()
        {
            if (db != null) return;
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "peakmind.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<User>();
            await db.CreateTableAsync<JournalEntry>();
            await db.CreateTableAsync<TestResult>();
        }

        public async Task<int> SaveUserAsync(User user)
        {
            await InitAsync();
            return await db.InsertAsync(user);
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            await InitAsync();
            return await db.Table<User>().Where(u => u.Name == name).FirstOrDefaultAsync();
        }

        public async Task CleanupOldDataAsync()
        {
            await InitAsync();
            var border = DateTime.Now.AddDays(-30);
            await db.ExecuteAsync("DELETE FROM JournalEntry WHERE CreatedAt < ?", border);
        }
        public async Task<List<JournalEntry>> GetEntriesByUserAsync(int userid)
        {
            await InitAsync();
            return await db.Table<JournalEntry>().Where(e => e.UserId == userid).OrderByDescending(e => e.CreatedAt).ToListAsync();

        }
        public async Task<int> SaveTestResultAsync(TestResult result)
        {
            await InitAsync();
            return await db.InsertAsync(result);
        }

        public async Task<List<TestResult>> GetResultsForUserAsync(int userId)
        {
            await InitAsync();
            return await db.Table<TestResult>()
                           .Where(r => r.UserId == userId)
                           .OrderByDescending(r => r.CreatedAt)
                           .ToListAsync();
        }
        public async Task<int> UpdateUserAsync(User user)
        {
            await InitAsync();
            return await db.UpdateAsync(user);
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            await InitAsync();
            return await db.Table<User>()
                           .Where(u => u.Id == id)
                           .FirstOrDefaultAsync();
        }
        public async Task<int> SaveJournalEntryAsync(JournalEntry entry)
        {
            await InitAsync();
            return await db.InsertAsync(entry);
        }
        public async Task<List<JournalEntry>> GetJournalEntriesAsync(int userId)
        {
            await InitAsync();
            return await db.Table<JournalEntry>()
                           .Where(x => x.UserId == userId)
                           .OrderByDescending(x => x.CreatedAt)
                           .ToListAsync();
        }
    }
}