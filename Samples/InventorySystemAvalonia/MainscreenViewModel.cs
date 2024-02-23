using System;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace Prosoft.ECAD.Drawing.Dialogs.Avalonia.ViewModels;

public partial class MainscreenViewModel : ObservableObject 
{
	public event Action RequestClose;


	public MainscreenViewModel() 
	{
		
	}

	[RelayCommand(CanExecute=nameof(CanExecuteExitButtonCommand))]
	void OnExitButtonCommand(object parameter)
	{
	}
	bool CanExecuteExitButtonCommand(object parameter)
	{
		return true;
	}
	[RelayCommand(CanExecute=nameof(CanExecuteDeleteProductButtonCommand))]
	void OnDeleteProductButtonCommand(object parameter)
	{
	}
	bool CanExecuteDeleteProductButtonCommand(object parameter)
	{
		return true;
	}
	[RelayCommand(CanExecute=nameof(CanExecuteDeletePartButtonCommand))]
	void OnDeletePartButtonCommand(object parameter)
	{
	}
	bool CanExecuteDeletePartButtonCommand(object parameter)
	{
		return true;
	}
	[RelayCommand(CanExecute=nameof(CanExecuteModifyProductButtonCommand))]
	void OnModifyProductButtonCommand(object parameter)
	{
	}
	bool CanExecuteModifyProductButtonCommand(object parameter)
	{
		return true;
	}
	[RelayCommand(CanExecute=nameof(CanExecuteModifyPartButtonCommand))]
	void OnModifyPartButtonCommand(object parameter)
	{
	}
	bool CanExecuteModifyPartButtonCommand(object parameter)
	{
		return true;
	}
	[RelayCommand(CanExecute=nameof(CanExecuteAddProductButtonCommand))]
	void OnAddProductButtonCommand(object parameter)
	{
	}
	bool CanExecuteAddProductButtonCommand(object parameter)
	{
		return true;
	}
	[RelayCommand(CanExecute=nameof(CanExecuteAddPartButtonCommand))]
	void OnAddPartButtonCommand(object parameter)
	{
	}
	bool CanExecuteAddPartButtonCommand(object parameter)
	{
		return true;
	}
	[RelayCommand(CanExecute=nameof(CanExecuteProductSearchButtonCommand))]
	void OnProductSearchButtonCommand(object parameter)
	{
	}
	bool CanExecuteProductSearchButtonCommand(object parameter)
	{
		return true;
	}
	[RelayCommand(CanExecute=nameof(CanExecuteSearchPartsButtonCommand))]
	void OnSearchPartsButtonCommand(object parameter)
	{
	}
	bool CanExecuteSearchPartsButtonCommand(object parameter)
	{
		return true;
	}

}