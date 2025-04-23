namespace EchoLife.Tests.Integration.Utils.Base;

internal class TestBase<TestService>
    where TestService : class
{
    public TestService Sut;

    [OneTimeSetUp]
    public void OneTimeSetUp() { }

    [OneTimeTearDown]
    public void OneTimeTearDown() { }
}
