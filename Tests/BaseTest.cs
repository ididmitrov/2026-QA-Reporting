using NUnit.Framework;
using RickAndMortyTests.Helpers;
using Allure.NUnit;
using System.Text;
using Allure.Net.Commons;
using NUnit.Framework.Interfaces;

namespace RickAndMortyTests.Tests;

[AllureNUnit]
public class BaseTest
{
    protected ApiClient _apiClient = null!;

    [SetUp]
    public void Setup()
    {
        _apiClient = new ApiClient();
    }

    [TearDown]
    public void TearDown()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;

        if (status == TestStatus.Failed && !string.IsNullOrWhiteSpace(_apiClient.LastResponseBody))
        {
            AllureApi.AddAttachment(
                "response-body",
                "application/json",
                Encoding.UTF8.GetBytes(_apiClient.LastResponseBody),
                "json"
            );
        }

        _apiClient.Dispose();
    }
}
