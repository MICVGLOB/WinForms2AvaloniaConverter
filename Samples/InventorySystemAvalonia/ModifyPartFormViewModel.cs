using System;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace Prosoft.ECAD.Drawing.Dialogs.Avalonia.ViewModels;

public partial class ModifyPartFormViewModel : ObservableObject 
{
	public event Action RequestClose;


	public ModifyPartFormViewModel() 
	{
		
	}

	[RelayCommand(CanExecute=nameof(CanExecuteCancelButtonCommand))]
	void OnCancelButtonCommand(object parameter)
	{
	}
	bool CanExecuteCancelButtonCommand(object parameter)
	{
		return true;
	}
	[RelayCommand(CanExecute=nameof(CanExecuteSaveButtonCommand))]
	void OnSaveButtonCommand(object parameter)
	{
	}
	bool CanExecuteSaveButtonCommand(object parameter)
	{
		return true;
	}

}