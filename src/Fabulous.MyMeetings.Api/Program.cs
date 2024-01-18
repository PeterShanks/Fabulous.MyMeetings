using Fabulous.MyMeetings.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var app = await builder.ConfigureServices()
    .Configure();

await app.RunAsync();
