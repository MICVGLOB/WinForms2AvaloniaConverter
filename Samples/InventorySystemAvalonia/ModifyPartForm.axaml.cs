using System;
using System.Collections.Generic;

using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace InventorySystem;
public partial class ModifyPartForm : Window {

	public ModifyPartForm() {
		InitializeComponent();
	}

	protected ModifyPartFormViewModel ViewModel { get; private set; }
	protected override void OnDataContextChanged(EventArgs e) {
		base.OnDataContextChanged(e);
		UnsubscribeEvents(ViewModel);
		ViewModel = DataContext as ModifyPartFormViewModel;
		SubscribeEvents(ViewModel);
	}

	private void SubscribeEvents(ModifyPartFormViewModel viewModel) {
		if(viewModel == null)
			return;
	}

	private void UnsubscribeEvents(ModifyPartFormViewModel viewModel) {
		if(viewModel == null)
			return;
	}
}