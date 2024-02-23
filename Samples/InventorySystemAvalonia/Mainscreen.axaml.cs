using System;
using System.Collections.Generic;

using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace InventorySystem;
public partial class Mainscreen : Window {

	public Mainscreen() {
		InitializeComponent();
	}

	protected MainscreenViewModel ViewModel { get; private set; }
	protected override void OnDataContextChanged(EventArgs e) {
		base.OnDataContextChanged(e);
		UnsubscribeEvents(ViewModel);
		ViewModel = DataContext as MainscreenViewModel;
		SubscribeEvents(ViewModel);
	}

	private void SubscribeEvents(MainscreenViewModel viewModel) {
		if(viewModel == null)
			return;
	}

	private void UnsubscribeEvents(MainscreenViewModel viewModel) {
		if(viewModel == null)
			return;
	}
}