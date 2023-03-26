//-----------------------------------------------------------------------
// <copyright file="AddressFilter.cs" company="Procare Software, LLC">
//     Copyright © 2021-2023 Procare Software, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Procare.Address.IntegrationTests;

using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;

public struct AddressFilter
{
    public string? CompanyName { get; set; }

    public string? Line1 { get; set; }

    public string? Line2 { get; set; }

    public string? City { get; set; }

    public string? StateCode { get; set; }

    public string? Urbanization { get; set; }

    public string? ZipCodeLeading5 { get; set; }

    public string? ZipCodeTrailing4 { get; set; }

    public string ToQueryString()
    {
        var result = new StringBuilder();

        foreach (var prop in this.GetType().GetProperties())
        {
            var value = (string?)prop.GetMethod!.Invoke(this, Array.Empty<object>());
            if (!string.IsNullOrEmpty(value))
            {
                result.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}={2}", result.Length == 0 ? "?" : "&", WebUtility.UrlEncode(prop.Name), WebUtility.UrlEncode(value));
            }
        }

        return result.ToString();
    }

    public HttpRequestMessage ToHttpRequest(Uri baseUri)
    {
        return new HttpRequestMessage(HttpMethod.Get, new Uri(baseUri, this.ToQueryString()));
    }
}
