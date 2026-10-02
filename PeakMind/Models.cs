using PeakMind.Services;
using SQLite;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PeakMind.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastTestDate { get; set; }
    }

    [Table("JournalEntry")]
    public class JournalEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
            
    }
    [Table("TestResult")]
    public class TestResult
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int correct { get; set; }
        public int errors { get; set; }
        public int missed { get; set; }
        public double accuracy { get; set; }
        public int performance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
    public class BourdonCell : INotifyPropertyChanged
    {
        public string Letter { get; set; }
        public bool IsTarget { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}