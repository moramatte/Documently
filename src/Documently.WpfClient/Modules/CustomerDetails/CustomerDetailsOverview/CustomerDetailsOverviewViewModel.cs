using System;
using Caliburn.Micro;
using Documently.ReadModel;
using MassTransit;

namespace Documently.WpfClient.Modules.CustomerDetails.CustomerDetailsOverview
{
	public class CustomerDetailsOverviewViewModel : Screen, IShowCustomerDetails
	{
		private readonly IReadRepository _readRepository;

		public CustomerDetailsOverviewViewModel(IReadRepository readRepository)
		{
			_readRepository = readRepository;
		}

		public void WithCustomer(string customerDtoId)
		{
			ViewModel = _readRepository.GetById<CustomerListDto>(customerDtoId);
		}

		public NewId GetCustomerId()
		{
			return ViewModel.AggregateId;
		}

		//TODO: Change CustomerListDto to something specific for this screen
		public CustomerListDto ViewModel { get; private set; }
	}
}