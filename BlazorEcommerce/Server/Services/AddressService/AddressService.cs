namespace BlazorEcommerce.Server.Services.AddressService;

public class AddressService : IAddressService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;

    public AddressService(DataContext context,
        IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<ServiceResponse<Address>> GetAddress()
    {
        var userId = _authService.GetUserId();
        var address = await _context.Addresses.FirstOrDefaultAsync(a => a.UserId == userId);

        return new ServiceResponse<Address> { Data = address };
    }

    public async Task<ServiceResponse<Address>> AddOrUpdateAddress(Address address)
    {
        var response = new ServiceResponse<Address>();
        var dbAddress = (await GetAddress()).Data;
        if (dbAddress == null)
        {
            address.UserId = _authService.GetUserId();
            _context.Addresses.Add(address);
            response.Data = address;
        }
        else
        {
            dbAddress.Name = address.Name;
            dbAddress.Country = address.Country;
            dbAddress.Province = address.Province;
            dbAddress.City = address.City;
            dbAddress.Gu = address.Gu;
            dbAddress.Street = address.Street;
            dbAddress.DetailAddress = address.DetailAddress;
            dbAddress.Zip = address.Zip;
            response.Data = dbAddress;
        }
        
        await _context.SaveChangesAsync();

        return response;
    }
}