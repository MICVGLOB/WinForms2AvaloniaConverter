using System;
using System.Collections.Generic;

using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace @NamespaceName@;
public partial class @TypeClass@ : @BaseClass@ {

	public @TypeClass@() {
		InitializeComponent();
	}

	protected @ViewModelClass@ ViewModel { get; private set; }
	protected override void OnDataContextChanged(EventArgs e) {
		base.OnDataContextChanged(e);
		UnsubscribeEvents(ViewModel);
		ViewModel = DataContext as @ViewModelClass@;
		SubscribeEvents(ViewModel);
	}

	private void SubscribeEvents(@ViewModelClass@ viewModel) {
		if(viewModel == null)
			return;
	}

	private void UnsubscribeEvents(@ViewModelClass@ viewModel) {
		if(viewModel == null)
			return;
	}
}