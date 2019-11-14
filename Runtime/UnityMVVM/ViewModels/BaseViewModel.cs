using Toastapp.MVVM;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class BaseViewModel : UnityViewModel
{
    [Binding]
	public virtual void CloseViewModel()
	{
		NavigationService.Get?.CloseViewModel(this);
	}
}
