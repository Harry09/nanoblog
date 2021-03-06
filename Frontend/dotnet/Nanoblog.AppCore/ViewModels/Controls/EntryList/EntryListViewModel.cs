﻿using Nanoblog.Common.Dto;
using Nanoblog.AppCore.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Nanoblog.AppCore.ViewModels.Controls.EntryList
{
    public class EntryListViewModel : BaseViewModel
    {

        private ObservableCollection<EntryListItemViewModel> _list;

        public ObservableCollection<EntryListItemViewModel> List
        {
            get => _list;
            private set => Update(ref _list, value);
        }

        public EntryListViewModel()
        {

        }

        public void LoadData(IEnumerable<EntryDto> data)
        {
            List = data.Select(m => new EntryListItemViewModel(m)).ToObservable();
        }
    }
}
