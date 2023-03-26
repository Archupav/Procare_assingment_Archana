//-----------------------------------------------------------------------
// <copyright file="GetAddressesResponse.cs" company="Procare Software, LLC">
//     Copyright © 2021-2023 Procare Software, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Procare.Address.IntegrationTests;

using System.Collections.Generic;

public class GetAddressesResponse
{
    public int? Count { get; set; }

    public IList<Address>? Addresses { get; set; }

    public class Address
    {
        public string? CompanyName { get; set; }

        public string? Line1 { get; set; }

        public string? Line2 { get; set; }

        public string? City { get; set; }

        public string? StateCode { get; set; }

        public string? Urbanization { get; set; }

        public string? ZipCodeLeading5 { get; set; }

        public string? ZipCodeTrailing4 { get; set; }

    }
}
