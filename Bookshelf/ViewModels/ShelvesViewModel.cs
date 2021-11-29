﻿using Bookshelf.Models;
using Bookshelf.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Bookshelf.ViewModels
{
    public class ShelvesViewModel : BaseViewModel
    {
        private ObservableCollection<ShelfViewModel> items;
        public ObservableCollection<ShelfViewModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }


        public ICommand AddShelfCommand { get; set; }

        public ShelvesViewModel()
        {
            SetupView();
            SetupCommands();
        }

        private void SetupCommands()
        {
            AddShelfCommand = new RelayCommand(o =>
            {
                IoC.UI.ShowDialogWindow(new AddShelfWindow());
                SetupView();
            });
        }

        public void SetupView()
        {
            var shelvesRepository = new Repository<Shelf>();

            List<Shelf> shelfItems = shelvesRepository.GetAll().ToList();

            Items = new ObservableCollection<ShelfViewModel>();

            foreach (var item in shelfItems)
            {
                Items.Add(new ShelfViewModel(item));
            }
        }
    }
}
