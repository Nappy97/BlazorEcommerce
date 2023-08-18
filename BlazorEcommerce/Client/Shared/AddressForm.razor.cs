using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Shared;

public partial class AddressForm
{
    [Inject] IAddressService AddressService { get; set; }

    private Address _address = null;

    bool _editAddress = false;

    protected override async Task OnInitializedAsync()
    {
        _address = await AddressService.GetAddress();
    }

    private async Task SubmitAddress()
    {
        _editAddress = false;
        _address = await AddressService.AddOrUpdateAddress(_address);
    }

    private void InitAddress()
    {
        _address = new Address();
        _editAddress = true;
    }

    private void EditAddress()
    {
        _editAddress = true;
    }
}