﻿using Bookshelf.Models;
using Bookshelf.Models.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class AddShelfViewModel : BaseViewModel
    {
        public string ShelfName { get; set; }

        public ICommand CloseCommand { get; set; }
        public ICommand AddShelfCommand { get; set; }

        public AddShelfViewModel(Window window)
        {
            CloseCommand = new RelayCommand(o =>
            {
                window.Close();
            });

            AddShelfCommand = new RelayCommand(o =>
            {
                if (!string.IsNullOrWhiteSpace(ShelfName))
                {
                    AddShelf();
                }
            });

        }

        private void AddShelf()
        {
            using (var context = new DataContextFactory().CreateDbContext())
            {
                var shelf = context.Set<Shelf>().FirstOrDefault(s => s.Name == ShelfName);

                if (shelf == null)
                {
                    context.Set<Shelf>().Add(new Shelf
                    {
                        Name = ShelfName
                    });
                    context.SaveChangesAsync();
                }
            }

            CloseCommand.Execute(this);
        }
    }

}