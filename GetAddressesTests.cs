//-----------------------------------------------------------------------
// <copyright file="GetAddressesTests.cs" company="Procare Software, LLC">
//     Copyright © 2021-2023 Procare Software, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Procare.Address.IntegrationTests;

using System;
using System.Net;
using System.Threading.Tasks;

using Xunit;

public class GetAddressesTests
{
    private readonly AddressService service = new AddressService(new Uri("https://address.dev-procarepay.com"));

    [Fact]
    public async Task GetAddresses_With_Owm_ShouldResultIn_OneMatchingAddress()
    {
        var result = await this.service.GetAddressesAsync(new AddressFilter { Line1 = "1 W Main St", City = "Medford", StateCode = "OR" }).ConfigureAwait(false);

        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
        Assert.NotNull(result.Addresses);
        Assert.Equal(result.Count, result.Addresses!.Count);
    }

    [Fact]
    public async Task GetAddresses_With_AmbiguousAddress_ShouldResultIn_MultipleMatchingAddresses()
    {
        var result = await this.service.GetAddressesAsync(new AddressFilter { Line1 = "123 Main St", City = "Ontario", StateCode = "CA" }).ConfigureAwait(false);

        // TODO: Complete the testre
        //throw new NotImplementedException();

        Assert.NotNull(result);
        Assert.Equal(result.Addresses.Count, result.Count);
        Console.WriteLine(result.Addresses.Count);
        Assert.NotNull(result.Addresses);
        Assert.Equal(result.Count, result.Addresses!.Count);
    }

    ///<summary>
    /// Positive test case to test Line 1, City, StateCode and Zipcode in the API response is same as
    ///the values provided in the API request
    ///</summary>
    [Fact]
    public async Task Line1_PositiveTestCase1()
    {
        var result = await this.service.GetAddressesAsync(new AddressFilter { Line1 = "14800 VIENNA CIR", City = "PARKER", ZipCodeLeading5 = "80134", StateCode = "CO" }).ConfigureAwait(false);

        // Assert.Equal(result.Addresses.Count, result.Count);
        String Line1 = result.Addresses[0].Line1;
        String City = result.Addresses[0].City;
        String StateCode = result.Addresses[0].StateCode;
        String ZipCode = result.Addresses[0].ZipCodeLeading5;
        Assert.Equal("14800 VIENNA CIR", Line1);
        Assert.Equal("PARKER", City);
        Assert.Equal("CO", StateCode);
        Assert.Equal("80134", ZipCode);
    }

    ///<summary>
    /// Negative test case to test invalid State Code with state code having characters > 2
    ///Sending "ORAP" as a state code and the API should return a message or error saying "Invalid State Code" 
    ///but the API is returning a valid response with 200 status code. Ideally the API should return status code 400 
    /// ( Bad Request )
    ///</summary>
    [Fact]
    public async Task StateCode_NegativeTestCase_1()
    {
        var result = await this.service.GetAddresseWithoutCityAndZipORInvalidStateCode(new AddressFilter { Line1 = "1 W Main St", City = "Medford", StateCode = "ORAP" }).ConfigureAwait(false);
        Assert.Contains("Invalid State Code", result);
    }

    ///<summary>
    /// Negative test case to test with no City and Zipcode 
    /// Calling the API request with only Line 1 with no City and Zip Code, API should return with 400 status code 
    /// with proper error message
    ///</summary>
    [Fact]
    public async Task CityAndZip_Missing_NegativeTestCase_2()
    {
        var result = await this.service.GetAddresseWithoutCityAndZipORInvalidStateCode(new AddressFilter { Line1 = "1 W Main St" }).ConfigureAwait(false);
        Assert.Contains("Either City of Zip Code is required. Please review and resubmit your request.", result);
    }
}
