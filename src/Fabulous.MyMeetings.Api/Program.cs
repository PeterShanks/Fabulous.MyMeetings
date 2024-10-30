using Fabulous.MyMeetings.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .Configure();

await app.RunAsync();
