﻿using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace PrismSample.ViewModels
{
    public class DialogExtensionPageViewModel : BaseViewModel
    {
        private readonly IDialogService _dialogService;

        public DialogExtensionPageViewModel(IDialogService dialogService)
        {
            Title = "Dialog Service Extension";
            _dialogService = dialogService;
            GetNameCommand = new DelegateCommand(OnGetNameTapped);
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public DelegateCommand GetNameCommand { get; }

        async void OnGetNameTapped()
        {
            Name = await GetNameAsync();
        }

        public Task<string> GetNameAsync()
        {
            var tcs = new TaskCompletionSource<string>();
            try
            {
                _dialogService.ShowDialog("NameDialog", new DialogParameters(), (dparams) =>
                {
                    tcs.SetResult(dparams.Parameters.GetValue<string>("Name"));
                });
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }
    }
}
