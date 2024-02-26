using System;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace @NamespaceName@;

public partial class @ViewModelClass@ : ObservableObject 
{
@Fields@

	public @ViewModelClass@() 
	{
	@FieldsInitialization@	
	}

@Methods@
}